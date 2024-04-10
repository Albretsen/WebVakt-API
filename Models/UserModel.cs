using System.ComponentModel.DataAnnotations;

namespace WebVakt_API.Models
{
    public class UserModel
    {
        [Key]
        public int user_id { get; set; }

        public string azure_oid { get; set; }

        public string email { get; set; }

        public string given_name { get; set; }

        public string family_name { get; set; }

        public DateTime registered_date { get; set; }

    }
}
