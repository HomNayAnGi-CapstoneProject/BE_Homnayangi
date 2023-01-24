using BE_Homnayangi.Modules.CustomerVoucherModule.Interface;
using BE_Homnayangi.Modules.CustomerVoucherModule.Response;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.VoucherModule
{
    public class CustomerVoucherService : ICustomerVoucherService
    {
        private readonly ICustomerVoucherRepository _customerVoucherRepository;

        public CustomerVoucherService(ICustomerVoucherRepository customerVoucherRepository)
        {
            _customerVoucherRepository = customerVoucherRepository;
        }

        public Task<ICollection<CustomerVoucher>> GetCustomerVouchersBy(
                Expression<Func<CustomerVoucher,
                bool>> filter = null,
                Func<IQueryable<CustomerVoucher>,
                ICollection<CustomerVoucher>> options = null,
                string includeProperties = null)
        {
            return _customerVoucherRepository.GetCustomerVouchersBy(filter);
        }

        public async Task<ICollection<CustomerVoucherResponse>> GetAllCustomerVouchers()
        {
            List<CustomerVoucherResponse> result = null;
            try
            {
                var list = await _customerVoucherRepository.GetCustomerVouchersBy(includeProperties: "Customer,Voucher");
                if (list != null && list.Count > 0)
                {
                    result = new List<CustomerVoucherResponse>();
                    foreach (var cv in list)
                    {
                        result.Add(ConvertDTO(cv));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetAllCustomerVouchers: " + ex.Message);
                throw;
            }
            return result;
        }

        public async Task<CustomerVoucher> GetCustomerVoucherByCombineID(Guid cusId, Guid voucherId)
        {
            CustomerVoucher result = null;
            try
            {
                var tmp = await _customerVoucherRepository.GetFirstOrDefaultAsync(x => x.CustomerId == cusId && x.VoucherId == voucherId, includeProperties: "Customer,Voucher");
                if (tmp != null)
                {
                    result = tmp;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetCustomerVoucherByCombineID: " + ex.Message);
                throw;
            }
            return result;
        }

        public async Task<CustomerVoucherResponse> AddCustomerVoucher(CustomerVoucher cv)
        {
            CustomerVoucherResponse result = null;
            try
            {
                var tmp = await GetCustomerVoucherByCombineID(cv.CustomerId, cv.VoucherId);
                if (tmp == null)
                {
                    cv.CreatedDate = DateTime.Now;
                    await _customerVoucherRepository.AddAsync(cv);
                    result = ConvertDTO(cv);
                }
                else
                {
                    tmp.Quantity += cv.Quantity != null ? cv.Quantity.Value : 0;
                    await _customerVoucherRepository.UpdateAsync(tmp);
                    result = ConvertDTO(tmp);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at AddCustomerVoucher: " + ex.Message);
                throw;
            }
            return result;

        }

        public async Task<ICollection<CustomerVoucherResponse>> GetAllCustomerVouchersByCusId(Guid cusid)
        {
            List<CustomerVoucherResponse> result = null;
            try
            {
                var list = await _customerVoucherRepository.GetCustomerVouchersBy(x => x.CustomerId == cusid, includeProperties: "Customer,Voucher");
                if (list != null && list.Count > 0)
                {
                    result = new List<CustomerVoucherResponse>();
                    foreach (var cv in list)
                    {
                        result.Add(ConvertDTO(cv));
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetAllCustomerVouchersByCusId: " + ex.Message);
                throw;
            }
            return result;
        }

        public async Task<bool> DeleteCustomerVouchersByVoucherId(Guid voucherId)
        {
            bool isDeleted = false;
            try
            {
                var list = await _customerVoucherRepository.GetCustomerVouchersBy(x => x.VoucherId == voucherId);
                if (list.Count > 0)
                {
                    foreach (var cv in list)
                    {
                        await _customerVoucherRepository.RemoveAsync(cv);
                    }
                    isDeleted = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at DeleteCustomerVouchersByVoucherId: " + ex.Message);
                throw;
            }
            return isDeleted;
        }

        public CustomerVoucherResponse ConvertDTO(CustomerVoucher cv)
        {
            return new CustomerVoucherResponse()
            {
                CustomerName = cv.Customer != null ? cv.Customer.Firstname + " " + cv.Customer.Lastname : "",
                VoucherName = cv.Voucher != null ? cv.Voucher.Name : "",
                CreatedDate = cv.Voucher != null ? cv.Voucher.CreatedDate : new DateTime(),
                Quantity = cv.Quantity.Value,
            };
        }
    }
}

