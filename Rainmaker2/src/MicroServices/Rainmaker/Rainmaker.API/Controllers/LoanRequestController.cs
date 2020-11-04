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
    public class LoanRequestController : Controller
    {
        private readonly ILoanRequestService _loanRequestService;

      
        private readonly ILogger<LoanRequest> _logger;
        public LoanRequestController(ILoanRequestService loanRequestService, ILogger<LoanRequest> logger)
        {
            this._loanRequestService = loanRequestService;
            this._logger = logger;
        }


        [Authorize(Roles = "MCU,Customer")]
        [HttpGet("[action]")]
        public IActionResult GetLoanRequest([FromQuery] int? loanApplicationId = null, [FromQuery] int? id = null, [FromQuery] int? opportunityId = null, long? includes = null)
        {
            var relatedEntities = (LoanRequestService.RelatedEntities?)includes;
            var loanRequest = _loanRequestService.GetLoanRequestWithDetails(id: id, loanApplicationId: loanApplicationId,opportunityId:opportunityId, includes: relatedEntities).SingleOrDefault();
            return Ok(loanRequest);
        }

     
    }
}