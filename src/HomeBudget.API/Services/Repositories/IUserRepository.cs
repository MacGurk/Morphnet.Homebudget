using HomeBudget.API.Entities;
using HomeBudget.API.Models.Authentication;

namespace HomeBudget.API.Services.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();

        Task<User?> GetUserByIdAsync(int userId);

        Task AddUserAsync(User user, string password);

        Task DeleteUserAsync(User user);

        Task<bool> UserExistsAsync(int userId);

        Task SaveChangesAsync();
        
        Task<(User?, string?)> Authenticate(string login, string password);
    }
}