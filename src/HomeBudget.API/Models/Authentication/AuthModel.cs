namespace HomeBudget.API.Models.Authentication;

public record AuthModel
{
    public string Login { get; set; }
    public string Password { get; set; }
}