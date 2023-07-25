using AutoMapper;
using HomeBudget.API.Models.Authentication;
using HomeBudget.API.Models.User;
using HomeBudget.API.Services;
using HomeBudget.API.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudget.API.Controllers
{
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public LoginController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthResponse>> GetTransactions([FromBody] AuthModel auth)
        {
            var (user, token) = await userRepository.Authenticate(auth.Username, auth.Password);

            if (user is null || token is null)
            {
                return BadRequest(new { message = "Wrong username or password" });
            }

            var authResponse = new AuthResponse
            {
                User = mapper.Map<UserDto>(user),
                Token = token
            };

            return Ok(authResponse);
        }
    }
}
