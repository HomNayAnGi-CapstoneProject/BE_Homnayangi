using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Interface;

namespace BE_Homnayangi.Modules.AdminModules.BadgeConditionModule
{
    public class BadgeConditionRepository : Repository<BadgeCondition>, IBadgeConditionRepository
    {
        private readonly HomnayangiContext _db;

        public BadgeConditionRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<BadgeCondition>> GetBadgeConditionsBy(
            Expression<Func<BadgeCondition, bool>> filter = null,
            Func<IQueryable<BadgeCondition>, ICollection<BadgeCondition>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<BadgeCondition> query = DbSet;

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
