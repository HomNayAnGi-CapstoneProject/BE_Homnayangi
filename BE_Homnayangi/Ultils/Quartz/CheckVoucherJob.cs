using BE_Homnayangi.Modules.AccomplishmentModule.Interface;
using BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Interface;
using BE_Homnayangi.Modules.BadgeModule.Interface;
using BE_Homnayangi.Modules.CustomerBadgeModule.Interface;
using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.CustomerVoucherModule.Interface;
using BE_Homnayangi.Modules.OrderModule.Interface;
using BE_Homnayangi.Modules.VoucherModule.Interface;
using Library.Models;
using Library.Models.Enum;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Ultils.Quartz
{



    [DisallowConcurrentExecution]
    public class CheckVoucherJob : IJob
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly ICustomerVoucherRepository _customerVoucherRepository;
        private readonly IBadgeRepository _badgeRepository;

        public CheckVoucherJob(IVoucherRepository voucherRepository, ICustomerVoucherRepository customerVoucherRepository, IBadgeRepository badgeRepository)
        {
            _voucherRepository = voucherRepository;
            _customerVoucherRepository = customerVoucherRepository;
            _badgeRepository = badgeRepository;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await CheckVoucherCondition();

        }
        public async Task CheckVoucherCondition()
        {
            Console.WriteLine("Hello from check voucher");
            var vouchers = await _voucherRepository.GetAll();
            foreach (Voucher voucher in vouchers)
            {
                var badges = await _badgeRepository.GetAll(includeProperties: "Voucher");
                var customerVouchers = await _customerVoucherRepository.GetAll(includeProperties: "Voucher");
                if (voucher.ValidTo <= DateTime.Now)
                {
                    foreach (Badge badge in badges)
                    {
                        if (badge.VoucherId == voucher.VoucherId)
                        {
                            badge.VoucherId = null;
                            badge.Status = 0;
                            await _badgeRepository.UpdateAsync(badge);
                        }
                    }
                    foreach (CustomerVoucher customerVoucher in customerVouchers)
                    {
                        if (customerVoucher.VoucherId == voucher.VoucherId)
                        {
                            await _customerVoucherRepository.RemoveAsync(customerVoucher);
                        }
                    }

                }
            }
        }
    }
}

