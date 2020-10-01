using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Milestone.Model;
using Milestone.Service;

namespace Milestone.API.Controllers
{
    [Route("api/Milestone/[controller]")]
    [ApiController]
    public class MilestoneController : Controller
    {
        private readonly IMilestoneService _milestoneService;
        private readonly IRainmakerService _rainmakerService;

        public MilestoneController(IMilestoneService milestoneService,
            IRainmakerService rainmakerService)
        {
            _milestoneService = milestoneService;
            _rainmakerService = rainmakerService;
        }
        [Authorize(Roles = "MCU")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAllMilestones()
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await _milestoneService.GetAllMilestones(tenantId));
        }
        [Authorize(Roles = "MCU")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMilestoneId(int loanApplicationId)
        {
            return Ok(await _rainmakerService.GetMilestoneId(loanApplicationId,Request.Headers["Authorization"].Select(x=>x.ToString())));
        }
        [Authorize(Roles = "MCU")]
        [HttpPost("[action]")]
        public async Task<IActionResult> SetMilestoneId(MilestoneIdModel model)
        {
            await _rainmakerService.SetMilestoneId(model.loanApplicationId, model.milestoneId, Request.Headers["Authorization"].Select(x => x.ToString()));
            return Ok();
        }
    }
}
