using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CategoryModule.Interface
{
    public interface ICategoryService
    {
        public Task AddNewCategory(Category newCategory);
        public Task UpdateCategory(Category categoryUpdate);
        public Task DeleteCategory(Guid? ID);
        public ICollection<Category> GetAll();
        public ICollection<Category> GetCategoriesBy(Expression<Func<Category, bool>> filter = null,
            Func<IQueryable<Category>, ICollection<Category>> options = null,
            string includeProperties = null);
        public Category GetCategoryByID(Guid? cateID);
    }
}
