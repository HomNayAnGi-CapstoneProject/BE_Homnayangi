using BE_Homnayangi.Modules.AdminModules.CronJobTimeConfigModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AdminModules.CronJobTimeConfigModule
{
    public class CronJobTimeConfigRepository : Repository<CronJobTimeConfig>, ICronJobTimeConfigRepository
    {
        private readonly HomnayangiContext _db;

        public CronJobTimeConfigRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<CronJobTimeConfig>> GetCronJobTimeConfigsBy(
           Expression<Func<CronJobTimeConfig, bool>> filter = null,
           Func<IQueryable<CronJobTimeConfig>, ICollection<CronJobTimeConfig>> options = null,
           string includeProperties = null
       )
        {
            IQueryable<CronJobTimeConfig> query = DbSet;

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
