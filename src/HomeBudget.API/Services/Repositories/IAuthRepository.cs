using HomeBudget.API.Entities;

namespace HomeBudget.API.Services.Repositories;

public interface IAuthRepository
{
    Task<(User?, string?)> Authenticate(string login, string password);
}