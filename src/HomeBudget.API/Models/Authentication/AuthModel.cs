namespace HomeBudget.API.Models.Authentication;

public record AuthModel
{
    public string Username { get; set; }
    public string Password { get; set; }
}