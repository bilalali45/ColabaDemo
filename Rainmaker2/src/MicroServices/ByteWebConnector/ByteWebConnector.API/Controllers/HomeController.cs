using Microsoft.AspNetCore.Mvc;

namespace LosIntegration.API.Controllers
{
    [ApiController]
    [Route(template: "api/ByteWebConnector/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("[action]")]
        [Route("/")]
        public string Index()
        {
            return "ByteWebConnector.API is running";
        }
    }
}