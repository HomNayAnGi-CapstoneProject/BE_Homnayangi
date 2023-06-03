
using System;
using System.Collections.Generic;
using System.Linq;
using Library.Models;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Repository.Repository.Interface;

namespace BE_Homnayangi.Modules.RegionModule.Interface
{
    public interface IRegionRepository : IRepository<Region>
    {
        public Task<ICollection<Region>> GetRegionsBy(
            Expression<Func<Region, bool>> filter = null,
            Func<IQueryable<Region>, ICollection<Region>> options = null,
            string includeProperties = null
        );
    }
}
