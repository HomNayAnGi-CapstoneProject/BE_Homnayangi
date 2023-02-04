using BE_Homnayangi.Modules.TypeModule.DTO;
using BE_Homnayangi.Modules.TypeModule.Interface;
using BE_Homnayangi.Modules.UnitModule.Interface;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using Library.Models;
using BE_Homnayangi.Modules.UnitModule.Request;
using BE_Homnayangi.Modules.UnitModule.Response;

namespace BE_Homnayangi.Modules.UnitModule
{
    public class UnitService : IUnitService
    {
        private readonly IUnitRepository _unitRepository;

        public UnitService(IUnitRepository unitRepository)
        {
            _unitRepository = unitRepository;
        }

        public async Task<ICollection<Unit>> GetAll()
        {
            return await _unitRepository.GetAll();
        }

        public async Task<ICollection<UnitDropdownResponse>> GetUnitDropdowns()
        {
            return _unitRepository.GetAll().Result.Select(x => new UnitDropdownResponse
            {
                UnitId = x.UnitId,
                UnitName = x.Name
            }).ToList();
        }

        public Task<ICollection<Unit>> GetUnitsBy(
                Expression<Func<Unit,
                bool>> filter = null,
                Func<IQueryable<Unit>,
                ICollection<Unit>> options = null,
                string includeProperties = null)
        {
            return _unitRepository.GetUnitsBy(filter);
        }


        public async Task AddNewUnit(CreateUnitRequest unitRequest)
        {
            var newUnit = new Unit();

            newUnit.UnitId = Guid.NewGuid();
            newUnit.Name = unitRequest.Name;
            newUnit.Description = unitRequest.Description;
            newUnit.Status = true;

            await _unitRepository.AddAsync(newUnit);
        }

        public async Task UpdateUnit(UpdateUnitRequest unitRequest)
        {
            var unitUpdate = GetUnitByID(unitRequest.UnitId).Result;

            unitUpdate.Name = unitRequest.Name == null ? unitUpdate.Name : unitRequest.Name;
            unitUpdate.Description = unitRequest.Description == null ? unitUpdate.Description : unitRequest.Description;
            unitUpdate.Status = unitRequest.Status == null ? unitUpdate.Status : unitRequest.Status;

            await _unitRepository.UpdateAsync(unitUpdate);
        }

        public async Task DeleteUnit(Unit unitDelete)
        {
            unitDelete.Status = true;
            await _unitRepository.UpdateAsync(unitDelete);
        }

        public async Task<Unit> GetUnitByID(Guid? id)
        {
            return await _unitRepository.GetFirstOrDefaultAsync(x => x.UnitId == id);
        }
    }
}
