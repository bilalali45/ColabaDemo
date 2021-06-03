using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Controllers.Areas.Mobile
{
    [Area("Mobile")]
    [Route("api/mobile/identity/[controller]")]
    [ApiController]
    public class McuBaseController : ControllerBase
    {
    }
}
