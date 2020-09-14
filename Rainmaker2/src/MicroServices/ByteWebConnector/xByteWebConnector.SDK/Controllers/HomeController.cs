using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Http;

namespace ByteWebConnector.SDK.Controllers
{
    //[Route("api/ByteWebConnectorSdk/[controller]")]
    public class HomeController : ApiController
    {
        
        [System.Web.Http.HttpGet]
      //  [System.Web.Http.Route(template: "[action]")]
        public string Index()
        {
            return "ByteWebConnectorSdk micro service is running";
        }
    }
}
