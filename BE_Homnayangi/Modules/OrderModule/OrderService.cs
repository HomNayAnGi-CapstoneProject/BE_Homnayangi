using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.OrderCookedDetailModule.Interface;
using BE_Homnayangi.Modules.OrderIngredientDetailModule.Interface;
using BE_Homnayangi.Modules.OrderModule.Interface;
using BE_Homnayangi.Modules.OrderPackageDetailModule.Interface;
using BE_Homnayangi.Modules.TransactionModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Models;
using Library.Models.Constant;
using Library.Models.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderModule
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _OrderRepository;
        private readonly IOrderCookedDetailRepository _orderCookedDetailRepository;
        private readonly IOrderIngredientDetailRepository _orderIngredientDetailRepository;
        private readonly IOrderPackageDetailRepository _orderPackageDetailRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITransactionRepository _transactionRepository;

        public OrderService(IOrderRepository OrderRepository,
            IOrderCookedDetailRepository orderCookedDetailRepository,
            IOrderIngredientDetailRepository orderIngredientDetailRepository,
            IOrderPackageDetailRepository orderPackageDetailRepository,
            ICustomerRepository customerRepository,
            IUserRepository userRepository,
            ITransactionRepository transactionRepository)
        {
            _OrderRepository = OrderRepository;
            _orderCookedDetailRepository = orderCookedDetailRepository;
            _orderIngredientDetailRepository = orderIngredientDetailRepository;
            _orderPackageDetailRepository = orderPackageDetailRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<ICollection<Order>> GetAll()
        {
            return await _OrderRepository.GetOrdersBy(
                includeProperties: "OrderCookedDetails,OrderIngredientDetails,OrderPackageDetails");
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
            var isCookedOrder = newOrder.OrderCookedDetails.Count > 0;

            // cooked order la don rieng biet
            if (isCookedOrder
                && (newOrder.OrderIngredientDetails.Count > 0 || newOrder.OrderPackageDetails.Count > 0))
                throw new Exception(ErrorMessage.OrderError.COOKED_ORDER_NOT_VALID);

            newOrder.OrderId = new Guid();
            newOrder.OrderDate = DateTime.Now;
            newOrder.OrderStatus = (int) Status.OrderStatus.PENDING;

            await _OrderRepository.AddAsync(newOrder);

            if (isCookedOrder)
            {
                foreach (var detail in newOrder.OrderCookedDetails)
                {
                    detail.OrderId = newOrder.OrderId;
                    await _orderCookedDetailRepository.AddAsync(detail);
                }
                    
            }
            else 
            {
                foreach (var detail in newOrder.OrderIngredientDetails)
                {
                    detail.OrderId = newOrder.OrderId;
                    await _orderIngredientDetailRepository.AddAsync(detail);
                }

                foreach (var detail in newOrder.OrderPackageDetails)
                {
                    detail.OrderId = newOrder.OrderId;
                    await _orderPackageDetailRepository.AddAsync(detail);
                }
            }
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
                includeProperties: "OrderCookedDetails,OrderIngredientDetails,OrderPackageDetails");
        }

        public async Task AcceptOrder(Guid id)
        {
            var order = await _OrderRepository.GetByIdAsync(id);

            if (order == null)
                throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

            var customer = await _customerRepository.GetByIdAsync(order.CustomerId.Value);

            if (order.OrderStatus != (int)Status.OrderStatus.PENDING)
                throw new Exception(ErrorMessage.OrderError.ORDER_CANNOT_CHANGE_STATUS);

            order.OrderStatus = (int) Status.OrderStatus.ACCEPTED;
            await _OrderRepository.UpdateAsync(order);

            #region tao transaction
            var transaction = new Transaction()
            {
                TransactionId = order.OrderId,
                TotalAmount = order.TotalPrice,
                CreatedDate = DateTime.Now,
                TransactionStatus = (int) Status.TransactionStatus.PENDING,
                CustomerId = customer.CustomerId
            };
            await _transactionRepository.AddAsync(transaction);
            #endregion

            #region sending mail
            // gui mail thong tin order
            var mailSubject = $"[Da duyet] Thong tin don hang #{order.OrderId}";
            var mailBody = $"Cam on ban da mua hang, don hang #{order.OrderId} da duoc duyet.\n" +
                $"Don hang cua ban se duoc giao sau khi hoan tat thanh toan";

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

        public async Task UpdateShippingStatusOrder(Guid id, [Range(6,8,ErrorMessage="Status must between 6 to 8")] int status)
        {
            var order = await _OrderRepository.GetByIdAsync(id);

            if (order == null)
                throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

            order.OrderStatus = status;
            await _OrderRepository.UpdateAsync(order);
        }

        public async Task PaidOrder(Guid id)
        {
            var order = await _OrderRepository.GetByIdAsync(id);

            if (order == null)
                throw new Exception(ErrorMessage.OrderError.ORDER_NOT_FOUND);

            order.OrderStatus = (int)Status.OrderStatus.PAID;

            // giao hang trong 1h sau khi thanh toan
            order.ShippedDate = DateTime.Now.AddHours(1);

            await _OrderRepository.UpdateAsync(order);
        }

        public void SendMail(string mailSubject, string mailBody, string receiver)
        {
            var sender = "phucvhdse151523@fpt.edu.vn";
            var password = "19091997p";

            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(sender, password),
                    EnableSsl = true,
                };

                smtpClient.Send(sender, receiver, mailSubject, mailBody);
            }
            catch
            {
                throw new Exception(ErrorMessage.MailError.MAIL_SENDING_ERROR);
            }
        }
    }
}