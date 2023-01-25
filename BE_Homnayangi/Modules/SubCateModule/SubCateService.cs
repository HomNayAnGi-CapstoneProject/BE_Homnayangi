using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.SubCateModule.Interface;
using BE_Homnayangi.Modules.SubCateModule.Response;
using Library.Models;

namespace BE_Homnayangi.Modules.SubCateModule
{
    public class SubCateService : ISubCateService
    {
        private readonly ISubCateRepository _subCateRepository;

        public SubCateService(ISubCateRepository subCateRepository)
        {
            _subCateRepository = subCateRepository;
        }

        public async Task<ICollection<SubCategory>> GetAll()
        {
            return await _subCateRepository.GetAll();
        }

        public Task<ICollection<SubCategory>> GetSubCatesBy(
                Expression<Func<SubCategory,
                bool>> filter = null,
                Func<IQueryable<SubCategory>,
                ICollection<SubCategory>> options = null,
                string includeProperties = null)
        {
            return _subCateRepository.GetSubCatesBy(filter);
        }

        public async Task AddNewSubCate(SubCategory newSubCate)
        {
            newSubCate.SubCategoryId = Guid.NewGuid();
            await _subCateRepository.AddAsync(newSubCate);
        }

        public async Task UpdateSubCate(SubCategory subCateUpdate)
        {
            await _subCateRepository.UpdateAsync(subCateUpdate);
        }

        public SubCategory GetSubCateByID(Guid? id)
        {
            return _subCateRepository.GetFirstOrDefaultAsync(x => x.SubCategoryId.Equals(id) && x.Status.Value).Result;
        }

        public async Task<ICollection<SubCateResponse>> GetSubCatesByCategoryId(Guid id)
        {
            try
            {
                List<SubCateResponse> result = new List<SubCateResponse>();
                ICollection<SubCategory> list = await _subCateRepository.GetSubCatesBy(x => x.CategoryId.Equals(id) && x.Status.Value);
                foreach (var subCate in list)
                {
                    result.Add(new SubCateResponse()
                    {
                        SubCateId = subCate.SubCategoryId,
                        Name = subCate.Name
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetSubCatesByCategoryId: " + ex.Message);
                throw;
            }
        }
    }
}

