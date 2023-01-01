using BE_Homnayangi.Modules.BlogModule.Interface;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using Library.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.RecipeModule
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public async Task<ICollection<Recipe>> GetAll()
        {
            return await _recipeRepository.GetAll();
        }

        public Task<ICollection<Recipe>> GetRecipesBy(
                Expression<Func<Recipe,
                bool>> filter = null,
                Func<IQueryable<Recipe>,
                ICollection<Recipe>> options = null,
                string includeProperties = null)
        {
            return _recipeRepository.GetRecipesBy(filter);
        }

        public Task<ICollection<Recipe>> GetRandomRecipesBy(
                Expression<Func<Recipe, bool>> filter = null,
                Func<IQueryable<Recipe>, ICollection<Recipe>> options = null,
                string includeProperties = null,
                int numberItem = 0)
        {
            return _recipeRepository.GetNItemRandom(filter, numberItem: numberItem);
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

        public Recipe GetRecipeByID(Guid? id)
        {
            return _recipeRepository.GetFirstOrDefaultAsync(x => x.RecipeId.Equals(id)).Result;
        }
    }
}
