using Microsoft.AspNetCore.Mvc;

namespace MainGateway.Controllers
{
    [ApiController]
    [Route("api/MainGateway/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("[action]")]
        [Route("/")]
        public string Index()
        {
            return "Main Gateway is running";
        }
    }
}