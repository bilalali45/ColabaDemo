using Microsoft.AspNetCore.Mvc;

namespace MainGateway.Controllers
{
    [ApiController]
    [Route("api/MainGateway/[controller]")]
    public class TestAPIController : Controller
    {
        [HttpGet("[action]")]
        public string TestGet(string text)
        {
            return "Get : " + text;
        }
        [HttpPost("[action]")]
        public string TestPost(string text)
        {
            return "Post : " + text;
        }
        [HttpPut("[action]")]
        public string TestPut(string text)
        {
            return "Put : " + text;
        }
    }
}