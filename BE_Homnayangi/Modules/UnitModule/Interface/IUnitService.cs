using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using Library.Models;
using BE_Homnayangi.Modules.UnitModule.Request;
using BE_Homnayangi.Modules.UnitModule.Response;

namespace BE_Homnayangi.Modules.UnitModule.Interface
{
    public interface IUnitService
    {
        public Task<ICollection<Unit>> GetUnitsBy(
           Expression<Func<Unit, bool>> filter = null,
           Func<IQueryable<Unit>, ICollection<Unit>> options = null,
           string includeProperties = null);

        public Task AddNewUnit(CreateUnitRequest unitRequest);

        public Task UpdateUnit(UpdateUnitRequest unitUpdate);

        public Task DeleteUnit(Unit unitDelete);

        public Task<ICollection<Unit>> GetAll();

        public Task<ICollection<UnitDropdownResponse>> GetUnitDropdowns();

        public Task<Unit> GetUnitByID(Guid? id);
    }
}
