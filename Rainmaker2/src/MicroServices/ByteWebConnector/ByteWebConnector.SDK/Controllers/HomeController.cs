using Microsoft.AspNetCore.Mvc;

namespace ByteWebConnector.SDK.Controllers
{
    [Route(template: "api/ByteWebConnectorSdk/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [Route(template: "[action]")]
        public string Index()
        {
            return "ByteWebConnectorSdk micro service is running";
        }
    }
}
