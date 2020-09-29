using Microsoft.AspNetCore.Mvc;

namespace Milestone.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        [Route("/")]
        [Route("[action]")]
        public string Index()
        {
            return "Milestone micro service is running";
        }
    }
}
