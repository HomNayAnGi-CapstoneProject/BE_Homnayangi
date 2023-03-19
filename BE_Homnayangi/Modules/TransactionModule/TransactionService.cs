using BE_Homnayangi.Modules.CustomerModule.Interface;
using BE_Homnayangi.Modules.OrderModule.Interface;
using BE_Homnayangi.Modules.TransactionModule.Interface;
using BE_Homnayangi.Modules.UserModule.Interface;
using Library.Models;
using Library.Models.Constant;
using Library.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BE_Homnayangi.Modules.TransactionModule
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _TransactionRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;

        private readonly IOrderService _orderService;

        public TransactionService(ITransactionRepository TransactionRepository,
            ICustomerRepository customerRepository,
            IUserRepository userRepository,
            IOrderService orderService)
        {
            _TransactionRepository = TransactionRepository;
            _customerRepository = customerRepository;
            _userRepository = userRepository;
            _orderService = orderService;
        }

        public async Task<ICollection<Transaction>> GetAll()
        {
            return await _TransactionRepository.GetAll();
        }

        public Task<ICollection<Transaction>> GetTransactionsBy(
                Expression<Func<Transaction,
                bool>> filter = null,
                Func<IQueryable<Transaction>,
                ICollection<Transaction>> options = null,
                string includeProperties = null)
        {
            return _TransactionRepository.GetTransactionsBy(filter, options, includeProperties);
        }

        public Task<ICollection<Transaction>> GetRandomTransactionsBy(
                Expression<Func<Transaction, bool>> filter = null,
                Func<IQueryable<Transaction>, ICollection<Transaction>> options = null,
                string includeProperties = null,
                int numberItem = 0)
        {
            return _TransactionRepository.GetNItemRandom(filter, numberItem: numberItem);
        }

        public async Task AddNewTransaction(Transaction newTransaction)
        {
            
            await _TransactionRepository.AddAsync(newTransaction);
        }

        public async Task UpdateTransaction(Transaction TransactionUpdate)
        {
            await _TransactionRepository.UpdateAsync(TransactionUpdate);
        }

        public Transaction GetTransactionByID(Guid? id)
        {
            return _TransactionRepository.GetFirstOrDefaultAsync(x => x.TransactionId.Equals(id)).Result;
        }

        // OFF: Transaction, TransactionDetails
        public async Task DeleteTransaction(Guid id)
        {
            var transaction = await _TransactionRepository.GetByIdAsync(id);

            if (transaction == null)
                throw new Exception(ErrorMessage.TransactionError.TRANSACTION_NOT_FOUND);

            transaction.TransactionStatus = (int)Status.TransactionStatus.DELETED;
            await _TransactionRepository.UpdateAsync(transaction);
        }
    }
}