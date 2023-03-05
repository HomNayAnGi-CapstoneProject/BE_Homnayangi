using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderCookedDetailModule.Interface
{
    public interface IOrderCookedDetailService
    {
        public Task AddNewOrderCookedDetail(OrderCookedDetail newOrderCookedDetail);

        public Task UpdateOrderCookedDetail(OrderCookedDetail OrderCookedDetailUpdate);

        public Task<ICollection<OrderCookedDetail>> GetAll();

        public Task<ICollection<OrderCookedDetail>> GetOrderCookedDetailsBy(
            Expression<Func<OrderCookedDetail, bool>> filter = null,
            Func<IQueryable<OrderCookedDetail>, ICollection<OrderCookedDetail>> options = null,
            string includeProperties = null);

        public Task<ICollection<OrderCookedDetail>> GetRandomOrderCookedDetailsBy(Expression<Func<OrderCookedDetail, bool>> filter = null,
            Func<IQueryable<OrderCookedDetail>, ICollection<OrderCookedDetail>> options = null,
            string includeProperties = null,
            int numberItem = 0);

        public OrderCookedDetail GetOrderCookedDetailByID(Guid? id);

        public Task DeleteOrderCookedDetail(Guid id);
    }
}
