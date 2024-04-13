using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebVakt_API.Models
{
    public class Audit
    {
        [Key]
        public int AuditId { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public int EntityId { get; set; }

        [Required]
        [StringLength(255)]
        public string EntityType { get; set; }

        [Required]
        [StringLength(255)]
        public string Action { get; set; }

        public string Change { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
