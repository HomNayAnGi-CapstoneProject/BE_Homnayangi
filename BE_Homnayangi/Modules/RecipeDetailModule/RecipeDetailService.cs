using System;
using BE_Homnayangi.Modules.RecipeModule.Interface;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.RecipeDetailModule.Interface;

namespace BE_Homnayangi.Modules.RecipeDetailModule
{
    public class RecipeDetailService : IRecipeDetailService
    {
        private readonly IRecipeDetailRepository _recipeDetailRepository;

        public RecipeDetailService(IRecipeDetailRepository recipeDetailRepository)
        {
            _recipeDetailRepository = recipeDetailRepository;
        }

        public async Task<ICollection<RecipeDetail>> GetAll()
        {
            return await _recipeDetailRepository.GetAll();
        }
        public Task<ICollection<RecipeDetail>> GetRecipeDetailsBy(Expression<Func<RecipeDetail, bool>> filter = null,
            Func<IQueryable<RecipeDetail>, ICollection<RecipeDetail>> options = null,
            string includeProperties = null)
        {
            return _recipeDetailRepository.GetRecipeDetailsBy(filter, options, includeProperties);
        }
        public async Task AddNewRecipeDetail(RecipeDetail newRecipeDetail)
        {
            newRecipeDetail.RecipeId = Guid.NewGuid();
            await _recipeDetailRepository.AddAsync(newRecipeDetail);
        }
        public async Task UpdateRecipeDetail(RecipeDetail recipeUpdateDetail)
        {
            await _recipeDetailRepository.UpdateAsync(recipeUpdateDetail);
        }
        public async Task DeleteRecipeDetail(Guid? id)
        {
            //Recipe recipeDelete = _recipeDetailRepository.GetFirstOrDefaultAsync(x => x.RecipeId.Equals(id) && x.Status == true).Result;
            //if (recipeDelete == null) return;
            //recipeDelete.Status = false;
            //await _recipeDetailRepository.UpdateAsync(recipeDelete);
            return;
        }
        public RecipeDetail GetRecipeDetailByID(Guid? recipeDetailID)
        {
            return _recipeDetailRepository.GetFirstOrDefaultAsync(x => x.RecipeId.Equals(recipeDetailID)).Result;
        }

        public async Task<ICollection<RecipeDetail>> GetRecipeDetailsByID(Guid recipeDetailID)
        {
            ICollection<RecipeDetail> list = null;
            try
            {
                list = await _recipeDetailRepository.GetRecipeDetailsBy(x => x.RecipeId.Equals(recipeDetailID), includeProperties: "Ingredient");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetRecipeDetailsByID: " + ex.Message);
                throw;
            }
            return list;
        }
    }
}

