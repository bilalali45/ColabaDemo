using Microsoft.AspNetCore.Mvc;

namespace LoanApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        [Route("/")]
        [Route("[action]")]
        public string Index()
        {
            return "LoanApplication micro service is running";
        }
    }
}
