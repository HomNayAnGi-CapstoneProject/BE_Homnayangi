using System;
using Library.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.Models;
using Repository.Repository;
using BE_Homnayangi.Modules.TagModule.Interface;
using Microsoft.EntityFrameworkCore;

namespace BE_Homnayangi.Modules.TagModule
{
    public class TagRepository : Repository<Tag>, ITagRepository
	{
        private readonly HomnayangiContext _db;

        public TagRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<Tag>> GetTagsBy(
            Expression<Func<Tag, bool>> filter = null,
            Func<IQueryable<Tag>, ICollection<Tag>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<Tag> query = DbSet;

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

