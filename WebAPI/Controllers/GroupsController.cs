using Business.Handlers.Groups.Commands;
using Business.Handlers.Groups.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
	/// <summary>
	/// 
	/// </summary>
	///  
	[Route("api/[controller]")]
	[ApiController]
	public class GroupsController : BaseApiController
	{

		///<summary>
		///Grupları listeler
		///</summary>
		///<remarks>bla bla bla Groups</remarks>
		///<return>Grup Listesi</return>
		///<response code="200"></response>  
		[HttpGet("getall")]
		//[AllowAnonymous]
		public async Task<IActionResult> GetList()
		{
			var result = await Mediator.Send(new GetGroupsQuery());
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
		///<return>Grup Listesi</return>
		///<response code="200"></response>  
		[HttpGet("getbyid")]
		public async Task<IActionResult> GetById(int groupId)
		{
			var result = await Mediator.Send(new GetGroupQuery { GroupId = groupId });
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		///<summary>
		///Group Lookup
		///</summary>
		///<remarks>Group Lookup döner </remarks>
		///<return>Grup Lokup</return>
		///<response code="200"></response>  
		[HttpGet("getgrouplookup")]
		public async Task<IActionResult> Getselectedlist()
		{
			var result = await Mediator.Send(new GetGroupLookupQuery());
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Group Ekler.
		/// </summary>
		/// <param name="createGroup"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateGroupCommand createGroup)
		{
			var result = await Mediator.Send(createGroup);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Group Günceller.
		/// </summary>
		/// <param name="updateGroup"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateGroupCommand updateGroup)
		{
			var result = await Mediator.Send(updateGroup);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Group Siler.
		/// </summary>
		/// <param name="deleteGroup"></param>
		/// <returns></returns>
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteGroupCommand deleteGroup)
		{
			var result = await Mediator.Send(deleteGroup);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}
	}
}
