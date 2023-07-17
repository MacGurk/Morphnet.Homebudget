using HomeBudget.API.Entities;
using HomeBudget.API.Services.Utils;

namespace HomeBudget.API.Services.Repositories
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync();

        Task<(IEnumerable<Transaction>, PaginationMetadata)> GetTransactionsAsync(string? searchQuery, int? month,
            int? year, int pageNumber, int pageSize);

        Task<Transaction?> GetTransactionByIdAsync(int transactionId);

        Task<IEnumerable<Transaction>> GetTransactionsByUserAsync(int userId);
        
        Task<IEnumerable<Transaction>> GetUnsettledTransactionsAsync();

        Task AddTransactionAsync(Transaction transaction);

        Task DeleteTransactionAsync(Transaction transaction);
        Task SaveChangedAsync();
        Task<IEnumerable<Transaction>> GetTransactionsByIdAsync(List<int> transactionIds);
    }
}