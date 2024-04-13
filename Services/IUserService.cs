using System.Security.Claims;
using WebVakt_API.Models;

namespace WebVakt_API.Services
{
    public interface IUserService
    {
        Task<(bool Success, User User, string Message)> UserToDB(User user);
        User ClaimsToUser(ClaimsPrincipal claims);
    }
}
