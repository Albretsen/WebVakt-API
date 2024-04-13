using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebVakt_API.Models
{
    public class Snapshot
    {
        [Key]
        public int SnapshotID { get; set; }

        [Required]
        public int MonitorID { get; set; }
        [ForeignKey("MonitorID")]
        public Monitor Monitor { get; set; }

        public string? Value { get; set; }

        [Required]
        public DateTime DateCaptured { get; set; }
    }
}
