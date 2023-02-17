using BE_Homnayangi.Modules.CartDetailModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CartDetailModule
{
    public class CartDetailRepository : Repository<CartDetail>, ICartDetailRepository
    {
        private readonly HomnayangiContext _db;

        public CartDetailRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<CartDetail>> GetCartDetailsBy(
            Expression<Func<CartDetail, bool>> filter = null,
            Func<IQueryable<CartDetail>, ICollection<CartDetail>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<CartDetail> query = DbSet;

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
