using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebVakt_API.Models;
using WebVakt_API.Services;
using Azure.Storage.Queues;
using System.Text.Json;
using NCrontab;

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
            try
            {
                var dueData = await _context.Websites
                .Select(w => new
                {
                    Website = w,
                    Monitors = w.Monitors
                                .Where(m => m.IsActive && m.ScheduledNext <= DateTime.UtcNow)
                                .Select(m => new
                                {
                                    Monitor = m,
                                    LatestSnapshot = _context.Snapshots
                                        .Where(s => s.MonitorID == m.MonitorID)
                                        .OrderByDescending(s => s.DateCaptured)
                                        .FirstOrDefault()
                                })
                                .ToList()
                })
                .Where(w => w.Monitors.Any())
                .ToListAsync();

                foreach (var websiteData in dueData)
                {
                    var messagePayload = JsonSerializer.Serialize(new
                    {
                        WebsiteURL = websiteData.Website.URL,
                        websiteData.Website.WebsiteID,
                        websiteData.Website.UserId,
                        Monitors = websiteData.Monitors.Select(m => new
                        {
                            m.Monitor.MonitorID,
                            m.Monitor.ScheduledNext,
                            m.Monitor.Selector,
                            m.Monitor.Type,
                            m.Monitor.Attributes,
                            m.LatestSnapshot?.SnapshotID,
                            m.LatestSnapshot?.Value
                        })
                    });

                    await QueueMonitorDataAsync(messagePayload);

                    foreach (var monitorInfo in websiteData.Monitors)
                    {
                        await UpdateMonitorScheduleAsync(monitorInfo.Monitor);
                    }
                }

                return dueData.Sum(w => w.Monitors.Count);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred while processing due monitors: {ex.Message}");
                return 0;
            }
        }

        public async Task QueueMonitorDataAsync(string messagePayload)
        {
            string connectionString = _configuration.GetConnectionString("QueueConnection");
            string queueName = "monitor-tasks";

            QueueClient queueClient = new QueueClient(connectionString, queueName);
            await queueClient.CreateIfNotExistsAsync();

            if (await queueClient.ExistsAsync())
            {
                await queueClient.SendMessageAsync(Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(messagePayload)));
            }
        }

        public async Task UpdateMonitorScheduleAsync(Models.Monitor monitor)
        {
            try
            {
                var cronSchedule = CrontabSchedule.Parse(monitor.CronExpression);

                var nextOccurrence = cronSchedule.GetNextOccurrence(DateTime.UtcNow);

                monitor.ScheduledNext = nextOccurrence;

                _context.Monitors.Update(monitor);
                await _context.SaveChangesAsync();
            }
            catch (CrontabException ex)
            {
                Console.Error.WriteLine($"Error parsing cron expression for monitor {monitor.MonitorID}: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                Console.Error.WriteLine($"Database update error for monitor {monitor.MonitorID}: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An unexpected error occurred for monitor {monitor.MonitorID}: {ex.Message}");
            }
        }
    }
}
