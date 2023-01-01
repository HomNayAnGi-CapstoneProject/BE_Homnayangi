using System;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.RecipeModule.Interface
{
	public interface IRecipeServices
	{
        public Task AddNewRecipe(Recipe newRecipe);
        public Task UpdateRecipe(Recipe RecipeUpdate);
        public Task DeleteRecipe(Guid? ID);
        public Task<ICollection<Recipe>> GetAll();
        public Task<ICollection<Recipe>> GetRecipesBy(Expression<Func<Recipe, bool>> filter = null,
            Func<IQueryable<Recipe>, ICollection<Recipe>> options = null,
            string includeProperties = null);
        public Recipe GetRecipeByID(Guid? recipeId);
    }
}

