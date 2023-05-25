using BE_Homnayangi.Modules.CategoryModule.Interface;
using BE_Homnayangi.Modules.CookingMethodModule.Interface;
using BE_Homnayangi.Modules.RegionModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CategoryModule
{
    public class RegionRepository : Repository<Region>, IRegionRepository
    {
        private readonly HomnayangiContext _db;

        public RegionRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<Region>> GetRegionsBy(
            Expression<Func<Region, bool>> filter = null,
            Func<IQueryable<Region>, ICollection<Region>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<Region> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return options != null ? options(query).ToList() : await query.ToListAsync();
        }
    }
}
