using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using Library.Models;

namespace BE_Homnayangi.Modules.RecipeModule
{
	public class RecipeServices : IRecipeServices
	{
        private readonly IRecipeRepository _recipeRepository;

		public RecipeServices(IRecipeRepository recipeRepository)
		{
            _recipeRepository = recipeRepository;
		}

        public async Task<ICollection<Recipe>> GetAll()
        {
            return await _recipeRepository.GetAll();
        }
        public Task<ICollection<Recipe>> GetRecipesBy(Expression<Func<Recipe, bool>> filter = null,
            Func<IQueryable<Recipe>, ICollection<Recipe>> options = null,
            string includeProperties = null)
        {
            return _recipeRepository.GetRecipesBy(filter, options, includeProperties);
        }
        public async Task AddNewRecipe(Recipe newRecipe)
        {
            newRecipe.RecipeId = Guid.NewGuid();
            await _recipeRepository.AddAsync(newRecipe);
        }
        public async Task UpdateRecipe(Recipe recipeUpdate)
        {
            await _recipeRepository.UpdateAsync(recipeUpdate);
        }
        public async Task DeleteRecipe(Guid? id)
        {
            //Recipe recipeDelete = _recipeRepository.GetFirstOrDefaultAsync(x => x.RecipeId.Equals(id) && x.Status == true).Result;
            //if (recipeDelete == null) return;
            //recipeDelete.Status = false;
            //await _recipeRepository.UpdateAsync(recipeDelete);
            return;
        }
        public Recipe GetRecipeByID(Guid? recipeID)
        {
            return _recipeRepository.GetFirstOrDefaultAsync(x => x.RecipeId.Equals(recipeID)).Result;
        }
    }
}

