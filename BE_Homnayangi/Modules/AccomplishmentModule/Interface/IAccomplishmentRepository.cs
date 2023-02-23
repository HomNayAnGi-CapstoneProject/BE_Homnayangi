using Library.Models;
using Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AccomplishmentModule.Interface
{
    public interface IAccomplishmentRepository : IRepository<Accomplishment>
    {
        public Task<ICollection<Accomplishment>> GetAccomplishmentsBy(
            Expression<Func<Accomplishment, bool>> filter = null,
            Func<IQueryable<Accomplishment>, ICollection<Accomplishment>> options = null,
            string includeProperties = null
        );
    }
}
