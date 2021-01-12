using Business.Handlers.Logs.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;



namespace WebAPI.Controllers
{
	/// <summary>
	/// Logs Controller Authorize olmayacaksa [AllowAnonymous] Kullanılır.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class LogsController : BaseApiController
	{
		///<summary>
		///Logs listeler
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
