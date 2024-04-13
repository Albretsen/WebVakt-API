using System.ComponentModel.DataAnnotations;

namespace WebVakt_API.Models
{
    public abstract class AuditableEntity
    {
        [Required]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public DateTime UpdatedAt { get; set; }
    }
}
