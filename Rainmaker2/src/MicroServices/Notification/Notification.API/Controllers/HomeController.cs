using Microsoft.AspNetCore.Mvc;

namespace Notification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        [Route("/")]
        [Route("[action]")]
        public string Index()
        {
            return "Notification micro service is running";
        }
    }
}
