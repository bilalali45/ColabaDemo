using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileGateway.Controllers
{
    [ApiController]
    [Route("api/mobile/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("[action]")]
        [Route("/")]
        public string Index()
        {
            return "Mobile Gateway is running";
        }
    }
}
