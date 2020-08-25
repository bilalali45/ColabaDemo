using Microsoft.AspNetCore.Mvc;

namespace LosIntegration.API.Controllers
{
    [ApiController]
    [Route(template: "api/LosIntegration/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("[action]")]
        [Route("/")]
        public string Index()
        {
            return "LosIntegration.API is running";
        }
    }
}