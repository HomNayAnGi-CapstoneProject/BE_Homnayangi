using BE_Homnayangi.Modules.CustomerBadgeModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CustomerBadgeModule
{
    public class CustomerBadgeRepository : Repository<CustomerBadge>, ICustomerBadgeRepository
    {

        private readonly HomnayangiContext _db;

        public CustomerBadgeRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ICollection<CustomerBadge>> GetCustomersBy(
            Expression<Func<CustomerBadge, bool>> filter = null,
            Func<IQueryable<CustomerBadge>, ICollection<CustomerBadge>> options = null,
            string includeProperties = null)
        {
            IQueryable<CustomerBadge> query = DbSet;

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

