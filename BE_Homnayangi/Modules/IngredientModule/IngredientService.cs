using System;
using BE_Homnayangi.Modules.IngredientModule.Interface;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.IngredientModule
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _IngredientRepository;

        public IngredientService(IIngredientRepository IngredientRepository)
        {
            _IngredientRepository = IngredientRepository;
        }

        public async Task<ICollection<Ingredient>> GetAll()
        {
            return await _IngredientRepository.GetAll();
        }
        public Task<ICollection<Ingredient>> GetIngredientsBy(Expression<Func<Ingredient, bool>> filter = null,
            Func<IQueryable<Ingredient>, ICollection<Ingredient>> options = null,
            string includeProperties = null)
        {
            return _IngredientRepository.GetIngredientsBy(filter, options, includeProperties);
        }
        public async Task AddNewIngredient(Ingredient newIngredient)
        {
            newIngredient.IngredientId = Guid.NewGuid();
            await _IngredientRepository.AddAsync(newIngredient);
        }
        public async Task UpdateIngredient(Ingredient IngredientUpdate)
        {
            await _IngredientRepository.UpdateAsync(IngredientUpdate);
        }
        public async Task DeleteIngredient(Guid? id)
        {
            //Ingredient IngredientDelete = _IngredientRepository.GetFirstOrDefaultAsync(x => x.IngredientId.Equals(id) && x.Status == true).Result;
            //if (IngredientDelete == null) return;
            //IngredientDelete.Status = false;
            //await _IngredientRepository.UpdateAsync(IngredientDelete);
            return;
        }
        public Ingredient GetIngredientByID(Guid? ingredientID)
        {
            return _IngredientRepository.GetFirstOrDefaultAsync(x => x.IngredientId.Equals(ingredientID)).Result;
        }
    }
}

