using Microsoft.AspNetCore.Mvc;

namespace McuGateway.Controllers
{
    [Route("api/mcu/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        [HttpGet("[action]")]
        [Route("/")]
        public string Index()
        {
            return "MCU Main Gateway is running";
        }
    }
}
