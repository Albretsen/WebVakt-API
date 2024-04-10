using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebVakt_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : Controller
    {
        [HttpGet]
        public ActionResult<UserProfile> Get()
        {
            return Ok(new { status = "Healthy" });
        }
    }
}
