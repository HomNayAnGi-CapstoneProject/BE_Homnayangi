using BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Interface;
using BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Request;
using Library.Models.Constant;
using Library.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Interface;
using System.Linq;
using BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Request;
using FluentValidation.Results;

namespace BE_Homnayangi.Modules.AdminModules.BadgeConditionModule
{
    public class BadgeConditionService : IBadgeConditionService
    {
        #region Define repository + Constructor
        private readonly IBadgeConditionRepository _badgeConditionRepository;
        public BadgeConditionService(IBadgeConditionRepository badgeConditionRepository)
        {
            _badgeConditionRepository = badgeConditionRepository;
        }
        #endregion

        #region Get

        public async Task<ICollection<BadgeCondition>> GetBadgeConditions()
        {
            try
            {
                var badgeConditions = await _badgeConditionRepository.GetAll(includeProperties: "Badge");

                if (badgeConditions.Count() == 0)
                {
                    throw new Exception(ErrorMessage.CommonError.LIST_IS_NULL);
                }

                return badgeConditions.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BadgeCondition> GetBadgeCondition(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                var badgeCondition = await _badgeConditionRepository.GetByIdAsync((Guid)id);

                if (badgeCondition == null)
                {
                    throw new Exception(ErrorMessage.BadgeConditionError.BADGE_CONDITION_NOT_FOUND);
                }

                return badgeCondition;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region CUD
        public async Task<Guid> CreateNewBadgeCondition(CreateNewBadgeConditionRequest newBadgeConditionRequest)
        {
            try
            {
                ValidationResult validationResult = new CreateNeBadgeConditionRequestValidator().Validate(newBadgeConditionRequest);
                if (!validationResult.IsValid)
                {
                    throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
                }

                var newBadgeCondition = new BadgeCondition();
                newBadgeCondition.BadgeConditionId = Guid.NewGuid();
                newBadgeCondition.Accomplishments = newBadgeConditionRequest.Accomplishments;
                newBadgeCondition.Orders = newBadgeConditionRequest.Orders;
                newBadgeCondition.CreatedDate = DateTime.Now;
                newBadgeCondition.Status = true;
                newBadgeCondition.BadgeId = newBadgeConditionRequest.BadgeId;

                await _badgeConditionRepository.AddAsync(newBadgeCondition);

                return newBadgeCondition.BadgeConditionId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateBadgeCondition(UpdateBadgeConditionRequest updateBadgeConditionRequest)
        {
            try
            {
                ValidationResult validationResult = new UpdateBadgeConditionRequestValidator().Validate(updateBadgeConditionRequest);
                if (!validationResult.IsValid)
                {
                    throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
                }

                var badgeConditionUpdate = _badgeConditionRepository.GetFirstOrDefaultAsync(x => x.BadgeConditionId == updateBadgeConditionRequest.BadgeConditionId).Result;
                var listBadgeConditionUpdate = new List<BadgeCondition>();

                if (badgeConditionUpdate == null)
                {
                    throw new Exception(ErrorMessage.CaloRefError.CALO_REF_NOT_FOUND);
                }

                badgeConditionUpdate.Accomplishments = updateBadgeConditionRequest.Accomplishments ?? badgeConditionUpdate.Accomplishments;
                badgeConditionUpdate.Orders = updateBadgeConditionRequest.Orders ?? badgeConditionUpdate.Orders;
                badgeConditionUpdate.Status = updateBadgeConditionRequest.Status ?? badgeConditionUpdate.Status;
                badgeConditionUpdate.BadgeId = updateBadgeConditionRequest.BadgeId;
                listBadgeConditionUpdate.Add(badgeConditionUpdate);

                if (badgeConditionUpdate.BadgeId != null)
                {
                    var badgeConditionCheck = await _badgeConditionRepository.GetFirstOrDefaultAsync(x => x.BadgeId == badgeConditionUpdate.BadgeId && badgeConditionUpdate.Status == true);
                    /*   if (badgeConditionCheck != null)
                       {
                           badgeConditionCheck.BadgeId = null;
                       }*/
                    listBadgeConditionUpdate.Add(badgeConditionCheck);
                }
                await _badgeConditionRepository.UpdateRangeAsync(listBadgeConditionUpdate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteBadgeCondition(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                var badgeConditionDelete = await _badgeConditionRepository.GetFirstOrDefaultAsync(x => x.BadgeConditionId.Equals(id) && x.Status == true);
                badgeConditionDelete.Status = false;

                if (badgeConditionDelete == null)
                {
                    throw new Exception(ErrorMessage.BadgeConditionError.BADGE_CONDITION_NOT_FOUND);
                }

                await _badgeConditionRepository.UpdateAsync(badgeConditionDelete);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
