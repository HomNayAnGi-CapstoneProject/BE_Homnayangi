using BE_Homnayangi.Modules.CartModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CartModule
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private readonly HomnayangiContext _db;

        public CartRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<Cart>> GetCartsBy(
            Expression<Func<Cart, bool>> filter = null,
            Func<IQueryable<Cart>, ICollection<Cart>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<Cart> query = DbSet;

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
