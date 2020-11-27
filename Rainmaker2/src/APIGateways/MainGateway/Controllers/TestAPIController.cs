using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace MainGateway.Controllers
{
    [ApiController]
    [Route("api/MainGateway/[controller]")]
    public class TestAPIController : Controller
    {
        [HttpGet("[action]")]
        [HttpHead("[action]")]
        public ActionResult Test(string text)
        {
            return Json(new { Success = true, Result =  " : " + text });
        }
        [HttpPost("[action]")]
        [HttpPut("[action]")]
        [HttpDelete("[action]")]
        [HttpPatch("[action]")]
        [HttpOptions("[action]")]
        public ActionResult Test(TextModel model)
        {
            return Json(new { Success = true, Result =  " : " + model.text });
        }
        public class TextModel
        {
            public string text { get; set; }
        }
    }
}