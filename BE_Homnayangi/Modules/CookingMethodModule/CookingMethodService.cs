using BE_Homnayangi.Modules.CategoryModule.Interface;
using BE_Homnayangi.Modules.CategoryModule.Request;
using BE_Homnayangi.Modules.CategoryModule.Response;
using BE_Homnayangi.Modules.CookingMethodModule.Interface;
using BE_Homnayangi.Modules.CookingMethodModule.Request;
using BE_Homnayangi.Modules.CookingMethodModule.Response;
using BE_Homnayangi.Modules.RegionModule.Interface;
using BE_Homnayangi.Modules.RegionModule.Response;
using BE_Homnayangi.Modules.SubCateModule.Interface;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CookingMethodModule
{
    public class CookingMethodService : ICookingMethodService
    {
        private readonly ICookingMethodRepository _cookingMethodRepository;

        public CookingMethodService(ICookingMethodRepository cookingMethodRepository)
        {
            _cookingMethodRepository = cookingMethodRepository;
        }
        public async Task<ICollection<CookingMethod>> GetAll()
        {
            return await _cookingMethodRepository.GetAll();
        }
        public Task<ICollection<CookingMethod>> GetCookingMethodsBy(Expression<Func<CookingMethod, bool>> filter = null,
            Func<IQueryable<CookingMethod>, ICollection<CookingMethod>> options = null,
            string includeProperties = null)
        {
            return _cookingMethodRepository.GetCookingMethodsBy(filter);
        }
        public async Task<Guid> AddNewCookingMethod(CreateCookingMethodRequest cookingMethodRequest)
        {
            var newCookingMethod = new CookingMethod();
            newCookingMethod.CookingMethodId = Guid.NewGuid();
            newCookingMethod.Name = cookingMethodRequest.CookingMethodName;
            newCookingMethod.Status = true;
            newCookingMethod.CreatedDate = DateTime.Now;
            await _cookingMethodRepository.AddAsync(newCookingMethod);

            return newCookingMethod.CookingMethodId;
        }
        public async Task<bool> UpdateCookingMethod(UpdateCookingMethodRequest cookingMethodRequest)
        {
            var cookingMethodUpdate = _cookingMethodRepository.GetFirstOrDefaultAsync(x => x.CookingMethodId == cookingMethodRequest.CookingMethodId).Result;
            if (cookingMethodUpdate == null) return false;

            cookingMethodUpdate.Name = cookingMethodRequest.CookingMethodName ?? cookingMethodUpdate.Name;
            cookingMethodUpdate.Status = cookingMethodRequest.Status ?? cookingMethodUpdate.Status;

            await _cookingMethodRepository.UpdateAsync(cookingMethodUpdate);
            return true;
        }
        public async Task DeleteCookingMethod(Guid? id)
        {
            CookingMethod cookingMethodDelete = _cookingMethodRepository.GetFirstOrDefaultAsync(x => x.CookingMethodId.Equals(id) && x.Status == true).Result;
            if (cookingMethodDelete == null) return;
            cookingMethodDelete.Status = false;
            await _cookingMethodRepository.UpdateAsync(cookingMethodDelete);
        }
        public CookingMethod GetCookingMethodByID(Guid? cookingMethodId)
        {
            return _cookingMethodRepository.GetFirstOrDefaultAsync(x => x.CookingMethodId.Equals(cookingMethodId)).Result;
        }

        public async Task<ICollection<DropdownCookingMethod>> GetDropdownCookingMethod()
        {
            List<DropdownCookingMethod> result = new List<DropdownCookingMethod>();
            try
            {
                var cookingMethods = await _cookingMethodRepository.GetCookingMethodsBy(c => c.Status == true);
                if (cookingMethods.Count > 0)
                {
                    result = cookingMethods.Select(x => new DropdownCookingMethod()
                    {
                        CookingMethodId = x.CookingMethodId,
                        CookingMethodName = x.Name
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }
    }
}
