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

namespace BE_Homnayangi.Modules.CategoryModule
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository _regionRepository;

        public RegionService(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }
        public async Task<ICollection<Region>> GetAll()
        {
            return await _regionRepository.GetAll();
        }
        public Task<ICollection<Region>> GetRegionsBy(Expression<Func<Region, bool>> filter = null,
            Func<IQueryable<Region>, ICollection<Region>> options = null,
            string includeProperties = null)
        {
            return _regionRepository.GetRegionsBy(filter);
        }
        public async Task<Guid> AddNewRegion(CreateRegionRequest regionRequest)
        {
            var newRegion = new Region();
            newRegion.RegionId = Guid.NewGuid();
            newRegion.RegionName = regionRequest.RegionName;
            newRegion.Status = true;
            newRegion.CreatedDate = DateTime.Now;
            await _regionRepository.AddAsync(newRegion);

            return newRegion.RegionId;
        }
        public async Task<Boolean> UpdateRegion(UpdateRegionRequest regionRequest)
        {
            var regionUpdate = _regionRepository.GetFirstOrDefaultAsync(x => x.RegionId == regionRequest.RegionId).Result;
            if (regionUpdate == null) return false;

            regionUpdate.RegionName = regionRequest.RegionName ?? regionUpdate.RegionName;
            regionUpdate.Status = regionRequest.Status ?? regionUpdate.Status;

            await _regionRepository.UpdateAsync(regionUpdate);
            return true;
        }
        public async Task DeleteRegion(Guid? id)
        {
            Region regionDelete = _regionRepository.GetFirstOrDefaultAsync(x => x.RegionId.Equals(id) && x.Status == true).Result;
            if (regionDelete == null) return;
            regionDelete.Status = false;
            await _regionRepository.UpdateAsync(regionDelete);
        }
        public Region GetRegionByID(Guid? regionID)
        {
            return _regionRepository.GetFirstOrDefaultAsync(x => x.RegionId.Equals(regionID)).Result;
        }

        public async Task<ICollection<DropdownRegion>> GetDropdownRegion()
        {
            List<DropdownRegion> result = new List<DropdownRegion>();
            try
            {
                var regions = await _regionRepository.GetRegionsBy(c => c.Status == true);
                if (regions.Count > 0)
                {
                    result = regions.Select(x => new DropdownRegion()
                    {
                        RegionId = x.RegionId,
                        RegionName = x.RegionName
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
