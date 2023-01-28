using BE_Homnayangi.Modules.CommentModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CommentModule
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly HomnayangiContext _db;

        public CommentRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<Comment>> GetCommentsBy(
            Expression<Func<Comment, bool>> filter = null,
            Func<IQueryable<Comment>, ICollection<Comment>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<Comment> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return options != null ? options(query).ToList() : await query.ToListAsync();
        }
    }
}
