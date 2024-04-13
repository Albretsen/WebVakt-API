using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebVakt_API.Models
{
    public class Website : AuditableEntity
    {
        [Key]
        public int WebsiteID { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public string URL { get; set; }

        public string Name { get; set; }

        public string Favicon { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }
    }
}
