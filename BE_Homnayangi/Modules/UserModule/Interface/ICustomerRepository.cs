using Library.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.UserModule.Interface
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        public Task<ICollection<Customer>> GetCustomersBy(
         Expression<Func<Customer, bool>> filter = null,
         Func<IQueryable<Customer>, ICollection<Customer>> options = null,
         string includeProperties = null
     );
    }
}
