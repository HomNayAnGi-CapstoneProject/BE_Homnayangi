using BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Request;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Interface
{
    public interface ICaloReferenceService
    {
        #region Get
        public Task<ICollection<CaloReference>> GetCaloReferences();
        public Task<CaloReference> GetCaloRef(Guid? id);
        #endregion

        #region CUD
        public Task UpdateCaloReference(UpdateCaloRefRequest request);
        public Task<Guid> CreateNewCaloRef(CreateNewCaloRefRequest newCaloRefRequest);
        public Task DeleteCaloReference(Guid? Id);
        #endregion
    }
}
