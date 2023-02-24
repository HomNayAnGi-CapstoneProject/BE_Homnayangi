using BE_Homnayangi.Modules.AccomplishmentModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.AccomplishmentModule
{
    public class AccomplishmentRepository : Repository<Accomplishment>, IAccomplishmentRepository
    {
        private readonly HomnayangiContext _db;

        public AccomplishmentRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ICollection<Accomplishment>> GetAccomplishmentsBy(
            Expression<Func<Accomplishment,
            bool>> filter = null,
            Func<IQueryable<Accomplishment>,
                ICollection<Accomplishment>> options = null,
            string includeProperties = null
            )
        {
            IQueryable<Accomplishment> query = DbSet;

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
