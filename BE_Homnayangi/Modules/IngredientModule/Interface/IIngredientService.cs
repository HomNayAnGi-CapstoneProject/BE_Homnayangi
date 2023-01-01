using System;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.IngredientModule.Interface
{
	public interface IIngredientService
	{
        public Task AddNewIngredient(Ingredient newIngredient);
        public Task UpdateIngredient(Ingredient IngredientUpdate);
        public Task DeleteIngredient(Guid? ID);
        public Task<ICollection<Ingredient>> GetAll();
        public Task<ICollection<Ingredient>> GetIngredientsBy(Expression<Func<Ingredient, bool>> filter = null,
            Func<IQueryable<Ingredient>, ICollection<Ingredient>> options = null,
            string includeProperties = null);
        public Ingredient GetIngredientByID(Guid? IngredientId);
    }

}

