using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebVakt_API.Models;

namespace WebVakt_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            string azure_oid = User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == "emails");
            string email = emailClaim != null ? emailClaim.Value : null;
            string given_name = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
            string family_name = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;

            DateTime registered_date = DateTime.Now;

            var user = new UserModel
            {
                azure_oid = azure_oid,
                email = email,
                given_name = given_name,
                family_name = family_name,
                registered_date = registered_date,
            };

            return Ok(user);
        }
    }
}
