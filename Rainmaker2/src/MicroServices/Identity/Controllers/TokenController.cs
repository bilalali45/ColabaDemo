using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("api/token")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        [Route("authorize")]
        [HttpGet]
        public async Task<IActionResult> GenerateToken()
        {
            HttpClient httpClient = new HttpClient();
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