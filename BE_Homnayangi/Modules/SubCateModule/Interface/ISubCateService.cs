using System;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.SubCateModule.Response;

namespace BE_Homnayangi.Modules.SubCateModule.Interface
{
    public interface ISubCateService
    {
        public Task AddNewSubCate(SubCategory newTag);

        public Task UpdateSubCate(SubCategory TagUpdate);

        public Task<ICollection<SubCategory>> GetAll();

        public Task<ICollection<SubCategory>> GetSubCatesBy(
            Expression<Func<SubCategory, bool>> filter = null,
            Func<IQueryable<SubCategory>, ICollection<SubCategory>> options = null,
            string includeProperties = null);

        public SubCategory GetSubCateByID(Guid? id);

        public Task<ICollection<SubCateResponse>> GetSubCatesByCategoryId(Guid id);

    }
}

