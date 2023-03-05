using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.OrderPackageDetailModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Models;
using Library.Models.Constant;
using Library.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.OrderPackageDetailModule
{
    public class OrderPackageDetailService : IOrderPackageDetailService
    {
        private readonly IOrderPackageDetailRepository _OrderPackageDetailRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;

        public OrderPackageDetailService(IOrderPackageDetailRepository OrderPackageDetailRepository,
            ICustomerRepository customerRepository,
            IUserRepository userRepository)
        {
            _OrderPackageDetailRepository = OrderPackageDetailRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        public async Task<ICollection<OrderPackageDetail>> GetAll()
        {
            return await _OrderPackageDetailRepository.GetAll();
        }

        public Task<ICollection<OrderPackageDetail>> GetOrderPackageDetailsBy(
                Expression<Func<OrderPackageDetail,
                bool>> filter = null,
                Func<IQueryable<OrderPackageDetail>,
                ICollection<OrderPackageDetail>> options = null,
                string includeProperties = null)
        {
            return _OrderPackageDetailRepository.GetOrderPackageDetailsBy(filter,options,includeProperties);
        }

        public Task<ICollection<OrderPackageDetail>> GetRandomOrderPackageDetailsBy(
                Expression<Func<OrderPackageDetail, bool>> filter = null,
                Func<IQueryable<OrderPackageDetail>, ICollection<OrderPackageDetail>> options = null,
                string includeProperties = null,
                int numberItem = 0)
        {
            return _OrderPackageDetailRepository.GetNItemRandom(filter, numberItem: numberItem);
        }

        public async Task AddNewOrderPackageDetail(OrderPackageDetail newOrderPackageDetail)
        {
            
            await _OrderPackageDetailRepository.AddAsync(newOrderPackageDetail);
        }

        public async Task UpdateOrderPackageDetail(OrderPackageDetail OrderPackageDetailUpdate)
        {
            await _OrderPackageDetailRepository.UpdateAsync(OrderPackageDetailUpdate);
        }

        public OrderPackageDetail GetOrderPackageDetailByID(Guid? id)
        {
            return _OrderPackageDetailRepository.GetFirstOrDefaultAsync(x => x.OrderId.Equals(id)).Result;
        }

        // OFF: OrderPackageDetail, OrderPackageDetailDetails
        public async Task DeleteOrderPackageDetail(Guid id)
        {

        }

    }
}