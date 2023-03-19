using BE_Homnayangi.Modules.AccomplishmentModule.Interface;
using BE_Homnayangi.Modules.AdminModules.BadgeConditionModule.Interface;
using BE_Homnayangi.Modules.BadgeModule.Interface;
using BE_Homnayangi.Modules.CustomerBadgeModule.Interface;
using BE_Homnayangi.Modules.CustomerModule.Interface;
using Library.Models;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Ultils.Quartz
{
    [DisallowConcurrentExecution]
    public class BadgeJob : IJob
    {
        private readonly IBadgeService BadgeService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IBadgeRepository _badgeRepository;
        private readonly IAccomplishmentRepository _accomplishmenttRepository;
        private readonly IBadgeConditionRepository _badgeConditionRepository;
        private readonly ICustomerBadgeRepository _customerBadgeRepository;
        public BadgeJob(ICustomerRepository customerRepository, IBadgeRepository badgeRepository, IAccomplishmentRepository accomplishmenttRepository, IBadgeConditionRepository badgeConditionRepository, ICustomerBadgeRepository customerBadgeRepository)
        {
            _customerRepository = customerRepository;
            _badgeRepository = badgeRepository;
            _accomplishmenttRepository = accomplishmenttRepository;
            _badgeConditionRepository = badgeConditionRepository;
            _customerBadgeRepository = customerBadgeRepository;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello from Quartz hê hê");
            await BadgeCondition();

        }
        public async Task BadgeCondition()
        {
            var customers = await _customerRepository.GetAll();
            var accomplishmentsList = await _accomplishmenttRepository.GetAll();
            customers = customers.Join(accomplishmentsList, x => x.CustomerId, y => y.AuthorId, (x, y) => x).ToList();
            foreach (Customer customer in customers)
            {
                int accomplishments = customer.Accomplishments.Count();
                int orders = customer.Orders.Count();
                Console.WriteLine(customer.Displayname);
                var badgeConditions = await _badgeConditionRepository.GetAll();
                badgeConditions = badgeConditions.Where(x => x.Accomplishments == accomplishments && x.Orders == orders).ToList();
                foreach (BadgeCondition badgeCondition in badgeConditions)
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
                    }
                };
            };
        }
    }
}
