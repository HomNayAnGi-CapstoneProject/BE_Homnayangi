using System;
using BE_Homnayangi.Modules.RewardModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.RewardModule
{
    public class RewardRepository : Repository<Reward>, IRewardRepository
    {
        private readonly HomnayangiContext _db;

        public RewardRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ICollection<Reward>> GetRewardsBy(Expression<Func<Reward, bool>> filter = null, Func<IQueryable<Reward>, ICollection<Reward>> options = null, string includeProperties = null)

        {
            IQueryable<Reward> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return options != null ? options(query).ToList() : await query.ToListAsync();
        }
    }
}

