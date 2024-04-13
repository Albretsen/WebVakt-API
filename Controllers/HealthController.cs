using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebVakt_API.Controllers
{
    [Route("api/health")]
    [ApiController]
    public class HealthController : Controller
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new { status = "Healthy" });
        }
    }
}
