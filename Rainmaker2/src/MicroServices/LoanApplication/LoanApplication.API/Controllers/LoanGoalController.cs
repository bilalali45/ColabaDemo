using LoanApplication.Model;
using LoanApplication.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;

namespace LoanApplication.API.Controllers
{
    [Route("api/loanapplication/[controller]")]
    [ApiController]
    public class LoanGoalController : ControllerBase
    {
        private readonly ILoanGoalService _loanGoalService;
        private readonly IInfoService _infoService;
        public LoanGoalController(ILoanGoalService loanGoalService, IInfoService infoService)
        {
            _loanGoalService = loanGoalService;
            _infoService = infoService;
        }
        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddOrUpdate(AddOrUpdateLoanGoalModel model)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _loanGoalService.AddOrUpdate(tenant, userId, model));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public IActionResult GetAllLoanGoal(int purposeId)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            return Ok(_infoService.GetAllLoanGoal(tenant, purposeId));
        }

        [Authorize(Roles = "Customer")]
        [ResolveWebTenant]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLoanGoal(int loanApplicationId)
        {
            TenantModel tenant = (TenantModel)HttpContext.Items[Constants.COLABA_TENANT];
            int userId = int.Parse(User.FindFirst("UserId").Value);
            return Ok(await _loanGoalService.GetLoanGoal(tenant, loanApplicationId, userId));
        }
    }
}
