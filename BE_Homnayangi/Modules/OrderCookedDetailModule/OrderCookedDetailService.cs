using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.OrderCookedDetailModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Models;
using Library.Models.Constant;
using Library.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderCookedDetailModule
{
    public class OrderCookedDetailService : IOrderCookedDetailService
    {
        private readonly IOrderCookedDetailRepository _OrderCookedDetailRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;

        public OrderCookedDetailService(IOrderCookedDetailRepository OrderCookedDetailRepository,
            ICustomerRepository customerRepository,
            IUserRepository userRepository)
        {
            _OrderCookedDetailRepository = OrderCookedDetailRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        public async Task<ICollection<OrderCookedDetail>> GetAll()
        {
            return await _OrderCookedDetailRepository.GetAll();
        }

        public Task<ICollection<OrderCookedDetail>> GetOrderCookedDetailsBy(
                Expression<Func<OrderCookedDetail,
                bool>> filter = null,
                Func<IQueryable<OrderCookedDetail>,
                ICollection<OrderCookedDetail>> options = null,
                string includeProperties = null)
        {
            return _OrderCookedDetailRepository.GetOrderCookedDetailsBy(filter,options,includeProperties);
        }

        public Task<ICollection<OrderCookedDetail>> GetRandomOrderCookedDetailsBy(
                Expression<Func<OrderCookedDetail, bool>> filter = null,
                Func<IQueryable<OrderCookedDetail>, ICollection<OrderCookedDetail>> options = null,
                string includeProperties = null,
                int numberItem = 0)
        {
            return _OrderCookedDetailRepository.GetNItemRandom(filter, numberItem: numberItem);
        }

        public async Task AddNewOrderCookedDetail(OrderCookedDetail newOrderCookedDetail)
        {
            await _OrderCookedDetailRepository.AddAsync(newOrderCookedDetail);
        }

        public async Task UpdateOrderCookedDetail(OrderCookedDetail OrderCookedDetailUpdate)
        {
            await _OrderCookedDetailRepository.UpdateAsync(OrderCookedDetailUpdate);
        }

        public OrderCookedDetail GetOrderCookedDetailByID(Guid? id)
        {
            return _OrderCookedDetailRepository.GetFirstOrDefaultAsync(x => x.OrderId.Equals(id)).Result;
        }

        // OFF: OrderCookedDetail, OrderCookedDetailDetails
        public async Task DeleteOrderCookedDetail(Guid id)
        {
        }

    }
}