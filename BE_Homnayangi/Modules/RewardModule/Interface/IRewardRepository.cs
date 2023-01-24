using System;
using Library.Models;
using Repository.Repository.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.RewardModule.Interface
{
    public interface IRewardRepository : IRepository<Reward>
    {
        public Task<ICollection<Reward>> GetRewardsBy(
               Expression<Func<Reward, bool>> filter = null,
               Func<IQueryable<Reward>, ICollection<Reward>> options = null,
               string includeProperties = null
           );
    }
}

