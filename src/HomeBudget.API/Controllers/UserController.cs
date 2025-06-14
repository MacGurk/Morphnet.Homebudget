using HomeBudget.API.CQRS.Command.User;
using HomeBudget.API.CQRS.Events;
using HomeBudget.API.CQRS.Query.User;
using HomeBudget.API.Models.Transaction;
using HomeBudget.API.Models.UserModels;
using MediatR;
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
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Get all or only contributing users
        /// </summary>
        /// <param name="isContributor">Filter for only contributing users</param>
        /// <returns>A list of users</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers(
            [FromQuery(Name = "isContributor")] bool isContributor = false)
        {
            var query = new GetUsersQuery(IsContributor: isContributor);
            var users = await _mediator.Send(query);
            
            return Ok(users);
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
            var query = new GetUserQuery(userId);
            var user = await _mediator.Send(query);
            
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
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
            var command = new CreateUserCommand(user, user.Password);
            var createdUser = await _mediator.Send(command);
            
            return CreatedAtRoute("GetUser", new { userId = createdUser.Id }, createdUser);
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

            var command = new UpdateUserCommand(user);
            var @event = await _mediator.Send(command);
            
            if (@event is UserNotFoundEvent)
            {
                return NotFound();
            }

            return NoContent();
        }
        
        [HttpPut("{userId}/password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateUserPassword(int userId, UserForUpdatePasswordDto userPasswordUpdate)
        {
            if (userPasswordUpdate.Id != userId)
            {
                return BadRequest();
            }

            var command = new UpdateUserPasswordCommand(userId, userPasswordUpdate);
            var @event = await _mediator.Send(command);
            
            if (@event is UserNotFoundEvent)
            {
                return NotFound();
            }
            
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
            var command = new DeleteUserCommand(userId);
            var @event = await _mediator.Send(command);
            if (@event is UserNotFoundEvent)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}