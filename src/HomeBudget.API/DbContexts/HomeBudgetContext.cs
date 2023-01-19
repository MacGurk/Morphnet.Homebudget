using HomeBudget.API.Entities;
using HomeBudget.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeBudget.API.DbContexts
{
    public class HomeBudgetContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;


        public HomeBudgetContext(DbContextOptions<HomeBudgetContext> options) : base(options)
        {
        }

    }
}
