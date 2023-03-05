    using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.OrderModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;

namespace BE_Homnayangi.Modules.OrderModule
{
    public class OrderRepository : Repository<Order>, IOrderRepository
	{
        private readonly HomnayangiContext _db;

        public OrderRepository(HomnayangiContext db) : base(db)
		{
            _db = db;
		}

        public async Task<ICollection<Order>> GetOrdersBy(Expression<Func<Order, bool>> filter = null, Func<IQueryable<Order>, ICollection<Order>> options = null, string includeProperties = null)

        {
            IQueryable<Order> query = DbSet;

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
