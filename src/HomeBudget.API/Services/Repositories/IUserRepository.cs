using HomeBudget.API.Entities;

namespace HomeBudget.API.Services.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync(bool isContributorFilter = false);

        Task<User?> GetUserByIdAsync(int userId);

        void AddUser(User user, string password);

        void DeleteUser(User user);

        Task<bool> UserExistsAsync(int userId);

        Task SaveChangesAsync();
    }
}