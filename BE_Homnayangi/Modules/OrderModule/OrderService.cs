﻿using AutoMapper;
using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.CustomerVoucherModule.Interface;
using BE_Homnayangi.Modules.IngredientModule.Interface;
using BE_Homnayangi.Modules.OrderDetailModule.Interface;
using BE_Homnayangi.Modules.OrderModule.Interface;
using BE_Homnayangi.Modules.OrderModule.Response;
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
using System.Net.Http;
using BE_Homnayangi.Modules.PackageModule.Interface;
using GoogleMapsApi.Entities.Common;

using System.Drawing;


namespace BE_Homnayangi.Modules.OrderModule
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IPackageRepository _packageRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<SignalRServer> _hubContext;
        private readonly ICustomerVoucherService _customerVoucherService;
        IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly HttpClient _httpClient;

        public OrderService(IOrderRepository OrderRepository,
            IOrderDetailRepository orderDetailRepository,
            ICustomerRepository customerRepository,
            IUserRepository userRepository,
            IIngredientRepository ingredientRepository,
            IPackageRepository packageRepository,
            IVoucherRepository voucherRepository,
            INotificationRepository notificationRepository,
            IHubContext<SignalRServer> hubContext,
            ICustomerVoucherService customerVoucherService,
            IMapper mapper,
            IConfiguration configuration,
            IEmailSender emailSender,
            HttpClient httpClient)
        {
            _OrderRepository = OrderRepository;
            _orderDetailRepository = orderDetailRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _ingredientRepository = ingredientRepository;
            _packageRepository = packageRepository;
            _configuration = configuration;
            _voucherRepository = voucherRepository;
            _notificationRepository = notificationRepository;
            _hubContext = hubContext;
            _mapper = mapper;
            _emailSender = emailSender;
            _customerVoucherService = customerVoucherService;
            _httpClient = httpClient;
        }

        public async Task<ICollection<OrderResponse>> GetOrderResponse(DateTime? fromDate, DateTime? toDate, int status = -1)
        {
            if (fromDate.HasValue && !toDate.HasValue)
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
            var packages = await _packageRepository.GetPackagesBy(includeProperties: "PackageDetails");

            var res = new List<OrderResponse>();
            foreach (var order in orders)
            {

                var packageOrderDetails = order.OrderDetails.Where(detail => detail.PackageId != null)
                    .Join(packages, x => x.PackageId, y => y.PackageId, (x, y) =>
                    {
                        return new OrderResponse.OrderDetailResponse
                        {
                            OrderId = order.OrderId,
                            PackageId = y.PackageId,
                            PackageImage = y.ImageUrl ?? "",
                            PackageName = y.Title ?? "",
                            PackageQuantity = x.Quantity.Value,
                            PackagePrice = y.PackagePrice,
                            PackageDetails = y.PackageDetails.Join(ingredients, x => x.IngredientId, y => y.IngredientId, (x, y) =>
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
                    PackagePrice = order.TotalPrice,
                    ShippingCost = order.ShippingCost,
                    TotalPrice = order.TotalPrice + order.ShippingCost,
                    OrderStatus = order.OrderStatus,
                    CustomerId = order.CustomerId,
                    IsCooked = order.IsCooked,
                    VoucherId = order.CustomerVoucherId,
                    PaymentMethod = order.PaymentMethod,
                    PaypalUrl = order.PaypalUrl,
                    OrderDetailRecipes = packageOrderDetails,
                };
                res.Add(orderResponse);
            }

            if (status == 2)
                return res.Where(r => (r.ShippedDate.HasValue && r.ShippedDate.Value.Date == DateTime.Today.Date)
                    || (r.ShippedDate == null && r.OrderDate.Value.Date == DateTime.Today.Date))
                    .OrderByDescending(r => r.OrderDate).ToList();

            return res.OrderByDescending(r => r.OrderDate).ToList();
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

                var voucher = newOrder.CustomerVoucherId.HasValue
                    ? await _voucherRepository.GetByIdAsync(newOrder.CustomerVoucherId.Value)
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
                                    newOrder.TransactionStatus = (int)Status.TransactionStatus.PENDING;
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

                if (order.PaymentMethod == (int)PaymentMethodEnum.PaymentMethods.PAYPAL)
                {
                    order.TransactionStatus = (int)Status.TransactionStatus.SUCCESS;
                }
                else
                {
                    order.TransactionStatus = (int)Status.TransactionStatus.PENDING;
                }

                await _OrderRepository.UpdateAsync(order);
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
            if (!order.ShippedDate.HasValue)
                order.ShippedDate = DateTime.Now.AddMinutes(30);

            await _OrderRepository.UpdateAsync(order);

            if (order.CustomerVoucherId.HasValue)
                await _customerVoucherService.DeleteCustomerVoucher(order.CustomerVoucherId.Value, customer.CustomerId);

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


            #region sending mail
            if (customer.Email != null)
            {
                // gui mail thong tin order
                var mailSubject = $"[Xác nhận đơn hàng] Xác nhận đơn đặt hàng của Quý Khách đã được duyệt thành công";
                var mailBody = $"Kính gửi Quý Khách hàng,<br>" +
                    $"<br>" +
                    $"Chúng tôi xin trân trọng thông báo rằng đơn đặt hàng của Quý Khách đã được duyệt thành công. Chúng tôi xin cảm ơn Quý Khách đã tin tưởng và lựa chọn sản phẩm/dịch vụ của chúng tôi<br>" +
                    $"<br>" +
                    $"Thông tin chi tiết của đơn đặt hàng của Quý Khách đã được xác nhận và sẽ được chúng tôi tiến hành xử lý trong thời gian sớm nhất.<br>" +
                    $"Thời gian giao hàng dự kiến: {order.ShippedDate?.ToLocalTime()}.<br>" +
                    $"Nếu Quý Khách cần hỗ trợ hoặc có bất kỳ yêu cầu nào khác, xin vui lòng liên hệ với chúng tôi qua thông tin liên lạc được cung cấp ở dưới đây.<br>" +
                    $"<br>" +
                    $"Email: homnayangii.info@gmail.com<br>" +
                    $"Hotline: 0123456789" +
                    $"<br>" +
                    $"Trân trọng cảm ơn và mong được phục vụ Quý Khách!<br>" +
                    $"<br>" +
                    $"Trân Trọng, <br>" +
                    $"Homnayangi"
                    ;

                SendMail(mailSubject, mailBody, customer.Email);
            }
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
                    order.TransactionStatus = (int)Status.TransactionStatus.FAIL;
                }

                #region sending mail
                if (customer.Email != null)
                {
                    // gui mail thong bao don hang bi tu choi
                    var mailSubject = $"[Từ chối đơn hàng] Thông tin đơn hàng #{order.OrderId}";
                    var mailBody = "Kính gửi Quý Khách hàng,<br>" +
                    $"<br>" +
                    $"Chúng tôi rất tiếc khi phải thông báo rằng đơn đặt hàng của Quý Khách đã bị từ chối.<br>" +
                    $"<br>" +
                    $"Nếu Quý Khách cần hỗ trợ hoặc có bất kỳ thắc mắc nào, xin vui lòng liên hệ với chúng tôi qua thông tin liên lạc được cung cấp ở dưới đây.<br>" +
                    $"<br>" +
                    $"Email: homnayangii.info@gmail.com<br>" +
                    $"Hotline: 0123456789" +
                    $"<br>" +
                    $"Trân trọng cảm ơn và mong được phục vụ Quý Khách!<br>" +
                    $"<br>" +
                    $"Trân Trọng, <br>" +
                    $"Homnayangi"
                    ; ;

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

            order.OrderStatus = order.PaymentMethod == (int)PaymentMethodEnum.PaymentMethods.PAYPAL
                ? (int)Status.OrderStatus.NEED_REFUND
                : (int)Status.OrderStatus.CANCEL;

            var transactionScope = _OrderRepository.Transaction();
            using (transactionScope)
            {
                await _OrderRepository.UpdateAsync(order);

                if (order.PaymentMethod == (int)PaymentMethodEnum.PaymentMethods.PAYPAL)
                {
                    order.TransactionStatus = (int)Status.TransactionStatus.FAIL;
                }

                #region sending mail
                if (customer.Email != null)
                {
                    // gui mail thong bao don hang bi huy
                    var mailSubject = $"[Hủy đơn hàng] Thông tin đơn hàng #{order.OrderId}";
                    var mailBody = "Kính gửi Quý Khách hàng,<br>" +
                    $"<br>" +
                    $"Chúng tôi xin xác nhận rằng Quý Khách đã hủy đơn đặt hàng của mình.<br>" +
                    $"<br>" +
                    $"Nếu Quý Khách cần hỗ trợ hoặc có bất kỳ thắc mắc nào, xin vui lòng liên hệ với chúng tôi qua thông tin liên lạc được cung cấp ở dưới đây.<br>" +
                    $"<br>" +
                    $"Email: homnayangii.info@gmail.com<br>" +
                    $"Hotline: 0123456789" +
                    $"<br>" +
                    $"Trân trọng cảm ơn và mong được phục vụ Quý Khách!<br>" +
                    $"<br>" +
                    $"Trân Trọng, <br>" +
                    $"Homnayangi"
                    ; ;

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

                var customer = await _customerRepository.GetByIdAsync(order.CustomerId.Value);

                #region sending mail
                if (customer.Email != null)
                {
                    // gui mail thong tin order
                    var mailSubject = $"Xác nhận đơn đặt hàng của Quý Khách đã được giao thành công";
                    var mailBody = $"Kính gửi Quý Khách hàng,<br>" +
                        $"<br>" +
                        $"Chúng tôi xin trân trọng thông báo rằng đơn đặt hàng của Quý Khách đã được giao thành công. Chúng tôi xin cảm ơn Quý Khách đã tin tưởng và lựa chọn sản phẩm/dịch vụ của chúng tôi<br>" +
                        $"<br>" +
                        $"Nếu Quý Khách cần hỗ trợ hoặc có bất kỳ yêu cầu nào khác, xin vui lòng liên hệ với chúng tôi qua thông tin liên lạc được cung cấp ở dưới đây.<br>" +
                        $"<br>" +
                        $"Email: homnayangii.info@gmail.com<br>" +
                        $"Hotline: 0123456789" +
                        $"<br>" +
                        $"Trân trọng cảm ơn và mong được phục vụ Quý Khách!<br>" +
                        $"<br>" +
                        $"Trân Trọng, <br>" +
                        $"Homnayangi"
                        ;

                    SendMail(mailSubject, mailBody, customer.Email);
                }
                #endregion
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

                var customer = await _customerRepository.GetByIdAsync(order.CustomerId.Value);

                #region sending mail
                if (customer.Email != null)
                {
                    // gui mail thong tin order
                    var mailSubject = $"Đơn đặt hàng của Quý Khách đã giao không thành công";
                    var mailBody = $"Kính gửi Quý Khách hàng,<br>" +
                        $"<br>" +
                        $"Chúng tôi xin thông báo rằng đơn đặt hàng của Quý Khách đã không thể gai thành công.<br>" +
                        $"<br>" +
                        $"Nếu Quý Khách cần hỗ trợ hoặc có bất kỳ yêu cầu nào khác, xin vui lòng liên hệ với chúng tôi qua thông tin liên lạc được cung cấp ở dưới đây.<br>" +
                        $"<br>" +
                        $"Email: homnayangii.info@gmail.com<br>" +
                        $"Hotline: 0123456789" +
                        $"<br>" +
                        $"Trân trọng cảm ơn và mong được phục vụ Quý Khách!<br>" +
                        $"<br>" +
                        $"Trân Trọng, <br>" +
                        $"Homnayangi"
                        ;

                    SendMail(mailSubject, mailBody, customer.Email);
                }
                #endregion
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

            var order = await _OrderRepository.GetByIdAsync(orderId);

            var payer = new PayPal.Api.Payer()
            {
                payment_method = "paypal"
            };

            var redirUrls = new PayPal.Api.RedirectUrls()
            {
                cancel_url = redirectUrl + $"&Cancel=true&IsCooked={order.IsCooked ?? false}",
                return_url = redirectUrl + $"&Cancel=false&IsCooked={order.IsCooked ?? false}"
            };
            decimal totalPrice = order.TotalPrice.GetValueOrDefault() + order.ShippingCost.GetValueOrDefault();
            var amount = new PayPal.Api.Amount()
            {
                currency = "USD",
                //ABOUT TRANSACTION NEED FIX
                total = (totalPrice / Decimal.Parse(currencyRate)).ToString("#.##")
            };

            var transactionList = new List<PayPal.Api.Transaction>();

            transactionList.Add(new PayPal.Api.Transaction()
            {
                //ABOUT TRANSACTION NEED FIX
                invoice_number = order.OrderId.ToString(),
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
            var res = await _OrderRepository.GetAll();
            return res.OrderByDescending(o => o.OrderDate).ToList();
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
            var packages = await _packageRepository.GetPackagesBy(includeProperties: "PackageDetails");

            var res = new List<OrderResponse>();
            foreach (var order in orders)
            {

                var packageOrderDetails = order.OrderDetails.Where(detail => detail.PackageId != null)
                    .Join(packages, x => x.PackageId, y => y.PackageId, (x, y) =>
                    {
                        return new OrderResponse.OrderDetailResponse
                        {
                            OrderId = order.OrderId,
                            PackageId = y.PackageId,
                            PackageImage = y.ImageUrl ?? " ",
                            PackageName = y.Title ?? " ",
                            PackageQuantity = x.Quantity.Value,
                            PackagePrice = y.PackagePrice,
                            PackageDetails = y.PackageDetails.Join(ingredients, x => x.IngredientId, y => y.IngredientId, (x, y) =>
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
                    PackagePrice = order.TotalPrice,
                    ShippingCost = order.ShippingCost,
                    TotalPrice = order.TotalPrice + order.ShippingCost,
                    OrderStatus = order.OrderStatus,
                    CustomerId = order.CustomerId,
                    IsCooked = order.IsCooked,
                    VoucherId = order.CustomerVoucherId,
                    PaymentMethod = order.PaymentMethod,
                    PaypalUrl = order.PaypalUrl,
                    OrderDetailRecipes = packageOrderDetails
                };
                res.Add(orderResponse);
            }
            return res.OrderByDescending(r => r.OrderDate).ToList();
        }
        public async Task<decimal> CalculateShippingCost(double lat2, double lon2)
        {
            bool isWithinHoChiMinhCity = IsWithinHoChiMinhCity(lat2, lon2);
            if (!isWithinHoChiMinhCity)
            {
                throw new Exception($"Is not in Ho Chi Minh City");
            }
            Location location1 = new Location(10.841611269790572, 106.809507568837);
            Location location2 = new Location(lat2, lon2);
            string origin = location1.ToString();
            string destination = location2.ToString();
            string vehicle = "bike";
            string apiKey = "YJmpmPyakHgWJyrxBqI8RUccgGT2szN7TERBVm9B";
            var apiUrl = $"https://rsapi.goong.io/Direction?origin={origin}&destination={destination}&vehicle={vehicle}&api_key={apiKey}";

            var response = await _httpClient.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            // Xử lý kết quả JSON trả về
            var goongResponse = JsonConvert.DeserializeObject<GoongDistanceResponse>(responseContent);

            // Lấy khoảng cách từ kết quả
            var distance = goongResponse?.Routes?.FirstOrDefault()?.Legs?.FirstOrDefault().Distance.Value;
            double distanceInKilometers = (double)distance / 1000;

            const double baseRate = 5000; // Phí cơ bản cho 2km đầu tiên
            const double additionalRate = 200; // Mốc tăng thêm phí sau mỗi 2km

            // Tính số mốc tăng thêm
            int additionalSteps = (int)Math.Ceiling((distanceInKilometers / 2) - 1);
            if (additionalSteps < 0)
            {
                additionalSteps = 0;
            }

            // Tính phí vận chuyển
            var shippingCost = baseRate * distanceInKilometers + (additionalRate * additionalSteps);

            return (decimal)shippingCost;
            /*  DirectionsRequest distanceRequest = new DirectionsRequest();

              distanceRequest.ApiKey = "AIzaSyBABc-SHz6bgZkLkH2higaSTpPUaSdWQyY";

              Location location1 = new Location(10.841611269790572, 106.809507568837);
              Location location2 = new Location(lat2, lon2);
              distanceRequest.Origin = location1.ToString();
              distanceRequest.Destination = location2.ToString();



              var distanceResponse = await GoogleMaps.Directions.QueryAsync(distanceRequest);

              if (distanceResponse.Status == DirectionsStatusCodes.OK && distanceResponse.Routes.Any())
              {

                  // The distance is returned in meters, so we convert it to kilometers
                  var distanceInMeters = distanceResponse.Routes.First().Legs.Sum(l => l.Distance.Value);
                  var distanceInKilometers = (double)distanceInMeters / 1000;
                  return distanceInKilometers;
              }*/

            throw new Exception($"Failed to calculate distance");
        }
        private bool IsWithinHoChiMinhCity(double latitude, double longitude)
        {
            // Tọa độ giới hạn vùng Hồ Chí Minh
            const double minLatitude = 10.361;
            const double maxLatitude = 11.196;
            const double minLongitude = 106.289;
            const double maxLongitude = 107.145;

            // Kiểm tra xem tọa độ có nằm trong vùng Hồ Chí Minh không
            bool isWithinHoChiMinh = latitude >= minLatitude && latitude <= maxLatitude
                                    && longitude >= minLongitude && longitude <= maxLongitude;

            return isWithinHoChiMinh;
        }
        public async Task<FinancialReport> GetYearlyFinancialReport(int year)
        {
            if (year < 2000 || year > DateTime.Now.Year)
                throw new Exception("Invalid year");

            var startDate = new DateTime(year, 1, 1);
            var endDate = new DateTime(year, 12, 31).AddDays(1).Date.AddSeconds(-1);

            var orders = await _OrderRepository.GetOrdersBy(o => o.OrderStatus != (int)Status.OrderStatus.DELETED
                && o.OrderDate.HasValue && o.OrderDate.Value >= startDate && o.OrderDate <= endDate, includeProperties: "OrderDetails");
            var deliveredOrders = orders.Where(o => o.OrderStatus == (int)Status.OrderStatus.DELIVERED);
            var deniedOrders = orders.Where(o => o.OrderStatus == (int)Status.OrderStatus.DENIED);
            var cancelOrders = orders.Where(o => o.OrderStatus == (int)Status.OrderStatus.CANCEL);

            decimal revenue = orders.Select(o => o.TotalPrice.GetValueOrDefault()).Sum();

            decimal totalShipCost = orders.Select(o => o.ShippingCost.GetValueOrDefault()).Sum();
            int totalOrder = orders.Count;
            int totalPackages = orders.Select(o => o.OrderDetails.Select(od => od.Quantity.GetValueOrDefault()).Sum()).Sum();
            int deliveredOrderCount = deliveredOrders.Count();
            int deniedOrderCount = deniedOrders.Count();
            int canceledOrderCount = cancelOrders.Count();

            FinancialReport financialReport = new FinancialReport
            {
                StartDate = startDate,
                EndDate = endDate,
                Revenue = revenue,
                ShipCost = totalShipCost,
                TotalOrders = totalOrder,
                TotalPackages = totalPackages,
                DeliveredOrders = deliveredOrderCount,
                DeniedOrders = deniedOrderCount,
                CanceledOrders = canceledOrderCount
            };
            return financialReport;
        }

        public async Task<FinancialReport> GetMonthlyFinancialReport(int month, int year)
        {
            if (month < 1 || month > 12) throw new Exception("Invalid month");
            if (year < 2023 || year > DateTime.Now.Year) throw new Exception("Invalid year");

            var startDate = new DateTime(year, month, 1);
            var endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month)).AddDays(1).Date.AddSeconds(-1);

            var orders = await _OrderRepository.GetOrdersBy(o => o.OrderStatus != (int)Status.OrderStatus.DELETED
                && o.OrderDate.HasValue && o.OrderDate.Value >= startDate && o.OrderDate <= endDate, includeProperties: "OrderDetails");
            var deliveredOrders = orders.Where(o => o.OrderStatus == (int)Status.OrderStatus.DELIVERED);
            var deniedOrders = orders.Where(o => o.OrderStatus == (int)Status.OrderStatus.DENIED);
            var cancelOrders = orders.Where(o => o.OrderStatus == (int)Status.OrderStatus.CANCEL);

            decimal revenue = orders.Select(o => o.TotalPrice.GetValueOrDefault()).Sum();
            decimal totalShipCost = orders.Select(o => o.ShippingCost.GetValueOrDefault()).Sum();

            int totalOrder = orders.Count;
            int totalPackages = orders.Select(o => o.OrderDetails.Select(od => od.Quantity.GetValueOrDefault()).Sum()).Sum();
            int deliveredOrderCount = deliveredOrders.Count();
            int deniedOrderCount = deniedOrders.Count();
            int canceledOrderCount = cancelOrders.Count();

            var orderDetails = await _orderDetailRepository.GetOrderDetailsBy(od => orders.Select(o => o.OrderId).Contains(od.OrderId));
            var packages = await _packageRepository.GetAll();
            var trendingGroup = from orderDetail in orderDetails
                                group orderDetail by orderDetail.PackageId
                                  into g
                                select new TrendingPackage { PackageId = g.Key.GetValueOrDefault(), PackageTitle = packages.Where(p => p.PackageId == g.Key).First()?.Title, Count = g.Select(s => s.Quantity.GetValueOrDefault()).Sum() };


            FinancialReport financialReport = new FinancialReport
            {
                StartDate = startDate,
                EndDate = endDate,
                Revenue = revenue,
                ShipCost = totalShipCost,
                TotalOrders = totalOrder,
                TotalPackages = totalPackages,
                DeliveredOrders = deliveredOrderCount,
                DeniedOrders = deniedOrderCount,
                CanceledOrders = canceledOrderCount
            };
            return financialReport;
        }

        public async Task<FinancialReport> GetDailyFinancialReport(int day, int month, int year)
        {
            if (month < 1 || month > 12) throw new Exception("Invalid month");
            if (year < 2023 || year > DateTime.Now.Year) throw new Exception("Invalid year");

            var date = new DateTime(year, month, day);

            var orders = await _OrderRepository.GetOrdersBy(o => o.OrderStatus != (int)Status.OrderStatus.DELETED
                && o.OrderDate.HasValue && o.OrderDate.Value.Date == date.Date, includeProperties: "OrderDetails");
            var deliveredOrders = orders.Where(o => o.OrderStatus == (int)Status.OrderStatus.DELIVERED);
            var deniedOrders = orders.Where(o => o.OrderStatus == (int)Status.OrderStatus.DENIED);
            var cancelOrders = orders.Where(o => o.OrderStatus == (int)Status.OrderStatus.CANCEL);

            decimal revenue = orders.Select(o => o.TotalPrice.GetValueOrDefault()).Sum();
            decimal totalShipCost = orders.Select(o => o.ShippingCost.GetValueOrDefault()).Sum();

            int totalOrder = orders.Count;
            int totalPackages = orders.Select(o => o.OrderDetails.Select(od => od.Quantity.GetValueOrDefault()).Sum()).Sum();
            int deliveredOrderCount = deliveredOrders.Count();
            int deniedOrderCount = deniedOrders.Count();
            int canceledOrderCount = cancelOrders.Count();

            FinancialReport financialReport = new FinancialReport
            {
                StartDate = date,
                EndDate = date.AddDays(1).Date.AddSeconds(-1),
                Revenue = revenue,
                ShipCost = totalShipCost,
                TotalOrders = totalOrder,
                TotalPackages = totalPackages,
                DeliveredOrders = deliveredOrderCount,
                DeniedOrders = deniedOrderCount,
                CanceledOrders = canceledOrderCount
            };
            return financialReport;
        }

        public async Task<FinancialReport> GetFinancialReport(DateTime startDate, DateTime endDate)
        {
            if (!Utils.DateTimeUtils.CheckValidFromAndToDate(startDate, endDate))
                throw new Exception("Invalid start and end date");

            var orders = await _OrderRepository.GetOrdersBy(o => o.OrderStatus != (int)Status.OrderStatus.DELETED
                && o.OrderDate.HasValue && o.OrderDate.Value >= startDate && o.OrderDate <= endDate, includeProperties: "OrderDetails");
            var deliveredOrders = orders.Where(o => o.OrderStatus == (int)Status.OrderStatus.DELIVERED);
            var deniedOrders = orders.Where(o => o.OrderStatus == (int)Status.OrderStatus.DENIED);
            var cancelOrders = orders.Where(o => o.OrderStatus == (int)Status.OrderStatus.CANCEL);

            decimal revenue = orders.Select(o => o.TotalPrice.GetValueOrDefault()).Sum();
            int totalPackages = orders.Select(o => o.OrderDetails.Select(od => od.Quantity.GetValueOrDefault()).Sum()).Sum();
            decimal totalShipCost = orders.Select(o => o.ShippingCost.GetValueOrDefault()).Sum();

            int totalOrder = orders.Count;
            int deliveredOrderCount = deliveredOrders.Count();
            int deniedOrderCount = deniedOrders.Count();
            int canceledOrderCount = cancelOrders.Count();

            FinancialReport financialReport = new FinancialReport
            {
                StartDate = startDate,
                EndDate = endDate,
                Revenue = revenue,
                ShipCost = totalShipCost,
                TotalOrders = totalOrder,
                TotalPackages = totalPackages,
                DeliveredOrders = deliveredOrderCount,
                DeniedOrders = deniedOrderCount,
                CanceledOrders = canceledOrderCount
            };
            return financialReport;
        }

        public async Task<ICollection<TrendingPackage>> GetTrendingPackages(DateTime startDate, DateTime endDate)
        {
            if (!Utils.DateTimeUtils.CheckValidFromAndToDate(startDate, endDate))
                throw new Exception("Invalid start and end date");

            var orders = await _OrderRepository.GetOrdersBy(o => o.OrderStatus != (int)Status.OrderStatus.DELETED
                && o.OrderDate.HasValue && o.OrderDate.Value >= startDate && o.OrderDate <= endDate, includeProperties: "OrderDetails");
            var orderDetails = await _orderDetailRepository.GetOrderDetailsBy(od => orders.Select(o => o.OrderId).Contains(od.OrderId));
            var packages = await _packageRepository.GetAll();
            var trendingGroup = from orderDetail in orderDetails
                                group orderDetail by orderDetail.PackageId
                                  into g
                                select new TrendingPackage { PackageId = g.Key.GetValueOrDefault(), PackageTitle = packages.Where(p => p.PackageId == g.Key).First()?.Title, Count = g.Select(s => s.Quantity.GetValueOrDefault()).Sum() };
            return trendingGroup.OrderByDescending(t => t.Count).Take(10).ToList();
        }

        public async Task<ICollection<FinancialReport>> ExportYearlyFinancialReport(int year)
        {
            if (year < 2000 || year > DateTime.Now.Year)
                throw new Exception("Invalid year");

            var yearlyReport = new List<FinancialReport>();
            for (int i = 1; i <= 12; i++)
            {
                var record = await GetMonthlyFinancialReport(i, year);
                yearlyReport.Add(record);
            }
            return yearlyReport;
        }

        public async Task<ICollection<FinancialReport>> ExportMonthlyFinancialReport(int month, int year)
        {
            if (month < 1 || month > 12) throw new Exception("Invalid month");
            if (year < 2023 || year > DateTime.Now.Year) throw new Exception("Invalid year");

            var monthlyReport = new List<FinancialReport>();
            var endDate = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            for (int i = 1; i <= endDate.Day; i++)
            {
                var record = await GetDailyFinancialReport(i, month, year);
                monthlyReport.Add(record);
            }
            return monthlyReport;
        }

    }
}