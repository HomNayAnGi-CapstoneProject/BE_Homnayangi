using BE_Homnayangi.Modules.RecipeDetailModule.Interface;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using Library.Models;
using Library.Models.Constant;
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
        private readonly IRecipeDetailRepository _recipeDetailRepository;

        public RecipeService(IRecipeRepository recipeRepository, IRecipeDetailRepository recipeDetailRepository)
        {
            _recipeRepository = recipeRepository;
            _recipeDetailRepository = recipeDetailRepository;
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

        #region DELETE - RESTORE RECIPE
        // OFF: recipe, recipeDetails
        public async Task DeleteRecipe(Guid id)
        {
            try
            {
                #region update Recipe status into 0
                Recipe removedRecipe = await _recipeRepository.GetFirstOrDefaultAsync(recipe => recipe.RecipeId == id && recipe.Status == 1);
                if (removedRecipe == null)
                    throw new Exception(ErrorMessage.RecipeError.RECIPE_NOT_FOUND);

                removedRecipe.Status = 0;
                await _recipeRepository.UpdateAsync(removedRecipe);
                #endregion

                #region update RecipeDetails status into 0
                ICollection<RecipeDetail> recipeDetails = await _recipeDetailRepository.GetRecipeDetailsBy(item => item.RecipeId == id && item.Status == 1);
                if (recipeDetails != null)
                {
                    foreach (var item in recipeDetails.ToList())
                    {
                        item.Status = 0;
                    }
                    await _recipeDetailRepository.UpdateRangeAsync(recipeDetails);
                }
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at DeleteRecipe: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        // ON: recipe, recipeDetails
        public async Task RestoreRecipe(Guid id)
        {
            try
            {
                #region update Recipe status into 1
                Recipe restoredRecipe = await _recipeRepository.GetFirstOrDefaultAsync(recipe => recipe.RecipeId == id && recipe.Status == 0);
                if (restoredRecipe == null)
                    throw new Exception(ErrorMessage.RecipeError.RECIPE_NOT_FOUND);

                restoredRecipe.Status = 1;
                await _recipeRepository.UpdateAsync(restoredRecipe);
                #endregion

                #region update RecipeDetails status into 0
                ICollection<RecipeDetail> recipeDetails = await _recipeDetailRepository.GetRecipeDetailsBy(item => item.RecipeId == id && item.Status == 0);
                if (recipeDetails != null)
                {
                    foreach (var item in recipeDetails.ToList())
                    {
                        item.Status = 1;
                    }
                    await _recipeDetailRepository.UpdateRangeAsync(recipeDetails);
                }
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at DeleteRecipe: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
