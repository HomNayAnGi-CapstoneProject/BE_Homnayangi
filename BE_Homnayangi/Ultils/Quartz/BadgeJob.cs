using BE_Homnayangi.Modules.AccomplishmentModule.Interface;
using BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Interface;
using BE_Homnayangi.Modules.BadgeModule.Interface;
using BE_Homnayangi.Modules.CustomerBadgeModule.Interface;
using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.CustomerVoucherModule.Interface;
using BE_Homnayangi.Modules.OrderModule.Interface;
using Library.Models;
using Library.Models.Enum;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Ultils.Quartz
{
    public class BadgeJob
    {
        private readonly IBadgeService BadgeService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBadgeRepository _badgeRepository;
        private readonly IAccomplishmentRepository _accomplishmenttRepository;
        private readonly IBadgeConditionRepository _badgeConditionRepository;
        private readonly ICustomerBadgeRepository _customerBadgeRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerVoucherRepository _customerVoucherRepository;
        public BadgeJob(ICustomerRepository customerRepository, IBadgeRepository badgeRepository, IAccomplishmentRepository accomplishmenttRepository, IBadgeConditionRepository badgeConditionRepository, ICustomerBadgeRepository customerBadgeRepository, IOrderRepository orderRepository, ICustomerVoucherRepository customerVoucherRepository)
        {
            _customerRepository = customerRepository;
            _badgeRepository = badgeRepository;
            _accomplishmenttRepository = accomplishmenttRepository;
            _badgeConditionRepository = badgeConditionRepository;
            _customerBadgeRepository = customerBadgeRepository;
            _orderRepository = orderRepository;
            _customerVoucherRepository = customerVoucherRepository;

        }
        public async Task BadgeCondition()
        {
            Console.WriteLine("Hello from Hangfire");
            var customers = await _customerRepository.GetAll();
            var accomplishmentsList = await _accomplishmenttRepository.GetAll();
            var ordersList = await _orderRepository.GetAll();
            customers = customers
         .GroupJoin(accomplishmentsList,
            x => x.CustomerId,
            y => y.AuthorId,
             (x, y) => new { Customer = x, Accomplishments = y.DefaultIfEmpty() })
         .SelectMany(x => x.Accomplishments
             .Select(y => new { Customer = x.Customer, Accomplishment = y }))
         .GroupJoin(ordersList,
            x => x.Customer.CustomerId,
             y => y.CustomerId,
             (x, y) => new { CustomerWithAccomplishment = x, Orders = y.DefaultIfEmpty() })
         .SelectMany(x => x.Orders
             .Select(y => x.CustomerWithAccomplishment.Customer))
         .ToList();
            foreach (Customer customer in customers)
            {

                Console.WriteLine(customer.Displayname);
                var badgeConditions = _badgeConditionRepository.GetAll(includeProperties: "Badge").Result.Where(x => x.Status == Convert.ToBoolean((int)Status.BadgeCondition.ACTIVE) && x.Badge.Status == (int)Status.BadgeStatus.ACTIVE);
                /*         badgeConditions = badgeConditions.Where(x => x.Accomplishments <= accomplishmentsCount && x.Orders <= ordersCount ).ToList();*/
                foreach (BadgeCondition badgeCondition in badgeConditions)
                {
                    var accomplishments = customer.Accomplishments.Where(x => x.CreatedDate >= badgeCondition.CreatedDate && x.CreatedDate >= DateTime.Now.AddMonths(-2) && x.Status == (int)Status.AccomplishmentStatus.ACTIVE);
                    var orders = customer.Orders.Where(x => x.OrderDate >= badgeCondition.CreatedDate && x.OrderDate >= DateTime.Now.AddMonths(-2) && x.OrderStatus == (int)Status.OrderStatus.DELIVERED);

                    if (badgeCondition.Accomplishments <= accomplishments.Count() && badgeCondition.Orders <= orders.Count())
                    {
                        var tmp = await _customerBadgeRepository.GetFirstOrDefaultAsync(x => x.CustomerId == customer.CustomerId && x.BadgeId == badgeCondition.BadgeId);
                        if (tmp == null)
                        {
                            CustomerBadge customerBadge = new CustomerBadge
                            {
                                CustomerId = customer.CustomerId,
                                BadgeId = badgeCondition.BadgeId,
                                CreatedDate = DateTime.Now

                            };

                            _customerBadgeRepository.Add(customerBadge);
                            var badge = await _badgeRepository.GetByIdAsync(customerBadge.BadgeId);
                            var cv = await _customerVoucherRepository.GetFirstOrDefaultAsync(x => x.CustomerId == customer.CustomerId && x.VoucherId == badge.VoucherId);
                            if (cv == null)
                            {
                                CustomerVoucher customerVoucher = new CustomerVoucher
                                {
                                    CustomerVoucherId = Guid.NewGuid(),
                                    CustomerId = customer.CustomerId,
                                    VoucherId = (Guid)badge.VoucherId,
                                    CreatedDate = DateTime.Now


                                };
                                _customerVoucherRepository.Add(customerVoucher);
                            }
                        }
                    }
                };
            };
        }
    }
}
