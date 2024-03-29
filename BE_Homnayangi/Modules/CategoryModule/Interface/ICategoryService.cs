﻿using BE_Homnayangi.Modules.CategoryModule.Request;
using BE_Homnayangi.Modules.CategoryModule.Response;
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
        public Task<Guid> AddNewCategory(CreateCategoryRequest newCategory);
        public Task<Boolean> UpdateCategory(UpdateCategoryRequest categoryUpdate);
        public Task DeleteCategory(Guid? ID);
        public Task<ICollection<Category>> GetAll();
        public Task<ICollection<DropdownCategory>> GetDropdownCategory();
        public Task<ICollection<Category>> GetCategoriesBy(Expression<Func<Category, bool>> filter = null,
            Func<IQueryable<Category>, ICollection<Category>> options = null,
            string includeProperties = null);
        public Task<ICollection<Category>> GetRandomCategoriesBy(Expression<Func<Category, bool>> filter = null,
            Func<IQueryable<Category>, ICollection<Category>> options = null,
            string includeProperties = null,
            int numberItem = 0);
        public Category GetCategoryByID(Guid? cateID);
    }
}
