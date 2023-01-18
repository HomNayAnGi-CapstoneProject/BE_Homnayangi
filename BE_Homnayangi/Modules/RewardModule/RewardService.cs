using System;
using BE_Homnayangi.Modules.RewardModule.Interface;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.PagedList;
using Library.Models.Enum;
using BE_Homnayangi.Modules.RewardModule.DTO.Request;

namespace BE_Homnayangi.Modules.RewardModule
{
    public class RewardService : IRewardService
    {
        private readonly IRewardRepository _rewardRepository;

        public RewardService(IRewardRepository rewardRepository)
        {
            _rewardRepository = rewardRepository;
        }

        public async Task<ICollection<Reward>> GetAll()
        {
            return await _rewardRepository.GetAll();
        }

        public Task<ICollection<Reward>> GetRewardsBy(
                Expression<Func<Reward,
                bool>> filter = null,
                Func<IQueryable<Reward>,
                ICollection<Reward>> options = null,
                string includeProperties = null)
        {
            return _rewardRepository.GetRewardsBy(filter);
        }

        public Task<ICollection<Reward>> GetRandomRewardsBy(
                Expression<Func<Reward, bool>> filter = null,
                Func<IQueryable<Reward>, ICollection<Reward>> options = null,
                string includeProperties = null,
                int numberItem = 0)
        {
            return _rewardRepository.GetNItemRandom(filter, numberItem: numberItem);
        }

        public async Task AddNewReward(Reward newReward)
        {
            newReward.RewardId = Guid.NewGuid();
            await _rewardRepository.AddAsync(newReward);
        }

        public async Task UpdateReward(Reward RewardUpdate)
        {
            await _rewardRepository.UpdateAsync(RewardUpdate);
        }

        public async Task<Reward> GetRewardByID(Guid? id)
        {
            return await _rewardRepository.GetFirstOrDefaultAsync(x => x.RewardId.Equals(id));
        }

        public async Task<PagedResponse<PagedList<Reward>>> GetAllPaged(RewardFilterRequest request)
        {
            try
            {
                var pageSize = request.PageSize;
                var pageNumber = request.PageNumber;
                var sort = request.sort;
                var sortDesc = request.sortDesc;
                var conditionType = request.conditionType;
                var fromDate = request.fromDate;
                var toDate = request.toDate;

                var rewards = await _rewardRepository.GetRewardsBy(r => r.Status.Value && r.ConditionType.Equals(conditionType));

                if (fromDate.HasValue && toDate.HasValue)
                {
                    rewards = rewards.Where(r => r.CreateDate.Value >= fromDate.Value && r.CreateDate.Value <= toDate.Value).ToList();
                }

                switch (sort)
                {
                    case (int) Sort.RewardsSortBy.CREATEDDATE:
                        rewards = sortDesc == true
                            ? rewards.OrderByDescending(r => r.CreateDate).ToList()
                            : rewards.OrderBy(r => r.CreateDate).ToList();
                        break;
                    case (int) Sort.RewardsSortBy.NAME:
                        rewards = sortDesc == true
                            ? rewards.OrderByDescending(r => r.Name).ToList()
                            : rewards.OrderBy(r => r.Name).ToList();
                        break;
                    case (int) Sort.RewardsSortBy.CONDITION_VALUE:
                        rewards = sortDesc == true
                            ? rewards.OrderByDescending(r => r.ConditionValue).ToList()
                            : rewards.OrderBy(r => r.ConditionValue).ToList();
                        break;
                    default:
                        rewards = sortDesc == true
                            ? rewards.OrderByDescending(r => r.CreateDate).ToList()
                            : rewards.OrderBy(r => r.CreateDate).ToList();
                        break;
                }

                var res = PagedList<Reward>.ToPagedList(source: rewards, pageSize: pageSize, pageNumber: pageNumber);
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
                var res = await _rewardRepository.GetRewardsBy(r => r.Name.Trim().ToUpper().Equals(name.Trim().ToUpper()));
                return res.Count > 0;
            }
            catch
            {
                throw new Exception("Name existed");
            }
        }
    }
}

