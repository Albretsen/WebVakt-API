using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using WebVakt_API.Services;

public class MonitorBackgroundService : BackgroundService
{
    private readonly ILogger<MonitorBackgroundService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public MonitorBackgroundService(ILogger<MonitorBackgroundService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Monitor Check Background Service is starting.");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Monitor Check Background Service is working.");

            using (var scope = _serviceProvider.CreateScope())
            {
                var monitorService = scope.ServiceProvider.GetRequiredService<IMonitorService>();
                await monitorService.ProcessDueMonitorsAsync();
            }

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }

        _logger.LogInformation("Monitor Check Background Service is stopping.");
    }
}
