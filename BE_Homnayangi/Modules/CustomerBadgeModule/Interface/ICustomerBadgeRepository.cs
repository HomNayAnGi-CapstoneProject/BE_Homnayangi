using Library.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CustomerBadgeModule.Interface
{
    public interface ICustomerBadgeRepository : IRepository<CustomerBadge>
    {
        public Task<ICollection<CustomerBadge>> GetCustomersBy(
         Expression<Func<CustomerBadge, bool>> filter = null,
         Func<IQueryable<CustomerBadge>, ICollection<CustomerBadge>> options = null,
         string includeProperties = null);
    }
}
