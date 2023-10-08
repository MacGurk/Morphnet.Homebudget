using HomeBudget.API.Models.UserModels;

namespace HomeBudget.API.Models.Authentication;

public record AuthResponse
{
    public UserDto User { get; init; } = new();
    public string Token { get; init; } = string.Empty;
}