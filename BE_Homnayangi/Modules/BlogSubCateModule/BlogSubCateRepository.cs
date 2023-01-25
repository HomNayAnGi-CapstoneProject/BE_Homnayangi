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
using BE_Homnayangi.Modules.BlogSubCateModule.Interface;

namespace BE_Homnayangi.Modules.BlogSubCateModule
{
    public class BlogSubCateRepository : Repository<BlogSubCate>, IBlogSubCateRepository
    {
        private readonly HomnayangiContext _db;

        public BlogSubCateRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<BlogSubCate>> GetBlogSubCatesBy(
            Expression<Func<BlogSubCate, bool>> filter = null,
            Func<IQueryable<BlogSubCate>, ICollection<BlogSubCate>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<BlogSubCate> query = DbSet;

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
