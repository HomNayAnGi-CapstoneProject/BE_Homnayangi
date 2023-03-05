    using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.OrderCookedDetailModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;

namespace BE_Homnayangi.Modules.OrderCookedDetailModule
{
    public class OrderCookedDetailRepository : Repository<OrderCookedDetail>, IOrderCookedDetailRepository
	{
        private readonly HomnayangiContext _db;

        public OrderCookedDetailRepository(HomnayangiContext db) : base(db)
		{
            _db = db;
		}

        public async Task<ICollection<OrderCookedDetail>> GetOrderCookedDetailsBy(Expression<Func<OrderCookedDetail, bool>> filter = null, Func<IQueryable<OrderCookedDetail>, ICollection<OrderCookedDetail>> options = null, string includeProperties = null)

        {
            IQueryable<OrderCookedDetail> query = DbSet;

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
