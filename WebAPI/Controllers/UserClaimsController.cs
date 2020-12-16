using Business.Handlers.UserClaims.Commands;
using Business.Handlers.UserClaims.Queries;
using Microsoft.AspNetCore.Authorization;
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
    public class UserClaimsController : BaseApiController
    {

        ///<summary>
        ///UserClaims listeler
        ///</summary>
        ///<remarks>bla bla bla Animals</remarks>
        ///<return>UserClaims Listesi</return>
        ///<response code="200"></response>  
        [HttpGet("getall")]
        //[AllowAnonymous]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetUserClaimsQuery());
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
        ///<return>UserClaims Listesi</return>
        ///<response code="200"></response>  
        [HttpGet("getbyuserid")]
        public async Task<IActionResult> GetByUserId(int userid)
        {
            var result = await Mediator.Send(new GetUserClaimLookupQuery { UserId = userid });
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
        ///<return>UserClaims Listesi</return>
        ///<response code="200"></response>  
        [HttpGet("getoperationclaimbyuserid")]
        public async Task<IActionResult> GetOperationClaimByUserId(int id)
        {
            var result = await Mediator.Send(new GetUserClaimLookupByUserIdQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        /// <summary>
        /// GroupClaim Ekler.
        /// </summary>
        /// <param name="createUserClaim"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateUserClaimCommand createUserClaim)
        {
            var result = await Mediator.Send(createUserClaim);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// GroupClaim Günceller.
        /// </summary>
        /// <param name="updateUserClaim"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateUserClaimCommand updateUserClaim)
        {
            var result = await Mediator.Send(updateUserClaim);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// GroupClaim Siler.
        /// </summary>
        /// <param name="deleteUserClaim"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserClaimCommand deleteUserClaim)
        {
            var result = await Mediator.Send(deleteUserClaim);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}

