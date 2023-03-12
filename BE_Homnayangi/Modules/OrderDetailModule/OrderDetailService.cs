using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.OrderDetailModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Models;
using Library.Models.Constant;
using Library.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderDetailModule
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _OrderDetailRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;

        public OrderDetailService(IOrderDetailRepository OrderDetailRepository,
            ICustomerRepository customerRepository,
            IUserRepository userRepository)
        {
            _OrderDetailRepository = OrderDetailRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        public async Task<ICollection<OrderDetail>> GetAll()
        {
            return await _OrderDetailRepository.GetAll();
        }

        public Task<ICollection<OrderDetail>> GetOrderDetailsBy(
                Expression<Func<OrderDetail,
                bool>> filter = null,
                Func<IQueryable<OrderDetail>,
                ICollection<OrderDetail>> options = null,
                string includeProperties = null)
        {
            return _OrderDetailRepository.GetOrderDetailsBy(filter, options, includeProperties);
        }

        public Task<ICollection<OrderDetail>> GetRandomOrderDetailsBy(
                Expression<Func<OrderDetail, bool>> filter = null,
                Func<IQueryable<OrderDetail>, ICollection<OrderDetail>> options = null,
                string includeProperties = null,
                int numberItem = 0)
        {
            return _OrderDetailRepository.GetNItemRandom(filter, numberItem: numberItem);
        }

        public async Task AddNewOrderDetail(OrderDetail newOrderDetail)
        {
            await _OrderDetailRepository.AddAsync(newOrderDetail);
        }

        public async Task UpdateOrderDetail(OrderDetail OrderDetailUpdate)
        {
            await _OrderDetailRepository.UpdateAsync(OrderDetailUpdate);
        }

        public OrderDetail GetOrderDetailByID(Guid? id)
        {
            return _OrderDetailRepository.GetFirstOrDefaultAsync(x => x.OrderId.Equals(id)).Result;
        }

        // OFF: OrderDetail, OrderDetailDetails
        public async Task DeleteOrderDetail(Guid id)
        {
        }

    }
}