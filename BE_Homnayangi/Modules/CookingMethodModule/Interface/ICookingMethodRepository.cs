
using System;
using System.Collections.Generic;
using System.Linq;
using Library.Models;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Repository.Repository.Interface;

namespace BE_Homnayangi.Modules.CookingMethodModule.Interface
{
    public interface ICookingMethodRepository : IRepository<CookingMethod>
    {
        public Task<ICollection<CookingMethod>> GetCookingMethodsBy(
            Expression<Func<CookingMethod, bool>> filter = null,
            Func<IQueryable<CookingMethod>, ICollection<CookingMethod>> options = null,
            string includeProperties = null
        );
    }
}
