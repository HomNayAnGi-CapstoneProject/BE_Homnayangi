using BE_Homnayangi.Modules.DTO.IngredientDTO;
using BE_Homnayangi.Modules.IngredientModule.IngredientDTO;
using BE_Homnayangi.Modules.IngredientModule.Interface;
using BE_Homnayangi.Modules.IngredientModule.Response;
using BE_Homnayangi.Modules.PriceNoteModule.Interface;
using BE_Homnayangi.Modules.Utils;
using Library.Models;
using Library.Models.Enum;
using Library.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.IngredientModule
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IPriceNoteRepository _priceNoteRepository;

        public IngredientService(IIngredientRepository ingredientRepository, IPriceNoteRepository priceNoteRepository)
        {
            _ingredientRepository = ingredientRepository;
            _priceNoteRepository = priceNoteRepository;
        }

        public async Task<ICollection<IngredientResponse>> GetAll()
        {
            var ingredients = await _ingredientRepository.GetIngredientsBy(includeProperties: "Type,Unit");

            return ingredients.Select(ToResponse).ToList();
        }

        public async Task<ICollection<IngredientResponse>> GetIngredientsBy(Expression<Func<Ingredient, bool>> filter = null,
            Func<IQueryable<Ingredient>, ICollection<Ingredient>> options = null,
            string includeProperties = null)
        {
            var ingredients = await _ingredientRepository.GetIngredientsBy(filter, options, includeProperties);
            return ingredients.Select(ToResponse).ToList();
        }
        public async Task AddNewIngredient(IngredientRequest newIngredient)
        {
            newIngredient.IngredientId = Guid.NewGuid();
            await _ingredientRepository.AddAsync(ToModel(newIngredient));
        }

        public IngredientResponse GetIngredientByID(Guid? ingredientID)
        {
            try
            {
                var ingredient = _ingredientRepository.GetFirstOrDefaultAsync(x => x.IngredientId == ingredientID.Value && x.Status.Value,
                    includeProperties: "Type,Unit").Result;
                return ToResponse(ingredient);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetIngredientByID: " + ex.Message);
                throw;
            }
        }

        public async Task<ICollection<IngredientResponse>> GetAllIngredients() // status = 1
        {
            try
            {
                var ingredients = await _ingredientRepository.GetIngredientsBy(x => x.Status.Value, includeProperties: "Type,Unit");
                return ingredients.Select(ToResponse).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetAllIngredients: " + ex.Message);
                throw;
            }
        }

        public async Task<PagedResponse<PagedList<IngredientResponse>>> GetAllIngredientsWithPagination(PagedRequest request)
        {
            var pageSize = request.PageSize;
            var pageNumber = request.PageNumber;
            var sort = request.sort;
            var sortDesc = request.sortDesc;
            try
            {
                var ingredients = await _ingredientRepository.GetIngredientsBy(i => i.Status.Value, includeProperties: "Type,Unit");
                var result = ingredients.Select(i => new IngredientResponse()
                {
                    IngredientId = i.IngredientId,
                    UnitId = i.UnitId,
                    Name = i.Name,
                    Description = i.Description,
                    Quantity = i.Quantity,
                    UnitName = i.Unit.Name,
                    Picture = i.Picture,
                    CreatedDate = i.CreatedDate,
                    UpdatedDate = i.UpdatedDate,
                    ListImage = i.ListImage != null ? StringUtils.ExtractContents(i.ListImage) : null,
                    Status = i.Status,
                    Price = i.Price,
                    Kcal = i.Kcal,
                    TypeId = i.TypeId,
                    TypeName = i.Type.Name,
                    TypeDescription = i.Type.Description
                }).OrderByDescending(i => i.CreatedDate).ToList();

                switch (sort)
                {
                    case (int)Sort.BlogsSortBy.CREATEDDATE:
                        ingredients = sortDesc ?
                            ingredients.OrderByDescending(i => i.CreatedDate).ToList() :
                            ingredients.OrderBy(i => i.CreatedDate).ToList();
                        break;
                    default:
                        ingredients = sortDesc ?
                            ingredients.OrderByDescending(r => r.Price).ToList() :
                            ingredients.OrderBy(r => r.Price).ToList();
                        break;
                }

                var response = PagedList<IngredientResponse>.ToPagedList(source: result, pageNumber: pageNumber, pageSize: pageSize);
                return response.ToPagedResponse();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetAllIngredients: " + ex.Message);
                throw;
            }
        }

        public async Task<ICollection<IngredientResponse>> GetIngredientsByTypeId(Guid typeId)
        {
            try
            {
                var ingredients = await _ingredientRepository.GetIngredientsBy(x => x.Status.Value && x.TypeId == typeId, includeProperties: "Type,Unit");
                return ingredients.Select(ToResponse).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetIngredientsByTypeId: " + ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteIngredient(Guid id)
        {
            bool isDeleted = false;
            try
            {
                Ingredient igDelete = _ingredientRepository.GetFirstOrDefaultAsync(x => x.IngredientId == id && x.Status.Value).Result;
                if (igDelete != null)
                {
                    igDelete.Status = false;
                    await _ingredientRepository.UpdateAsync(igDelete);
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
                Ingredient current = await _ingredientRepository.GetFirstOrDefaultAsync(x => newIg.IngredientId == x.IngredientId, includeProperties: "Unit");
                if (current != null)
                {
                    current.Name = newIg.Name;
                    current.Description = newIg.Description;
                    current.Quantity = newIg.Quantity;
                    current.UnitId = newIg.UnitId;
                    current.Picture = newIg.Picture;
                    current.ListImage = StringUtils.CompressContents(newIg.ListImage);
                    current.Kcal = newIg.Kcal;
                    current.UpdatedDate = DateTime.Now;
                    current.Status = newIg.Status;
                    current.TypeId = newIg.TypeId;
                    current.ListImagePosition = newIg.ListImagePosition;
                    
                    // update new price note for ingredient
                    if (current.Price != newIg.Price)
                    {
                        // Disable old price
                        var currentPriceNote = await _priceNoteRepository.GetFirstOrDefaultAsync(p =>
                                                                                p.IngredientId == newIg.IngredientId
                                                                                && p.Status.Value);
                        if (currentPriceNote != null)
                        {
                            currentPriceNote.Status = false;
                            await _priceNoteRepository.UpdateAsync(currentPriceNote);
                        }

                        // Add new price note
                        PriceNote priceNote = new PriceNote()
                        {
                            PriceNoteId = Guid.NewGuid(),
                            IngredientId = newIg.IngredientId,
                            Price = newIg.Price,
                            DateApply = DateTime.Now,
                            Status = true
                        };
                        await _priceNoteRepository.AddAsync(priceNote);
                    }
                    current.Price = newIg.Price;
                    await _ingredientRepository.UpdateAsync(current);
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
                await _ingredientRepository.AddAsync(ToModel(newIg));
                ingredientId = newIg.IngredientId;

                // Create price note record
                PriceNote priceNote = new PriceNote()
                {
                    PriceNoteId = Guid.NewGuid(),
                    IngredientId = ingredientId,
                    Price = newIg.Price,
                    DateApply = DateTime.Now,
                    Status = true
                };
                await _priceNoteRepository.AddAsync(priceNote);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CreateIngredient: " + ex.Message);
                throw;
            }
            return ingredientId;
        }
        public async Task<ICollection<SearchIngredientsResponse>> GetIngredientByName(String name)
        {

            var Ingredients = await _ingredientRepository.GetIngredientsBy(x => x.Status == true, includeProperties: "Unit");

            return Ingredients.Where(x => ConvertToUnSign(x.Name).Contains(name, StringComparison.CurrentCultureIgnoreCase) || x.Name.Contains(name, StringComparison.CurrentCultureIgnoreCase)).ToList().Select(ToSearchResponse).ToList();
        }

        public SearchIngredientsResponse ToSearchResponse(Ingredient ingredient)
        {
            if (ingredient != null)
            {
                return new SearchIngredientsResponse()
                {
                    IngredientId = ingredient.IngredientId,
                    Name = ingredient.Name,
                    UnitName = ingredient.Unit.Name,
                    Price = ingredient.Price.Value,
                    Kcal = ingredient.Kcal.Value
                };
            }
            else
            {
                return null;
            }
        }
        private string ConvertToUnSign(string input)
        {
            input = input.Trim();
            for (int i = 0x20; i < 0x30; i++)
            {
                input = input.Replace(((char)i).ToString(), " ");
            }
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            return str2;
        }

        public IngredientResponse ToResponse(Ingredient ingredient)
        {
            if (ingredient != null)
            {

                return new IngredientResponse()
                {
                    IngredientId = ingredient.IngredientId,
                    UnitId = ingredient.UnitId,
                    Name = ingredient.Name,
                    Description = ingredient.Description,
                    Quantity = ingredient.Quantity,
                    UnitName = ingredient.Unit?.Name,
                    Picture = ingredient.Picture,
                    ListImage = ingredient.ListImage != null ? StringUtils.ExtractContents(ingredient.ListImage) : null,
                    CreatedDate = ingredient.CreatedDate,
                    UpdatedDate = ingredient.UpdatedDate,
                    Status = ingredient.Status,
                    Price = ingredient.Price,
                    Kcal = ingredient.Kcal,
                    TypeId = ingredient.Type?.TypeId,
                    TypeName = ingredient.Type?.Name,
                    TypeDescription = ingredient.Type?.Description,
                    ListImagePosition = ingredient.ListImagePosition
                };
            }
            else
            {
                return null;
            }
        }

        public Ingredient ToModel(IngredientRequest ingredientRequest)
        {
            if (ingredientRequest != null)
            {

                return new Ingredient()
                {
                    IngredientId = ingredientRequest.IngredientId,
                    Name = ingredientRequest.Name,
                    Description = ingredientRequest.Description,
                    Quantity = ingredientRequest.Quantity,
                    UnitId = ingredientRequest.UnitId,
                    Picture = ingredientRequest.Picture,
                    ListImage = StringUtils.CompressContents(ingredientRequest.ListImage),
                    CreatedDate = ingredientRequest.CreatedDate,
                    UpdatedDate = ingredientRequest.UpdatedDate,
                    Status = ingredientRequest.Status,
                    Price = ingredientRequest.Price,
                    Kcal = ingredientRequest.Kcal,
                    TypeId = ingredientRequest.TypeId,
                    ListImagePosition = ingredientRequest.ListImagePosition
                };
            }
            else
            {
                return null;
            }
        }

        public async Task<ICollection<IngredientResponse>> GetIngredientsByTypeId(Guid typeId, Guid currentIngredientId)
        {
            try
            {
                var ingredients = await _ingredientRepository.GetNItemRandom(i => i.Status.Value && i.TypeId == typeId && i.IngredientId != currentIngredientId,
                                                                            includeProperties: "Type,Unit", numberItem: 10);

                return ingredients.Select(i => new IngredientResponse()
                {
                    IngredientId = i.IngredientId,
                    UnitId = i.UnitId,
                    Name = i.Name,
                    Description = i.Description,
                    Quantity = i.Quantity,
                    UnitName = i.Unit.Name,
                    Picture = i.Picture,
                    CreatedDate = i.CreatedDate,
                    UpdatedDate = i.UpdatedDate,
                    ListImage = i.ListImage != null ? StringUtils.ExtractContents(i.ListImage) : null,
                    Status = i.Status,
                    Price = i.Price,
                    Kcal = i.Kcal,
                    TypeId = i.TypeId,
                    TypeName = i.Type.Name,
                    TypeDescription = i.Type.Description
                }).OrderByDescending(i => i.CreatedDate).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetIngredientsByTypeId: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}

