using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderDetailModule.Interface
{
    public interface IOrderDetailService
    {
        public Task AddNewOrderDetail(OrderDetail newOrderDetail);

        public Task UpdateOrderDetail(OrderDetail OrderDetailUpdate);

        public Task<ICollection<OrderDetail>> GetAll();

        public Task<ICollection<OrderDetail>> GetOrderDetailsBy(
            Expression<Func<OrderDetail, bool>> filter = null,
            Func<IQueryable<OrderDetail>, ICollection<OrderDetail>> options = null,
            string includeProperties = null);

        public Task<ICollection<OrderDetail>> GetRandomOrderDetailsBy(Expression<Func<OrderDetail, bool>> filter = null,
            Func<IQueryable<OrderDetail>, ICollection<OrderDetail>> options = null,
            string includeProperties = null,
            int numberItem = 0);

        public OrderDetail GetOrderDetailByID(Guid? id);

        public Task DeleteOrderDetail(Guid id);
    }
}
