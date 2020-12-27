
using Business.Handlers.Languages.Commands;
using Business.Handlers.Languages.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;


namespace WebAPI.Controllers
{
    /// <summary>
    /// Languages Controller Authorize olmayacaksa [AllowAnonymous] Kullanılır.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LanguagesController : BaseApiController
    {
        ///<summary>
        ///Language listeler
        ///</summary>
        ///<remarks>bla bla bla Languages</remarks>
        ///<return>Languages Listesi</return>
        ///<response code="200"></response>  
        [HttpGet("getall")]
        public async Task<IActionResult> GetList()
        {
            var result = await Mediator.Send(new GetLanguagesQuery());
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
        ///<return>Language Listesi</return>
        ///<response code="200"></response>  
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetById(int languageId)
        {
            var result = await Mediator.Send(new GetLanguageQuery { Id = languageId });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Language Ekler.
        /// </summary>
        /// <param name="createLanguage"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateLanguageCommand createLanguage)
        {
            var result = await Mediator.Send(createLanguage);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Language Günceller.
        /// </summary>
        /// <param name="updateLanguage"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateLanguageCommand updateLanguage)
        {
            var result = await Mediator.Send(updateLanguage);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Language Siler.
        /// </summary>
        /// <param name="deleteLanguage"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteLanguageCommand deleteLanguage)
        {
            var result = await Mediator.Send(deleteLanguage);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///LanguageLookUp listeler
        ///</summary>
        ///<remarks>bla bla bla Languages</remarks>
        ///<return>Languages Listesi</return>
        ///<response code="200"></response>  
        [HttpGet("getlookup")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLookupList()
        {
            var result = await Mediator.Send(new GetLanguagesLookUpQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}
