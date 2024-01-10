using HomeBudget.API.Entities;
using HomeBudget.API.Services.Utils;

namespace HomeBudget.API.Services.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync();

        Task<IEnumerable<Transaction>> GetTransactionsAsync(string? searchQuery, int? month, int? year);

        Task<Transaction?> GetTransactionByIdAsync(int transactionId);

        Task<IEnumerable<Transaction>> GetTransactionsByUserAsync(int userId);
        
        Task<IEnumerable<Transaction>> GetUnsettledTransactionsAsync();

        void AddTransaction(Transaction transaction);

        void DeleteTransaction(Transaction transaction);
        Task SaveChangedAsync();
        Task<IEnumerable<Transaction>> GetTransactionsByIdAsync(List<int> transactionIds);
    }
}