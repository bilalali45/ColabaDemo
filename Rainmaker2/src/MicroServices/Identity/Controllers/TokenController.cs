using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        public TokenController(IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }

        [Route("authorize")]
        [HttpGet]
        public async Task<IActionResult> GenerateToken()
        {
            HttpClient httpClient = clientFactory.CreateClient();
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