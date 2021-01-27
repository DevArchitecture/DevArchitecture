using Business.Handlers.Logs.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;



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
		///<return>Logs Listesi</return>
		///<response code="200"></response>  
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
