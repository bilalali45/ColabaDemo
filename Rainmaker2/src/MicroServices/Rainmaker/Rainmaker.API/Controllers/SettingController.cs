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
    public class SettingController : Controller
    {
        #region Private Variables
        private readonly IUserProfileService userProfileService;
        private readonly ISettingService settingService;
        #endregion
        #region Constructors
        public SettingController( IUserProfileService userProfileService,ISettingService settingService)
        {
            this.userProfileService = userProfileService;
            this.settingService = settingService;
        }
        #endregion
        #region Action Methods

        #region Get
        [Authorize(Roles = "MCU")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserRoles()
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var userRoles = await userProfileService.GetUserRoles(userId:userProfileId);
            return Ok(userRoles);
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> RenderEmailTokens(EmailTemplateModel emailTemplateModel)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var templateQuery = await settingService.RenderEmailTokens(id:emailTemplateModel.id,loanApplicationId: emailTemplateModel.loanApplicationId, userProfileId: userProfileId, fromAddess: emailTemplateModel.fromAddress, ccAddess: emailTemplateModel.ccAddress, subject: emailTemplateModel.subject, emailBody: emailTemplateModel.emailBody, lsTokenModels: emailTemplateModel.lstTokens);
            return Ok(value: templateQuery);
        }

        [Authorize(Roles = "MCU")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLoanOfficers()
        {
            var loanOfficers = await settingService.GetLoanOfficers();
            return Ok(loanOfficers);
        }
        [HttpGet("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> GetBusinessUnits()
        {
            var businessUnits = await settingService.GetBusinessUnits();
            return Ok(businessUnits);
        }
        #endregion
        #region Post
        [HttpPost("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> UpdateUserRoles(List<Model.UserRole> userRoles)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            await userProfileService.UpdateUserRoles(userRoles: userRoles, userId: userProfileId);
            return Ok();
        }
        [HttpPost("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> UpdateByteUsersName(List<Model.ByteUserNameModel> byteUserNameModel)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            await settingService.UpdateByteUserName(byteUserNameModel: byteUserNameModel, userId: userProfileId);
            return Ok();
        }
        [HttpPost("[action]")]
        [Authorize(Roles = "MCU")]
        public async Task<IActionResult> UpdateByteOrganizationCode(List<Model.ByteBusinessUnitModel> byteBusinessUnitModel)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            await settingService.UpdateByteOrganizationCode(byteBusinessUnitModel: byteBusinessUnitModel, userId: userProfileId);
            return Ok();
        }
        #endregion
        #endregion
    }
}
