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

namespace BE_Homnayangi.Modules.RegionModule.Interface
{
    public interface IRegionService
    {
        public Task<Guid> AddNewRegion(CreateRegionRequest newRegion);
        public Task<bool> UpdateRegion(UpdateRegionRequest regionUpdate);
        public Task DeleteRegion(Guid? ID);
        public Task<ICollection<Region>> GetAll();
        public Task<ICollection<DropdownRegion>> GetDropdownRegion();
        public Task<ICollection<Region>> GetRegionsBy(Expression<Func<Region, bool>> filter = null,
            Func<IQueryable<Region>, ICollection<Region>> options = null,
            string includeProperties = null);
        public Region GetRegionByID(Guid? regionID);
    }
}
