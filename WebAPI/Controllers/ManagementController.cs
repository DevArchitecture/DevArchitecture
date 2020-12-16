using Business.BusinessAspects;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Uygulamanın performans metriklerini sunar.
    /// </summary>

    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly IActivityMonitor monitor;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="monitor"></param> 
        public ManagementController(IActivityMonitor monitor)
        {
            this.monitor = monitor;
        }

        /// <summary>
        /// Metot çağırma ve kullanıcı istatistiklerini verir.
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(ActivityMonitor.ActivitySummary), 200)]
        [HttpGet("performance")]
        public ActionResult Performance()
        {
            return Ok(monitor.Summarize());
        }
    }
}
