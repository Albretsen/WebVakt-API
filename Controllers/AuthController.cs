using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebVakt_API.Models;
using Newtonsoft.Json;

namespace WebVakt_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("exchange-code")]
        public async Task<IActionResult> ExchangeCodeForToken([FromBody] TokenRequestModel model)
        {
            var tokenEndpoint = _configuration["AzureAdB2C:TokenEndpoint"]; // Get from configuration
            var clientId = _configuration["AzureAdB2C:ClientId"]; // Your app's client ID
            var clientSecret = _configuration["AzureAdB2C:ClientSecret"]; // Your app's client secret
            var scope = _configuration["AzureAdB2C:Scope"]; // The scope of the token request

            var client = new HttpClient();
            var requestContent = new FormUrlEncodedContent(new[]
            {
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("scope", scope),
            new KeyValuePair<string, string>("code", model.Code),
            new KeyValuePair<string, string>("redirect_uri", model.RedirectUri),
            new KeyValuePair<string, string>("grant_type", "authorization_code"),
            new KeyValuePair<string, string>("client_secret", clientSecret),
        });

            var response = await client.PostAsync(tokenEndpoint, requestContent);
            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponseModel>(jsonContent);
                // Use the token as needed (e.g., save it, return it to the client, etc.)
                return Ok(tokenResponse);
            }

            return BadRequest("Failed to exchange code for token.");
        }
    }
}
