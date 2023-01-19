using HomeBudget.API.DbContexts;
using HomeBudget.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeBudget.API.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly HomeBudgetContext _context;
        
        public UserRepository(HomeBudgetContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<User>> GetUsersAsync() => await _context.Users.ToListAsync();

        public async Task<User?> GetUserByIdAsync(int userId) => await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        
        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(int userId) => await _context.Users.AnyAsync(u => u.Id == userId);
        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
