using AutoMapper;
using BE_Homnayangi.Modules.AdminModules.CronJobTimeConfigModule.Interface;
using BE_Homnayangi.Modules.CustomerVoucherModule.Interface;
using BE_Homnayangi.Modules.CustomerVoucherModule.Response;
using BE_Homnayangi.Modules.CustomerVoucherModule.Validation;
using BE_Homnayangi.Ultils.Quartz;
using Hangfire;
using Library.Models;
using Library.Models.Enum;
using Quartz;
using Quartz.Impl.Triggers;
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
        private readonly ICronJobTimeConfigRepository _cronJobTimeConfigRepository;
        private readonly IMapper _mapper;
        public CustomerVoucherService(ICustomerVoucherRepository customerVoucherRepository, ICronJobTimeConfigRepository cronJobTimeConfigRepository, IMapper mapper)
        {
            _customerVoucherRepository = customerVoucherRepository;
            _cronJobTimeConfigRepository = cronJobTimeConfigRepository;
            _mapper = mapper;
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
                Voucher = cv.Voucher != null ? cv.Voucher : null
            };
        }

        public void AwardVoucher()
        {
            try
            {
                var badgeTime = _cronJobTimeConfigRepository.GetFirstOrDefaultAsync(x => x.TargetObject == (int)CronJobTimeConfigType.CronJobTimeConfig.VOUCHER).Result;
                var badgeTimeVoucher = _mapper.Map<CronJobTimeVoucher>(badgeTime);
                var hour = badgeTimeVoucher.Hour;
                var date = badgeTimeVoucher.Day;
                var month = badgeTimeVoucher.Month;
                var minute = badgeTimeVoucher.Minute;
                if (month == null && date == null && hour == null)
                {
                    RecurringJob.AddOrUpdate<VoucherJob>("awardvoucher", x => x.AddVoucher(), Cron.Hourly((int)minute), TimeZoneInfo.Local);
                }
                else if (month == null && date == null)
                {
                    RecurringJob.AddOrUpdate<VoucherJob>("awardvoucher", x => x.AddVoucher(), Cron.Daily((int)hour, (int)minute), TimeZoneInfo.Local);

                }
                else if (month == null)
                {
                    RecurringJob.AddOrUpdate<VoucherJob>("awardvoucher", x => x.AddVoucher(), Cron.Monthly((int)hour, (int)minute), TimeZoneInfo.Local);
                }
                else
                {
                    RecurringJob.AddOrUpdate<VoucherJob>("awardvoucher", x => x.AddVoucher(), Cron.Yearly((int)month, (int)date, (int)hour, (int)minute), TimeZoneInfo.Local);


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at AwardVoucher: " + ex.Message);
                throw;
            }
        }
    }
}

