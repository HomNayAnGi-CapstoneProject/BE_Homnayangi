using BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AdminModules.CaloReferenceModule
{
    public class CaloReferenceRepository : Repository<CaloReference>, ICaloReferenceRepository
    {
        private readonly HomnayangiContext _db;

        public CaloReferenceRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<CaloReference>> GetCaloReferencesBy(
            Expression<Func<CaloReference, bool>> filter = null,
            Func<IQueryable<CaloReference>, ICollection<CaloReference>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<CaloReference> query = DbSet;

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
