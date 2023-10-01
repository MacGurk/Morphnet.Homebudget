using HomeBudget.API.Models.UserModels;

namespace HomeBudget.API.Models.Authentication;

public record AuthResponse
{
    public UserDto User { get; init; }
    public string Token { get; init; }
}