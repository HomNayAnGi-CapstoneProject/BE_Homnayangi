using BE_Homnayangi.Modules.CategoryModule.Request;
using BE_Homnayangi.Modules.CategoryModule.Response;
using BE_Homnayangi.Modules.CookingMethodModule.Request;
using BE_Homnayangi.Modules.CookingMethodModule.Response;
using BE_Homnayangi.Modules.RegionModule.Response;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CookingMethodModule.Interface
{
    public interface ICookingMethodService
    {
        public Task<Guid> AddNewCookingMethod(CreateCookingMethodRequest newCookingMethod);
        public Task<bool> UpdateCookingMethod(UpdateCookingMethodRequest cookingMethodUpdate);
        public Task DeleteCookingMethod(Guid? ID);
        public Task<ICollection<CookingMethod>> GetAll();
        public Task<ICollection<DropdownCookingMethod>> GetDropdownCookingMethod();
        public Task<ICollection<CookingMethod>> GetCookingMethodsBy(Expression<Func<CookingMethod, bool>> filter = null,
            Func<IQueryable<CookingMethod>, ICollection<CookingMethod>> options = null,
            string includeProperties = null);
        public CookingMethod GetCookingMethodByID(Guid? cookingMethodID);
    }
}
