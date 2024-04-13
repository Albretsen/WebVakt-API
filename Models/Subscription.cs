using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebVakt_API.Models
{
    public class Subscription
    {
        [Key]
        public int SubscriptionID { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        [Required]
        public int StripeID { get; set; }

        [Required]
        public string Tier { get; set; }

        [Required]
        public bool IsTrial { get; set; }

        public DateTime? TrialStart { get; set; }

        public DateTime? TrialEnd { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
