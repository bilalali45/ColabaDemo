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
        public async Task<IActionResult> SetLosMilestone(int tenantId, string loanId, string milestone, short losId, short rainmakerLosId)
        {
            int id = await _milestoneService.GetLosMilestone(tenantId, milestone,losId);
            if(id<=0)
            {
                await _rainmakerService.SendEmailToSupport(tenantId,milestone, loanId, rainmakerLosId);
            }
            else
            {
                int loanApplicationId = await _rainmakerService.GetLoanApplicationId(loanId, rainmakerLosId, Request.Headers["Authorization"].Select(x => x.ToString()));
                if(loanApplicationId<=0)
                {
                    return BadRequest("Unable to find loan application");
                }
                await _rainmakerService.SetMilestoneId(loanApplicationId, id, Request.Headers["Authorization"].Select(x => x.ToString()));
                await _milestoneService.UpdateMilestoneLog(loanApplicationId, id);
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
        
        [Authorize(Roles ="MCU")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMilestoneSettingList(int tenantId)
        {
            return Ok(await _milestoneService.GetMilestoneSettingList(tenantId));
        }
        [Authorize(Roles = "MCU")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMilestoneSetting(int tenantId,int milestoneId)
        {
            return Ok(await _milestoneService.GetMilestoneSetting(tenantId,milestoneId));
        }
        
        [Authorize(Roles = "MCU")]
        [HttpPost("[action]")]
        public async Task<IActionResult> SetMilestoneSetting([FromBody] MilestoneSettingModel model)
        {
            await _milestoneService.SetMilestoneSetting(model);
            return Ok();
        }
        
        [Authorize(Roles = "MCU")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLosAll()
        {
            return Ok(await _milestoneService.GetLosAll());
        }
        [Authorize(Roles = "MCU")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMappingAll(int tenantId, short losId)
        {
            return Ok(await _milestoneService.GetMappingAll(tenantId,losId));
        }
        [Authorize(Roles = "MCU")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetMapping(int tenantId, int milestoneId)
        {
            return Ok(await _milestoneService.GetMapping(tenantId, milestoneId));
        }

        [Authorize(Roles = "MCU")]
        [HttpPost("[action]")]
        public async Task<IActionResult> SetMapping([FromBody] MilestoneMappingModel model)
        {
            await _milestoneService.SetMapping(model);
            return Ok();
        }
        [Authorize(Roles = "MCU")]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddMapping([FromBody] MilestoneAddMappingModel model)
        {
            await _milestoneService.AddMapping(model);
            return Ok();
        }
        [Authorize(Roles = "MCU")]
        [HttpPut("[action]")]
        public async Task<IActionResult> EditMapping([FromBody] MilestoneAddMappingModel model)
        {
            await _milestoneService.EditMapping(model);
            return Ok();
        }
        [Authorize(Roles = "MCU")]
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteMapping([FromBody] MilestoneAddMappingModel model)
        {
            await _milestoneService.DeleteMapping(model);
            return Ok();
        }
    }
}
