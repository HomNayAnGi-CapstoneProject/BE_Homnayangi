using AutoMapper;
using BE_Homnayangi.Modules.CustomerBadgeModule.DTO;
using BE_Homnayangi.Modules.CustomerBadgeModule.Interface;
using Library.Models;
using Library.Models.Enum;
using Library.PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CustomerBadgeModule
{

    public class CustomerBadgeService : ICustomerBadgeService
    {
        private readonly ICustomerBadgeRepository _customerBadgeRepository;
        private readonly IMapper _mapper;

        public CustomerBadgeService(ICustomerBadgeRepository customerBadgeRepository, IMapper mapper)
        {
            _customerBadgeRepository = customerBadgeRepository;
            _mapper = mapper;
        }
        public async Task AddNewCustomerBadge(CustomerBadge newCustomerBadge)
        {
            newCustomerBadge.CreatedDate = DateTime.Now;
            await _customerBadgeRepository.AddAsync(newCustomerBadge);
        }

        public async Task<bool> DeleteCustomerBadgeByCombineId(Guid cusId, Guid badgeId)
        {
            bool isDeleted = false;
            try
            {
                var list = await _customerBadgeRepository.GetCustomerBadgeBy(x => x.CustomerId == cusId && x.BadgeId == badgeId);
                if (list.Count > 0)
                {
                    foreach (var cb in list)
                    {
                        await _customerBadgeRepository.RemoveAsync(cb);
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

        public async Task<ICollection<CustomerBadge>> GetAll()
        {
            return await _customerBadgeRepository.GetAll();
        }

        public async Task<PagedResponse<PagedList<CustomerBadge>>> GetAllPaged(CustomerBadgeFilterRequest request)
        {
            try
            {
                var pageSize = request.PageSize;
                var pageNumber = request.PageNumber;
                var sort = request.sort;
                var sortDesc = request.sortDesc;

                var customerBadges = await _customerBadgeRepository.GetAll();

                var res = PagedList<CustomerBadge>.ToPagedList(source: customerBadges, pageSize: pageSize, pageNumber: pageNumber);
                return res.ToPagedResponse();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ICollection<CustomerBadge>> GetBadgeByCusID(Guid? cusId)
        {

            try
            {
                var list = await _customerBadgeRepository.GetCustomerBadgeBy(x => x.CustomerId == cusId, includeProperties: "Customer,Badge");
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetCustomerBadgeByCombineID: " + ex.Message);
                throw;
            }

        }

        public Task<CustomerBadge> GetBadgeByID(Guid? id)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerBadge> GetCustomerBadgeByCombineID(Guid cusId, Guid badgeId)
        {
            CustomerBadge result = null;
            try
            {
                var tmp = await _customerBadgeRepository.GetFirstOrDefaultAsync(x => x.CustomerId == cusId && x.BadgeId == badgeId, includeProperties: "Customer,Badge");
                if (tmp != null)
                {
                    result = tmp;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error at GetCustomerBadgeByCombineID: " + ex.Message);
                throw;
            }
            return result;
        }

        public Task<ICollection<CustomerBadge>> GetCustomerBadgesBy(Expression<Func<CustomerBadge, bool>> filter = null, Func<IQueryable<CustomerBadge>, ICollection<CustomerBadge>> options = null, string includeProperties = null)
        {
            return _customerBadgeRepository.GetCustomerBadgeBy(filter);
        }


        public async Task UpdateCustomerBadge(CustomerBadge customerBadgeUpdate)
        {
            await _customerBadgeRepository.UpdateAsync(customerBadgeUpdate);
        }
    }
}
