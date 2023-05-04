using BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Interface;
using BE_Homnayangi.Modules.AdminModules.CaloReferenceModule.Request;
using FluentValidation.Results;
using Library.Models;
using Library.Models.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AdminModules.CaloReferenceModule
{
    public class CaloReferenceService : ICaloReferenceService
    {
        #region Define repository + Constructor
        private readonly ICaloReferenceRepository _caloReferenceRepository;
        public CaloReferenceService(ICaloReferenceRepository caloReferenceRepository)
        {
            _caloReferenceRepository = caloReferenceRepository;
        }
        #endregion

        #region Get

        public async Task<ICollection<CaloReference>> GetCaloReferences()
        {
            try
            {
                var caloReferences = await _caloReferenceRepository.GetAll();

                if (caloReferences.Count() == 0)
                {
                    throw new Exception(ErrorMessage.CommonError.LIST_IS_NULL);
                }

                return caloReferences.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CaloReference> GetCaloRef(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                var caloReference = await _caloReferenceRepository.GetByIdAsync((Guid)id);

                if (caloReference == null)
                {
                    throw new Exception(ErrorMessage.CaloRefError.CALO_REF_NOT_FOUND);
                }

                return caloReference;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region CUD
        public async Task<Guid> CreateNewCaloRef(CreateNewCaloRefRequest newCaloRefRequest)
        {
            try
            {
                ValidationResult validationResult = new CreateNewCaloRefRequestValidator().Validate(newCaloRefRequest);
                if (!validationResult.IsValid || newCaloRefRequest.FromAge >= newCaloRefRequest.ToAge)
                {
                    throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
                }

                if (await CheckRangeExisted(newCaloRefRequest.FromAge, newCaloRefRequest.ToAge))
                    throw new Exception(ErrorMessage.CaloRefError.CALO_REF_IS_EXISTED);

                var newCaloRef = new CaloReference();
                newCaloRef.CaloReferenceId = Guid.NewGuid();
                newCaloRef.FromAge = newCaloRefRequest.FromAge;
                newCaloRef.ToAge = newCaloRefRequest.ToAge;
                newCaloRef.Calo = newCaloRefRequest.Calo;
                newCaloRef.IsMale = newCaloRefRequest.IsMale;

                await _caloReferenceRepository.AddAsync(newCaloRef);

                return newCaloRef.CaloReferenceId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<bool> CheckRangeExisted(int min, int max)
        {
            try
            {
                var existedItems = await _caloReferenceRepository.GetAll();
                var item = existedItems.Where(c => Enumerable.Range(c.FromAge.Value, c.ToAge.Value).Contains(min)
                                                || Enumerable.Range(c.FromAge.Value, c.ToAge.Value).Contains(max));
                if (item.Count() > 0)
                {
                    Console.WriteLine("Co ton tai roi");
                }
                return item.Count() > 0;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateCaloReference(UpdateCaloRefRequest updateCaloRefRequest)
        {
            try
            {
                ValidationResult validationResult = new UpdateCaloRefRequestValidator().Validate(updateCaloRefRequest);
                if (!validationResult.IsValid)
                {
                    throw new Exception(ErrorMessage.CommonError.INVALID_REQUEST);
                }

                var caloRefUpdate = _caloReferenceRepository.GetFirstOrDefaultAsync(x => x.CaloReferenceId == updateCaloRefRequest.CaloRefId).Result;

                if (caloRefUpdate == null)
                {
                    throw new Exception(ErrorMessage.CaloRefError.CALO_REF_NOT_FOUND);
                }

                caloRefUpdate.FromAge = updateCaloRefRequest.FromAge == null ? caloRefUpdate.FromAge : (int)updateCaloRefRequest.FromAge;
                caloRefUpdate.ToAge = updateCaloRefRequest.ToAge == null ? caloRefUpdate.ToAge : (int)updateCaloRefRequest.ToAge;
                caloRefUpdate.Calo = updateCaloRefRequest.Calo == null ? caloRefUpdate.Calo : (int)updateCaloRefRequest.Calo;
                caloRefUpdate.IsMale = updateCaloRefRequest.IsMale == null ? caloRefUpdate.IsMale : (bool)updateCaloRefRequest.IsMale;

                await _caloReferenceRepository.UpdateAsync(caloRefUpdate);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteCaloReference(Guid? id)
        {
            try
            {
                if (id == null)
                {
                    throw new Exception(ErrorMessage.CommonError.ID_IS_NULL);
                }

                CaloReference caloReferenceDelete = _caloReferenceRepository.GetFirstOrDefaultAsync(x => x.CaloReferenceId.Equals(id)).Result;

                if (caloReferenceDelete == null)
                {
                    throw new Exception(ErrorMessage.CaloRefError.CALO_REF_NOT_FOUND);
                }

                await _caloReferenceRepository.RemoveAsync(caloReferenceDelete);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
