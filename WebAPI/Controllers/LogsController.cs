using System.Collections.Generic;
using Business.Handlers.Logs.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Http;


namespace WebAPI.Controllers
{
    /// <summary>
    /// If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : BaseApiController
    {
        ///<summary>
        ///List Logs
        ///</summary>
        ///<remarks>bla bla bla Logs</remarks>
        ///<return>Logs List</return>
        ///<response code="200"></response>  
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OperationClaim>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetLogDtoQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }
    }
}