namespace WebVakt_API
{
    using Microsoft.EntityFrameworkCore;
    using WebVakt_API.Models;

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
    }
}
