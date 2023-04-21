using BE_Homnayangi.Modules.VoucherModule.Interface;
using BE_Homnayangi.Modules.VoucherModule.Request;
using BE_Homnayangi.Modules.VoucherModule.Response;
using Library.Models;
using Library.Models.Constant;
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
                        vvr.MinimumOrderPrice = voucher.MinimumOrderPrice != null ? voucher.MinimumOrderPrice : 0;
                        vvr.MaximumOrderPrice = voucher.MaximumOrderPrice != null ? voucher.MaximumOrderPrice : 0;
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

        public async Task<ICollection<OverviewVoucher>> GetAllActiveVoucher()
        {
            try
            {
                var vouchers = await _voucherRepository.GetVouchersBy(v => v.Status == 1);
                return vouchers.Select(v => new OverviewVoucher()
                {
                    VoucherId = v.VoucherId,
                    VoucherName = v.Name
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetAllActiveVoucher: " + ex.Message);
                throw new Exception(ex.Message);
            }
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
                    result.MinimumOrderPrice = voucher.MinimumOrderPrice != null ? voucher.MinimumOrderPrice : 0;
                    result.MaximumOrderPrice = voucher.MaximumOrderPrice != null ? voucher.MaximumOrderPrice : 0;
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
        public async Task<bool> UpdateVoucher(Guid authorId, UpdateVoucherRequest newVoucher)
        {
            bool isUpdated = false;
            try
            {
                ValidateVoucher(newVoucher.ValidFrom.Value, newVoucher.ValidTo.Value,
                    newVoucher.MinimumOrderPrice.Value, newVoucher.MaximumOrderPrice, newVoucher.Discount.Value);

                Voucher voucher = await _voucherRepository.GetFirstOrDefaultAsync(x => x.VoucherId == newVoucher.VoucherId);

                if (voucher != null)
                {
                    voucher.Name = newVoucher.Name == null ? voucher.Name : newVoucher.Name;
                    voucher.Description = newVoucher.Description == null ? voucher.Description : newVoucher.Description;
                    voucher.Status = newVoucher.Status == null ? voucher.Status : newVoucher.Status;
                    //voucher.CreatedDate = newVoucher.CreatedDate == null ? voucher.CreatedDate : newVoucher.CreatedDate;
                    voucher.ValidFrom = newVoucher.ValidFrom == null ? voucher.ValidFrom : newVoucher.ValidFrom;
                    voucher.ValidTo = newVoucher.ValidTo == null ? voucher.ValidTo : newVoucher.ValidTo;
                    voucher.Discount = newVoucher.Discount == null ? voucher.Discount : newVoucher.Discount;
                    voucher.MinimumOrderPrice = newVoucher.MinimumOrderPrice == null ? voucher.MinimumOrderPrice : newVoucher.MinimumOrderPrice;
                    voucher.MaximumOrderPrice = voucher.Discount > 1 ? 0 : newVoucher.MaximumOrderPrice;
                    voucher.AuthorId = authorId;

                    await _voucherRepository.UpdateAsync(voucher);
                    isUpdated = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at UpdateVoucher: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return isUpdated;
        }

        public async Task<bool> CreateByUser(Guid userId, CreateVoucherRequest request)
        {
            bool isInserted = false;
            try
            {
                ValidateVoucher(request.ValidFrom, request.ValidTo, request.MinimumOrderPrice, request.MaximumOrderPrice, request.Discount);

                Voucher voucher = new Voucher()
                {
                    VoucherId = Guid.NewGuid(),
                    Name = request.Name.Trim(),
                    Description = request.Description.Trim(),
                    Status = 1,
                    ValidFrom = request.ValidFrom,
                    ValidTo = request.ValidTo,
                    Discount = request.Discount,
                    MinimumOrderPrice = request.MinimumOrderPrice,
                    MaximumOrderPrice = request.Discount > 1 ? 0 : request.MaximumOrderPrice,
                    AuthorId = userId,
                    CreatedDate = DateTime.Now,
                };

                await _voucherRepository.AddAsync(voucher);
                isInserted = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at CreateByUser: " + ex.Message);
                throw new Exception(ex.Message);
            }
            return isInserted;
        }

        private void ValidateVoucher(DateTime start, DateTime end, decimal min, decimal? max, decimal discount)
        {
            try
            {
                if (end <= start)
                    throw new Exception(ErrorMessage.VoucherError.DATETIME_NOT_VALID);

                if (max != null)
                    if (max <= min || min < 0)
                        throw new Exception(ErrorMessage.VoucherError.DISCOUNT_CONDITION_NOT_VALID);

                if (discount <= 0)
                    throw new Exception(ErrorMessage.VoucherError.DISCOUNT_PRICE_NOT_VALID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

