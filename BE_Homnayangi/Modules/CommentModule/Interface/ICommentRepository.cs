using Library.Models;
using Repository.Repository.Interface;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace BE_Homnayangi.Modules.CommentModule.Interface
{
    public interface ICommentRepository : IRepository<Comment>
    {
        public Task<ICollection<Comment>> GetCommentsBy(
            Expression<Func<Comment, bool>> filter = null,
            Func<IQueryable<Comment>, ICollection<Comment>> options = null,
            string includeProperties = null
        );
    }
}
