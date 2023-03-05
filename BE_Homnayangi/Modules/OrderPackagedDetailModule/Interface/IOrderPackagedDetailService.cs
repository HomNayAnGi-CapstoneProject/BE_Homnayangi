using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderPackageDetailModule.Interface
{
    public interface IOrderPackageDetailService
    {
        public Task AddNewOrderPackageDetail(OrderPackageDetail newOrderPackageDetail);

        public Task UpdateOrderPackageDetail(OrderPackageDetail OrderPackageDetailUpdate);

        public Task<ICollection<OrderPackageDetail>> GetAll();

        public Task<ICollection<OrderPackageDetail>> GetOrderPackageDetailsBy(
            Expression<Func<OrderPackageDetail, bool>> filter = null,
            Func<IQueryable<OrderPackageDetail>, ICollection<OrderPackageDetail>> options = null,
            string includeProperties = null);

        public Task<ICollection<OrderPackageDetail>> GetRandomOrderPackageDetailsBy(Expression<Func<OrderPackageDetail, bool>> filter = null,
            Func<IQueryable<OrderPackageDetail>, ICollection<OrderPackageDetail>> options = null,
            string includeProperties = null,
            int numberItem = 0);

        public OrderPackageDetail GetOrderPackageDetailByID(Guid? id);

        public Task DeleteOrderPackageDetail(Guid id);
    }
}
