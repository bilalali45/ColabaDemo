using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("api/Identity/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        public TokenController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        static HttpClient client = new HttpClient();

        [Route("authorize")]
        [HttpGet]
        public async Task<IActionResult> GenerateToken()
        {
            HttpClient httpClient = clientFactory.CreateClient();

            HttpResponseMessage response = await client.GetAsync("https://localhost:5031/api/membership/validateuser?userName=danish&password=Rainsoft&employee=true");
            if (response.IsSuccessStatusCode)
            {
                var dd = await response.Content.ReadAsStringAsync();
            }


            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = "http://localhost:5010/connect/token",
                ClientId = "ClientId",
                ClientSecret = "ClientSecret",
                Scope = "SampleService"
            });
            return Ok(tokenResponse.Json);
        }
    }
}