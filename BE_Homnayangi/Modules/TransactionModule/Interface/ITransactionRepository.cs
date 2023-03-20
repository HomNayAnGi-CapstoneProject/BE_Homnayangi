using System;
using Library.Models;
using Repository.Repository.Interface;﻿
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.TransactionModule.Interface
{
	public interface ITransactionRepository : IRepository<Transaction>
	{
        public Task<ICollection<Transaction>> GetTransactionsBy(
               Expression<Func<Transaction, bool>> filter = null,
               Func<IQueryable<Transaction>, ICollection<Transaction>> options = null,
               string includeProperties = null
           );
    }
}
