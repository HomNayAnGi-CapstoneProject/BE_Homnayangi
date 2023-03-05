using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.TransactionModule.Interface
{
    public interface ITransactionService
    {
        public Task AddNewTransaction(Transaction newTransaction);

        public Task UpdateTransaction(Transaction TransactionUpdate);

        public Task<ICollection<Transaction>> GetAll();

        public Task<ICollection<Transaction>> GetTransactionsBy(
            Expression<Func<Transaction, bool>> filter = null,
            Func<IQueryable<Transaction>, ICollection<Transaction>> options = null,
            string includeProperties = null);

        public Task<ICollection<Transaction>> GetRandomTransactionsBy(Expression<Func<Transaction, bool>> filter = null,
            Func<IQueryable<Transaction>, ICollection<Transaction>> options = null,
            string includeProperties = null,
            int numberItem = 0);

        public Transaction GetTransactionByID(Guid? id);

        public Task DeleteTransaction(Guid id);

        public Task CompleteTransaction(Guid id);

        public Task DenyTransaction(Guid id);
    }
}
