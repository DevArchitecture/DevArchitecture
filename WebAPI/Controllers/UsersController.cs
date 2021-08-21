using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.Users.Commands;
using Business.Handlers.Users.Queries;
using Core.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        /// <summary>
        /// List Users
        /// </summary>
        /// <remarks>bla bla bla Users</remarks>
        /// <return>Users List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUsersQuery()));
        }

        /// <summary>
        /// User Lookup
        /// </summary>
        /// <remarks>bla bla bla Users</remarks>
        /// <return>Users List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getuserlookup")]
        public async Task<IActionResult> GetUserLookup()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUserLookupQuery()));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>Users List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int userId)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUserQuery { UserId = userId }));
        }

        /// <summary>
        /// Add User.
        /// </summary>
        /// <param name="createUser"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserCommand createUser)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createUser));
        }

        /// <summary>
        /// Update User.
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUser)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(updateUser));
        }

        /// <summary>
        /// Delete User.
        /// </summary>
        /// <param name="deleteUser"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserCommand deleteUser)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(deleteUser));
        }
    }
}