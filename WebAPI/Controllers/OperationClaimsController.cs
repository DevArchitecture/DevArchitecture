using Business.Handlers.OperationClaims.Commands;
using Business.Handlers.OperationClaims.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

/// <summary>
/// If controller methods will not be Authorize, [AllowAnonymous] is used.
/// </summary>
///
[Route("api/[controller]")]
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
    [HttpGet("getall")]
    public async Task<IActionResult> GetList() 
        => GetResponseOnlyResultData(await Mediator.Send(new GetOperationClaimsQuery()));

    /// <summary>
    /// It brings the details according to its id.
    /// </summary>
    /// <remarks>bla bla bla OperationClaims</remarks>
    /// <return>OperationClaims List</return>
    /// <response code="200"></response>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OperationClaim))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpGet("getbyid")]
    public async Task<IActionResult> GetByid(int id) 
        => GetResponseOnlyResultData(await Mediator.Send(new GetOperationClaimQuery() { Id = id }));

    /// <summary>
    /// List OperationClaims
    /// </summary>
    /// <remarks>bla bla bla OperationClaims</remarks>
    /// <return>OperationClaims List</return>
    /// <response code="200"></response>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpGet("getoperationclaimlookup")]
    public async Task<IActionResult> GetOperationClaimLookup() 
        => GetResponseOnlyResultData(await Mediator.Send(new GetOperationClaimLookupQuery()));

    /// <summary>
    /// Update OperationClaim .
    /// </summary>
    /// <param name="updateOperationClaim"></param>
    /// <returns></returns>
    [Consumes("application/json")]
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommand updateOperationClaim) 
        => GetResponseOnlyResultMessage(await Mediator.Send(updateOperationClaim));

    /// <summary>
    /// List OperationClaims
    /// </summary>
    /// <remarks>bla bla bla OperationClaims</remarks>
    /// <return>OperationClaims List</return>
    /// <response code="200"></response>
    [Produces("application/json", "text/plain")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OperationClaim>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
    [HttpGet("getuserclaimsfromcache")]
    public async Task<IActionResult> GetUserClaimsFromCache() 
        => GetResponseOnlyResultData(await Mediator.Send(new GetUserClaimsFromCacheQuery()));
}
