using Microsoft.AspNetCore.Mvc;

namespace KeyStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("[action]")]
        [Route("/")]
        public string Index()
        {
            return "Key Store API is running";
        }
    }
}