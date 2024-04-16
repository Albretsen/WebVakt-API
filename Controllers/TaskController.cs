using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebVakt_API.Models;

namespace WebVakt_API.Controllers
{
    [Route("api/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskRequest request)
        {
            if (request == null || request.Changes == null || !request.Changes.Any())
            {
                return BadRequest("Invalid request data.");
            }

            foreach (var change in request.Changes)
            {
                var snapshot = await _context.Snapshots.FirstOrDefaultAsync(s => s.SnapshotID == change.SnapshotID);
                if (snapshot != null)
                {
                    snapshot.Value = change.CurrentValue;
                    snapshot.DateCaptured = DateTime.UtcNow;
                }
                else
                {
                }
            }

            await _context.SaveChangesAsync();

            return Ok();
        }
    }

    public class TaskRequest
    {
        public string Message { get; set; }
        public List<Change> Changes { get; set; }
    }

    public class Change
    {
        public int SnapshotID { get; set; }
        public string CurrentValue { get; set; }
        // Include other fields from the changes array as needed
    }
}
