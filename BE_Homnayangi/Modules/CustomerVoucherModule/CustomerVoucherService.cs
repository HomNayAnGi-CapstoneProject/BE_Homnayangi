using AutoMapper;
using BE_Homnayangi.Modules.AdminModules.CronJobTimeConfigModule.Interface;
using BE_Homnayangi.Modules.CustomerModule;
using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.CustomerVoucherModule.Interface;
using BE_Homnayangi.Modules.CustomerVoucherModule.Request;
using BE_Homnayangi.Modules.CustomerVoucherModule.Response;
using BE_Homnayangi.Modules.CustomerVoucherModule.Validation;
using BE_Homnayangi.Modules.OrderModule.Interface;
using BE_Homnayangi.Modules.VoucherModule.Interface;
using BE_Homnayangi.Ultils.Quartz;
using GSF;
using GSF.Collections;
using Hangfire;
using Library.Models;
using Library.Models.Constant;
using Library.Models.Enum;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
        private readonly ICustomerRepository _customerRepository;
        private readonly IVoucherRepository _voucherRepository;
        private readonly IOrderRepository _orderRepository;

        public CustomerVoucherService(ICustomerVoucherRepository customerVoucherRepository, ICronJobTimeConfigRepository cronJobTimeConfigRepository, IMapper mapper,
            ICustomerRepository customerRepository, IVoucherRepository voucherRepository, IOrderRepository orderRepository)
        {
            _customerVoucherRepository = customerVoucherRepository;
            _cronJobTimeConfigRepository = cronJobTimeConfigRepository;
            _mapper = mapper;
            _customerRepository = customerRepository;
            _voucherRepository = voucherRepository;
            _orderRepository = orderRepository;
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

        //public async Task<CustomerVoucherResponse> AddCustomerVoucher(CustomerVoucher cv)
        //{
        //    CustomerVoucherResponse result = null;
        //    try
        //    {
        //        var tmp = await GetCustomerVoucherByCombineID(cv.CustomerId, cv.VoucherId);
        //        if (tmp == null)
        //        {
        //            cv.CreatedDate = DateTime.Now;
        //            await _customerVoucherRepository.AddAsync(cv);
        //            result = ConvertDTO(cv);
        //        }
        //        else
        //        {
        //            await _customerVoucherRepository.UpdateAsync(tmp);
        //            result = ConvertDTO(tmp);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error at AddCustomerVoucher: " + ex.Message);
        //        throw;
        //    }
        //    return result;

        //}

        public async Task<ICollection<CustomerVoucherResponse>> GetAllCustomerVouchersByCusId(Guid cusid)
        {
            List<CustomerVoucherResponse> tmpResult = null;
            List<CustomerVoucherResponse> result = null;
            try
            {
                var list = await _customerVoucherRepository.GetCustomerVouchersBy(x => x.CustomerId == cusid
                                                                                        && x.Voucher.ValidTo > DateTime.Now, // còn hsd
                                                                            includeProperties: "Customer,Voucher");
                if (list != null && list.Count > 0)
                {
                    tmpResult = list.Select(x => new CustomerVoucherResponse()
                    {
                        VoucherId = x.VoucherId,
                        CustomerId = x.CustomerId,
                        VoucherName = x.Voucher.Name,
                        CustomerVoucherId = x.CustomerVoucherId,
                        CustomerName = x.Customer.Displayname != null
                                         ? x.Customer.Displayname
                                         : x.Customer.Firstname + " " + x.Customer.Lastname,
                        Discount = x.Voucher.Discount.Value,
                        MinimumOrderPrice = x.Voucher.MinimumOrderPrice.Value,
                        MaximumOrderPrice = x.Voucher.MaximumOrderPrice.Value,
                        ValidFrom = x.Voucher.ValidFrom.Value,
                        ValidTo = x.Voucher.ValidTo.Value,
                        Status = x.Voucher.Status.Value,
                        CreatedDate = x.CreatedDate.Value
                    }).OrderByDescending(x => x.CreatedDate).ToList();

                    result = tmpResult;
                    foreach (var customerVoucher in tmpResult.DistinctBy(x => x.VoucherId).ToList())
                    {
                        var orders = await _orderRepository.GetOrdersBy(o => o.VoucherId != null && o.VoucherId == customerVoucher.VoucherId
                                                                            && o.CustomerId == customerVoucher.CustomerId
                                                                            && o.OrderStatus == (int)Status.OrderStatus.PENDING);
                        // xoá n item nếu nó đang xài trong orders
                        for (int i = 0; i < orders.Count; i++)
                        {
                            result.RemoveAt(result.IndexOf(customerVoucher));
                            Console.WriteLine("Removed 1 customer voucher!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetAllCustomerVouchersByCusId: " + ex.Message);
                throw new Exception(ex.Message);
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

        public async Task GiveVoucherForCustomer(GiveVoucherForCustomer request)
        {
            try
            {
                var customer = await _customerRepository.GetFirstOrDefaultAsync(c => c.CustomerId == request.CustomerId && !c.IsBlocked.Value);
                if (customer == null)
                    throw new Exception(ErrorMessage.CustomerError.CUSTOMER_NOT_FOUND);

                var voucher = await _voucherRepository.GetFirstOrDefaultAsync(v => v.VoucherId == request.VoucherId
                                                                                    && v.Status.Value == 1);
                if (voucher == null)
                    throw new Exception(ErrorMessage.VoucherError.VOUCHER_NOT_AVAILABLE);

                //var tmp = await _customerVoucherRepository.GetFirstOrDefaultAsync(x => x.CustomerId == request.CustomerId && x.VoucherId == request.VoucherId);
                //if (tmp != null)
                //    throw new Exception(ErrorMessage.CustomerVoucherError.CUSTOMER_VOUCHER_EXISTED);

                CustomerVoucher customerVoucher = new CustomerVoucher()
                {
                    CustomerVoucherId = Guid.NewGuid(),
                    CustomerId = request.CustomerId,
                    VoucherId = request.VoucherId,
                    CreatedDate = DateTime.Now
                };
                await _customerVoucherRepository.AddAsync(customerVoucher);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GiveVoucherForCustomer: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteCustomerVoucher(Guid voucherId, Guid customerId)
        {
            try
            {
                var tmp = await _customerVoucherRepository.GetFirstOrDefaultAsync(x => x.CustomerId == customerId && x.VoucherId == voucherId);
                if (tmp == null)
                    throw new Exception(ErrorMessage.CustomerVoucherError.CUSTOMER_VOUCHER_NOT_FOUND);
                await _customerVoucherRepository.RemoveAsync(tmp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at DeleteCustomerVoucher: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}

