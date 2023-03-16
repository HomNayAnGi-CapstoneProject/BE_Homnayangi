using Library.Models;
using Repository.Repository.Interface;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Interface
{
    public interface IBadgeConditionRepository : IRepository<BadgeCondition>
    {
        public Task<ICollection<BadgeCondition>> GetBadgeConditionsBy(
            Expression<Func<BadgeCondition, bool>> filter = null,
            Func<IQueryable<BadgeCondition>, ICollection<BadgeCondition>> options = null,
            string includeProperties = null
        );
    }
}
