    using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.OrderDetailModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;

namespace BE_Homnayangi.Modules.OrderDetailModule
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
	{
        private readonly HomnayangiContext _db;

        public OrderDetailRepository(HomnayangiContext db) : base(db)
		{
            _db = db;
		}

        public async Task<ICollection<OrderDetail>> GetOrderDetailsBy(Expression<Func<OrderDetail, bool>> filter = null, Func<IQueryable<OrderDetail>, ICollection<OrderDetail>> options = null, string includeProperties = null)

        {
            IQueryable<OrderDetail> query = DbSet;

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
