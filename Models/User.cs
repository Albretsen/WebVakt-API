using System.ComponentModel.DataAnnotations;

namespace WebVakt_API.Models
{
    public class User : AuditableEntity
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string AzureOID { get; set; }

        public string Email { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        [Required]
        public DateTime RegisteredDate { get; set; }
    }
}
