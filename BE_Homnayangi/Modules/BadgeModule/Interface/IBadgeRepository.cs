using System;
using Library.Models;
using Repository.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.BadgeModule.Interface
{
    public interface IBadgeRepository : IRepository<Badge>
    {
        public Task<ICollection<Badge>> GetBadgesBy(
               Expression<Func<Badge, bool>> filter = null,
               Func<IQueryable<Badge>, ICollection<Badge>> options = null,
               string includeProperties = null
           );
    }
}

