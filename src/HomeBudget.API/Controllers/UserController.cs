using AutoMapper;
using HomeBudget.API.Entities;
using HomeBudget.API.Models.User;
using HomeBudget.API.Services.Repositories;
using HomeBudget.API.Services.Utils;
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
        /// Get all or only contributing users
        /// </summary>
        /// <param name="isContributor">Filter for only contributing users</param>
        /// <returns>A list of users</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers([FromQuery(Name = "isContributor")] bool isContributor = false)
        {
            var users = await userRepository.GetUsersAsync(isContributor);
            
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
        public async Task<ActionResult> UpdateUser(int userId, UserDto user)
        {
            if (user.Id != userId)
            {
                return BadRequest();
            }
            
            var userEntity = await userRepository.GetUserByIdAsync(userId);
            
            if (userEntity == null)
            {
                return NotFound();
            }

            mapper.Map(user, userEntity);
            
            await userRepository.SaveChangesAsync();

            return NoContent();
        }
        
        [HttpPut("{userId}/password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateUserPassword(int userId, UserForUpdatePasswordDto userUpdate)
        {
            if (userUpdate.Id != userId)
            {
                return BadRequest();
            }
            
            var userEntity = await userRepository.GetUserByIdAsync(userId);
            
            if (userEntity == null)
            {
                return NotFound();
            }

            var salt = Argon2Hasher.GenerateSalt();
            var passwordHash = Argon2Hasher.GenerateHash(userUpdate.Password, salt);
            userEntity.PasswordSalt = salt;
            userEntity.PasswordHash = passwordHash;

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