using Library.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CartModule.Interface
{
    public interface ICartRepository : IRepository<Cart>
    {
        public Task<ICollection<Cart>> GetCartsBy(
            Expression<Func<Cart, bool>> filter = null,
            Func<IQueryable<Cart>, ICollection<Cart>> options = null,
            string includeProperties = null
        );
    }
}
