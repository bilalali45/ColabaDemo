using Microsoft.AspNetCore.Mvc;

namespace Rainmaker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("[action]")]
        [Route("/")]
        public string Index()
        {
            return "Rainmaker API is running";
        }
    }
}