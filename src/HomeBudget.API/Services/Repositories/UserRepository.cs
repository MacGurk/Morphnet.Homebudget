using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HomeBudget.API.DbContexts;
using HomeBudget.API.Entities;
using HomeBudget.API.Models.Authentication;
using HomeBudget.API.Services.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HomeBudget.API.Services.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly HomeBudgetContext context;
        private readonly AuthOptions authOptions;

        public UserRepository(HomeBudgetContext context, IOptions<AuthOptions> authOptions)
        {
            this.context = context;
            this.authOptions = authOptions.Value;
        }

        public async Task<IEnumerable<User>> GetUsersAsync() => await context.Users.ToListAsync();


        public async Task<User?> GetUserByIdAsync(int userId) =>
            await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        public async Task AddUserAsync(User user, string password)
        {
            var salt = Argon2Hasher.GenerateSalt();
            var passwordHash = Argon2Hasher.GenerateHash(password, salt);
            user.PasswordSalt = salt;
            user.PasswordHash = passwordHash;
            context.Users.Add(user);
            await context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(User user)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }

        public async Task<bool> UserExistsAsync(int userId) => await context.Users.AnyAsync(u => u.Id == userId);

        public async Task SaveChangesAsync() => await context.SaveChangesAsync();

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
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}