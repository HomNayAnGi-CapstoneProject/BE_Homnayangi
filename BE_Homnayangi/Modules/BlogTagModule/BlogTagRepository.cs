using BE_Homnayangi.Modules.CategoryModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using BE_Homnayangi.Modules.BlogTagModule.Interface;

namespace BE_Homnayangi.Modules.BlogTagModule
{
    public class BlogTagRepository : Repository<BlogTag>, IBlogTagRepository
    {
        private readonly HomnayangiContext _db;

        public BlogTagRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<BlogTag>> GetBlogTagsBy(
            Expression<Func<BlogTag, bool>> filter = null,
            Func<IQueryable<BlogTag>, ICollection<BlogTag>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<BlogTag> query = DbSet;

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
