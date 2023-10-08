using HomeBudget.API.DbContexts;
using HomeBudget.API.Entities;
using HomeBudget.API.Services.Utils;
using Microsoft.EntityFrameworkCore;

namespace HomeBudget.API.Services.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly HomeBudgetContext context;

        public TransactionRepository(HomeBudgetContext context)
        {
            this.context = context;
        }


        public async Task<IEnumerable<Transaction>> GetTransactionsAsync() => await context.Transactions.ToListAsync();

        public async Task<(IEnumerable<Transaction>, PaginationMetadata)> GetTransactionsAsync(string? searchQuery,
            int? month, int? year, int pageNumber, int pageSize)
        {
            var collection = context.Transactions as IQueryable<Transaction>;

            if (!string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(x =>
                    x.Description.Contains(searchQuery) || x.User.Name.Contains(searchQuery));
            }

            if (month.HasValue && year.HasValue)
            {
                collection = collection.Where(x => x.Date.Year == year.Value && x.Date.Month == month.Value);
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .Include(x => x.User)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int transactionId) =>
            await context.Transactions.Include(x => x.User).FirstOrDefaultAsync(t => t.Id == transactionId);

        public async Task<IEnumerable<Transaction>> GetTransactionsByUserAsync(int userId) =>
            await context.Transactions.Where(t => t.User.Id == userId).ToListAsync();

        public async Task<IEnumerable<Transaction>> GetUnsettledTransactionsAsync() =>
            await context.Transactions
                .Where(t => t.IsSettled == false)
                .Include(t => t.User)
                .ToListAsync();

        public void AddTransaction(Transaction transaction)
        {
            context.Transactions.Add(transaction);
        }

        public void DeleteTransaction(Transaction transaction)
        {
            context.Transactions.Remove(transaction);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByIdAsync(List<int> transactionIds)
        {
            return await context.Transactions.Where(t => transactionIds.Contains(t.Id)).ToListAsync();
        }

        public async Task SaveChangedAsync() => await context.SaveChangesAsync();
    }
}