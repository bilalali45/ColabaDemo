using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rainmaker.Model;
using Rainmaker.Service;
using RainMaker.Service;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rainmaker.API.Controllers
{
    [Route("api/rainmaker/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly IUserProfileService userProfileService;
        private readonly ISettingService settingService;

        public SettingController( IUserProfileService userProfileService,ISettingService settingService)
        {
            this.userProfileService = userProfileService;
            this.settingService = settingService;
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> GetUserRoles()
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var userRoles = await userProfileService.GetUserRoles(userId:userProfileId);
            return Ok(userRoles);
        }

        [HttpPost("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> UpdateUserRoles(List<Model.UserRole> userRoles)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            await userProfileService.UpdateUserRoles(userRoles:userRoles,userId: userProfileId);
            return Ok();
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> RenderEmailTokens(EmailTemplateModel emailTemplateModel)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var templateQuery = await settingService.RenderEmailTokens(id:emailTemplateModel.id,loanApplicationId: emailTemplateModel.loanApplicationId, userProfileId: userProfileId, fromAddess: emailTemplateModel.fromAddress, subject: emailTemplateModel.subject, emailBody: emailTemplateModel.emailBody, lsTokenModels: emailTemplateModel.lstTokens);
            return Ok(value: templateQuery);
        }
    }
}
