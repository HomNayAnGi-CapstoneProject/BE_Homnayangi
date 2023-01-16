using System;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.CustomerVoucherModule.Response;

namespace BE_Homnayangi.Modules.CustomerVoucherModule.Interface
{
    public interface ICustomerVoucherService
    {
        public Task<ICollection<CustomerVoucher>> GetCustomerVouchersBy(
            Expression<Func<CustomerVoucher, bool>> filter = null,
            Func<IQueryable<CustomerVoucher>, ICollection<CustomerVoucher>> options = null,
            string includeProperties = null);

        public Task<ICollection<CustomerVoucherResponse>> GetAllCustomerVouchers();

        public Task<ICollection<CustomerVoucherResponse>> GetAllCustomerVouchersByCusId(Guid cusid);

        public Task<bool> DeleteCustomerVouchersByVoucherId(Guid voucherId);

        public Task<bool> DeleteCustomerVouchersByCusAndVoucherId(Guid voucherId, Guid customerId);

        public Task<CustomerVoucherResponse> AddCustomerVoucher(CustomerVoucher cv);

        public Task<CustomerVoucher> GetCustomerVoucherByCombineID(Guid cusId, Guid voucherId);

    }
}

