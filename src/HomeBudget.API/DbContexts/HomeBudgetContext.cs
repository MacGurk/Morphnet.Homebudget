using HomeBudget.API.Entities;
using HomeBudget.API.Services.Utils;
using Microsoft.EntityFrameworkCore;

namespace HomeBudget.API.DbContexts
{
    public class HomeBudgetContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Transaction> Transactions { get; set; } = null!;


        public HomeBudgetContext(DbContextOptions<HomeBudgetContext> options) :
            base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "admin",
                    Email = "",
                    PasswordHash = new byte[] { 72, 191, 220, 112, 232, 12, 207, 95, 217, 110, 190, 186, 33, 153, 226, 171 },
                    PasswordSalt = new byte[] { 23, 221, 35, 137, 84, 205, 181, 136, 212, 93, 228, 146, 152, 177, 82, 3 },
                    IsContributor = false
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}