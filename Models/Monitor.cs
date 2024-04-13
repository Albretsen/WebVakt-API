using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebVakt_API.Models
{
    public class Monitor : AuditableEntity
    {
        [Key]
        public int MonitorID { get; set; }

        [Required]
        public int WebsiteID { get; set; }
        [ForeignKey("WebsiteID")]
        public Website Website { get; set; }

        [Required]
        public string Selector { get; set; }

        public string? Name { get; set; }

        [Required]
        public string CronExpression { get; set; }

        public DateTime? LastChecked { get; set; }

        public DateTime? ScheduledNext { get; set; }

        public bool IsActive { get; set; }
    }
}
