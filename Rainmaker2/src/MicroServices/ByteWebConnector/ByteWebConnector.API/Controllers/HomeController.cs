using Microsoft.AspNetCore.Mvc;

namespace ByteWebConnector.API.Controllers
{
    [ApiController]
    [Route(template: "api/ByteWebConnector/[controller]")]
    public class HomeController : Controller
    {
        [Route("/")]
        [Route("[action]")]
        public string Index()
        {
            return "ByteWebConnector micro service is running";
        }
    }
}