
using System;
using System.Collections.Generic;
using System.Linq;
using Library.Models;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Repository.Repository.Interface;

namespace BE_Homnayangi.Modules.BlogModule.Interface
{
    public interface IBlogRepository : IRepository<Blog>
    {
        public Task<ICollection<Blog>> GetBlogsBy(
            Expression<Func<Blog, bool>> filter = null,
            Func<IQueryable<Blog>, ICollection<Blog>> options = null,
            string includeProperties = null
        );
    }
}
