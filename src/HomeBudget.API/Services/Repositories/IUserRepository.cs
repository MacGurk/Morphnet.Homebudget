using HomeBudget.API.Entities;

namespace HomeBudget.API.Services.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync(bool isContributorFilter = false);

        Task<User?> GetUserByIdAsync(int userId);

        Task AddUserAsync(User user, string password);

        Task DeleteUserAsync(User user);

        Task<bool> UserExistsAsync(int userId);

        Task SaveChangesAsync();
    }
}