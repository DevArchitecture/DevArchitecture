using Business.Handlers.Users.Commands;
using Business.Handlers.Users.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
  /// <summary>
  /// If controller methods will not be Authorize, [AllowAnonymous] is used.
  /// </summary>
  [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        ///<summary>
        ///List Users 
        ///</summary>
        ///<remarks>bla bla bla Users</remarks>
        ///<return>Users List</return>
        ///<response code="200"></response>  
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetUsersQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///User Lookup
        ///</summary>
        ///<remarks>bla bla bla Users</remarks>
        ///<return>Users List</return>
        ///<response code="200"></response>  
        [HttpGet("getuserlookup")]
        public async Task<IActionResult> GetUserLookup()
        {
            var result = await Mediator.Send(new GetUserLookupQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    ///<summary>
    ///It brings the details according to its id.
    ///</summary>
    ///<remarks>bla bla bla </remarks>
    ///<return>Users List</return>
    ///<response code="200"></response>  
    [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int userId)
        {
            var result = await Mediator.Send(new GetUserQuery { UserId = userId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add User.
        /// </summary>
        /// <param name="createUser"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserCommand createUser)
        {
            var result = await Mediator.Send(createUser);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update User.
        /// </summary>
        /// <param name="updateUser"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserCommand updateUser)
        {
            var result = await Mediator.Send(updateUser);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete User.
        /// </summary>
        /// <param name="deleteUser"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserCommand deleteUser)
        {
            var result = await Mediator.Send(deleteUser);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
