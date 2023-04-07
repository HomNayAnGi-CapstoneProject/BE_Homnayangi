using BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Request;
using BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Request;
using BE_Homnayangi.Modules.AdminModules.CronJobTimeConfigModule.Request;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AdminModules.CronJobTimeConfigModule.Interface
{
    public interface ICronJobTimeConfigService
    {
        public Task<ICollection<CronJobTimeConfig>> GetAllCronJobTimeConfigs();
        public Task<CronJobTimeConfig> GetCronJobTimeConfig(Guid? id);
        public Task UpdateCronJobTimeConfig(UpdateCronJobTimeConfig updateCronJobTimeConfigRequest);
        public Task<Guid> CreateNewCronJobTimeConfig(CreateNewCronJobTimeConfig newCronJobTimeConfig);
        public Task DeleteCronJobTimeConfig(Guid? Id);
    }
}
