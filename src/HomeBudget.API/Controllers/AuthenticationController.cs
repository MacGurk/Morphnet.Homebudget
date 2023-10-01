using AutoMapper;
using HomeBudget.API.Models.Authentication;
using HomeBudget.API.Models.UserModels;
using HomeBudget.API.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudget.API.Controllers
{
    [Route("api/v{version:apiVersion}")]
    [ApiVersion("1")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthRepository authRepository;
        private readonly IMapper mapper;

        public LoginController(IAuthRepository authRepository, IMapper mapper)
        {
            this.authRepository = authRepository;
            this.mapper = mapper;
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthResponse>> GetTransactions([FromBody] AuthModel auth)
        {
            var (user, token) = await authRepository.Authenticate(auth.Username, auth.Password);

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
