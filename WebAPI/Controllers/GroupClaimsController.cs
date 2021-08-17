using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.GroupClaims.Commands;
using Business.Handlers.GroupClaims.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    ///
    /// </summary>
    ///
    [Route("api/[controller]")]
    [ApiController]
    public class GroupClaimsController : BaseApiController
    {
        /// <summary>
        /// GroupClaims list
        /// </summary>
        /// <remarks>GroupClaims</remarks>
        /// <return>GroupClaims List</return>
        /// <response code="200"></response>
        // [AllowAnonymous]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GroupClaim>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new GetGroupClaimsQuery()));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>GroupClaims List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupClaim))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int id)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new GetGroupClaimQuery { Id = id }));
        }

        /// <summary>
        /// Brings up Claims by Group Id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>GroupClaims List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getgroupclaimsbygroupid")]
        public async Task<IActionResult> GetGroupClaimsByGroupId(int id)
        {
            return GetResponseOnlyResultData(
                await Mediator.Send(new GetGroupClaimsLookupByGroupIdQuery { GroupId = id }));
        }

        /// <summary>
        /// Addded GroupClaim .
        /// </summary>
        /// <param name="createGroupClaim"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGroupClaimCommand createGroupClaim)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createGroupClaim));
        }

        /// <summary>
        /// Update GroupClaim.
        /// </summary>
        /// <param name="updateGroupClaim"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGroupClaimCommand updateGroupClaim)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(updateGroupClaim));
        }

        /// <summary>
        /// Delete GroupClaim.
        /// </summary>
        /// <param name="deleteGroupClaim"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteGroupClaimCommand deleteGroupClaim)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(deleteGroupClaim));
        }
    }
}