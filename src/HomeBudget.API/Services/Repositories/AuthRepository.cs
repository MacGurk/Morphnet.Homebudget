using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HomeBudget.API.DbContexts;
using HomeBudget.API.Entities;
using HomeBudget.API.Services.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HomeBudget.API.Services.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly HomeBudgetContext context;
    private readonly AuthOptions authOptions;
    
    public AuthRepository(HomeBudgetContext context, IOptions<AuthOptions> authOptions)
    {
        this.context = context;
        this.authOptions = authOptions.Value;
    }
    
    public async Task<(User?, string?)> Authenticate(string login, string password)
    {
        var user = await context.Users.SingleOrDefaultAsync(u => u.Name == login);
        if (user is null || !Argon2Hasher.ValidateHash(password, user.PasswordHash, user.PasswordSalt))
        {
            return (null, null);
        }

        var token = GenerateJwtToken(user);

        return (user, token);
    }
    
    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(authOptions.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = authOptions.Issuer,
            Audience = authOptions.Audience,
            Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }   
}