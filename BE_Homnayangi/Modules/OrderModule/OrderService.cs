using AutoMapper;
using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.IngredientModule.Interface;
using BE_Homnayangi.Modules.OrderDetailModule.Interface;
using BE_Homnayangi.Modules.OrderModule.Interface;
using BE_Homnayangi.Modules.OrderModule.Response;
using BE_Homnayangi.Modules.RecipeModule;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using BE_Homnayangi.Modules.TransactionModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Models;
using Library.Models.Constant;
using Library.Models.Enum;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Transactions;

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
        IConfiguration _configuration;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository OrderRepository,
            IOrderDetailRepository orderDetailRepository,
            ICustomerRepository customerRepository,
            IUserRepository userRepository,
            ITransactionRepository transactionRepository,
            IIngredientRepository ingredientRepository,
            IRecipeRepository recipeRepository,
            IMapper mapper,
            IConfiguration configuration)
        {
            _OrderRepository = OrderRepository;
            _orderDetailRepository = orderDetailRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
            _ingredientRepository = ingredientRepository;
            _recipeRepository = recipeRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ICollection<OrderResponse>> GetOrderResponse(int status = -1)
        {
            var orders = status > -1
                ? await _OrderRepository.GetOrdersBy(o => o.OrderStatus.GetValueOrDefault() == status,
                includeProperties: "OrderDetails")
                : await _OrderRepository.GetOrdersBy(
                includeProperties: "OrderDetails");

            var ingredients = await _ingredientRepository.GetAll();
            var recipes = await _recipeRepository.GetAll();

            var res = new List<OrderResponse>();
            foreach(var order in orders)
            {
                var orderDetailResponses = recipes.Where(r => order.OrderDetails.Where(od => od.RecipeId == r.RecipeId).Any())
                    .GroupJoin(order.OrderDetails, x => x.RecipeId, y => y.RecipeId,
                        (recipe, ingredientGroup) => new OrderResponse.OrderDetailResponse
                        {
                            OrderId = order.OrderId,
                            RecipeId = recipe.RecipeId,
                            RecipeImage = recipe.ImageUrl ?? "",
                            RecipeName = recipe.Title ?? "",
                            RecipeDetails = ingredientGroup.Select(ig => {
                                var i = ingredients.Where(i => i.IngredientId == ig.IngredientId).FirstOrDefault();
                                return new OrderResponse.RecipeDetailResponse
                                {
                                    OrderDetailId = ig.OrderDetailId,
                                    IngredientId = ig.IngredientId,
                                    IngredientImage = i.Picture ?? "",
                                    IngredientName = i.Name ?? "",
                                    Price = ig.Price,
                                    Quantity = ig.Quantity
                                };
                            }).ToList()
                        }).ToList();
                var orderResponse = _mapper.Map<OrderResponse>(order);
                orderResponse.OrderDetails = orderDetailResponses;
                res.Add(orderResponse);
            }
            return res;
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

                if (string.IsNullOrEmpty(newOrder.ShippedAddress))
                    throw new Exception(ErrorMessage.OrderError.ORDER_SHIPPING_ADDRESS_REQUIRED);
                if (newOrder.TotalPrice < 10000)
                    throw new Exception(ErrorMessage.OrderError.ORDER_TOTAL_PRICE_NOT_VALID);

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
                            if (detail.RecipeId == null)
                                throw new Exception("Recipe id is required");
                        });
                        await _OrderRepository.AddAsync(newOrder);

                        await _transactionRepository.AddAsync(transaction);

                        if (newOrder.PaymentMethod.HasValue)
                        {
                            switch (newOrder.PaymentMethod.GetValueOrDefault())
                            {
                                case (int)PaymentMethodEnum.PaymentMethods.PAYPAL:
                                    redirectUrl = await PaymentWithPaypal(newOrder.OrderId);
                                    break;
                                case (int)PaymentMethodEnum.PaymentMethods.COD:
                                    break;
                                default:
                                    throw new Exception(ErrorMessage.OrderError.ORDER_PAYMENT_METHOD_NOT_VALID);
                            }
                        }
                        else
                            throw new Exception(ErrorMessage.OrderError.ORDER_PAYMENT_METHOD_NOT_VALID);
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"Transaction fail - {e.Message}");
                    }
                    
                    transactionScope.Commit();
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

            order.OrderStatus = (int) Status.OrderStatus.DELETED;
            await _OrderRepository.UpdateAsync(order);
        }

        public async Task<ICollection<Order>> GetByCustomer(Guid id)
        {
            return await _OrderRepository.GetOrdersBy(o => o.CustomerId.Equals(id) && o.OrderStatus != (int) Status.OrderStatus.DELETED,
                includeProperties: "OrderDetails");
        }

        public async Task AcceptOrder(Guid orderId)
        {
            var order = await _OrderRepository.GetByIdAsync(orderId);

            if (order == null || order.OrderStatus == (int)Status.OrderStatus.DELETED)
                throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

            var customer = await _customerRepository.GetByIdAsync(order.CustomerId.Value);

            if (order.OrderStatus != (int)Status.OrderStatus.PENDING)
                throw new Exception(ErrorMessage.OrderError.ORDER_CANNOT_CHANGE_STATUS);

            order.OrderStatus = (int)Status.OrderStatus.ACCEPTED;

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

                #region sending mail
                if (customer.Email != null)
                {
                    // gui mail thong tin order
                    var mailSubject = $"[Da duyet] Thong tin don hang #{order.OrderId}";
                    var mailBody = $"Cam on ban da mua hang, don hang #{order.OrderId} da duoc duyet.\n" +
                        $"Don hang cua ban dang duoc giao";

                    SendMail(mailSubject, mailBody, customer.Email);
                }
                #endregion

                transactionScope.Commit();
            }
            #endregion

        }

        public async Task DenyOrder(Guid id)
        {
            var order = await _OrderRepository.GetByIdAsync(id);

            if (order == null || order.OrderStatus == (int)Status.OrderStatus.DELETED)
                throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

            var customer = await _customerRepository.GetByIdAsync(order.CustomerId.Value);

            order.OrderStatus = (int)Status.OrderStatus.DENIED;

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
                    var mailSubject = $"[Tu choi don hang] Thong tin don hang #{order.OrderId}";
                    var mailBody = $"Don hang #{order.OrderId} da bi tu choi.";

                    SendMail(mailSubject, mailBody, customer.Email);
                }
                #endregion

                transactionScope.Commit();
            }
        }

        public async Task CancelOrder(Guid id)
        {
            var order = await _OrderRepository.GetByIdAsync(id);

            if (order == null || order.OrderStatus == (int)Status.OrderStatus.DELETED)
                throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

            var customer = await _customerRepository.GetByIdAsync(order.CustomerId.Value);

            order.OrderStatus = (int)Status.OrderStatus.CANCEL;

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
                    // gui mail thong bao don hang bi huy
                    var mailSubject = $"[Huy don hang] Thong tin don hang #{order.OrderId}";
                    var mailBody = $"Don hang #{order.OrderId} da bi huy.";

                    SendMail(mailSubject, mailBody, customer.Email);
                }
                #endregion

                transactionScope.Commit();
            }
        }

        public async Task Shipping(Guid id)
        {
            try
            {
                var order = await _OrderRepository.GetByIdAsync(id);

                if (order == null || order.OrderStatus == (int)Status.OrderStatus.DELETED)
                    throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

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
            var address = _configuration.GetValue<string>("MailService:Address");
            var password = _configuration.GetValue<string>("MailService:Password");
            var smtpClientConfig = _configuration.GetValue<string>("MailService:SmtpClient");

            try
            {
                var smtpClient = new SmtpClient(smtpClientConfig)
                {
                    Port = 587,
                    Credentials = new NetworkCredential(address, password),
                    EnableSsl = true,
                };

                smtpClient.Send(address, receiver, mailSubject, mailBody);
            }
            catch(Exception ex)
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
    }
}