using BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Request;
using BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Request;
using BE_Homnayangi.Modules.AdminModules.CronJobTimeConfigModule.Interface;
using BE_Homnayangi.Modules.AdminModules.CronJobTimeConfigModule.Request;
using Library.Models;
using Library.Models.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AdminModules.CronJobTimeConfigModule
{
    public class CronJobTimeConfigService : ICronJobTimeConfigService
    {
        #region Define repository + Constructor
        private readonly ICronJobTimeConfigRepository _cronJobTimeConfigRepository;
        public CronJobTimeConfigService(ICronJobTimeConfigRepository cronJobTimeConfigRepository)
        {
            _cronJobTimeConfigRepository = cronJobTimeConfigRepository;
        }
        #endregion

        #region Get

        public async Task<ICollection<CronJobTimeConfig>> GetAllCronJobTimeConfigs()
        {
            try
            {
                var badgeConditions = await _cronJobTimeConfigRepository.GetAll();
                return badgeConditions.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CronJobTimeConfig> GetCronJobTimeConfig(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                var cronJobTimeConfig = await _cronJobTimeConfigRepository.GetByIdAsync((Guid)id);

                if (cronJobTimeConfig == null)
                {
                    throw new Exception(ErrorMessage.CronJobTimeConfigError.CRON_JOB_TIME_CONFIG_NOT_FOUND);
                }

                return cronJobTimeConfig;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region CUD
        public async Task<Guid> CreateNewCronJobTimeConfig(CreateNewCronJobTimeConfig newCronJobTimeConfig)
        {
            try
            {

                var newCronJobTime = new CronJobTimeConfig();
                newCronJobTime.CronJobTimeConfigId = Guid.NewGuid();
                newCronJobTime.Minute = newCronJobTimeConfig.Minute;
                newCronJobTime.Hour = newCronJobTimeConfig.Hour;
                newCronJobTime.CreatedDate = DateTime.Now;
                newCronJobTime.Day = newCronJobTimeConfig.Day;
                newCronJobTime.Month = newCronJobTimeConfig.Month;
                newCronJobTime.UpdatedDate = DateTime.Now;

                await _cronJobTimeConfigRepository.AddAsync(newCronJobTime);
                return newCronJobTime.CronJobTimeConfigId;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CreateNewBadgeCondition: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return new Guid(); // dummy code, sure it never come here
        }

        public async Task UpdateCronJobTimeConfig(UpdateCronJobTimeConfig updateCronJobTimeConfigRequest)
        {
            try
            {



                var cronJobTimeConfigUpdate = _cronJobTimeConfigRepository.GetFirstOrDefaultAsync(x => x.CronJobTimeConfigId == updateCronJobTimeConfigRequest.CronJobTimeConfigId).Result;
                var listCronJobTimeConfigUpdate = new List<CronJobTimeConfig>();

                if (cronJobTimeConfigUpdate == null)
                {
                    throw new Exception(ErrorMessage.CronJobTimeConfigError.CRON_JOB_TIME_CONFIG_NOT_FOUND);
                }

                cronJobTimeConfigUpdate.Minute = updateCronJobTimeConfigRequest.Minute;
                cronJobTimeConfigUpdate.Hour = updateCronJobTimeConfigRequest.Hour;
                cronJobTimeConfigUpdate.Month = updateCronJobTimeConfigRequest.Month;
                cronJobTimeConfigUpdate.Day = updateCronJobTimeConfigRequest.Day;
                cronJobTimeConfigUpdate.TargetObject = updateCronJobTimeConfigRequest.TargetObject;
                cronJobTimeConfigUpdate.UpdatedDate = DateTime.Now;
                listCronJobTimeConfigUpdate.Add(cronJobTimeConfigUpdate);
                await _cronJobTimeConfigRepository.UpdateRangeAsync(listCronJobTimeConfigUpdate);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateBadgeCondition: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteCronJobTimeConfig(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                var badgeConditionDelete = await _cronJobTimeConfigRepository.GetFirstOrDefaultAsync(x => x.CronJobTimeConfigId.Equals(id));


                if (badgeConditionDelete == null)
                {
                    throw new Exception(ErrorMessage.BadgeConditionError.BADGE_CONDITION_NOT_FOUND);
                }

                await _cronJobTimeConfigRepository.RemoveAsync(badgeConditionDelete);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        #endregion
    }
}
