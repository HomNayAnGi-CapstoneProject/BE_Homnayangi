using System;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Library.PagedList;
using BE_Homnayangi.Modules.RewardModule.DTO.Request;

namespace BE_Homnayangi.Modules.RewardModule.Interface
{
    public interface IRewardService
    {
        public Task AddNewReward(Reward newReward);

        public Task UpdateReward(Reward rewardUpdate);

        public Task<ICollection<Reward>> GetAll();

        public Task<ICollection<Reward>> GetRewardsBy(
            Expression<Func<Reward, bool>> filter = null,
            Func<IQueryable<Reward>, ICollection<Reward>> options = null,
            string includeProperties = null);

        public Task<ICollection<Reward>> GetRandomRewardsBy(Expression<Func<Reward, bool>> filter = null,
            Func<IQueryable<Reward>, ICollection<Reward>> options = null,
            string includeProperties = null,
            int numberItem = 0);

        public Task<Reward> GetRewardByID(Guid? id);

        public Task<PagedResponse<PagedList<Reward>>> GetAllPaged(RewardFilterRequest request);

        public Task<bool> CheckExistedName(string name);
    }
}

