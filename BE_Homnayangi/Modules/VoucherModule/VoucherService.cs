using BE_Homnayangi.Modules.VoucherModule.Interface;
using BE_Homnayangi.Modules.VoucherModule.Response;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.VoucherModule
{
    public class VoucherService : IVoucherService
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherService(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }

        public Task<ICollection<Voucher>> GetVouchersBy(
                Expression<Func<Voucher,
                bool>> filter = null,
                Func<IQueryable<Voucher>,
                ICollection<Voucher>> options = null,
                string includeProperties = null)
        {
            return _voucherRepository.GetVouchersBy(filter);
        }

        public async Task<ICollection<ViewVoucherResponse>> GetAllVoucher()
        {
            List<ViewVoucherResponse> result = null;
            try
            {
                var vouchers = await _voucherRepository.GetVouchersBy(includeProperties: "Author");
                if (vouchers.Count() > 0)
                {
                    result = new List<ViewVoucherResponse>();
                    foreach (var voucher in vouchers)
                    {
                        ViewVoucherResponse vvr = new ViewVoucherResponse();

                        vvr.VoucherId = voucher.VoucherId;
                        vvr.Name = voucher.Name != null ? voucher.Name : "";
                        vvr.Description = voucher.Description != null ? voucher.Description : "";
                        vvr.Status = voucher.Status != null ? voucher.Status : 0;
                        vvr.CreatedDate = voucher.CreatedDate != null ? voucher.CreatedDate : DateTime.Now; // tmp solution because dont have clear requirement about it!!!
                        vvr.ValidFrom = voucher.ValidFrom != null ? voucher.ValidFrom : DateTime.Now;
                        vvr.ValidTo = voucher.ValidTo != null ? voucher.ValidTo : DateTime.Now;
                        vvr.Discount = voucher.Discount != null ? voucher.Discount : 0;
                        vvr.MinimumOrder = voucher.MinimumOrder != null ? voucher.MinimumOrder : 0;
                        vvr.MaximumOrder = voucher.MaximumOrder != null ? voucher.MaximumOrder : 0;
                        vvr.AuthorName = voucher.Author != null ? voucher.Author.Firstname + " " + voucher.Author.Lastname : "";

                        result.Add(vvr);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetAllVoucher: " + ex.Message);
                throw;
            }
            return result;
        }

        public async Task<ViewVoucherResponse> GetVoucherByID(Guid id)
        {
            ViewVoucherResponse result = null;
            try
            {
                //Note: can not use GetByIdAsync here because GetByIdAsync can not get data from another table
                var tmp = await _voucherRepository.GetFirstOrDefaultAsync(x => x.VoucherId == id, includeProperties: "Author");
                if (tmp != null)
                {
                    Voucher voucher = tmp;
                    result = new ViewVoucherResponse();
                    result.VoucherId = voucher.VoucherId;
                    result.Name = voucher.Name != null ? voucher.Name : "";
                    result.Description = voucher.Description != null ? voucher.Description : "";
                    result.Status = voucher.Status != null ? voucher.Status : 0;
                    result.CreatedDate = voucher.CreatedDate != null ? voucher.CreatedDate : DateTime.Now; // tmp solution because dont have clear requirement about it!!!
                    result.ValidFrom = voucher.ValidFrom != null ? voucher.ValidFrom : DateTime.Now;
                    result.ValidTo = voucher.ValidTo != null ? voucher.ValidTo : DateTime.Now;
                    result.Discount = voucher.Discount != null ? voucher.Discount : 0;
                    result.MinimumOrder = voucher.MinimumOrder != null ? voucher.MinimumOrder : 0;
                    result.MaximumOrder = voucher.MaximumOrder != null ? voucher.MaximumOrder : 0;
                    result.AuthorName = voucher.Author != null ? voucher.Author.Firstname + " " + voucher.Author.Lastname : "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetVoucherByID: " + ex.Message);
                throw;
            }
            return result;
        }

        public async Task<bool> DeleteVoucherById(Guid id)
        {
            bool isDeleted = false;
            try
            {
                Voucher voucher = await _voucherRepository.GetFirstOrDefaultAsync(x => x.VoucherId == id);
                if (voucher != null)
                {
                    voucher.Status = 0;
                }
                await _voucherRepository.UpdateAsync(voucher);
                isDeleted = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at DeleteVoucherById: " + ex.Message);
                throw;
            }
            return isDeleted;
        }

        // Note: Không update AuthorId
        public async Task<bool> UpdateVoucher(Voucher newVoucher)
        {
            bool isUpdated = false;
            try
            {
                Voucher voucher = await _voucherRepository.GetFirstOrDefaultAsync(x => x.VoucherId == newVoucher.VoucherId);
                if (voucher != null)
                {
                    voucher.Name = newVoucher.Name == null ? voucher.Name : newVoucher.Name;
                    voucher.Description = newVoucher.Description == null ? voucher.Description : newVoucher.Description;
                    voucher.Status = newVoucher.Status == null ? voucher.Status : newVoucher.Status;
                    voucher.CreatedDate = newVoucher.CreatedDate == null ? voucher.CreatedDate : newVoucher.CreatedDate;
                    voucher.ValidFrom = newVoucher.ValidFrom == null ? voucher.ValidFrom : newVoucher.ValidFrom;
                    voucher.ValidTo = newVoucher.ValidTo == null ? voucher.ValidTo : newVoucher.ValidTo;
                    voucher.Discount = newVoucher.Discount == null ? voucher.Discount : newVoucher.Discount;
                    voucher.MinimumOrder = newVoucher.MinimumOrder == null ? voucher.MinimumOrder : newVoucher.MinimumOrder;
                    voucher.MaximumOrder = newVoucher.MaximumOrder == null ? voucher.MaximumOrder : newVoucher.MaximumOrder;

                    await _voucherRepository.UpdateAsync(voucher);
                    isUpdated = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateVoucher: " + ex.Message);
                throw;
            }
            return isUpdated;
        }

        public async Task<bool> CreateByUser(Voucher voucher)
        {
            bool isInserted = false;
            try
            {
                voucher.VoucherId = Guid.NewGuid();
                voucher.Name = voucher.Name.Trim();
                voucher.Status = 1;
                voucher.CreatedDate = DateTime.Now;
                await _voucherRepository.AddAsync(voucher);
                isInserted = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CreateByUser: " + ex.Message);
                throw;
            }
            return isInserted;
        }


    }
}

