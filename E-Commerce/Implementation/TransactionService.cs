using E_Commerce.Contracts;
using E_Commerce.Contracts.Base;
using E_Commerce.Models;

namespace E_Commerce.Implementation
{
    public class TransactionService:ITransactionService
    {
        private readonly IBaseRepository<Transaction> repository;
        public TransactionService(IBaseRepository<Transaction> repository)
        {
            this.repository = repository;
        }

        public async Task<Transaction> Add(Transaction transaction)
        {
            try
            {
                return await repository.Add(transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
