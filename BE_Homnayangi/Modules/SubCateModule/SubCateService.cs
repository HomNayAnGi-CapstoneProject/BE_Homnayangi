using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.BlogSubCateModule.Interface;
using BE_Homnayangi.Modules.CategoryModule.Interface;
using BE_Homnayangi.Modules.SubCateModule.Interface;
using BE_Homnayangi.Modules.SubCateModule.Request;
using BE_Homnayangi.Modules.SubCateModule.Response;
using Library.Models;

namespace BE_Homnayangi.Modules.SubCateModule
{
    public class SubCateService : ISubCateService
    {
        private readonly ISubCateRepository _subCateRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBlogSubCateRepository _blogSubCateRepository;

        public SubCateService(ISubCateRepository subCateRepository, ICategoryRepository categoryRepository, IBlogSubCateRepository blogSubCateRepository)
        {
            _subCateRepository = subCateRepository;
            _categoryRepository = categoryRepository;
            _blogSubCateRepository = blogSubCateRepository;
        }

        public async Task<ICollection<SubCategory>> GetAll()
        {
            return await _subCateRepository.GetSubCatesBy(x => x.Status == true);
        }

        public async Task<ICollection<SubCategory>> GetAllForStaff()
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

        public async Task<Boolean> AddNewSubCate(CreateSubCategoryRequest newSubCateReq)
        {
            var category = _categoryRepository.GetByIdAsync(newSubCateReq.CategoryId).Result;
            if(category == null)
            {
                return false;
            }

            var newSubCate = new SubCategory();
            newSubCate.SubCategoryId = Guid.NewGuid();
            newSubCate.Name = newSubCateReq.Name;
            newSubCate.Description = newSubCateReq.Description;
            newSubCate.CreatedDate = DateTime.Now;
            newSubCate.Status = true;

            await _subCateRepository.AddAsync(newSubCate);
            return true;
        }

        public async Task<Boolean> UpdateSubCate(UpdateSubCategoryRequest subCateUpdateReq)
        {
            var subCateUpdate = _subCateRepository.GetByIdAsync(subCateUpdateReq.SubCategoryId).Result;
            if (subCateUpdate == null)
            {
                return false;
            }
            if (subCateUpdateReq.CategoryId != null && _categoryRepository.GetByIdAsync((Guid)subCateUpdateReq.CategoryId).Result == null)
            {
                return false;
            }
            subCateUpdate.Name = subCateUpdateReq.Name ?? subCateUpdate.Name;
            subCateUpdate.Description = subCateUpdateReq.Description ?? subCateUpdate.Description;
            if(subCateUpdate.Status == false && subCateUpdateReq.Status == true)
            {
                var listBlogSubCate = _blogSubCateRepository.GetBlogSubCatesBy(x => x.SubCateId == subCateUpdate.SubCategoryId).Result;
                foreach( var item in listBlogSubCate)
                {
                    item.Status = true;

                    await _blogSubCateRepository.UpdateRangeAsync(listBlogSubCate);
            }
            subCateUpdate.Status = subCateUpdateReq.Status ?? subCateUpdate.Status;
            subCateUpdate.CategoryId = subCateUpdateReq.CategoryId ?? subCateUpdate.CategoryId;

            await _subCateRepository.UpdateAsync(subCateUpdate);

            return true;
        }

        public async Task<Boolean> DeleteSubCate(Guid? deleteSubCateId)
        {
            if (deleteSubCateId == null)
            {
                return false;
            }
            var _deleteSubCate = _subCateRepository.GetByIdAsync((Guid)deleteSubCateId).Result;
            if (_deleteSubCate == null || _deleteSubCate.Status == false)
            {
                return false;
            }

            var listBlogSubCateDelete = _blogSubCateRepository.GetBlogSubCatesBy(x => x.SubCateId == _deleteSubCate.SubCategoryId).Result.ToList();
            foreach(var item in listBlogSubCateDelete)
            {
                item.Status = false;
            }

            await _blogSubCateRepository.UpdateRangeAsync(listBlogSubCateDelete);

            _deleteSubCate.Status = false;
            await _subCateRepository.UpdateAsync(_deleteSubCate);

            return true;
        }

        public SubCategory GetSubCateByID(Guid? id)
        {
            return _subCateRepository.GetFirstOrDefaultAsync(x => x.SubCategoryId.Equals(id) && x.Status.Value).Result;
        }

        public SubCategory GetSubCateByIDForStaff(Guid? id)
        {
            return _subCateRepository.GetFirstOrDefaultAsync(x => x.SubCategoryId.Equals(id)).Result;
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

        public async Task<ICollection<SubCateResponse>> GetSubCatesByCategoryIdForStaff(Guid id)
        {
            try
            {
                List<SubCateResponse> result = new List<SubCateResponse>();
                ICollection<SubCategory> list = await _subCateRepository.GetSubCatesBy(x => x.CategoryId.Equals(id));
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

