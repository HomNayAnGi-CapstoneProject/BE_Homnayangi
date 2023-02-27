using BE_Homnayangi.Modules.AdminModules.SeasonReferenceModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AdminModules.SeasonReferenceModule
{
    public class SeasonReferenceRepository : Repository<SeasonReference>, ISeasonReferenceRepository
    {
        private readonly HomnayangiContext _db;

        public SeasonReferenceRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<SeasonReference>> GetSeasonReferencesBy(
            Expression<Func<SeasonReference, bool>> filter = null,
            Func<IQueryable<SeasonReference>, ICollection<SeasonReference>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<SeasonReference> query = DbSet;

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
