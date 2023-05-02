using BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Request;
using Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Request;

namespace BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Interface
{
    public interface IBadgeConditionService
    {
        #region Get
        public Task<ICollection<BadgeCondition>> GetBadgeConditions();
        public Task<ICollection<BadgeCondition>> GetBadgeConditionsByCustomer();
        public Task<BadgeCondition> GetBadgeCondition(Guid? id);
        #endregion

        #region CUD
        public Task UpdateBadgeCondition(UpdateBadgeConditionRequest request);
        public Task<Guid> CreateNewBadgeCondition(CreateNewBadgeConditionRequest newBadgeConditionRequest);
        public Task DeleteBadgeCondition(Guid? Id);
        #endregion
    }
}
