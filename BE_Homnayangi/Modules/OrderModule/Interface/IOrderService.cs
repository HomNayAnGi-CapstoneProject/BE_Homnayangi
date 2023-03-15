using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderModule.Interface
{
    public interface IOrderService
    {
        public Task AddNewOrder(Order newOrder);

        public Task UpdateOrder(Order OrderUpdate);

        public Task<ICollection<Order>> GetAll();

        public Task<ICollection<Order>> GetByCustomer(Guid id);

        public Task<ICollection<Order>> GetOrdersBy(
            Expression<Func<Order, bool>> filter = null,
            Func<IQueryable<Order>, ICollection<Order>> options = null,
            string includeProperties = null);

        public Task<ICollection<Order>> GetRandomOrdersBy(Expression<Func<Order, bool>> filter = null,
            Func<IQueryable<Order>, ICollection<Order>> options = null,
            string includeProperties = null,
            int numberItem = 0);

        public Order GetOrderByID(Guid? id);

        public Task DeleteOrder(Guid id);

        public Task AcceptOrder(Guid id);

        public Task DeniedOrder(Guid id);

        public Task CancelOrder(Guid id);

        public Task PaidOrder(Guid id);

        public Task UpdateShippingStatusOrder(Guid id, int status);

        public Task<string> PaymentWithPaypal(
            Guid orderId,
            string Cancel = null,
            string blogId = "",
            string PayerID = "",
            string guid = "");

        public Task<string> Checkout(Guid orderId);

        public Task<Order> GetCart(Guid customerId);

        public Task UpdateCart(Order OrderUpdate);
    }
}
