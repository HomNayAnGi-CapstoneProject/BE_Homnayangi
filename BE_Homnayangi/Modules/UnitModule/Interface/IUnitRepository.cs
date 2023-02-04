using Repository.Repository.Interface;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using Library.Models;
using System;

namespace BE_Homnayangi.Modules.UnitModule.Interface
{
    public interface IUnitRepository : IRepository<Unit>
    {
        public Task<ICollection<Unit>> GetUnitsBy(
               Expression<Func<Unit, bool>> filter = null,
               Func<IQueryable<Unit>, ICollection<Unit>> options = null,
               string includeProperties = null
           );
    }
}
