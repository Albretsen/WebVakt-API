using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebVakt_API.Models
{
    public class ChangeLog
    {
        [Key]
        public int ChangeLogID { get; set; }

        [Required]
        public int SnapshotID { get; set; }
        [ForeignKey("SnapshotID")]
        public Snapshot Snapshot { get; set; }

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }

        [Required]
        public DateTime DateDetected { get; set; }
    }
}
