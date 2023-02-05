using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Handlers.Translates.Commands;
using Business.Handlers.Translates.Queries;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Entities.Dtos;

namespace WebAPI.Controllers
{
    /// <summary>
    /// If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TranslatesController : BaseApiController
    {
        /// <summary>
        /// Get translates by lang
        /// </summary>
        /// <param name="lang"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("languages/{lang}")]
        public async Task<IActionResult> GetTranslatesByLang([FromRoute] string lang)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new GetTranslatesByLangQuery() { Lang = lang }));
        }

        /// <summary>
        /// List Translate
        /// </summary>
        /// <remarks>bla bla bla Translates</remarks>
        /// <return>Translates List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Translate>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetTranslatesQuery()));
        }

        /// <summary>
        /// List Dto Translate
        /// </summary>
        /// <remarks>bla bla bla Translates</remarks>
        /// <return>Translates List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Translate>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("dtos")]
        public async Task<IActionResult> GetTranslateListDto()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetTranslateListDtoQuery()));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>Translate List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Translate))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetTranslateQuery { Id = id }));
        }

        /// <summary>
        /// Add Translate.
        /// </summary>
        /// <param name="createTranslate"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateTranslateCommand createTranslate)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createTranslate));
        }

        /// <summary>
        /// Update Translate.
        /// </summary>
        /// <param name="updateTranslateDto"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTranslateDto updateTranslateDto)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new UpdateTranslateCommand{Id = updateTranslateDto.Id, LangId = updateTranslateDto.LangId, Value = updateTranslateDto.Value,Code = updateTranslateDto.Code }));
        }

        /// <summary>
        /// Delete Translate.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new DeleteTranslateCommand{Id = id}));
        }
    }
}