using System.Security.Claims;
using WebVakt_API.Models;

namespace WebVakt_API.Services
{
    public interface IUserService
    {
        Task<(bool Success, UserModel User, string Message)> CreateUserAsync(ClaimsPrincipal userPrincipal);
    }
}
