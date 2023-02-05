using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.OperationClaims.Commands;
using Business.Handlers.OperationClaims.Queries;
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
    [Route("api/v{version:apiVersion}/operation-claims")]
    [ApiController]
    public class OperationClaimsController : BaseApiController
    {
        /// <summary>
        /// List OperationClaims
        /// </summary>
        /// <remarks>bla bla bla OperationClaims</remarks>
        /// <return>OperationClaims List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OperationClaim>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetOperationClaimsQuery()));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla OperationClaims</remarks>
        /// <return>OperationClaims List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OperationClaim))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByid([FromRoute]int id)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetOperationClaimQuery() { Id = id }));
        }

        /// <summary>
        /// List OperationClaims
        /// </summary>
        /// <remarks>bla bla bla OperationClaims</remarks>
        /// <return>OperationClaims List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("lookups")]
        public async Task<IActionResult> GetOperationClaimLookup()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetOperationClaimLookupQuery()));
        }

        /// <summary>
        /// Update OperationClaim .
        /// </summary>
        /// <param name="updateOperationClaimDto"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOperationClaimDto updateOperationClaimDto)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new UpdateOperationClaimCommand{Id = updateOperationClaimDto.Id, Alias = updateOperationClaimDto.Alias, Description = updateOperationClaimDto.Description}));
        }

        /// <summary>
        /// List OperationClaims
        /// </summary>
        /// <remarks>bla bla bla OperationClaims</remarks>
        /// <return>OperationClaims List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OperationClaim>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("cache")]
        public async Task<IActionResult> GetUserClaimsFromCache()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUserClaimsFromCacheQuery()));
        }
    }
}