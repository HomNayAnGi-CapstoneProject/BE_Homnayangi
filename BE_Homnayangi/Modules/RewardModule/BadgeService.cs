using System;
using BE_Homnayangi.Modules.BadgeModule.Interface;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.PagedList;
using Library.Models.Enum;
using BE_Homnayangi.Modules.BadgeModule.DTO.Request;

namespace BE_Homnayangi.Modules.BadgeModule
{
    public class BadgeService : IBadgeService
    {
        private readonly IBadgeRepository _rewardRepository;

        public BadgeService(IBadgeRepository rewardRepository)
        {
            _rewardRepository = rewardRepository;
        }

        public async Task<ICollection<Badge>> GetAll()
        {
            return await _rewardRepository.GetAll();
        }

        public Task<ICollection<Badge>> GetBadgesBy(
                Expression<Func<Badge,
                bool>> filter = null,
                Func<IQueryable<Badge>,
                ICollection<Badge>> options = null,
                string includeProperties = null)
        {
            return _rewardRepository.GetBadgesBy(filter);
        }

        public Task<ICollection<Badge>> GetRandomBadgesBy(
                Expression<Func<Badge, bool>> filter = null,
                Func<IQueryable<Badge>, ICollection<Badge>> options = null,
                string includeProperties = null,
                int numberItem = 0)
        {
            return _rewardRepository.GetNItemRandom(filter, numberItem: numberItem);
        }

        public async Task AddNewBadge(Badge newBadge)
        {
            newBadge.BadgeId = Guid.NewGuid();
            await _rewardRepository.AddAsync(newBadge);
        }

        public async Task UpdateBadge(Badge BadgeUpdate)
        {
            await _rewardRepository.UpdateAsync(BadgeUpdate);
        }

        public async Task<Badge> GetBadgeByID(Guid? id)
        {
            return await _rewardRepository.GetFirstOrDefaultAsync(x => x.BadgeId.Equals(id));
        }

        public async Task<PagedResponse<PagedList<Badge>>> GetAllPaged(BadgeFilterRequest request)
        {
            try
            {
                var pageSize = request.PageSize;
                var pageNumber = request.PageNumber;
                var sort = request.sort;
                var sortDesc = request.sortDesc;
                var fromDate = request.fromDate;
                var toDate = request.toDate;

                var rewards = await _rewardRepository.GetBadgesBy(r => r.Status == 1);

                if (fromDate.HasValue && toDate.HasValue)
                {
                    rewards = rewards.Where(r => r.CreateDate.Value >= fromDate.Value && r.CreateDate.Value <= toDate.Value).ToList();
                }

                switch (sort)
                {
                    case (int) Sort.BadgesSortBy.CREATEDDATE:
                        rewards = sortDesc == true
                            ? rewards.OrderByDescending(r => r.CreateDate).ToList()
                            : rewards.OrderBy(r => r.CreateDate).ToList();
                        break;
                    case (int) Sort.BadgesSortBy.NAME:
                        rewards = sortDesc == true
                            ? rewards.OrderByDescending(r => r.Name).ToList()
                            : rewards.OrderBy(r => r.Name).ToList();
                        break;
                    default:
                        rewards = sortDesc == true
                            ? rewards.OrderByDescending(r => r.CreateDate).ToList()
                            : rewards.OrderBy(r => r.CreateDate).ToList();
                        break;
                }

                var res = PagedList<Badge>.ToPagedList(source: rewards, pageSize: pageSize, pageNumber: pageNumber);
                return res.ToPagedResponse();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public async Task<bool> CheckExistedName(string name)
        {
            try
            {
                var res = await _rewardRepository.GetBadgesBy(r => r.Name.Trim().ToUpper().Equals(name.Trim().ToUpper()));
                return res.Count > 0;
            }
            catch
            {
                throw new Exception("Name existed");
            }
        }
    }
}

