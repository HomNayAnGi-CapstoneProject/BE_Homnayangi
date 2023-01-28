using Library.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.UserModule.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<ICollection<User>> GetUsersBy(
         Expression<Func<User, bool>> filter = null,
         Func<IQueryable<User>, ICollection<User>> options = null,
         string includeProperties = null
     );
    }
}
