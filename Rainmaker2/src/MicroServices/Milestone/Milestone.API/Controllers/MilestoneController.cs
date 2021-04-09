using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly ISettingService _settingService;

        public MilestoneController(IMilestoneService milestoneService,
            IRainmakerService rainmakerService, IConfiguration configuration,ISettingService settingService)
        {
            _milestoneService = milestoneService;
            _rainmakerService = rainmakerService;
            _configuration = configuration;
            _settingService = settingService;
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
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            await _rainmakerService.SetMilestoneId(model.loanApplicationId, model.milestoneId, Request.Headers["Authorization"].Select(x => x.ToString()));
            await _settingService.SendEmailReminderLog(model.loanApplicationId, model.milestoneId, tenantId, Request.Headers["Authorization"].Select(x => x.ToString()));
            await _milestoneService.UpdateMilestoneLog(model.loanApplicationId, model.milestoneId,userProfileId);
            return Ok();
        }
        [Authorize(Roles ="MCU")]
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertMilestoneLog(MilestoneIdModel model)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            await _milestoneService.InsertMilestoneLog(model.loanApplicationId,model.milestoneId,userProfileId);
            return Ok();
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("[action]")]
        public async Task<IActionResult> InsertMilestoneLogForCustomer(MilestoneIdModel model)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            await _milestoneService.InsertMilestoneLog(model.loanApplicationId, model.milestoneId, userProfileId);
            return Ok();
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
            var milestone = await _rainmakerService.GetBothLosAndMilestoneId(loanApplicationId,
                Request.Headers["Authorization"].Select(x => x.ToString()));
            var status = await _milestoneService.GetMilestoneForMcuDashboard(loanApplicationId, milestone, tenantId);
            return Ok(status);
        }
        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> SetLosMilestone(LosMilestoneModel model)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            int id = await _milestoneService.GetLosMilestone(model.tenantId, model.milestone, model.losId);
            if(id<=0)
            {
                var url = $"{_configuration["RainMaker:Url"]}/api/milestone/milestone/setlosmilestone";
                await _rainmakerService.SendEmailToSupport(model.tenantId, model.milestone.ToString(), model.loanId, model.rainmakerLosId, url, Request.Headers["Authorization"].Select(x => x.ToString()));
            }
            else
            {
                int loanApplicationId = await _rainmakerService.GetLoanApplicationId(model.loanId, model.rainmakerLosId, Request.Headers["Authorization"].Select(x => x.ToString()));
                if(loanApplicationId<=0)
                {
                    return BadRequest(new ErrorModel { Message = "Unable to find loan application" });
                }
                await _rainmakerService.SetBothLosAndMilestoneId(loanApplicationId, id,model.milestone, Request.Headers["Authorization"].Select(x => x.ToString()));
                await _settingService.SendEmailReminderLog(loanApplicationId, id, tenantId, Request.Headers["Authorization"].Select(x => x.ToString()));
                await _milestoneService.UpdateMilestoneLog(loanApplicationId, id,userProfileId);
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

        [Authorize(Roles = "Customer")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GetMilestoneForBorrowerDashboard(MilestoneloanIdsModel milestoneloanIdsModel)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            List<MilestoneForBorrowerDashboard> MilestoneForBorrowerDashboard = new List<MilestoneForBorrowerDashboard>();
            if (milestoneloanIdsModel.loanApplicationId != null)
            {
                for (int i = 0; i < milestoneloanIdsModel.loanApplicationId.Length; i++)
                {
                    int milestone = await _rainmakerService.GetMilestoneId(milestoneloanIdsModel.loanApplicationId[i],
                    Request.Headers["Authorization"].Select(x => x.ToString()));
                    if (milestone != -1)
                    {
                        var status = await _milestoneService.GetMilestoneForBorrowerDashboard(milestoneloanIdsModel.loanApplicationId[i], milestone, tenantId);
                        status.loanAplicationId = milestoneloanIdsModel.loanApplicationId[i];
                        status.milestoneId = milestone;
                        MilestoneForBorrowerDashboard.Add(status);
                    }
                }
            }
            if (MilestoneForBorrowerDashboard.Count <= 0)
                return Ok(null);
          
            return Ok(MilestoneForBorrowerDashboard);
        }

        [Authorize(Roles = "MCU")]
        [HttpGet("[action]")]
        public  async Task<bool> IsMilestoneMappAgainstStatusId( int MilestoneId)
        {
             return await  _milestoneService.IsMilestoneMappAgainstStatusId(MilestoneId);
        }

    }
}
