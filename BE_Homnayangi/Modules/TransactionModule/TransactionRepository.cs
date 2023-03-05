    using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BE_Homnayangi.Modules.TransactionModule.Interface;
using Library.DataAccess;
using Library.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Repository;

namespace BE_Homnayangi.Modules.TransactionModule
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
	{
        private readonly HomnayangiContext _db;

        public TransactionRepository(HomnayangiContext db) : base(db)
		{
            _db = db;
		}

        public async Task<ICollection<Transaction>> GetTransactionsBy(Expression<Func<Transaction, bool>> filter = null, Func<IQueryable<Transaction>, ICollection<Transaction>> options = null, string includeProperties = null)

        {
            IQueryable<Transaction> query = DbSet;

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
