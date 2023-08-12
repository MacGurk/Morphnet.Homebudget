using AutoMapper;
using HomeBudget.API.Entities;
using HomeBudget.API.Models.User;
using HomeBudget.API.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudget.API.Controllers
{
    /// <summary>
    /// Handle user-related operations
    /// </summary>
    [Route("api/v{version:apiVersion}/user")]
    [ApiVersion("1")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>A list of all users</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await userRepository.GetUsersAsync();
            return Ok(mapper.Map<IEnumerable<UserDto>>(users));
        }

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="userId">The id of the user to get</param>
        /// <returns>The requestsd user when it exists</returns>
        [HttpGet("{userId}", Name = "GetUser")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUser(int userId)
        {
            var user = await userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<UserDto>(user));
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user">User to create</param>
        /// <returns>The created user and the corresponding route</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> CreateUser(UserForCreationDto user)
        {
            var userToAdd = mapper.Map<User>(user);

            await userRepository.AddUserAsync(userToAdd, user.Password);
            var createdUser = mapper.Map<UserDto>(userToAdd);

            return CreatedAtRoute("GetUser", new { userId = userToAdd.Id }, createdUser);
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="userId">The id of the user to update</param>
        /// <param name="user">The updated user</param>
        /// <returns>No Content</returns>
        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateUser(int userId, UserForUpdateDto user)
        {
            var userEntity = await userRepository.GetUserByIdAsync(userId);
            if (userEntity == null)
            {
                return NotFound();
            }

            mapper.Map(userEntity, user);

            await userRepository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="userId">The id of the user to delete</param>
        /// <returns>No Content</returns>
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            var userEntity = await userRepository.GetUserByIdAsync(userId);
            if (userEntity == null)
            {
                return NotFound();
            }

            await userRepository.DeleteUserAsync(userEntity);

            return NoContent();
        }
    }
}