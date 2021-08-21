using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.UserClaims.Commands;
using Business.Handlers.UserClaims.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    ///
    [Route("api/[controller]")]
    [ApiController]
    public class UserClaimsController : BaseApiController
    {
        /// <summary>
        /// List UserClaims
        /// </summary>
        /// <remarks>bla bla bla UserClaims</remarks>
        /// <return>UserClaims List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserClaim>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUserClaimsQuery()));
        }

        /// <summary>
        /// Id sine göre detaylarını getirir.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>UserClaims List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserClaim>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyuserid")]
        public async Task<IActionResult> GetByUserId(int userid)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUserClaimLookupQuery { UserId = userid }));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>UserClaims List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getoperationclaimbyuserid")]
        public async Task<IActionResult> GetOperationClaimByUserId(int id)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUserClaimLookupByUserIdQuery { Id = id }));
        }

        /// <summary>
        /// Add GroupClaim.
        /// </summary>
        /// <param name="createUserClaim"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserClaimCommand createUserClaim)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createUserClaim));
        }

        /// <summary>
        /// Update GroupClaim.
        /// </summary>
        /// <param name="updateUserClaim"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserClaimCommand updateUserClaim)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(updateUserClaim));
        }

        /// <summary>
        /// Delete GroupClaim.
        /// </summary>
        /// <param name="deleteUserClaim"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserClaimCommand deleteUserClaim)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(deleteUserClaim));
        }
    }
}