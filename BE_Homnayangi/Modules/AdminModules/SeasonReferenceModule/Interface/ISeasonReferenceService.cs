using BE_Homnayangi.Modules.AdminModules.SeasonReferenceModule.Request;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AdminModules.SeasonReferenceModule.Interface
{
    public interface ISeasonReferenceService
    {
        #region Get
        public Task<ICollection<SeasonReference>> GetSeasonReferences();
        public Task<SeasonReference> GetSeasonRef(Guid? id);
        #endregion

        #region CUD
        public Task UpdateSeasonReference(UpdateSeasonRefRequest request);
        public Task<Guid> CreateNewSeasonRef(CreateNewSeasonRefRequest newCaloRefRequest);
        public Task DeleteSeasonReference(Guid? Id);
        #endregion
    }
}
