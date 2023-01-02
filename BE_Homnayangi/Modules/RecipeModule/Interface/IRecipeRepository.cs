using System;
using Library.Models;
using Repository.Repository.Interface;﻿
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.RecipeModule.Interface
{
	public interface IRecipeRepository : IRepository<Recipe>
	{
        public Task<ICollection<Recipe>> GetRecipesBy(
               Expression<Func<Recipe, bool>> filter = null,
               Func<IQueryable<Recipe>, ICollection<Recipe>> options = null,
               string includeProperties = null
           );
    }
}
