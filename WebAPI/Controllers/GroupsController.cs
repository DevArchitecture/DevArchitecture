using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.Groups.Commands;
using Business.Handlers.Groups.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Dtos;

namespace WebAPI.Controllers
{
    /// <summary>
    /// If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    ///
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class GroupsController : BaseApiController
    {
        /// <summary>
        /// List Groups
        /// </summary>
        /// <remarks>bla bla bla Groups</remarks>
        /// <return>Grup List</return>
        /// <response code="200"></response>
        // [AllowAnonymous]
        // [Produces("application/json","text/plain")]
        // [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Group>))]
        // [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetGroupsQuery()));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>Grup List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Group))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById( int id)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetGroupQuery { GroupId = id }));
        }

        /// <summary>
        /// Group Lookup
        /// </summary>
        /// <remarks>Group Lookup döner </remarks>
        /// <return>Grup Lokup</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("lookups")]
        public async Task<IActionResult> Getselectedlist()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetGroupLookupQuery()));
        }

        /// <summary>
        /// Add Group .
        /// </summary>
        /// <param name="createGroup"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGroupCommand createGroup)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createGroup));
        }

        /// <summary>
        /// Update Group.
        /// </summary>
        /// <param name="updateGroupDto"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateGroupDto updateGroupDto)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new UpdateGroupCommand{ Id = updateGroupDto.Id, GroupName = updateGroupDto.GroupName }));
        }

        /// <summary>
        /// Delete Group.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new DeleteGroupCommand{ Id = id }));
        }
    }
}