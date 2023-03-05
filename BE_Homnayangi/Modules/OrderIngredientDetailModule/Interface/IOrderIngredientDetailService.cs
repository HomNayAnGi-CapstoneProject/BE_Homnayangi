using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderIngredientDetailModule.Interface
{
    public interface IOrderIngredientDetailService
    {
        public Task AddNewOrderIngredientDetail(OrderIngredientDetail newOrderIngredientDetail);

        public Task UpdateOrderIngredientDetail(OrderIngredientDetail OrderIngredientDetailUpdate);

        public Task<ICollection<OrderIngredientDetail>> GetAll();

        public Task<ICollection<OrderIngredientDetail>> GetOrderIngredientDetailsBy(
            Expression<Func<OrderIngredientDetail, bool>> filter = null,
            Func<IQueryable<OrderIngredientDetail>, ICollection<OrderIngredientDetail>> options = null,
            string includeProperties = null);

        public Task<ICollection<OrderIngredientDetail>> GetRandomOrderIngredientDetailsBy(Expression<Func<OrderIngredientDetail, bool>> filter = null,
            Func<IQueryable<OrderIngredientDetail>, ICollection<OrderIngredientDetail>> options = null,
            string includeProperties = null,
            int numberItem = 0);

        public OrderIngredientDetail GetOrderIngredientDetailByID(Guid? id);

        public Task DeleteOrderIngredientDetail(Guid id);
    }
}
