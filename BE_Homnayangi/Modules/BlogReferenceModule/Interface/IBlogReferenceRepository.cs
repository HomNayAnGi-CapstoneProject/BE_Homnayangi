using Library.Models;
using Repository.Repository.Interface;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace BE_Homnayangi.Modules.BlogReferenceModule.Interface
{
    public interface IBlogReferenceRepository : IRepository<BlogReference>
    {
        public Task<ICollection<BlogReference>> GetBlogReferencesBy(
            Expression<Func<BlogReference, bool>> filter = null,
            Func<IQueryable<BlogReference>, ICollection<BlogReference>> options = null,
            string includeProperties = null
        );
    }
}
