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

        public DbSet<User> Users { get; set; }

        public DbSet<Website> Websites { get; set; }

        public DbSet<Audit> Audits { get; set; }

        public DbSet<Error> Errors { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        public DbSet<Monitor> Monitors { get; set; }

        public DbSet<MonitorNotificationPreference> MonitorNotificationPreferences { get; set; }

        public DbSet<Snapshot> Snapshots { get; set; }

        public DbSet<ChangeLog> ChangeLogs { get; set; }

    }
}
