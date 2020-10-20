﻿using System;
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
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> SetMilestoneId(MilestoneIdModel model)
        {
            await _loanApplicationService.SetMilestoneId(model.loanApplicationId,model.milestoneId);
            return Ok();
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> GetLoanApplicationId(LoanIdModel model)
        {
            return Ok(await _loanApplicationService.GetLoanApplicationId(model.loanId, model.losId));
        }
    }
}
