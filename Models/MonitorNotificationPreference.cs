using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebVakt_API.Models
{
    public class MonitorNotificationPreference
    {
        [Key]
        public int MonitorNotificationPreferenceID { get; set; }

        [Required]
        public int MonitorID { get; set; }
        [ForeignKey("MonitorID")]
        public Monitor Monitor { get; set; }

        [Required]
        public bool PerChange { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
