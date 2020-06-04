using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rainmaker.Service;

namespace Rainmaker.API.Controllers
{
    [ApiController]
    [Route("api/RainMaker/[controller]")]
    public class LoanApplicationController : Controller
    {
        private readonly ILoanApplicationService loanApplicationService;
        public LoanApplicationController(ILoanApplicationService loanApplicationService)
        {
            this.loanApplicationService = loanApplicationService;
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLoanInfo(int loanApplicationId)
        {
            var loanApplication = await loanApplicationService.GetLoanSummary(loanApplicationId);
            return Ok(loanApplication);
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLOInfo(int loanApplicationId, int businessUnitId)
        {
            var lo = await loanApplicationService.GetLOInfo(loanApplicationId,businessUnitId);
            return Ok(lo);
        }
    }
}