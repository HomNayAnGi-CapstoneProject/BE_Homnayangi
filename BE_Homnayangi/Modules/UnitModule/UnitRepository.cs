using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using Library.Models;
using BE_Homnayangi.Modules.UnitModule.Interface;
using Library.DataAccess;

namespace BE_Homnayangi.Modules.UnitModule
{
    public class UnitRepository : Repository<Unit>, IUnitRepository
    {
        private readonly HomnayangiContext _db;

        public UnitRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<Unit>> GetUnitsBy(
            Expression<Func<Unit, bool>> filter = null,
            Func<IQueryable<Unit>, ICollection<Unit>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<Unit> query = DbSet;

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
