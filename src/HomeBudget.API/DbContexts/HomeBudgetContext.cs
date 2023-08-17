using HomeBudget.API.Entities;
using HomeBudget.API.Services.Utils;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var salt = Argon2Hasher.GenerateSalt();
            var passwordHash = Argon2Hasher.GenerateHash("admin", salt);
            
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "admin",
                    Email = "",
                    PasswordHash = passwordHash,
                    PasswordSalt = salt,
                    IsContributor = false
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}