using System.Threading.Tasks;
using WebVakt_API.Models;

namespace WebVakt_API.Services
{
    public interface IMonitorService
    {
        /// <summary>
        /// Fetches all monitors that are due to run (where ScheduledNext is in the past),
        /// adds them to a processing queue, and then updates their ScheduledNext times
        /// based on their cron expressions.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, with a result indicating
        /// the number of monitors processed.</returns>
        Task<int> ProcessDueMonitorsAsync();

        /// <summary>
        /// Adds a specific monitor to the processing queue. This method can be used to
        /// queue monitors for processing outside the regular scheduling mechanism,
        /// for example in response to a user action.
        /// </summary>
        /// <param name="monitor">The monitor to add to the queue.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task QueueMonitorAsync(Models.Monitor monitor, Models.Snapshot snapshot, string URL);

        /// <summary>
        /// Updates the ScheduledNext time for a given monitor based on its cron expression.
        /// This method determines the next time the monitor should be scheduled to run
        /// and updates the monitor's ScheduledNext property accordingly.
        /// </summary>
        /// <param name="monitor">The monitor to update.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UpdateMonitorScheduleAsync(Models.Monitor monitor);
    }
}
