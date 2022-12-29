
using System;
using System.Collections.Generic;
using System.Linq;
using Library.Models;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Repository.Repository.Interface;

namespace BE_Homnayangi.Modules.CategoryModule.Interface
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Task<ICollection<Category>> GetCategoriesBy(
            Expression<Func<Category, bool>> filter = null,
            Func<IQueryable<Category>, ICollection<Category>> options = null,
            string includeProperties = null
        );
    }
}
