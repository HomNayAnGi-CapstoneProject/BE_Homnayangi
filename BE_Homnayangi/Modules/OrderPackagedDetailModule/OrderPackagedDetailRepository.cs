    using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.OrderPackageDetailModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;

namespace BE_Homnayangi.Modules.OrderPackageDetailModule
{
    public class OrderPackageDetailRepository : Repository<OrderPackageDetail>, IOrderPackageDetailRepository
	{
        private readonly HomnayangiContext _db;

        public OrderPackageDetailRepository(HomnayangiContext db) : base(db)
		{
            _db = db;
		}

        public async Task<ICollection<OrderPackageDetail>> GetOrderPackageDetailsBy(Expression<Func<OrderPackageDetail, bool>> filter = null, Func<IQueryable<OrderPackageDetail>, ICollection<OrderPackageDetail>> options = null, string includeProperties = null)

        {
            IQueryable<OrderPackageDetail> query = DbSet;

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
