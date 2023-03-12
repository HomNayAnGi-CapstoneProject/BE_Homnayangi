using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using BE_Homnayangi.Modules.BlogReferenceModule.Interface;

namespace BE_Homnayangi.Modules.BlogReferenceModule
{
    public class BlogReferenceRepository : Repository<BlogReference>, IBlogReferenceRepository
    {
        private readonly HomnayangiContext _db;

        public BlogReferenceRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<BlogReference>> GetBlogReferencesBy(
            Expression<Func<BlogReference, bool>> filter = null,
            Func<IQueryable<BlogReference>, ICollection<BlogReference>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<BlogReference> query = DbSet;

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
