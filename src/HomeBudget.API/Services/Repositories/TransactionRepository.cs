﻿using HomeBudget.API.DbContexts;
using HomeBudget.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeBudget.API.Services
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly HomeBudgetContext _context;

        public TransactionRepository(HomeBudgetContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Transaction>> GetTransactionsAsync() => await _context.Transactions.ToListAsync();

        public async Task<(IEnumerable<Transaction>, PaginationMetadata)> GetTransactionsAsync(string? searchQuery,
            int? month, int? year, int pageNumber, int pageSize)
        {
            var collection = _context.Transactions as IQueryable<Transaction>;

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
            await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId);

        public async Task<IEnumerable<Transaction>> GetTransactionsByUserAsync(int userId) =>
            await _context.Transactions.Where(t => t.User.Id == userId).ToListAsync();

        public async Task AddTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTransactionAsync(Transaction transaction)
        {
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangedAsync() => await _context.SaveChangesAsync();
    }
}