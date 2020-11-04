using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rainmaker.Model;
using Rainmaker.Service;
using Rainmaker.Service.Helpers;
using RainMaker.Common;
using RainMaker.Common.Extensions;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;


namespace Rainmaker.API.Controllers
{
    [ApiController]
    [Route("api/RainMaker/[controller]")]
    public class ThirdPartyCodeController : Controller
    {
        private readonly IThirdPartyCodeService _iThirdPartyCodeService;

      
        private readonly ILogger<LoanApplication> _logger;
        public ThirdPartyCodeController(IThirdPartyCodeService iThirdPartyCodeService, ILogger<LoanApplication> logger)
        {
            this._iThirdPartyCodeService = iThirdPartyCodeService;
            this._logger = logger;
        }

        [Authorize(Roles = "MCU,Customer")]
        [HttpGet("[action]")]
        public IActionResult GetRefIdByThirdPartyId([FromQuery] int thirdPartyId)
        {
            var thirdPartyCodes = _iThirdPartyCodeService.GetRefIdByThirdPartyId(thirdPartyId: thirdPartyId);
            return Ok(thirdPartyCodes);
        }

    }
}