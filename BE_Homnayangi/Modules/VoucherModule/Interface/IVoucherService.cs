using BE_Homnayangi.Modules.VoucherModule.Response;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.VoucherModule.Interface
{
    public interface IVoucherService 
	{
        public Task<ICollection<Voucher>> GetVouchersBy(
            Expression<Func<Voucher, bool>> filter = null,
            Func<IQueryable<Voucher>, ICollection<Voucher>> options = null,
            string includeProperties = null);

        public Task<ICollection<Voucher>> GetRandomVouchersBy(Expression<Func<Voucher, bool>> filter = null,
            Func<IQueryable<Voucher>, ICollection<Voucher>> options = null,
            string includeProperties = null,
            int numberItem = 0);
        public Task<ICollection<ViewVoucherResponse>> GetAllVoucher();

        public Task<ViewVoucherResponse> GetVoucherByID(Guid id);

        public Task<bool> DeleteVoucherById(Guid id); // change status

        public Task<bool> UpdateQuantityVoucher(Guid id, int newQuantity);

        public Task<bool> UpdateVoucher(Voucher voucher);

        public Task<bool> CreateByUser(Voucher voucher);

    }
}

