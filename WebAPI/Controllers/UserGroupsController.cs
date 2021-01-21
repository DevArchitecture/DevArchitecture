using Business.Handlers.UserGroups.Commands;
using Business.Handlers.UserGroups.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
	/// <summary>
	/// If controller methods will not be Authorize, [AllowAnonymous] is used.
	/// </summary>
	///  
	[Route("api/[controller]")]
	[ApiController]
	public class UserGroupsController : BaseApiController
	{
		///<summary>
		///List UserGroup
		///</summary>
		///<remarks>bla bla bla UserGroups</remarks>
		///<return>Kullanıcı Grup Listesi</return>
		///<response code="200"></response>  
		[HttpGet("getall")]
	
		public async Task<IActionResult> GetList()
		{
			var result = await Mediator.Send(new GetUserGroupsQuery());
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// It brings the details according to its id.
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		[HttpGet("getbyuserid")]
		[AllowAnonymous]
		public async Task<IActionResult> GetByUserId(int userId)
		{
			var result = await Mediator.Send(new GetUserGroupLookupQuery { UserId = userId });
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}



		///<summary>
		///It brings the details according to its id.
		///</summary>
		///<remarks>bla bla bla </remarks>
		///<return>UserGroups Listesi</return>
		///<response code="200"></response>  
		[HttpGet("getusergroupbyuserid")]
		public async Task<IActionResult> GetGroupClaimsByUserId(int id)
		{
			var result = await Mediator.Send(new GetUserGroupLookupByUserIdQuery { UserId = id });
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		///<summary>
		///It brings the details according to its id.
		///</summary>
		///<remarks>bla bla bla </remarks>
		///<return>UserGroups Listesi</return>
		///<response code="200"></response>  
		[HttpGet("getusersingroupbygroupid")]
		public async Task<IActionResult> GetUsersInGroupByGroupid(int id)
		{
			var result = await Mediator.Send(new GetUsersInGroupLookupByGroupId { GroupId = id });
			if (result.Success)
			{
				return Ok(result.Data);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Add UserGroup.
		/// </summary>
		/// <param name="createUserGroup"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Add([FromBody] CreateUserGroupCommand createUserGroup)
		{
			var result = await Mediator.Send(createUserGroup);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Update UserGroup.
		/// </summary>
		/// <param name="updateUserGroup"></param>
		/// <returns></returns>
		[HttpPut]
		[AllowAnonymous]
		public async Task<IActionResult> Update([FromBody] UpdateUserGroupCommand updateUserGroup)
		{
			var result = await Mediator.Send(updateUserGroup);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Update UserGroup by Id.
		/// </summary>
		/// <param name="updateUserGroup"></param>
		/// <returns></returns>
		[HttpPut("updatebygroupid")]
		[AllowAnonymous]
		public async Task<IActionResult> UpdateByGroupId([FromBody] UpdateUserGroupByGroupIdCommand updateUserGroup)
		{
			var result = await Mediator.Send(updateUserGroup);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}

		/// <summary>
		/// Delete UserGroup.
		/// </summary>
		/// <param name="deleteUserGroup"></param>
		/// <returns></returns>
		[HttpDelete]
		public async Task<IActionResult> Delete([FromBody] DeleteUserGroupCommand deleteUserGroup)
		{
			var result = await Mediator.Send(deleteUserGroup);
			if (result.Success)
			{
				return Ok(result.Message);
			}
			return BadRequest(result.Message);
		}
	}
}
