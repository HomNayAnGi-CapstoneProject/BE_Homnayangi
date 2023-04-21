using BE_Homnayangi.Modules.DTO.IngredientDTO;
using BE_Homnayangi.Modules.IngredientModule.IngredientDTO;
using BE_Homnayangi.Modules.IngredientModule.Request;
using BE_Homnayangi.Modules.IngredientModule.Response;
using Library.Models;
using Library.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.IngredientModule.Interface
{
    public interface IIngredientService
    {
        public Task AddNewIngredient(IngredientRequest newIngredient);
        public Task<ICollection<IngredientResponse>> GetAll(string? searchString);
        public Task<ICollection<IngredientResponse>> GetIngredientsBy(Expression<Func<Ingredient, bool>> filter = null,
            Func<IQueryable<Ingredient>, ICollection<Ingredient>> options = null,
            string includeProperties = null);
        public IngredientResponse GetIngredientByID(Guid? IngredientId);

        public Task<ICollection<IngredientResponse>> GetAllIngredients();
        public Task<PagedResponse<PagedList<IngredientResponse>>> GetAllIngredientsWithPagination(IngredientsByTypeRequest request);

        public Task<ICollection<IngredientResponse>> GetIngredientsByTypeId(Guid typeId);

        public Task<bool> DeleteIngredient(Guid id);

        public Task<bool> UpdateIngredient(IngredientRequest newIg);

        public Task<Guid> CreateIngredient(IngredientRequest newIg);
        public Task<ICollection<SearchIngredientsResponse>> GetIngredientByName(string name);

        public Task<ICollection<IngredientResponse>> GetIngredientsByTypeId(Guid typeId, Guid currentIngredientId);
    }

}

