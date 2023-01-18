using System;
using Library.DataAccess;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.Models;
using Repository.Repository;
using BE_Homnayangi.Modules.CustomerVoucherModule.Interface;
using Microsoft.EntityFrameworkCore;

namespace BE_Homnayangi.Modules.CustomerVoucherModule
{
    public class CustomerVoucherRepository : Repository<CustomerVoucher>, ICustomerVoucherRepository
	{
        private readonly HomnayangiContext _db;

        public CustomerVoucherRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<CustomerVoucher>> GetCustomerVouchersBy(
            Expression<Func<CustomerVoucher, bool>> filter = null,
            Func<IQueryable<CustomerVoucher>, ICollection<CustomerVoucher>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<CustomerVoucher> query = DbSet;

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

