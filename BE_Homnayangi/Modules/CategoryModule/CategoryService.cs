using BE_Homnayangi.Modules.CategoryModule.Interface;
using BE_Homnayangi.Modules.CategoryModule.Request;
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
        public async Task<Guid> AddNewCategory(CreateCategoryRequest categoryRequest)
        {
            var newCategory = new Category();
            newCategory.CategoryId = Guid.NewGuid();
            newCategory.Name = categoryRequest.Name;
            newCategory.Description = categoryRequest.Description;
            newCategory.Status = true;
            newCategory.CreatedDate = DateTime.Now;
            await _categoryRepository.AddAsync(newCategory);

            return newCategory.CategoryId;
        }
        public async Task<Boolean> UpdateCategory(UpdateCategoryRequest categoryRequest)
        {
            var categoryUpdate = _categoryRepository.GetFirstOrDefaultAsync(x => x.CategoryId == categoryRequest.CategoryId).Result;
            if (categoryUpdate == null) return false;

            categoryUpdate.Name = categoryRequest.Name ?? categoryUpdate.Name;
            categoryUpdate.Description = categoryRequest.Description ?? categoryUpdate.Description;
            categoryUpdate.Status = categoryRequest.Status ?? categoryUpdate.Status;

            await _categoryRepository.UpdateAsync(categoryUpdate);
            return true;
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

        public async Task<ICollection<DropdownCategory>> GetDropdownCategory()
        {
            List<DropdownCategory> result = new List<DropdownCategory>();
            try
            {
                var categories = await _categoryRepository.GetCategoriesBy(c => c.Status.Value);
                if (categories.Count > 0)
                {
                    result = categories.Select(cate => new DropdownCategory()
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
