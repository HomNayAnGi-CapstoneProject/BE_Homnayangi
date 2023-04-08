using Library.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AdminModules.CronJobTimeConfigModule.Interface
{
    public interface ICronJobTimeConfigRepository : IRepository<CronJobTimeConfig>
    {
        public Task<ICollection<CronJobTimeConfig>> GetCronJobTimeConfigsBy(
           Expression<Func<CronJobTimeConfig, bool>> filter = null,
           Func<IQueryable<CronJobTimeConfig>, ICollection<CronJobTimeConfig>> options = null,
           string includeProperties = null
       );
    }
}
