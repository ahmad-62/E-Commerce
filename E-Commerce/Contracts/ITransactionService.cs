
using E_Commerce.Models;

namespace E_Commerce.Contracts
{
    public interface ITransactionService
    {
        Task<Transaction> Add(Transaction transaction);
    }
}
