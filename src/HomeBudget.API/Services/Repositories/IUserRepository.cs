using HomeBudget.API.Entities;

namespace HomeBudget.API.Services
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<User?> GetUserByIdAsync(int userId);

        Task AddUserAsync(User user);

        Task DeleteUserAsync(User user);

        Task<bool> UserExistsAsync(int userId);
        
        Task SaveChangesAsync();
    }
}
