using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebVakt_API.Models
{
    public class Error
    {
        [Key]
        public int ErrorID { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public string? Message { get; set; }

        public string? StackTrace { get; set; }

        [Required]
        public DateTime DateOccurred { get; set; }
    }
}
