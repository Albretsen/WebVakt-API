using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebVakt_API.Models;
using WebVakt_API.Services;
using Azure.Storage.Queues;

namespace WebVakt_API.Services
{
    public class MonitorService : IMonitorService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public MonitorService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<int> ProcessDueMonitorsAsync()
        {
            var dueMonitorsWithLatestSnapshot = await _context.Monitors
                .Where(m => m.IsActive && m.ScheduledNext <= DateTime.UtcNow)
                .Select(m => new
                {
                    Monitor = m,
                    Snapshot = _context.Snapshots
                                             .Where(s => s.MonitorID == m.MonitorID)
                                             .OrderByDescending(s => s.DateCaptured)
                                             .FirstOrDefault(),
                    WebsiteUrl = m.Website.URL,
                })
                .ToListAsync();

            foreach (var item in dueMonitorsWithLatestSnapshot)
            {
                await QueueMonitorAsync(item.Monitor, item.Snapshot, item.WebsiteUrl);

                await UpdateMonitorScheduleAsync(item.Monitor);
            }

            return dueMonitorsWithLatestSnapshot.Count;
        }

        public async Task QueueMonitorAsync(Models.Monitor monitor, Models.Snapshot snapshot, string URL)
        {
            // Use the configuration to get the connection string
            string connectionString = _configuration.GetConnectionString("QueueConnection");
            string queueName = "monitor-tasks";

            QueueClient queueClient = new QueueClient(connectionString, queueName);
            await queueClient.CreateIfNotExistsAsync();

            if (await queueClient.ExistsAsync())
            {
                string message = $"MonitorID: {monitor.MonitorID}, SnapshotValue: {snapshot?.Value}, URL: {URL}";
                await queueClient.SendMessageAsync(Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(message)));
            }
        }

        public async Task UpdateMonitorScheduleAsync(Models.Monitor monitor)
        {
            // Placeholder for cron parsing and calculation. Consider using a library like NCrontab or Quartz.NET.
            monitor.ScheduledNext = DateTime.UtcNow.AddSeconds(15); 

            _context.Monitors.Update(monitor);
            await _context.SaveChangesAsync();
        }
    }
}
