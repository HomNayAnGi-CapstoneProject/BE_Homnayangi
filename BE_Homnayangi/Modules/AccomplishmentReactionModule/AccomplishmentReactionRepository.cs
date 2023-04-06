using BE_Homnayangi.Modules.AccomplishmentReactionModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AccomplishmentReactionModule
{
    public class AccomplishmentReactionRepository : Repository<AccomplishmentReaction>, IAccomplishmentReactionRepository
    {
        private readonly HomnayangiContext _db;

        public AccomplishmentReactionRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ICollection<AccomplishmentReaction>> GetAccomplishmentReactionsBy(
            Expression<Func<AccomplishmentReaction, bool>> filter = null,
            Func<IQueryable<AccomplishmentReaction>, ICollection<AccomplishmentReaction>> options = null,
            string includeProperties = null
        )
        {
            IQueryable<AccomplishmentReaction> query = DbSet;

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
