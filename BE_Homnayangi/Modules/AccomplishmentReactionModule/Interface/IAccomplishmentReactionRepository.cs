using Library.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AccomplishmentReactionModule.Interface
{
    public interface IAccomplishmentReactionRepository : IRepository<AccomplishmentReaction>
    {
        public Task<ICollection<AccomplishmentReaction>> GetAccomplishmentReactionsBy(
           Expression<Func<AccomplishmentReaction, bool>> filter = null,
           Func<IQueryable<AccomplishmentReaction>, ICollection<AccomplishmentReaction>> options = null,
           string includeProperties = null
       );
    }
}
