using BE_Homnayangi.Modules.CustomerModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.CustomerModule
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {

        private readonly HomnayangiContext _db;

        public CustomerRepository(HomnayangiContext db) : base(db)
        {
            _db = db;
        }

        public async Task<ICollection<Customer>> GetCustomersBy(
            Expression<Func<Customer, bool>> filter = null, 
            Func<IQueryable<Customer>, ICollection<Customer>> options = null, 
            string includeProperties = null)
        {
            IQueryable<Customer> query = DbSet;

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
