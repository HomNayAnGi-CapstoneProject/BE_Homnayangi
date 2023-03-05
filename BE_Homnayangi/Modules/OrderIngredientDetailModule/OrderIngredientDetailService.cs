using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.OrderIngredientDetailModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Models;
using Library.Models.Constant;
using Library.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderIngredientDetailModule
{
    public class OrderIngredientDetailService : IOrderIngredientDetailService
    {
        private readonly IOrderIngredientDetailRepository _OrderIngredientDetailRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;

        public OrderIngredientDetailService(IOrderIngredientDetailRepository OrderIngredientDetailRepository,
            ICustomerRepository customerRepository,
            IUserRepository userRepository)
        {
            _OrderIngredientDetailRepository = OrderIngredientDetailRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        public async Task<ICollection<OrderIngredientDetail>> GetAll()
        {
            return await _OrderIngredientDetailRepository.GetAll();
        }

        public Task<ICollection<OrderIngredientDetail>> GetOrderIngredientDetailsBy(
                Expression<Func<OrderIngredientDetail,
                bool>> filter = null,
                Func<IQueryable<OrderIngredientDetail>,
                ICollection<OrderIngredientDetail>> options = null,
                string includeProperties = null)
        {
            return _OrderIngredientDetailRepository.GetOrderIngredientDetailsBy(filter, options, includeProperties);
        }

        public Task<ICollection<OrderIngredientDetail>> GetRandomOrderIngredientDetailsBy(
                Expression<Func<OrderIngredientDetail, bool>> filter = null,
                Func<IQueryable<OrderIngredientDetail>, ICollection<OrderIngredientDetail>> options = null,
                string includeProperties = null,
                int numberItem = 0)
        {
            return _OrderIngredientDetailRepository.GetNItemRandom(filter, numberItem: numberItem);
        }

        public async Task AddNewOrderIngredientDetail(OrderIngredientDetail newOrderIngredientDetail)
        {
            await _OrderIngredientDetailRepository.AddAsync(newOrderIngredientDetail);
        }

        public async Task UpdateOrderIngredientDetail(OrderIngredientDetail OrderIngredientDetailUpdate)
        {
            await _OrderIngredientDetailRepository.UpdateAsync(OrderIngredientDetailUpdate);
        }

        public OrderIngredientDetail GetOrderIngredientDetailByID(Guid? id)
        {
            return _OrderIngredientDetailRepository.GetFirstOrDefaultAsync(x => x.OrderId.Equals(id)).Result;
        }

        // OFF: OrderIngredientDetail, OrderIngredientDetailDetails
        public async Task DeleteOrderIngredientDetail(Guid id)
        {
        }

    }
}