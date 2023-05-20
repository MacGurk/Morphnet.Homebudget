using AutoMapper;
using HomeBudget.API.Entities;
using HomeBudget.API.Models.User;
using HomeBudget.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudget.API.Controllers
{
    /// <summary>
    /// Handle user-related operations
    /// </summary>
    [Route("api/v{version:apiVersion}/user")]
    [ApiVersion("1")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
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
            var users = await _userRepository.GetUsersAsync();
            return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
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
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserDto>(user));
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
            var userToAdd = _mapper.Map<User>(user);

            await _userRepository.AddUserAsync(userToAdd);
            var createdUser = _mapper.Map<UserDto>(userToAdd);

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
            var userEntity = await _userRepository.GetUserByIdAsync(userId);
            if (userEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(userEntity, user);

            await _userRepository.SaveChangesAsync();

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
            var userEntity = await _userRepository.GetUserByIdAsync(userId);
            if (userEntity == null)
            {
                return NotFound();
            }

            await _userRepository.DeleteUserAsync(userEntity);

            return NoContent();
        }
    }
}