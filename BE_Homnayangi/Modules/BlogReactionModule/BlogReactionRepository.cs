using BE_Homnayangi.Modules.BlogReactionModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.BlogReactionModule
{
    public class BlogReactionRepository : Repository<BlogReaction>, IBlogReactionRepository
    {
        private readonly HomnayangiContext _db;

        public BlogReactionRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<BlogReaction>> GetBlogsBy(
            Expression<Func<BlogReaction, bool>> filter = null,
            Func<IQueryable<BlogReaction>, ICollection<BlogReaction>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<BlogReaction> query = DbSet;

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
