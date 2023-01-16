using Library.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.VoucherModule.Interface
{
    public interface IVoucherRepository : IRepository<Voucher>
	{
        public Task<ICollection<Voucher>> GetVouchersBy(
            Expression<Func<Voucher, bool>> filter = null,
            Func<IQueryable<Voucher>, ICollection<Voucher>> options = null,
            string includeProperties = null
        );
    }
}

