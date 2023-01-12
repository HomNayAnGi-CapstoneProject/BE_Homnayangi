using BE_Homnayangi.Modules.IngredientModule.Interface;
using Library.Models;
using System;
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

        public Ingredient GetIngredientByID(Guid? ingredientID)
        {
            try
            {
                return _IngredientRepository.GetFirstOrDefaultAsync(x => x.IngredientId == ingredientID.Value && x.Status.Value).Result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetIngredientByID: " + ex.Message);
                throw;
            }
        }

        public Task<ICollection<Ingredient>> GetAllIngredient() // status = 1
        {
            try
            {
                return _IngredientRepository.GetIngredientsBy(x => x.Status.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetAllIngredient: " + ex.Message);
                throw;
            }
        }

        public Task<ICollection<Ingredient>> GetIngredientsByTypeId(Guid typeId)
        {
            try
            {
                return _IngredientRepository.GetIngredientsBy(x => x.Status.Value && x.TypeId == typeId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetIngredientByTypeId: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteIngredient(Guid id)
        {
            bool isDeleted = false;
            try
            {
                Ingredient igDelete = _IngredientRepository.GetFirstOrDefaultAsync(x => x.IngredientId == id && x.Status.Value).Result;
                if (igDelete != null)
                {
                    igDelete.Status = false;
                    await _IngredientRepository.UpdateAsync(igDelete);
                    isDeleted = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at DeleteIngredient: " + ex.Message);
                throw;
            }
            return isDeleted;
        }

        public async Task<bool> UpdateIngredient(Ingredient newIg)
        {
            bool isUpdated = false;
            try
            {
                Ingredient current = await _IngredientRepository.GetFirstOrDefaultAsync(x => newIg.IngredientId == x.IngredientId);
                if (current != null)
                {
                    current.Name = newIg.Name;
                    current.Description = newIg.Description;
                    current.Quantitative = newIg.Quantitative;
                    current.Picture = newIg.Picture;
                    current.UpdatedDate = DateTime.Now;
                    current.Status = newIg.Status;
                    current.Price = newIg.Price;
                    current.TypeId = newIg.TypeId;
                    await _IngredientRepository.UpdateAsync(current);
                    isUpdated = true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateIngredient: " + ex.Message);
                throw;
            }
            return isUpdated;
        }

        public async Task<bool> CreateIngredient(Ingredient newIg)
        {
            bool isInserted = false;
            try
            {
                newIg.IngredientId = Guid.NewGuid();
                newIg.Status = true;
                newIg.CreatedDate = DateTime.Now;
                await _IngredientRepository.AddAsync(newIg);
                isInserted = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CreateIngredient: " + ex.Message);
                throw;
            }
            return isInserted;
        }
    }
}

