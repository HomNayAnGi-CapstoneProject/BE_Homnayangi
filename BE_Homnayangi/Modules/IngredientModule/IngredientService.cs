using BE_Homnayangi.Modules.DTO.IngredientDTO;
using BE_Homnayangi.Modules.IngredientModule.IngredientDTO;
using BE_Homnayangi.Modules.IngredientModule.Interface;
using BE_Homnayangi.Modules.Utils;
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

        public async Task<ICollection<IngredientResponse>> GetAll()
        {
            var ingredients = await _IngredientRepository.GetAll();

            return ingredients.Select(ToResponse).ToList();
        }

        public async Task<ICollection<IngredientResponse>> GetIngredientsBy(Expression<Func<Ingredient, bool>> filter = null,
            Func<IQueryable<Ingredient>, ICollection<Ingredient>> options = null,
            string includeProperties = null)
        {
            var ingredients = await _IngredientRepository.GetIngredientsBy(filter, options, includeProperties);
            return ingredients.Select(ToResponse).ToList();
        }
        public async Task AddNewIngredient(IngredientRequest newIngredient)
        {
            newIngredient.IngredientId = Guid.NewGuid();
            await _IngredientRepository.AddAsync(ToModel(newIngredient));
        }

        public IngredientResponse GetIngredientByID(Guid? ingredientID)
        {
            try
            {
                var ingredient = _IngredientRepository.GetFirstOrDefaultAsync(x => x.IngredientId == ingredientID.Value && x.Status.Value).Result;
                return ToResponse(ingredient);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetIngredientByID: " + ex.Message);
                throw;
            }
        }

        public async Task<ICollection<IngredientResponse>> GetAllIngredient() // status = 1
        {
            try
            {
                var ingredients = await _IngredientRepository.GetIngredientsBy(x => x.Status.Value);
                return ingredients.Select(ToResponse).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetAllIngredient: " + ex.Message);
                throw;
            }
        }

        public async Task<ICollection<IngredientResponse>> GetIngredientsByTypeId(Guid typeId)
        {
            try
            {
                var ingredients = await _IngredientRepository.GetIngredientsBy(x => x.Status.Value && x.TypeId == typeId);
                return ingredients.Select(ToResponse).ToList();
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

        public async Task<bool> UpdateIngredient(IngredientRequest newIg)
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
                    current.ListImage = StringUtils.CompressContents(newIg.ListImage);
                    current.UpdatedDate = DateTime.Now;
                    current.Status = newIg.Status;
                    current.Price = newIg.Price;
                    current.TypeId = newIg.TypeId;
                    await _IngredientRepository.UpdateAsync(current);
                    current.ListImagePosition = newIg.ListImagePosition;
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

        public async Task<Guid> CreateIngredient(IngredientRequest newIg)
        {
            Guid ingredientId;
            try
            {
                newIg.IngredientId = Guid.NewGuid();
                newIg.Status = true;
                newIg.CreatedDate = DateTime.Now;
                await _IngredientRepository.AddAsync(ToModel(newIg));
                ingredientId = newIg.IngredientId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CreateIngredient: " + ex.Message);
                throw;
            }
            return ingredientId;
        }

        public IngredientResponse ToResponse(Ingredient ingredient)
        {
            return new IngredientResponse()
            {
                IngredientId = ingredient.IngredientId,
                Name = ingredient.Name,
                Description = ingredient.Description,
                Quantitative = ingredient.Quantitative,
                Picture = ingredient.Picture,
                ListImage = StringUtils.ExtractContents(ingredient.ListImage),
                CreatedDate = ingredient.CreatedDate,
                UpdatedDate = ingredient.UpdatedDate,
                Status = ingredient.Status,
                Price = ingredient.Price,
                TypeId = ingredient.Type?.TypeId,
                TypeName = ingredient.Type?.Name,
                TypeDescription = ingredient.Type?.Description,
                ListImagePosition = ingredient.ListImagePosition
            };
        }

        public Ingredient ToModel(IngredientRequest ingredientRequest)
        {
            return new Ingredient()
            {
                IngredientId = ingredientRequest.IngredientId,
                Name = ingredientRequest.Name,
                Description = ingredientRequest.Description,
                Quantitative = ingredientRequest.Quantitative,
                Picture = ingredientRequest.Picture,
                ListImage = StringUtils.CompressContents(ingredientRequest.ListImage),
                CreatedDate = ingredientRequest.CreatedDate,
                UpdatedDate = ingredientRequest.UpdatedDate,
                Status = ingredientRequest.Status,
                Price = ingredientRequest.Price,
                TypeId = ingredientRequest.TypeId,
                ListImagePosition = ingredientRequest.ListImagePosition
            };
        }
    }
}

