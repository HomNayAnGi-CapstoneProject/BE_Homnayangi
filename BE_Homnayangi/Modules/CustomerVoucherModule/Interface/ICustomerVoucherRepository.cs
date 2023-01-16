using System;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Repository.Repository.Interface;

namespace BE_Homnayangi.Modules.CustomerVoucherModule.Interface
{
	public interface ICustomerVoucherRepository : IRepository<CustomerVoucher>
	{
        public Task<ICollection<CustomerVoucher>> GetCustomerVouchersBy(
            Expression<Func<CustomerVoucher, bool>> filter = null,
            Func<IQueryable<CustomerVoucher>, ICollection<CustomerVoucher>> options = null,
            string includeProperties = null
        );
    }
}

