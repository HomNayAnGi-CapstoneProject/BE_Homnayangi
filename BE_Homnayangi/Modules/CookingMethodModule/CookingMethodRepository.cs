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

namespace BE_Homnayangi.Modules.CookingMethodModule
{
    public class CookingMethodRepository : Repository<CookingMethod>, ICookingMethodRepository
    {
        private readonly HomnayangiContext _db;

        public CookingMethodRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<CookingMethod>> GetCookingMethodsBy(
            Expression<Func<CookingMethod, bool>> filter = null,
            Func<IQueryable<CookingMethod>, ICollection<CookingMethod>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<CookingMethod> query = DbSet;

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
