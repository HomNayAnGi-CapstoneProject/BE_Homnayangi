using Library.Models;
using Repository.Repository.Interface;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace BE_Homnayangi.Modules.BlogTagModule.Interface
{
    public interface IBlogTagRepository : IRepository<BlogTag>
    {
        public Task<ICollection<BlogTag>> GetBlogTagsBy(
            Expression<Func<BlogTag, bool>> filter = null,
            Func<IQueryable<BlogTag>, ICollection<BlogTag>> options = null,
            string includeProperties = null
        );
    }
}
