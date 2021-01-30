using Business.Handlers.OperationClaims.Commands;
using Business.Handlers.OperationClaims.Queries;
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
    public class OperationClaimsController : BaseApiController
    {
        ///<summary>
        ///List OperationClaims 
        ///</summary>
        ///<remarks>bla bla bla OperationClaims</remarks>
        ///<return>OperationClaims List</return>
        ///<response code="200"></response>  
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetOperationClaimsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

    ///<summary>
    ///It brings the details according to its id.
    ///</summary>
    ///<remarks>bla bla bla OperationClaims</remarks>
    ///<return>OperationClaims List</return>
    ///<response code="200"></response>  
    [HttpGet("getbyid")]
        public async Task<IActionResult> GetByid(int id)
        {
            var result = await Mediator.Send(new GetOperationClaimQuery() { Id=id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///List OperationClaims
        ///</summary>
        ///<remarks>bla bla bla OperationClaims</remarks>
        ///<return>OperationClaims List</return>
        ///<response code="200"></response>  
        [HttpGet("getoperationclaimlookup")]
        public async Task<IActionResult> GetOperationClaimLookup()
        {
            var result = await Mediator.Send(new GetOperationClaimLookupQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update OperationClaim .
        /// </summary>
        /// <param name="updateOperationClaim"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateOperationClaimCommand updateOperationClaim)
        {
            var result = await Mediator.Send(updateOperationClaim);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

    }
}

