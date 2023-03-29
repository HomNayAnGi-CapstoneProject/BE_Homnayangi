using BE_Homnayangi.Modules.CustomerBadgeModule.DTO;
using Library.Models;
using Library.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CustomerBadgeModule.Interface
{
    public interface ICustomerBadgeService
    {
        public Task AddNewCustomerBadge(CustomerBadge newCusyomerBadge);

        public Task UpdateCustomerBadge(CustomerBadge customerBadgeUpdate);

        public Task<ICollection<CustomerBadge>> GetAll();

        public Task<ICollection<CustomerBadge>> GetCustomerBadgesBy(
            Expression<Func<CustomerBadge, bool>> filter = null,
            Func<IQueryable<CustomerBadge>, ICollection<CustomerBadge>> options = null,
            string includeProperties = null);

        public Task<ICollection<CustomerBadge>> GetBadgeByCusID(Guid? cusid);
        public Task<CustomerBadge> GetCustomerBadgeByCombineID(Guid cusId, Guid badgeId);
        public Task<PagedResponse<PagedList<CustomerBadge>>> GetAllPaged(CustomerBadgeFilterRequest request);
        public Task<bool> DeleteCustomerBadgeByCombineId(Guid cusId, Guid badgeId);

    }
}