
using Business.Handlers.Companies.Commands;
using Business.Handlers.Companies.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Core.Entities.Concrete;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Companies If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CompaniesController : BaseApiController
    {
        ///<summary>
        ///List Companies
        ///</summary>
        ///<remarks>Companies</remarks>
        ///<return>List Companies</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Company>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetCompaniesQuery()));
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>Companies</remarks>
        ///<return>Companies List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Company))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetCompanyQuery { Id = id }));
        }

        /// <summary>
        /// Add Company.
        /// </summary>
        /// <param name="createCompany"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateCompanyCommand createCompany)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createCompany));
        }

        /// <summary>
        /// Update Company.
        /// </summary>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateCompanyCommand updateCompany)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(updateCompany));
        }

        /// <summary>
        /// Delete Company.
        /// </summary>        
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new DeleteCompanyCommand { Id = id }));
        }
    }
}
