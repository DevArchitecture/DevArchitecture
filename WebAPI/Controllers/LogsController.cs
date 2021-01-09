
using Business.Handlers.Logs.Commands;
using Business.Handlers.Logs.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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
			var result = await Mediator.Send(new GetLogsQuery());
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		///<summary>
		///Id sine göre detaylarını getirir.
		///</summary>
		///<remarks>bla bla bla </remarks>
		///<return>Logs Listesi</return>
		///<response code="200"></response>  
		[HttpGet("getbyid")]
		public async Task<IActionResult> GetById(int logId)
		{
			var result = await Mediator.Send(new GetLogQuery { Id = logId });
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Log Ekler.
		/// </summary>
		/// <param name="createLog"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateLogCommand createLog)
		{
			var result = await Mediator.Send(createLog);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Log Günceller.
		/// </summary>
		/// <param name="updateLog"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateLogCommand updateLog)
		{
			var result = await Mediator.Send(updateLog);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Log Siler.
		/// </summary>
		/// <param name="deleteLog"></param>
		/// <returns></returns>
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteLogCommand deleteLog)
		{
			var result = await Mediator.Send(deleteLog);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}
	}
}
