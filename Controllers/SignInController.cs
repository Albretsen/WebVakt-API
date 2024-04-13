using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebVakt_API.Models;
using WebVakt_API.Services;
using System.Threading.Tasks;

namespace WebVakt_API.Controllers
{
    [Route("api/sign-in")]
    [ApiController]
    public class SignInController : Controller
    {
        private readonly IUserService _userService;

        public SignInController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SignIn()
        {
            User user = _userService.ClaimsToUser(User);

            var result = await _userService.UserToDB(user);

            return result.Success ? Ok(result.User) : Conflict(result.Message);
        }
    }
}