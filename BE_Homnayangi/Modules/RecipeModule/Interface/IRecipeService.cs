using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.RecipeModule.Interface
{
    public interface IRecipeService
    {
        public Task AddNewRecipe(Recipe newRecipe);

        public Task UpdateRecipe(Recipe recipeUpdate);

        public Task<ICollection<Recipe>> GetAll();

        public Task<ICollection<Recipe>> GetRecipesBy(
            Expression<Func<Recipe, bool>> filter = null,
            Func<IQueryable<Recipe>, ICollection<Recipe>> options = null,
            string includeProperties = null);

        public Task<ICollection<Recipe>> GetRandomRecipesBy(Expression<Func<Recipe, bool>> filter = null,
            Func<IQueryable<Recipe>, ICollection<Recipe>> options = null,
            string includeProperties = null,
            int numberItem = 0);

        public Recipe GetRecipeByID(Guid? id);

        public Task DeleteRecipe(Guid id);

        public Task RestoreRecipe(Guid id);
    }
}
