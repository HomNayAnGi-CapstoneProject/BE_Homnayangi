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
        public Task<ICollection<Ingredient>> GetAll();
        public Task<ICollection<Ingredient>> GetIngredientsBy(Expression<Func<Ingredient, bool>> filter = null,
            Func<IQueryable<Ingredient>, ICollection<Ingredient>> options = null,
            string includeProperties = null);
        public Ingredient GetIngredientByID(Guid? IngredientId);

        public Task<ICollection<Ingredient>> GetAllIngredient();

        public Task<ICollection<Ingredient>> GetIngredientsByTypeId(Guid typeId);

        public Task<bool> DeleteIngredient(Guid id);

        public Task<bool> UpdateIngredient(Ingredient newIg);

        public Task<bool> CreateIngredient(Ingredient newIg);
    }

}

