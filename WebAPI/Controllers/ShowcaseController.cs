using System.Threading.Tasks;
using Business.Handlers.Showcase.Queries;
using Core.Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Development showcase endpoints
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ShowcaseController : BaseApiController
    {
        /// <summary>
        /// In-memory paged rows for 1M showcase
        /// </summary>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShowcasePageDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("rows")]
        [EnableRateLimiting("read")]
        public async Task<IActionResult> GetRows([FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetShowcaseRowsQuery
            {
                Page = page,
                PageSize = pageSize
            }));
        }
    }
}
