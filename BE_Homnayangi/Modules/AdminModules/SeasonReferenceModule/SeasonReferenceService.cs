using BE_Homnayangi.Modules.AdminModules.SeasonReferenceModule.Interface;
using BE_Homnayangi.Modules.AdminModules.SeasonReferenceModule.Request;
using FluentValidation.Results;
using Library.Models;
using Library.Models.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AdminModules.SeasonReferenceModule
{
    public class SeasonReferenceService : ISeasonReferenceService
    {
        #region Define repository + Constructor
        private readonly ISeasonReferenceRepository _seasonReferenceRepository;
        public SeasonReferenceService(ISeasonReferenceRepository seasonReferenceRepository)
        {
            _seasonReferenceRepository = seasonReferenceRepository;
        }
        #endregion

        #region Get

        public async Task<ICollection<SeasonReference>> GetSeasonReferences()
        {
            try
            {
                //get list of season ref
                var seasonReferences = await _seasonReferenceRepository.GetAll();

                if (seasonReferences.Count() == 0)
                {
                    throw new Exception(ErrorMessage.CommonError.LIST_IS_NULL);
                }

                return seasonReferences.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<SeasonReference> GetSeasonRef(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                var seasonReference = await _seasonReferenceRepository.GetByIdAsync((Guid)id);

                if (seasonReference == null)
                {
                    throw new Exception(ErrorMessage.SeasonRefError.SEASON_REF_NOT_FOUND);
                }

                return seasonReference;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region CUD
        public async Task<Guid> CreateNewSeasonRef(CreateNewSeasonRefRequest newSeasonRefRequest)
        {
            try
            {
                //check request is valid
                ValidationResult validationResult = new CreateNewSeasonRefRequestValidator().Validate(newSeasonRefRequest);
                if (!validationResult.IsValid)
                {
                    throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
                }

                //create new season ref
                var newSeasonRef = new SeasonReference();
                newSeasonRef.SeasonReferenceId = Guid.NewGuid();
                newSeasonRef.Name = newSeasonRefRequest.Name;
                newSeasonRef.Status = newSeasonRefRequest.Status == null ? false : newSeasonRefRequest.Status;

                //if status of new season ref is true => all of others will be false ( 1 season ref available in one time )
                if(newSeasonRefRequest.Status == true)
                {
                    var listSeasonRef = _seasonReferenceRepository.GetAll().Result.ToList();

                    if(listSeasonRef.Count != 0)
                    {
                        foreach (var item in listSeasonRef)
                        {
                            item.Status = false;
                        }
                    }

                    await _seasonReferenceRepository.UpdateRangeAsync(listSeasonRef);
                }

                await _seasonReferenceRepository.AddAsync(newSeasonRef);

                return newSeasonRef.SeasonReferenceId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateSeasonReference(UpdateSeasonRefRequest updateSeasonRefRequest)
        {
            try
            {
                //check request is valid
                ValidationResult validationResult = new UpdateSeasonRefRequestValidator().Validate(updateSeasonRefRequest);
                if (!validationResult.IsValid)
                {
                    throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
                }
                
                //get season ref object
                var seasonRefUpdate = _seasonReferenceRepository.GetFirstOrDefaultAsync(x => x.SeasonReferenceId == updateSeasonRefRequest.SeasonRefId).Result;

                if (seasonRefUpdate == null)
                {
                    throw new Exception(ErrorMessage.SeasonRefError.SEASON_REF_NOT_FOUND);
                }

                seasonRefUpdate.Name = updateSeasonRefRequest.Name == null ? seasonRefUpdate.Name : updateSeasonRefRequest.Name;
                seasonRefUpdate.Status = updateSeasonRefRequest.Status == null ? seasonRefUpdate.Status : (bool) updateSeasonRefRequest.Status;

                //if status of update season ref is true => all of others will be false ( 1 season ref available in one time )
                if (updateSeasonRefRequest.Status == true)
                {
                    var listSeasonRef = _seasonReferenceRepository.GetAll().Result.ToList();

                    if (listSeasonRef.Count != 0)
                    {
                        foreach (var item in listSeasonRef)
                        {
                            item.Status = false;
                        }
                    }

                    await _seasonReferenceRepository.UpdateRangeAsync(listSeasonRef);
                }

                await _seasonReferenceRepository.UpdateAsync(seasonRefUpdate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteSeasonReference(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                //only available season ref can be deleted
                var seasonReferenceDelete = _seasonReferenceRepository.GetFirstOrDefaultAsync(x => x.SeasonReferenceId.Equals(id) && x.Status == true).Result;

                if (seasonReferenceDelete == null)
                {
                    throw new Exception(ErrorMessage.SeasonRefError.SEASON_REF_NOT_FOUND);
                }

                seasonReferenceDelete.Status = false;

                await _seasonReferenceRepository.UpdateAsync(seasonReferenceDelete);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
