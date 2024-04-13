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

        public async Task<(bool Success, User User, string Message)> UserToDB(User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.AzureOID == user.AzureOID);
            if (existingUser != null)
            {
                return (false, null, "A user with this Azure OID already exists.");
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return (true, user, null);
        }

        public User ClaimsToUser(ClaimsPrincipal claims)
        {
            string AzureOID = claims.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
            var emailClaim = claims.Claims.FirstOrDefault(c => c.Type == "emails");
            string Email = emailClaim != null ? emailClaim.Value : null;
            string GivenName = claims.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")?.Value;
            string FamilyName = claims.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname")?.Value;
            DateTime RegisteredDate = DateTime.Now;

            return new User
            {
                AzureOID = AzureOID,
                Email = Email,
                GivenName = GivenName,
                FamilyName = FamilyName,
                RegisteredDate = RegisteredDate,
            };
        }
    }
}
