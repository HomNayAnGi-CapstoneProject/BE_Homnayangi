using System;
using BE_Homnayangi.Modules.BadgeModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.BadgeModule
{
    public class BadgeRepository : Repository<Badge>, IBadgeRepository
    {
        private readonly HomnayangiContext _db;

        public BadgeRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ICollection<Badge>> GetBadgesBy(Expression<Func<Badge, bool>> filter = null, Func<IQueryable<Badge>, ICollection<Badge>> options = null, string includeProperties = null)

        {
            IQueryable<Badge> query = DbSet;

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

