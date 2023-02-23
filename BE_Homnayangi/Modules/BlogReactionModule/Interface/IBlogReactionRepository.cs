using Library.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;
using Repository.Repository.Interface;

namespace BE_Homnayangi.Modules.BlogReactionModule.Interface
{
    public interface IBlogReactionRepository : IRepository<BlogReaction>
    {
        public Task<ICollection<BlogReaction>> GetBlogReactionsBy(
            Expression<Func<BlogReaction, bool>> filter = null,
            Func<IQueryable<BlogReaction>, ICollection<BlogReaction>> options = null,
            string includeProperties = null
        );
    }
}
