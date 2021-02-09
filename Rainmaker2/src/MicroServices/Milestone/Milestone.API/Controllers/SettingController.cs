using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Milestone.Model;
using Milestone.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Milestone.API.Controllers
{
    [Route("api/Milestone/[controller]")]
    [ApiController]
    [Authorize(Roles = "MCU")]
    public class SettingController : ControllerBase
    {
        #region Private Variables

        private readonly ISettingService settingService;

        #endregion

        #region Constructors
        public SettingController(ISettingService settingService)
        {
            this.settingService = settingService;
        }
        #endregion
        #region Action Methods

        #region GetMethods

        [HttpGet(template: "GetLoanStatuses")]
        public async Task<IActionResult> GetLoanStatuses()
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var loanStatusQuery = await settingService.GetLoanStatuses(tenantId: tenantId, authHeader: Request.Headers["Authorization"].Select(x => x.ToString()));
            return Ok(value: loanStatusQuery);
        }

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> GetEmailConfigurationsByIds(GetEmailConfigurationByIds getEmailConfigurationByIds)
        {
            var emailReminderQuery = await settingService.GetEmailConfigurations(emailIds: getEmailConfigurationByIds.id.ToList());
            return Ok(value: emailReminderQuery);
        }

        #endregion

        #region PostMethods

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> UpdateLoanStatuses(StatusConfigurationModel statusConfigurationModel)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var updateLoanStatusQuery = await settingService.UpdateLoanStatuses(statusConfigurationModel: statusConfigurationModel,tenantId:tenantId);
            return Ok(value:updateLoanStatusQuery);
        }

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> EnableDisableEmailReminder(EnableDisableEmailReminderModel enableDisableEmailReminderModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);

            var query = await settingService.EnableDisableEmailReminder(id: enableDisableEmailReminderModel.id, isActive: enableDisableEmailReminderModel.isActive, userProfileId: userProfileId, authHeader: Request.Headers["Authorization"].Select(x => x.ToString()));

            if (query)
                return Ok();
            return NotFound();
        }

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> EnableDisableAllEmailReminders(EnableDisableAllEmailReminderModel enableDisableAllEmailReminderModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);

            await settingService.EnableDisableAllEmailReminders(isActive: enableDisableAllEmailReminderModel.isActive, userProfileId: userProfileId, authHeader: Request.Headers["Authorization"].Select(x => x.ToString()));

            return Ok();
        }
        #endregion

        #endregion
    }
}
