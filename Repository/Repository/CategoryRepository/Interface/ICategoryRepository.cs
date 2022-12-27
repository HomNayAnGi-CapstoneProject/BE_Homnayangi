using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using Library.Models;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Repository.Repository.CategoryRepository.Interface
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
