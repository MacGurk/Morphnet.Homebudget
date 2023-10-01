using HomeBudget.API.DbContexts;
using HomeBudget.API.Entities;
using HomeBudget.API.Services.Utils;
using Microsoft.EntityFrameworkCore;

namespace HomeBudget.API.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HomeBudgetContext context;

        public UserRepository(HomeBudgetContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(bool isContributorFilter = false)
        {
            var collection = context.Users as IQueryable<User>;
            if (isContributorFilter)
            {
                collection = collection.Where(u => u.IsContributor);
            }
            return await collection.ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int userId) =>
            await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        public void AddUser(User user, string password)
        {
            var salt = Argon2Hasher.GenerateSalt();
            var passwordHash = Argon2Hasher.GenerateHash(password, salt);
            user.PasswordSalt = salt;
            user.PasswordHash = passwordHash;
            context.Users.Add(user);
        }

        public void DeleteUser(User user)
        {
            context.Users.Remove(user);
        }

        public async Task<bool> UserExistsAsync(int userId) => await context.Users.AnyAsync(u => u.Id == userId);

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();

        

        
    }
}