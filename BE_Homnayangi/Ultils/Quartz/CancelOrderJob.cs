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
using System.Linq;
using System.Threading.Tasks;

namespace BE_Homnayangi.Ultils.Quartz
{



    [DisallowConcurrentExecution]
    public class CancelOrderJob : IJob
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICustomerRepository _customerRepository;
        public CancelOrderJob(IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await OrderCondition();

        }
        public async Task OrderCondition()
        {
            var customers = await _customerRepository.GetAll(includeProperties: "Orders");
            foreach (Customer customer in customers)
            {
                if (customer.Orders != null)
                {
                    var orders = customer.Orders;
                    foreach (Order order in orders)
                    {
                        if (order.IsCooked == true && order.OrderStatus == (int)Status.OrderStatus.PENDING && DateTime.Now.AddHours(2) >= order.ShippedDate)
                        {
                            order.OrderStatus = (int)Status.OrderStatus.DENIED;
                            await _orderRepository.UpdateAsync(order);
                        }
                        else if (order.IsCooked == false && order.OrderStatus == (int)Status.OrderStatus.PENDING && DateTime.Now.AddHours(-2) >= order.OrderDate)
                        {
                            order.OrderStatus = (int)Status.OrderStatus.DENIED;
                            await _orderRepository.UpdateAsync(order);
                        }
                    }
                }
            }
        }
    }
}
