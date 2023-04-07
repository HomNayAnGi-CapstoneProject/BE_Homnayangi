using BE_Homnayangi.Modules.BadgeModule.Interface;
using BE_Homnayangi.Modules.CustomerBadgeModule.Interface;
using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.CustomerVoucherModule.Interface;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Ultils.Quartz
{
    public class VoucherJob
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerBadgeRepository _customerBadgeRepository;
        private readonly ICustomerVoucherRepository _customerVoucherRepository;
        private readonly IBadgeRepository _badgeRepository;
        public VoucherJob(ICustomerRepository customerRepository, ICustomerBadgeRepository customerBadgeRepository, ICustomerVoucherRepository customerVoucherRepository, IBadgeRepository badgeRepository)
        {
            _customerRepository = customerRepository;
            _customerBadgeRepository = customerBadgeRepository;
            _customerVoucherRepository = customerVoucherRepository;
            _badgeRepository = badgeRepository;

        }

        public async Task AddVoucher()
        {
            var customers = await _customerRepository.GetAll(includeProperties: "CustomerBadges");
            foreach (var customer in customers)
            {
                var customerBadges = await _customerBadgeRepository.GetCustomerBadgeBy(x => x.CustomerId == customer.CustomerId, includeProperties: "Badge,Customer");
                foreach (var customerBadge in customerBadges)
                {
                    var customerVoucher = await _customerVoucherRepository.GetFirstOrDefaultAsync(x => x.VoucherId == customerBadge.Badge.VoucherId);
                    if (customerVoucher != null && DateTime.Now >= customerVoucher.CreatedDate.Value.AddDays(7))
                    {
                        CustomerVoucher cv = new CustomerVoucher
                        {
                            CustomerVoucherId = Guid.NewGuid(),
                            CustomerId = customer.CustomerId,
                            VoucherId = (Guid)customerVoucher.VoucherId,
                            CreatedDate = DateTime.Now


                        };
                        _customerVoucherRepository.Add(cv);
                    }
                }
            }
        }
    }
}
