using BE_Homnayangi.Modules.RecipeDetailModule.Interface;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.RecipeDetailModule
{
    public class RecipeDetailService : IRecipeDetailService
    {
        private readonly IRecipeDetailRepository _recipeDetailRepository;

        public RecipeDetailService(IRecipeDetailRepository recipeDetailRepository)
        {
            _recipeDetailRepository = recipeDetailRepository;
        }

        public Task<ICollection<RecipeDetail>> GetRecipeDetailsBy(Expression<Func<RecipeDetail, bool>> filter = null,
            Func<IQueryable<RecipeDetail>, ICollection<RecipeDetail>> options = null,
            string includeProperties = null)
        {
            return _recipeDetailRepository.GetRecipeDetailsBy(filter, options, includeProperties);
        }
    }
}

