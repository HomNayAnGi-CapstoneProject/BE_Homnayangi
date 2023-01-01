using System;
using Library.Models;
using Repository.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.RecipeDetailModule.Interface
{
    public interface IRecipeDetailRepository : IRepository<RecipeDetail>
    {
        public Task<ICollection<RecipeDetail>> GetRecipeDetailsBy(
               Expression<Func<RecipeDetail, bool>> filter = null,
               Func<IQueryable<RecipeDetail>, ICollection<RecipeDetail>> options = null,
               string includeProperties = null
           );
    }
}

