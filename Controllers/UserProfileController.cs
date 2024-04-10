using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebVakt_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public ActionResult<UserProfile> Get()
        {
            var givenName = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
            var familyName = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;

            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }

            // Assuming you have a UserProfile class that can hold this information
            var userProfile = new UserProfile
            {
                GivenName = givenName,
                FamilyName = familyName,
            };

            return Ok(userProfile);
        }

    }
}
