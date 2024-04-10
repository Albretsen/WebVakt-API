using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebVakt_API.Models;

namespace WebVakt_API.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<(bool Success, UserModel User, string Message)> CreateUserAsync(ClaimsPrincipal userPrincipal)
        {
            string azure_oid = userPrincipal.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            var emailClaim = userPrincipal.Claims.FirstOrDefault(c => c.Type == "emails");
            string email = emailClaim != null ? emailClaim.Value : null;
            string given_name = userPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
            string family_name = userPrincipal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;
            DateTime registered_date = DateTime.Now;

            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.azure_oid == azure_oid);
            if (existingUser != null)
            {
                return (false, null, "A user with this Azure OID already exists.");
            }

            var user = new UserModel
            {
                azure_oid = azure_oid,
                email = email,
                given_name = given_name,
                family_name = family_name,
                registered_date = registered_date,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return (true, user, null);
        }
    }
}
