namespace HomeBudget.API.Services.Utils;

public class AuthOptions
{
    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;
    
    public string Secret { get; set; } = string.Empty;
}