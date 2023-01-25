using System;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Repository.Repository.Interface;

namespace BE_Homnayangi.Modules.SubCateModule.Interface
{
    public interface ISubCateRepository : IRepository<SubCategory>
    {
        public Task<ICollection<SubCategory>> GetSubCatesBy(
            Expression<Func<SubCategory, bool>> filter = null,
            Func<IQueryable<SubCategory>, ICollection<SubCategory>> options = null,
            string includeProperties = null
        );
    }
}

