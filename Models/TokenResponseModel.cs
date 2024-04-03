using Newtonsoft.Json;

namespace WebVakt_API.Models
{
    public class TokenResponseModel
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("id_token")]
        public string IdToken { get; set; }
    }
}
