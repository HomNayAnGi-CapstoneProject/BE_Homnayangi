using Library.Models;
using Repository.Repository.Interface;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace BE_Homnayangi.Modules.BlogSubCateModule.Interface
{
    public interface IBlogSubCateRepository : IRepository<BlogSubCate>
    {
        public Task<ICollection<BlogSubCate>> GetBlogSubCatesBy(
            Expression<Func<BlogSubCate, bool>> filter = null,
            Func<IQueryable<BlogSubCate>, ICollection<BlogSubCate>> options = null,
            string includeProperties = null
        );
    }
}
