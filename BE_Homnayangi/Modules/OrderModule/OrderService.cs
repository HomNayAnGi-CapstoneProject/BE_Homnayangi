using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.OrderDetailModule.Interface;
using BE_Homnayangi.Modules.OrderModule.Interface;
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
        IConfiguration _configuration;

        public OrderService(IOrderRepository OrderRepository,
            IOrderDetailRepository orderDetailRepository,
            ICustomerRepository customerRepository,
            IUserRepository userRepository,
            ITransactionRepository transactionRepository,
            IConfiguration configuration)
        {
            _OrderRepository = OrderRepository;
            _orderDetailRepository = orderDetailRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
            _configuration = configuration;
        }

        public async Task<ICollection<Order>> GetAll()
        {
            return await _OrderRepository.GetOrdersBy(
                includeProperties: "OrderDetails");
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

        public async Task AddNewOrder(Order newOrder)
        {
            //try
            //{
            //    newOrder.OrderId = Guid.NewGuid();
            //    newOrder.OrderDate = DateTime.Now;
            //    newOrder.OrderStatus = (int)Status.OrderStatus.CART;

            //    foreach (var detail in newOrder.OrderDetails)
            //    {
            //        detail.OrderId = newOrder.OrderId;
            //    }

            //    var transactionScope = _OrderRepository.Transaction();
            //    using (transactionScope)
            //    {
            //        await _OrderRepository.AddAsync(newOrder);
            //        await _orderDetailRepository.AddRangeAsync(newOrder.OrderDetails);

            //        transactionScope.Commit();
            //    }
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //    throw new Exception($"Add new order fail - {ex.Message}");
            //}
        }

        public async Task UpdateOrder(Order OrderUpdate)
        {
            await _OrderRepository.UpdateAsync(OrderUpdate);
        }

        public Order GetOrderByID(Guid? id)
        {
            return _OrderRepository.GetFirstOrDefaultAsync(x => x.OrderId.Equals(id)).Result;
        }

        // OFF: Order, OrderDetails
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

            if (order == null)
                throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

            var customer = await _customerRepository.GetByIdAsync(order.CustomerId.Value);

            if (order.OrderStatus != (int)Status.OrderStatus.PENDING)
                throw new Exception(ErrorMessage.OrderError.ORDER_CANNOT_CHANGE_STATUS);

            order.OrderStatus = (int)Status.OrderStatus.ACCEPTED;
            await _OrderRepository.UpdateAsync(order);

            if (order.PaymentMethod == (int)PaymentMethodEnum.PaymentMethods.PAYPAL)
            {
                var transaction = await _transactionRepository.GetByIdAsync(orderId);
                if (transaction == null)
                    throw new Exception("Transaction not found");
                transaction.TransactionStatus = (int)Status.TransactionStatus.SUCCESS;
            }

            #region sending mail
            // gui mail thong tin order
            var mailSubject = $"[Da duyet] Thong tin don hang #{order.OrderId}";
            var mailBody = $"Cam on ban da mua hang, don hang #{order.OrderId} da duoc duyet.\n" +
                $"Don hang cua ban dang duoc giao";

            SendMail(mailSubject, mailBody, customer.Email);
            #endregion
        }

        public async Task DeniedOrder(Guid id)
        {
            var order = await _OrderRepository.GetByIdAsync(id);

            if (order == null)
                throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

            var customer = await _customerRepository.GetByIdAsync(order.CustomerId.Value);

            order.OrderStatus = (int)Status.OrderStatus.DENIED;
            await _OrderRepository.UpdateAsync(order);

            #region sending mail
            // gui mail thong bao don hang bi tu choi
            var mailSubject = $"[That bai] Thong tin don hang #{order.OrderId}";
            var mailBody = $"Don hang #{order.OrderId} da bi tu choi.";

            SendMail(mailSubject, mailBody, customer.Email);
            #endregion

        }

        public async Task CancelOrder(Guid id)
        {
            var order = await _OrderRepository.GetByIdAsync(id);

            if (order == null)
                throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

            order.OrderStatus = (int)Status.OrderStatus.CANCEL;
            await _OrderRepository.UpdateAsync(order);
        }

        public async Task UpdateShippingStatusOrder(
            Guid id,
            [Range(6,8,ErrorMessage="Status must between 6 to 8")] int status)
        {
            var order = await _OrderRepository.GetByIdAsync(id);

            if (order == null)
                throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

            order.OrderStatus = status;
            await _OrderRepository.UpdateAsync(order);
        }

        public async Task PaidOrder(Guid id)
        {
            //var order = await _OrderRepository.GetByIdAsync(id);

            //if (order == null)
            //    throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

            //order.OrderStatus = (int)Status.OrderStatus.PAID;

            //// giao hang trong 1h sau khi thanh toan
            //order.ShippedDate = DateTime.Now.AddHours(1);

            //await _OrderRepository.UpdateAsync(order);
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
            catch
            {
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
            if (transaction == null)
                throw new Exception("Transaction not found");

            var payer = new PayPal.Api.Payer()
            {
                payment_method = "paypal"
            };

            var redirUrls = new PayPal.Api.RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
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

        public async Task<string> Checkout(Guid orderId)
        {
            var redirectUrl = "";

            var order = await _OrderRepository.GetByIdAsync(orderId);

            if (order == null)
                throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);
            if (!order.OrderStatus.Equals((int)Status.OrderStatus.CART))
                throw new Exception("Order not available for checkout");
            if (string.IsNullOrEmpty(order.ShippedAddress))
                throw new Exception("Shipping address required");
            if(order.TotalPrice < 1)
                throw new Exception("Total price must higher than 1");

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

            var transactionScope = _OrderRepository.Transaction();
            using (transactionScope)
            {
                await _transactionRepository.AddAsync(transaction);

                try
                {
                    if (order.PaymentMethod.HasValue)
                    {
                        switch (order.PaymentMethod.GetValueOrDefault())
                        {
                            case (int)PaymentMethodEnum.PaymentMethods.PAYPAL:
                                order.OrderDate = DateTime.Now;
                                order.OrderStatus = (int)Status.OrderStatus.PENDING;
                                await _OrderRepository.UpdateAsync(order);
                                redirectUrl = await PaymentWithPaypal(order.OrderId);
                                break;
                            case (int)PaymentMethodEnum.PaymentMethods.COD:
                                order.OrderStatus = (int)Status.OrderStatus.ACCEPTED;
                                await _OrderRepository.UpdateAsync(order);
                                break;
                            default:
                                throw new Exception("Payment method not valid");
                        }
                    }
                    else
                        throw new Exception("Payment method must not null");
                }
                catch (Exception e)
                {
                    throw new Exception($"Transaction fail - {e.Message}");
                }

                transactionScope.Commit();
            }
            

            return redirectUrl;
        }

        public async Task<Order> GetCart(Guid customerId)
        {
            var cart = _OrderRepository
                .GetOrdersBy(o =>
                        o.OrderStatus.Equals((int)Status.OrderStatus.CART)
                        && o.CustomerId.Equals(customerId),
                    includeProperties:"OrderDetails")
                .Result
                .FirstOrDefault();

            if (cart == null)
            {
                cart = new Order();
                cart.OrderId = Guid.NewGuid();
                cart.OrderStatus = (int) Status.OrderStatus.CART;
                cart.CustomerId = customerId;

                await _OrderRepository.AddAsync(cart);
            }

            return cart;
        }

        public async Task UpdateCart(Order order)
        {
            try
            {
                if (!order.OrderStatus.Equals((int)Status.OrderStatus.CART))
                    throw new Exception("Cannot update");

                var newOrderDetails = order.OrderDetails;
                order.OrderDetails = null;

                var transactionScope = _OrderRepository.Transaction();
                using (transactionScope)
                {
                    await _OrderRepository.UpdateAsync(order);
                    var oldDetails =
                        await _orderDetailRepository
                        .GetOrderDetailsBy(od => od.OrderId.Equals(order.OrderId));
                    if (oldDetails.Count > 0) await _orderDetailRepository.RemoveRangeAsync(oldDetails);
                    await _orderDetailRepository.AddRangeAsync(newOrderDetails);

                    transactionScope.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Update cart fail - {ex.Message}");
            }
        }
    }
}