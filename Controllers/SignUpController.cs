using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebVakt_API.Models;
using WebVakt_API.Services;
using System.Threading.Tasks;

namespace WebVakt_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : Controller
    {
        private readonly IUserService _userService;

        public SignUpController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SignUp()
        {
            var result = await _userService.CreateUserAsync(User);

            return result.Success ? Ok(result.User) : Conflict(result.Message);
        }
    }
}