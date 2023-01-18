using BE_Homnayangi.Modules.UserModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.UserModule
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        private readonly HomnayangiContext _db;

        public UserRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<User>> GetUsersBy(
          Expression<Func<User, bool>> filter = null,
          Func<IQueryable<User>, ICollection<User>> options = null,
          string includeProperties = null
      )
        {
            IQueryable<User> query = DbSet;

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
