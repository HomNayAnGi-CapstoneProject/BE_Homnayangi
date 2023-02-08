using System;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.SubCateModule.Response;
using BE_Homnayangi.Modules.SubCateModule.Request;

namespace BE_Homnayangi.Modules.SubCateModule.Interface
{
    public interface ISubCateService
    {
        public Task<Boolean> AddNewSubCate(CreateSubCategoryRequest newSubCate);

        public Task<Boolean> UpdateSubCate(UpdateSubCategoryRequest subCateUpdate);
        public Task<Boolean> DeleteSubCate(Guid? deleteSubCateId);

        public Task<ICollection<SubCategory>> GetAll();

        public Task<ICollection<SubCategory>> GetAllForStaff();

        public Task<ICollection<SubCategory>> GetSubCatesBy(
            Expression<Func<SubCategory, bool>> filter = null,
            Func<IQueryable<SubCategory>, ICollection<SubCategory>> options = null,
            string includeProperties = null);

        public SubCategory GetSubCateByID(Guid? id);

        public SubCategory GetSubCateByIDForStaff(Guid? id);

        public Task<ICollection<SubCateResponse>> GetSubCatesByCategoryId(Guid id);
        public Task<ICollection<SubCateResponse>> GetSubCatesByCategoryIdForStaff(Guid id);
    }
}

