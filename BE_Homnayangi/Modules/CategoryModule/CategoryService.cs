using BE_Homnayangi.Modules.CategoryModule.Interface;
using BE_Homnayangi.Modules.CategoryModule.Response;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CategoryModule
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<ICollection<Category>> GetAll()
        {
            return await _categoryRepository.GetAll();
        }
        public Task<ICollection<Category>> GetCategoriesBy(Expression<Func<Category, bool>> filter = null,
            Func<IQueryable<Category>, ICollection<Category>> options = null,
            string includeProperties = null)
        {
            return _categoryRepository.GetCategoriesBy(filter);
        }
        public Task<ICollection<Category>> GetRandomCategoriesBy(Expression<Func<Category, bool>> filter = null,
            Func<IQueryable<Category>, ICollection<Category>> options = null,
            string includeProperties = null,
            int numberItem = 0)
        {
            return _categoryRepository.GetNItemRandom(filter, numberItem: numberItem);
        }
        public async Task AddNewCategory(Category newCategory)
        {
            newCategory.CategoryId = Guid.NewGuid();
            await _categoryRepository.AddAsync(newCategory);
        }
        public async Task UpdateCategory(Category categoryUpdate)
        {
            await _categoryRepository.UpdateAsync(categoryUpdate);
        }
        public async Task DeleteCategory(Guid? id)
        {
            Category categoryDelete = _categoryRepository.GetFirstOrDefaultAsync(x => x.CategoryId.Equals(id) && x.Status == true).Result;
            if (categoryDelete == null) return;
            categoryDelete.Status = false;
            await _categoryRepository.UpdateAsync(categoryDelete);
        }
        public Category GetCategoryByID(Guid? cateID)
        {
            return _categoryRepository.GetFirstOrDefaultAsync(x => x.CategoryId.Equals(cateID)).Result;
        }

        public async Task<ICollection<OverviewCategory>> GetAllAvailableCategories()
        {
            List<OverviewCategory> result = new List<OverviewCategory>();
            try
            {
                var categories = await _categoryRepository.GetCategoriesBy(c => c.Status.Value);
                if (categories.Count > 0)
                {
                    result = categories.Select(cate => new OverviewCategory()
                    {
                        CategoryId = cate.CategoryId,
                        Name = cate.Name
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetAllAvailable: " + ex.Message);
                throw;
            }
            return result;
        }
    }
}
