

using HomeBudget.API.Entities;

namespace HomeBudget.API.Services
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsAsync();
        Task<(IEnumerable<Transaction>, PaginationMetadata)> GetTransactionsAsync(string? searchQuery, int? month, int? year, int pageNumber, int pageSize);

        Task<Transaction?> GetTransactionByIdAsync(int transactionId);

        Task<IEnumerable<Transaction>> GetTransactionsByUserAsync(int userId);

        Task AddTransactionAsync(Transaction transaction);

        Task DeleteTransactionAsync(Transaction transaction);
        Task SaveChangedAsync();
    }
}
