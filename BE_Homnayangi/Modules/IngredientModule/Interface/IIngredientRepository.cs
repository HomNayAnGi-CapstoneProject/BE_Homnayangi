using System;
using Library.Models;
using Repository.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.IngredientModule.Interface
{
    public interface IIngredientRepository : IRepository<Ingredient>
    {
        public Task<ICollection<Ingredient>> GetIngredientsBy(
               Expression<Func<Ingredient, bool>> filter = null,
               Func<IQueryable<Ingredient>, ICollection<Ingredient>> options = null,
               string includeProperties = null
           );
    }
}

