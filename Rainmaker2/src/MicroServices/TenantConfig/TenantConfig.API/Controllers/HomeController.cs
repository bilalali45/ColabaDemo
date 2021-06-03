using Microsoft.AspNetCore.Mvc;

namespace TenantConfig.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        [Route("/")]
        [Route("[action]")]
        public string Index()
        {
            return "TenantConfig micro service is running";
        }
    }
}
