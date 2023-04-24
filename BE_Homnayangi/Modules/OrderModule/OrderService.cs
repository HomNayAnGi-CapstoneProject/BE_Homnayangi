﻿using AutoMapper;
using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.CustomerVoucherModule.Interface;
using BE_Homnayangi.Modules.IngredientModule.Interface;
using BE_Homnayangi.Modules.OrderDetailModule.Interface;
using BE_Homnayangi.Modules.OrderModule.Interface;
using BE_Homnayangi.Modules.OrderModule.Response;
using BE_Homnayangi.Modules.RecipeModule;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using BE_Homnayangi.Modules.TransactionModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using BE_Homnayangi.Modules.VoucherModule.Interface;
using BE_Homnayangi.Ultils.EmailServices;
using Library.Models;
using Library.Models.Constant;
using Library.Models.Enum;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Transactions;
using BE_Homnayangi.Modules.NotificationModule.Interface;
using static Library.Models.Enum.PaymentMethodEnum;
using static Library.Models.Enum.Status;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BE_Homnayangi.Modules.OrderModule
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<SignalRServer> _hubContext;
        private readonly ICustomerVoucherService _customerVoucherService;
        IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;

        public OrderService(IOrderRepository OrderRepository,
            IOrderDetailRepository orderDetailRepository,
            ICustomerRepository customerRepository,
            IUserRepository userRepository,
            ITransactionRepository transactionRepository,
            IIngredientRepository ingredientRepository,
            IRecipeRepository recipeRepository,
            IVoucherRepository voucherRepository,
            INotificationRepository notificationRepository,
            IHubContext<SignalRServer> hubContext,
            ICustomerVoucherService customerVoucherService,
            IMapper mapper,
            IConfiguration configuration,
            IEmailSender emailSender)
        {
            _OrderRepository = OrderRepository;
            _orderDetailRepository = orderDetailRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
            _ingredientRepository = ingredientRepository;
            _recipeRepository = recipeRepository;
            _configuration = configuration;
            _voucherRepository = voucherRepository;
            _notificationRepository = notificationRepository;
            _hubContext = hubContext;
            _mapper = mapper;
            _emailSender = emailSender;
            _customerVoucherService = customerVoucherService;
        }

        public async Task<ICollection<OrderResponse>> GetOrderResponse(DateTime? fromDate, DateTime? toDate, int status = -1)
        {
            if(fromDate.HasValue && !toDate.HasValue)
                throw new Exception("To date is required");
            if (!fromDate.HasValue && toDate.HasValue)
                throw new Exception("From date is required");

            var orders = status > -1
                ? await _OrderRepository.GetOrdersBy(o => o.OrderStatus.GetValueOrDefault() == status,
                includeProperties: "OrderDetails")
                : await _OrderRepository.GetOrdersBy(
                includeProperties: "OrderDetails");

            if (fromDate.HasValue && toDate.HasValue)
            {
                if (fromDate.Value.CompareTo(toDate.Value) > 0)
                    throw new Exception("From date must before To date");
                orders = orders.Where(o => o.OrderDate.GetValueOrDefault() >= fromDate && o.OrderDate.GetValueOrDefault() <= toDate).ToList();
            }

            var ingredients = await _ingredientRepository.GetAll();
            var recipes = await _recipeRepository.GetRecipesBy(includeProperties: "RecipeDetails");

            var res = new List<OrderResponse>();
            foreach (var order in orders)
            {
                var ingOrderDetails = order.OrderDetails.Where(detail => detail.RecipeId == null)
                    .Join(ingredients, x => x.IngredientId, y => y.IngredientId, (x, y) =>
                    {
                        return new OrderResponse.IngredientResponse
                        {
                            IngredientId = x.IngredientId,
                            Quantity = x.Quantity,
                            Price = x.Price,
                            IngredientImage = y.Picture,
                            IngredientName = y.Name
                        };
                    })
                    .ToList();

                var recipeOrderDetails = order.OrderDetails.Where(detail => detail.RecipeId != null)
                    .Join(recipes, x => x.RecipeId, y => y.RecipeId, (x, y) =>
                    {
                        return new OrderResponse.OrderDetailResponse
                        {
                            OrderId = order.OrderId,
                            RecipeId = y.RecipeId,
                            RecipeImage = y.ImageUrl ?? "",
                            RecipeName = y.Title ?? "",
                            RecipeQuantity = x.Quantity.Value,
                            PackagePrice = y.PackagePrice,
                            CookedPrice = y.CookedPrice,
                            RecipeDetails = y.RecipeDetails.Join(ingredients, x => x.IngredientId, y => y.IngredientId, (x, y) =>
                            {
                                return new OrderResponse.IngredientResponse
                                {
                                    IngredientId = y.IngredientId,
                                    Quantity = x.Quantity,
                                    Price = y.Price,
                                    IngredientImage = y.Picture,
                                    IngredientName = y.Name
                                };
                            }).ToList()
                        };
                    })
                    .ToList();

                var orderResponse = new OrderResponse
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    ShippedDate = order.ShippedDate,
                    ShippedAddress = order.ShippedAddress,
                    TotalPrice = order.TotalPrice,
                    OrderStatus = order.OrderStatus,
                    CustomerId = order.CustomerId,
                    IsCooked = order.IsCooked,
                    VoucherId = order.VoucherId,
                    PaymentMethod = order.PaymentMethod,
                    PaypalUrl = order.PaypalUrl,
                    OrderDetailRecipes = recipeOrderDetails,
                    OrderDetailIngredients = ingOrderDetails
                };
                res.Add(orderResponse);
            }
            return res.OrderByDescending(r=>r.OrderDate).ToList();
        }

        public Task<ICollection<Order>> GetOrdersBy(
                Expression<Func<Order,
                bool>> filter = null,
                Func<IQueryable<Order>,
                ICollection<Order>> options = null,
                string includeProperties = null)
        {
            return _OrderRepository.GetOrdersBy(filter, options, includeProperties);
        }

        public Task<ICollection<Order>> GetRandomOrdersBy(
                Expression<Func<Order, bool>> filter = null,
                Func<IQueryable<Order>, ICollection<Order>> options = null,
                string includeProperties = null,
                int numberItem = 0)
        {
            return _OrderRepository.GetNItemRandom(filter, numberItem: numberItem);
        }

        public async Task<string> AddNewOrder(Order newOrder)
        {
            var redirectUrl = "";
            try
            {
                newOrder.OrderId = Guid.NewGuid();
                newOrder.OrderDate = DateTime.Now;
                newOrder.OrderStatus = (int)Status.OrderStatus.PENDING;

                #region Validation
                if (string.IsNullOrEmpty(newOrder.ShippedAddress))
                    throw new Exception(ErrorMessage.OrderError.ORDER_SHIPPING_ADDRESS_REQUIRED);
                if (newOrder.TotalPrice < 10000)
                    throw new Exception(ErrorMessage.OrderError.ORDER_TOTAL_PRICE_NOT_VALID);

                var voucher = newOrder.VoucherId.HasValue
                    ? await _voucherRepository.GetByIdAsync(newOrder.VoucherId.Value)
                    : null;
                if (voucher != null)
                {
                    decimal price = 0;
                    foreach (var detail in newOrder.OrderDetails)
                    {
                        price += detail.Price.GetValueOrDefault() * detail.Quantity.GetValueOrDefault();
                    }

                    if (voucher.Discount > 0 && voucher.Discount <= 1)
                    {
                        if (price < voucher.MinimumOrderPrice.GetValueOrDefault())
                            throw new Exception(ErrorMessage.OrderError.ORDER_TOTAL_PRICE_NOT_VALID_TO_USE_VOUCHER);
                        var discountAmount = price * voucher.Discount > voucher.MaximumOrderPrice
                            ? voucher.MaximumOrderPrice
                            : price * voucher.Discount;
                        if (newOrder.TotalPrice != price - discountAmount)
                            throw new Exception(ErrorMessage.OrderError.ORDER_TOTAL_PRICE_NOT_VALID);
                    }
                    else
                    {
                        if (price < voucher.MinimumOrderPrice.GetValueOrDefault())
                            throw new Exception(ErrorMessage.OrderError.ORDER_TOTAL_PRICE_NOT_VALID_TO_USE_VOUCHER);
                        if (newOrder.TotalPrice != price - voucher.Discount)
                            throw new Exception(ErrorMessage.OrderError.ORDER_TOTAL_PRICE_NOT_VALID);
                    }
                }
                #endregion


                #region create transaction
                var transaction = new Library.Models.Transaction()
                {
                    TransactionId = newOrder.OrderId,
                    TotalAmount = newOrder.TotalPrice.Value,
                    CreatedDate = DateTime.Now,
                    TransactionStatus = (int)Status.TransactionStatus.PENDING,
                    CustomerId = newOrder.CustomerId
                };
                #endregion

                var transactionScope = _OrderRepository.Transaction();
                using (transactionScope)
                {
                    try
                    {
                        newOrder.OrderDetails.ToList().ForEach(detail =>
                        {
                            detail.OrderDetailId = Guid.NewGuid();
                            detail.OrderId = newOrder.OrderId;
                        });

                        await _OrderRepository.AddAsync(newOrder);

                        if (newOrder.PaymentMethod.HasValue)
                        {
                            switch (newOrder.PaymentMethod.GetValueOrDefault())
                            {
                                case (int)PaymentMethodEnum.PaymentMethods.PAYPAL:
                                    newOrder.OrderStatus = (int)Status.OrderStatus.PAYING;
                                    await _transactionRepository.AddAsync(transaction);
                                    redirectUrl = await PaymentWithPaypal(newOrder.OrderId);
                                    break;
                                case (int)PaymentMethodEnum.PaymentMethods.COD:
                                    break;
                                default:
                                    throw new Exception(ErrorMessage.OrderError.ORDER_PAYMENT_METHOD_NOT_VALID);
                            }

                            newOrder.PaypalUrl = redirectUrl;

                            await _OrderRepository.UpdateAsync(newOrder);
                        }
                        else
                            throw new Exception(ErrorMessage.OrderError.ORDER_PAYMENT_METHOD_NOT_VALID);
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Transaction fail - {e.Message}");
                    }
                    transactionScope.Commit();

                    #region Create notification
                    var id = Guid.NewGuid();
                    var noti = new Notification
                    {
                        NotificationId = id,
                        Description = $"Đơn hàng - {newOrder.OrderId} đã được tạo",
                        CreatedDate = DateTime.Now,
                        Status = false,
                        ReceiverId = null
                    };
                    await _notificationRepository.AddAsync(noti);

                    await _hubContext.Clients.All.SendAsync("OrderCreated", JsonConvert.SerializeObject(noti));
                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Add new order fail - {ex.Message}");
            }

            return redirectUrl;
        }

        public async Task UpdateOrder(Order OrderUpdate)
        {
            await _OrderRepository.UpdateAsync(OrderUpdate);
        }

        public Order GetOrderByID(Guid? id)
        {
            return _OrderRepository.GetFirstOrDefaultAsync(x => x.OrderId.Equals(id)).Result;
        }

        public async Task DeleteOrder(Guid id)
        {
            var order = await _OrderRepository.GetByIdAsync(id);

            if (order == null)
                throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

            order.OrderStatus = (int)Status.OrderStatus.DELETED;
            await _OrderRepository.UpdateAsync(order);
        }

        public async Task<ICollection<Order>> GetByCustomer(Guid id)
        {
            return await _OrderRepository.GetOrdersBy(o => o.CustomerId.Equals(id) && o.OrderStatus != (int)Status.OrderStatus.DELETED,
                includeProperties: "OrderDetails");
        }

        public async Task PaidOrder(Guid orderId)
        {
            var order = await _OrderRepository.GetByIdAsync(orderId);

            if (order == null || order.OrderStatus == (int)Status.OrderStatus.DELETED)
                throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

            if (order.OrderStatus == (int)Status.OrderStatus.PENDING)
                return;
            if (order.PaymentMethod != (int)PaymentMethods.PAYPAL)
                throw new Exception(ErrorMessage.OrderError.ORDER_CANNOT_CHANGE_STATUS);

            var customer = await _customerRepository.GetByIdAsync(order.CustomerId.Value);

            if (order.OrderStatus != (int)Status.OrderStatus.PAYING)
                throw new Exception(ErrorMessage.OrderError.ORDER_CANNOT_CHANGE_STATUS);

            order.OrderStatus = (int)Status.OrderStatus.PENDING;

            #region update order status, create transaction if COD, update status if Paypal
            var transactionScope = _OrderRepository.Transaction();
            using (transactionScope)
            {
                await _OrderRepository.UpdateAsync(order);

                if (order.PaymentMethod == (int)PaymentMethodEnum.PaymentMethods.PAYPAL)
                {
                    var transaction = await _transactionRepository.GetByIdAsync(orderId);
                    if (transaction == null)
                        throw new Exception(ErrorMessage.TransactionError.TRANSACTION_NOT_FOUND);
                    transaction.TransactionStatus = (int)Status.TransactionStatus.SUCCESS;
                    await _transactionRepository.UpdateAsync(transaction);
                }
                else
                {
                    #region create transaction
                    var transaction = new Library.Models.Transaction()
                    {
                        TransactionId = order.OrderId,
                        TotalAmount = order.TotalPrice.Value,
                        CreatedDate = DateTime.Now,
                        TransactionStatus = (int)Status.TransactionStatus.PENDING,
                        CustomerId = order.CustomerId
                    };
                    #endregion
                    await _transactionRepository.AddAsync(transaction);
                }

                transactionScope.Commit();
            }
            #endregion
        }

        public async Task AcceptOrder(Guid orderId)
        {
            var order = await _OrderRepository.GetByIdAsync(orderId);

            if (order == null || order.OrderStatus == (int)Status.OrderStatus.DELETED)
                throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

            if (order.OrderStatus == (int)Status.OrderStatus.ACCEPTED)
                return;

            var customer = await _customerRepository.GetByIdAsync(order.CustomerId.Value);

            if (order.OrderStatus != (int)Status.OrderStatus.PENDING)
                throw new Exception(ErrorMessage.OrderError.ORDER_CANNOT_CHANGE_STATUS);

            order.OrderStatus = (int)Status.OrderStatus.ACCEPTED;

            await _OrderRepository.UpdateAsync(order);

            if(order.VoucherId.HasValue)
                await _customerVoucherService.DeleteCustomerVoucher(order.VoucherId.Value);

            #region sending mail
            if (customer.Email != null)
            {
                // gui mail thong tin order
                var mailSubject = $"Xác nhận đơn đặt hàng của Quý Khách đã được duyệt thành công";
                var mailBody = $"Kính gửi Quý Khách hàng,\n" +
                    $"\n" +
                    $"Chúng tôi xin trân trọng thông báo rằng đơn đặt hàng của Quý Khách đã được duyệt thành công. Chúng tôi xin cảm ơn Quý Khách đã tin tưởng và lựa chọn sản phẩm/dịch vụ của chúng tôi\n" +
                    $"\n" +
                    $"Thông tin chi tiết của đơn đặt hàng của Quý Khách đã được xác nhận và sẽ được chúng tôi tiến hành xử lý trong thời gian sớm nhất.\n" +
                    $"Nếu Quý Khách cần hỗ trợ hoặc có bất kỳ yêu cầu nào khác, xin vui lòng liên hệ với chúng tôi qua thông tin liên lạc được cung cấp ở dưới đây.\n" +
                    $"\n" +
                    $"Email: homnayangii.info@gmail.com\n" +
                    $"Hotline: 0123456789" +
                    $"\n" +
                    $"Trân trọng cảm ơn và mong được phục vụ Quý Khách!\n" +
                    $"\n" +
                    $"Trân Trọng, \n" +
                    $"Homnayangi"
                    ;

                SendMail(mailSubject, mailBody, customer.Email);
            }
            #endregion

            #region Create notification
            var id = Guid.NewGuid();
            var noti = new Notification
            {
                NotificationId = id,
                Description = $"Đơn hàng - {order.OrderId} đã được chấp nhận",
                CreatedDate = DateTime.Now,
                Status = false,
                ReceiverId = customer.CustomerId
            };
            await _notificationRepository.AddAsync(noti);

            await _hubContext.Clients.All.SendAsync($"{customer.CustomerId}_OrderStatusChanged", JsonConvert.SerializeObject(noti));
            #endregion
        }

        public async Task DenyOrder(Guid id)
        {
            var order = await _OrderRepository.GetByIdAsync(id);

            if (order == null || order.OrderStatus == (int)Status.OrderStatus.DELETED)
                throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

            if (order.OrderStatus == (int)Status.OrderStatus.DENIED)
                return;

            var customer = await _customerRepository.GetByIdAsync(order.CustomerId.Value);


            order.OrderStatus = order.PaymentMethod == (int)PaymentMethodEnum.PaymentMethods.PAYPAL
                ? (int)Status.OrderStatus.NEED_REFUND
                : (int)Status.OrderStatus.DENIED;

            var transactionScope = _OrderRepository.Transaction();
            using (transactionScope)
            {
                await _OrderRepository.UpdateAsync(order);

                if (order.PaymentMethod == (int)PaymentMethodEnum.PaymentMethods.PAYPAL)
                {
                    var transaction = await _transactionRepository.GetByIdAsync(order.OrderId);
                    if (transaction == null)
                        throw new Exception(ErrorMessage.TransactionError.TRANSACTION_NOT_FOUND);
                    transaction.TransactionStatus = (int)Status.TransactionStatus.FAIL;
                    await _transactionRepository.UpdateAsync(transaction);
                }

                #region sending mail
                if (customer.Email != null)
                {
                    // gui mail thong bao don hang bi tu choi
                    var mailSubject = $"[Từ chối đơn hàng] Thông tin đơn hàng #{order.OrderId}";
                    var mailBody = $"Đơn hàng #{order.OrderId} đã bị từ chối.";

                    SendMail(mailSubject, mailBody, customer.Email);
                }
                #endregion

                transactionScope.Commit();
            }
        }

        public async Task RefundOrder(Guid id)
        {
            var order = await _OrderRepository.GetByIdAsync(id);

            if (order == null || order.OrderStatus == (int)Status.OrderStatus.DELETED)
                throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

            if (order.OrderStatus == (int)Status.OrderStatus.REFUND)
                return;

            var transaction = await _transactionRepository.GetByIdAsync(id);
            if (order.OrderStatus != (int)Status.OrderStatus.NEED_REFUND)
                throw new Exception(ErrorMessage.OrderError.ORDER_CANNOT_CHANGE_STATUS);

            order.OrderStatus = (int)Status.OrderStatus.REFUND;

            await _OrderRepository.UpdateAsync(order);

        }

        public async Task CancelOrder(Guid id)
        {
            var order = await _OrderRepository.GetByIdAsync(id);

            if (order == null || order.OrderStatus == (int)Status.OrderStatus.DELETED)
                throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

            if (order.OrderStatus == (int)Status.OrderStatus.CANCEL)
                return;

            var customer = await _customerRepository.GetByIdAsync(order.CustomerId.Value);

            if (order.OrderStatus == (int)Status.OrderStatus.PAYING)
                order.OrderStatus = (int)Status.OrderStatus.CANCEL;
            if (order.OrderStatus == (int)Status.OrderStatus.PENDING)
                order.OrderStatus = (int)Status.OrderStatus.NEED_REFUND;

            var transactionScope = _OrderRepository.Transaction();
            using (transactionScope)
            {
                await _OrderRepository.UpdateAsync(order);

                if (order.PaymentMethod == (int)PaymentMethodEnum.PaymentMethods.PAYPAL)
                {
                    var transaction = await _transactionRepository.GetByIdAsync(order.OrderId);
                    if (transaction != null)
                    {
                        transaction.TransactionStatus = (int)Status.TransactionStatus.FAIL;
                        await _transactionRepository.UpdateAsync(transaction);
                    }
                }

                #region sending mail
                if (customer.Email != null)
                {
                    // gui mail thong bao don hang bi huy
                    var mailSubject = $"[Hủy đơn hàng] Thông tin đơn hàng #{order.OrderId}";
                    var mailBody = $"Bạn đã hủy đơn hàng #{order.OrderId}.";

                    SendMail(mailSubject, mailBody, customer.Email);
                }
                #endregion

                transactionScope.Commit();
            }

            #region Create notification
            var notiId = Guid.NewGuid();
            var noti = new Notification
            {
                NotificationId = notiId,
                Description = $"Đơn hàng - {order.OrderId} đã bị hủy",
                CreatedDate = DateTime.Now,
                Status = false,
                ReceiverId = null
            };
            await _notificationRepository.AddAsync(noti);

            await _hubContext.Clients.All.SendAsync("OrderStatusChanged", JsonConvert.SerializeObject(noti));
            #endregion
        }

        public async Task Shipping(Guid id)
        {
            try
            {
                var order = await _OrderRepository.GetByIdAsync(id);

                if (order == null || order.OrderStatus == (int)Status.OrderStatus.DELETED)
                    throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

                if (order.OrderStatus == (int)Status.OrderStatus.SHIPPING)
                    return;

                if (order.OrderStatus != (int)Status.OrderStatus.ACCEPTED)
                    throw new Exception(ErrorMessage.OrderError.ORDER_CANNOT_CHANGE_STATUS);

                order.OrderStatus = (int)Status.OrderStatus.SHIPPING;
                await _OrderRepository.UpdateAsync(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Shipping Status fail - {ex.Message}");
                throw new Exception($"Update Shipping Status fail");
            }

        }

        public async Task Delivered(Guid id)
        {
            try
            {
                var order = await _OrderRepository.GetByIdAsync(id);

                if (order == null || order.OrderStatus == (int)Status.OrderStatus.DELETED)
                    throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

                if (order.OrderStatus == (int)Status.OrderStatus.DELIVERED)
                    return;

                if (order.OrderStatus != (int)Status.OrderStatus.SHIPPING)
                    throw new Exception(ErrorMessage.OrderError.ORDER_CANNOT_CHANGE_STATUS);

                order.OrderStatus = (int)Status.OrderStatus.DELIVERED;
                await _OrderRepository.UpdateAsync(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Delivered Status fail - {ex.Message}");
                throw new Exception($"Update Delivered Status fail");
            }

        }

        public async Task DeliveredFail(Guid id)
        {
            try
            {
                var order = await _OrderRepository.GetByIdAsync(id);

                if (order == null || order.OrderStatus == (int)Status.OrderStatus.DELETED)
                    throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

                if (order.OrderStatus == (int)Status.OrderStatus.DELIVERED_FAIL)
                    return;

                if (order.OrderStatus != (int)Status.OrderStatus.SHIPPING)
                    throw new Exception(ErrorMessage.OrderError.ORDER_CANNOT_CHANGE_STATUS);

                order.OrderStatus = (int)Status.OrderStatus.DELIVERED_FAIL;
                await _OrderRepository.UpdateAsync(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Delivered Status fail - {ex.Message}");
                throw new Exception($"Update Delivered Status fail");
            }

        }

        public void SendMail(string mailSubject, string mailBody, string receiver)
        {


            try
            {
                var message = new Message(
                  to: new string[] {
                   receiver
                  },
                  subject: mailSubject,
                  content: mailBody);
                _emailSender.SendEmail(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ErrorMessage.MailError.MAIL_SENDING_ERROR} - {ex.Message}");
                throw new Exception(ErrorMessage.MailError.MAIL_SENDING_ERROR);
            }
        }

        public async Task<string> PaymentWithPaypal(
            Guid orderId,
            string Cancel = null,
            string blogId = "",
            string PayerID = "",
            string guid = "")
        {
            var clientId = _configuration.GetValue<string>("Paypal:Key");
            var clientSecret = _configuration.GetValue<string>("Paypal:Secret");
            var mode = _configuration.GetValue<string>("Paypal:mode");
            PayPal.Api.APIContext apiContext = GetAPIContext(clientId, clientSecret, mode);
            string returnURI = _configuration.GetValue<string>("Paypal:returnURI");

            try
            {
                guid = Convert.ToString(orderId);

                Console.WriteLine($"Creating payment for order {guid}");
                var createdPayment = await CreatePaymentAsync(apiContext, returnURI + "guid=" + guid, blogId, orderId);
                Console.WriteLine($"Created - {createdPayment.ConvertToJson()}");

                var links = createdPayment.links.GetEnumerator();
                string paypalRedirectUrl = null;

                while (links.MoveNext())
                {
                    PayPal.Api.Links lnk = links.Current;
                    if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                    {
                        paypalRedirectUrl = lnk.href;
                    }
                }

                if (string.IsNullOrEmpty(paypalRedirectUrl))
                    throw new Exception("PaymentFailed");
                return paypalRedirectUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Creating payment Failed - " + ex.Message);
                throw new Exception("PaymentFailed");
            }
        }

        private async Task<PayPal.Api.Payment> CreatePaymentAsync(
            PayPal.Api.APIContext apiContext,
            string redirectUrl,
            string blogId,
            Guid orderId)
        {
            var currencyRate = _configuration.GetValue<string>("Paypal:currencyRate");

            var transaction = await _transactionRepository.GetByIdAsync(orderId);
            var order = await _OrderRepository.GetByIdAsync(orderId);
            if (transaction == null)
                throw new Exception(ErrorMessage.TransactionError.TRANSACTION_NOT_FOUND);

            var payer = new PayPal.Api.Payer()
            {
                payment_method = "paypal"
            };

            var redirUrls = new PayPal.Api.RedirectUrls()
            {
                cancel_url = redirectUrl + $"&Cancel=true&IsCooked={order.IsCooked ?? false}",
                return_url = redirectUrl + $"&Cancel=false&IsCooked={order.IsCooked ?? false}"
            };

            var amount = new PayPal.Api.Amount()
            {
                currency = "USD",
                total = (transaction.TotalAmount.GetValueOrDefault() / Decimal.Parse(currencyRate)).ToString("#.##")
            };

            var transactionList = new List<PayPal.Api.Transaction>();

            transactionList.Add(new PayPal.Api.Transaction()
            {
                invoice_number = transaction.TransactionId.ToString(),
                amount = amount
            });

            var payment = new PayPal.Api.Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            return payment.Create(apiContext);
        }

        private static string GetAccessToken(string clientId, string clientSecret, string mode)
        {
            string accessToken = new PayPal.Api.OAuthTokenCredential(
                clientId,
                clientSecret,
                new Dictionary<string, string>()
            {
                { "mode", mode }
            }).GetAccessToken();

            return accessToken;
        }

        public static PayPal.Api.APIContext GetAPIContext(string clientId, string clientSecret, string mode)
        {
            PayPal.Api.APIContext apiContext = new PayPal.Api.APIContext(GetAccessToken(clientId, clientSecret, mode));
            apiContext.Config = new Dictionary<string, string>()
            {
                { "mode", mode }
            };
            return apiContext;
        }

        public List<string> GetLocalDistrict()
        {
            return Constants.LOCAL_DISTRICT.Keys.ToList();
        }

        public List<string> GetLocalWard(string district)
        {
            return Constants.LOCAL_DISTRICT.GetValueOrDefault(district);
        }

        public async Task<ICollection<Order>> GetAll()
        {
            return await _OrderRepository.GetAll();
        }

        public async Task<ICollection<OrderResponse>> GetOrderByCustomer(DateTime? fromDate, DateTime? toDate, Guid customerId, int status = -1)
        {
            if (fromDate.HasValue && !toDate.HasValue)
                throw new Exception("To date is required");
            if (!fromDate.HasValue && toDate.HasValue)
                throw new Exception("From date is required");

            var orders = status > -1
                ? await _OrderRepository.GetOrdersBy(o => o.OrderStatus.GetValueOrDefault() == status && o.CustomerId.Equals(customerId),
                includeProperties: "OrderDetails")
                : await _OrderRepository.GetOrdersBy(o => o.CustomerId.Equals(customerId),
                includeProperties: "OrderDetails");

            if (fromDate.HasValue && toDate.HasValue)
            {
                if (fromDate.Value.CompareTo(toDate.Value) > 0)
                    throw new Exception("From date must before To date");
                orders = orders.Where(o => o.OrderDate.GetValueOrDefault() >= fromDate && o.OrderDate.GetValueOrDefault() <= toDate).ToList();
            }

            var ingredients = await _ingredientRepository.GetAll();
            var recipes = await _recipeRepository.GetRecipesBy(includeProperties: "RecipeDetails");

            var res = new List<OrderResponse>();
            foreach (var order in orders)
            {
                var ingOrderDetails = order.OrderDetails.Where(detail => detail.RecipeId == null)
                    .Join(ingredients, x => x.IngredientId, y => y.IngredientId, (x, y) =>
                    {
                        return new OrderResponse.IngredientResponse
                        {
                            IngredientId = x.IngredientId,
                            Quantity = x.Quantity,
                            Price = x.Price,
                            IngredientImage = y.Picture,
                            IngredientName = y.Name
                        };
                    })
                    .ToList();

                var recipeOrderDetails = order.OrderDetails.Where(detail => detail.RecipeId != null)
                    .Join(recipes, x => x.RecipeId, y => y.RecipeId, (x, y) =>
                    {
                        return new OrderResponse.OrderDetailResponse
                        {
                            OrderId = order.OrderId,
                            RecipeId = y.RecipeId,
                            RecipeImage = y.ImageUrl ?? "",
                            RecipeName = y.Title ?? "",
                            RecipeQuantity = x.Quantity.Value,
                            PackagePrice = y.PackagePrice,
                            CookedPrice = y.CookedPrice,
                            RecipeDetails = y.RecipeDetails.Join(ingredients, x => x.IngredientId, y => y.IngredientId, (x, y) =>
                            {
                                return new OrderResponse.IngredientResponse
                                {
                                    IngredientId = y.IngredientId,
                                    Quantity = x.Quantity,
                                    Price = y.Price,
                                    IngredientImage = y.Picture,
                                    IngredientName = y.Name
                                };
                            }).ToList()
                        };
                    })
                    .ToList();

                var orderResponse = new OrderResponse
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    ShippedDate = order.ShippedDate,
                    ShippedAddress = order.ShippedAddress,
                    TotalPrice = order.TotalPrice,
                    OrderStatus = order.OrderStatus,
                    CustomerId = order.CustomerId,
                    IsCooked = order.IsCooked,
                    VoucherId = order.VoucherId,
                    PaymentMethod = order.PaymentMethod,
                    PaypalUrl = order.PaypalUrl,
                    OrderDetailRecipes = recipeOrderDetails,
                    OrderDetailIngredients = ingOrderDetails
                };
                res.Add(orderResponse);
            }
            return res.OrderByDescending(r => r.OrderDate).ToList();
        }

    }
}