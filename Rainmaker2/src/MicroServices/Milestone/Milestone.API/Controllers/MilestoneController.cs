using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Milestone.Model;
using Milestone.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            await _milestoneService.UpdateMilestoneLog(model.loanApplicationId, model.milestoneId);
            return Ok();
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMilestoneForBorrowerDashboard(int loanApplicationId)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            int milestone = await _rainmakerService.GetMilestoneId(loanApplicationId,
                Request.Headers["Authorization"].Select(x => x.ToString()));
            if (milestone <= 0)
                return Ok(null);
            var status = await _milestoneService.GetMilestoneForBorrowerDashboard(loanApplicationId,milestone,tenantId);
            return Ok(status);
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMilestoneForLoanCenter(int loanApplicationId)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            int milestone = await _rainmakerService.GetMilestoneId(loanApplicationId,
                Request.Headers["Authorization"].Select(x => x.ToString()));
            if (milestone <= 0)
                return Ok(null);
            var status = await _milestoneService.GetMilestoneForLoanCenter(loanApplicationId, milestone, tenantId);
            return Ok(status);
        }
        [Authorize(Roles = "MCU")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMilestoneForMcuDashboard(int loanApplicationId)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            int milestone = await _rainmakerService.GetMilestoneId(loanApplicationId,
                Request.Headers["Authorization"].Select(x => x.ToString()));
            if (milestone <= 0)
                return Ok("");
            var status = await _milestoneService.GetMilestoneForMcuDashboard(milestone, tenantId);
            return Ok(status);
        }
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> SetLosMilestone(int tenantId, int loanApplicationId, string milestone, short losId)
        {
            int id = await _milestoneService.GetLosMilestone(tenantId, milestone,losId);
            if(id<=0)
            {
                // todo: send email to support
            }
            else
            {
                await _rainmakerService.SetMilestoneId(loanApplicationId, id, Request.Headers["Authorization"].Select(x => x.ToString()));
            }
            return Ok();
        }
        [Authorize(Roles = "MCU")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetGlobalMilestoneSetting(int tenantId)
        {
            return Ok(await _milestoneService.GetGlobalMilestoneSetting(tenantId));
        }
        [Authorize(Roles = "MCU")]
        [HttpPost("[action]")]
        public async Task<IActionResult> SetGlobalMilestoneSetting(GlobalMilestoneSettingModel setting)
        {
            await _milestoneService.SetGlobalMilestoneSetting(setting);
            return Ok();
        }
        /*
        [Authorize(Roles ="MCU")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMilestoneSetting()
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await _milestoneService.GetMilestoneSetting(tenantId));
        }

        [Authorize(Roles = "MCU")]
        [HttpPost("[action]")]
        public async Task<IActionResult> SetMilestoneSetting([FromBody] List<MilestoneSettingModel> model)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            await _milestoneService.SetMilestoneSetting(tenantId, model);
            return Ok();
        }

        [Authorize(Roles = "MCU")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMilestoneMapping(short losId)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            return Ok(await _milestoneService.GetMilestoneMapping(tenantId,losId));
        }

        [Authorize(Roles = "MCU")]
        [HttpPost("[action]")]
        public async Task<IActionResult> SetMilestoneMapping([FromBody] List<MilestoneMappingModel> model)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            await _milestoneService.SetMilestoneMapping(tenantId, model);
            return Ok();
        }
        */
    }
}
