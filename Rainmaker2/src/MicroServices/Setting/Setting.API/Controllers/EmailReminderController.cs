using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Setting.Model;
using Setting.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Setting.API.Controllers
{
    [Route("api/Setting/[controller]")]
    [ApiController]
    [Authorize(Roles = "MCU")]
    public class EmailReminderController : ControllerBase
    {
        #region Private Variables

        private readonly IEmailReminderService emailReminderService;
        private readonly IBackgroundService backgroundService;

        #endregion

        #region Constructors
        public EmailReminderController(IEmailReminderService emailReminderService,IBackgroundService backgroundService)
        {
            this.emailReminderService = emailReminderService;
            this.backgroundService = backgroundService;
        }
        #endregion

        #region Action Methods

        #region GetMethods
        [HttpGet(template: "GetJobType")]
        public async Task<IActionResult> GetJobType(GetJobTypeModel getJobTypeModel)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var emailReminderQuery = await emailReminderService.GetJobType(id: getJobTypeModel.jobTypeId,tenantId: tenantId);
            return Ok(value: emailReminderQuery);
        }
        #endregion

        #region PostMethods

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> InsertEmailReminderLog(EmailReminderLogModel emailReminderLogModel)
        {
            var query = await emailReminderService.InsertEmailReminderLog(tenantId: emailReminderLogModel.tenantId, jobTypeId: emailReminderLogModel.jobTypeId, loanApplicationId: emailReminderLogModel.loanApplicationId, Request.Headers["Authorization"].Select(x => x.ToString()));

            if (query)
                return Ok();
           return NotFound();
        }
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> InsertLoanStatusLog(EmailReminderLogModel emailReminderLogModel)
        {
            var query = await emailReminderService.InsertLoanStatusLog(tenantId: emailReminderLogModel.tenantId, jobTypeId: emailReminderLogModel.jobTypeId, loanApplicationId: emailReminderLogModel.loanApplicationId, statusId: emailReminderLogModel.loanStatusId ,  Request.Headers["Authorization"].Select(x => x.ToString()));

            if (query)
                return Ok();
            return NotFound();
        }
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> EnableDisableEmailReminders(EnableDisableReminderModel enableDisableReminderModel)
        {
            await emailReminderService.EnableDisableEmailReminders(id: enableDisableReminderModel.id.ToList(), isActive: enableDisableReminderModel.isActive);

            return Ok();
        }
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> EnableDisableEmailRemindersByStatusUpdateId(EnableDisableLoanStatusReminderModel enableDisableReminderModel)
        {
            await emailReminderService.EnableDisableEmailReminders(id: enableDisableReminderModel.id.ToList(), isActive: enableDisableReminderModel.isActive);

            return Ok();
        }

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> EnableDisableAllEmailReminders(EnableDisableAllRemindersModel enableDisableReminderModel)
        {
            await emailReminderService.EnableDisableAllEmailReminders(isActive: enableDisableReminderModel.isActive,jobTypeId:enableDisableReminderModel.jobTypeId);

            return Ok();
        }

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> UpdateEmailReminder(UpdateEmailReminderLogModel updateEmailReminderLogModel)
        {
            await emailReminderService.UpdateEmailReminder(id: updateEmailReminderLogModel.id,noOfDays: updateEmailReminderLogModel.noOfDays, recurringTime: updateEmailReminderLogModel.recurringTime);

            return Ok();
        }

        #endregion

        #region DeleteMethods

        [HttpDelete(template: "[action]")]
        public async Task<IActionResult> DeleteEmailReminder(DeleteEmailReminderLogModel deleteEmailReminderLogModel)
        {
            await emailReminderService.DeleteEmailReminder(id: deleteEmailReminderLogModel.id);

            return Ok();
        }
        [AllowAnonymous]
        [HttpGet(template: "[action]")]
        public async Task<IActionResult> EmailReminderJob()
        {
            //await backgroundService.EmailReminderJob();
            await backgroundService.LoanStatusJob();
            return Ok();
        }
        #endregion

        #endregion
    }
}
