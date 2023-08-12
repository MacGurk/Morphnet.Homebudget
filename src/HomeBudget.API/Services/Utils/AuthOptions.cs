namespace HomeBudget.API.Services.Utils;

public class AuthOptions
{
    public string Issuer { get; set; }
    
    public string Audience { get; set; }
    
    public string Secret { get; set; }
}