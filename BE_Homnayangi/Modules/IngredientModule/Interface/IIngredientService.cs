using System;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.DTO.IngredientDTO;
using BE_Homnayangi.Modules.IngredientModule.IngredientDTO;

namespace BE_Homnayangi.Modules.IngredientModule.Interface
{
	public interface IIngredientService
	{
        public Task AddNewIngredient(IngredientRequest newIngredient);
        public Task<ICollection<IngredientResponse>> GetAll();
        public Task<ICollection<IngredientResponse>> GetIngredientsBy(Expression<Func<Ingredient, bool>> filter = null,
            Func<IQueryable<Ingredient>, ICollection<Ingredient>> options = null,
            string includeProperties = null);
        public IngredientResponse GetIngredientByID(Guid? IngredientId);

        public Task<ICollection<IngredientResponse>> GetAllIngredient();

        public Task<ICollection<IngredientResponse>> GetIngredientsByTypeId(Guid typeId);

        public Task<bool> DeleteIngredient(Guid id);

        public Task<bool> UpdateIngredient(IngredientRequest newIg);

        public Task<bool> CreateIngredient(IngredientRequest newIg);
    }

}

