using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rainmaker.Model;
using Rainmaker.Service;

namespace Rainmaker.API.Controllers
{
    [Route("api/rainmaker/[controller]")]
    [ApiController]
    public class MilestoneController : Controller
    {
        private readonly ILoanApplicationService _loanApplicationService;

        public MilestoneController(ILoanApplicationService loanApplicationService)
        {
            _loanApplicationService = loanApplicationService;
        }
        [Authorize(Roles = "MCU,Customer")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMilestoneId(int loanApplicationId)
        {
            return Ok(await _loanApplicationService.GetMilestoneId(loanApplicationId));
        }
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> SetMilestoneId(MilestoneIdModel model)
        {
            await _loanApplicationService.SetMilestoneId(model.loanApplicationId,model.milestoneId);
            return Ok();
        }
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> GetLoanApplicationId(LoanIdModel model)
        {
            return Ok(await _loanApplicationService.GetLoanApplicationId(model.loanId, model.losId));
        }
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> SetBothLosAndMilestoneId(LosMilestoneIdModel model)
        {
            await _loanApplicationService.SetBothLosAndMilestoneId(model.loanApplicationId, model.milestoneId,model.losMilestoneId);
            return Ok();
        }
        [Authorize]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetBothLosAndMilestoneId(int loanApplicationId)
        {
            var result = await _loanApplicationService.GetBothLosAndMilestoneId(loanApplicationId);
            return Ok(result);
        }
    }
}
