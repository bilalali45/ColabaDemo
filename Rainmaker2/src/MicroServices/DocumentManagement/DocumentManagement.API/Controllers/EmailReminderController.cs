using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.API.Controllers
{
    [Route("api/DocumentManagement/[controller]")]
    [ApiController]
    [Authorize(Roles = "MCU")]
    public class EmailReminderController : ControllerBase
    {
        #region Private Variables

        private readonly IEmailReminderService emailReminderService;

        #endregion

        #region Constructors
        public EmailReminderController(IEmailReminderService emailReminderService)
        {
            this.emailReminderService = emailReminderService;
        }
        #endregion

        #region Action Methods

        #region GetMethods

        [HttpGet(template: "GetEmailReminders")]
        public async Task<IActionResult> GetEmailReminders()
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var emailReminderQuery = await emailReminderService.GetEmailReminders(tenantId: tenantId, authHeader: Request.Headers["Authorization"].Select(x => x.ToString()));
            return Ok(value: emailReminderQuery);
        }

        [HttpGet(template: "GetDocumentStatusByLoanIds")]
        public async Task<IActionResult> GetDocumentStatusByLoanIds(RemainingDocumentsModel remainingDocumentsModel)
        {
            var query = await emailReminderService.GetDocumentStatusByLoanIds(remainingDocumentsModel.remainingDocuments);
            return Ok(value: query);
        }

        [HttpGet(template: "GetEmailReminderById")]
        public async Task<IActionResult> GetEmailReminderById(EmailReminderByIdModel emailReminderByIdModel)
        {
            var emailReminderQuery = await emailReminderService.GetEmailReminderById(id: emailReminderByIdModel.id);
            return Ok(value: emailReminderQuery);
        }

        #endregion

        #region PostMethods

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> AddEmailReminder(AddEmailReminder addEmailReminderModel)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var emailReminderQuery = await emailReminderService.AddEmailReminder(tenantId: tenantId,
                                                                                    noOfDays: addEmailReminderModel.noOfDays,
                                                                                    recurringTime: addEmailReminderModel.recurringTime,
                                                                                    fromAddress: addEmailReminderModel.email[0].fromAddress,
                                                                                    ccAddress: addEmailReminderModel.email[0].CCAddress,
                                                                                    subject: addEmailReminderModel.email[0].subject,
                                                                                    emailBody: addEmailReminderModel.email[0].emailBody,
                                                                                    userProfileId:userProfileId
                                                                                    );

            return Ok(value: emailReminderQuery);
        }

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> UpdateEmailReminder(UpdateEmailReminder updateEmailReminderModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var emailReminderQuery = await emailReminderService.UpdateEmailReminder(id: updateEmailReminderModel.id,
                                                                                    noOfDays: updateEmailReminderModel.noOfDays,
                                                                                    recurringTime: updateEmailReminderModel.recurringTime,
                                                                                    fromAddress: updateEmailReminderModel.email[0].fromAddress,
                                                                                    ccAddress: updateEmailReminderModel.email[0].CCAddress,
                                                                                    subject: updateEmailReminderModel.email[0].subject,
                                                                                    emailBody: updateEmailReminderModel.email[0].emailBody,
                                                                                    userProfileId: userProfileId,
                                                                                    authHeader: Request.Headers["Authorization"].Select(x => x.ToString())
                                                                                    );

            return Ok(value: emailReminderQuery);
        }

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> EnableDisableEmailReminder(EnableDisableEmailReminderModel enableDisableEmailReminderModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);

            var query = await emailReminderService.EnableDisableEmailReminder(id: enableDisableEmailReminderModel.id,isActive: enableDisableEmailReminderModel.isActive, userProfileId: userProfileId, authHeader: Request.Headers["Authorization"].Select(x => x.ToString()));

            if (query)
                return Ok();
            return NotFound();
        }

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> EnableDisableAllEmailReminders(EnableDisableAllEmailReminderModel enableDisableAllEmailReminderModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);

            await emailReminderService.EnableDisableAllEmailReminders(isActive: enableDisableAllEmailReminderModel.isActive, userProfileId: userProfileId, authHeader: Request.Headers["Authorization"].Select(x => x.ToString()));

            return Ok();
        }
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> GetEmailReminderByIds(GetEmailRemidersByIds getEmailRemidersByIds)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var emailReminderQuery = await emailReminderService.GetEmailReminderByIds(emailReminderIds: getEmailRemidersByIds.id.ToList(), tenantId: tenantId);
            return Ok(value: emailReminderQuery);
        }

        #endregion

        #region DeleteMethods

        [HttpDelete(template: "[action]")]
        public async Task<IActionResult> DeleteEmailReminder(DeleteEmailReminderModel deleteEmailReminderModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);

            var query = await emailReminderService.DeleteEmailReminder(id: deleteEmailReminderModel.id, userProfileId: userProfileId,authHeader: Request.Headers["Authorization"].Select(x => x.ToString()));

            if (query)
                return Ok();
            return NotFound();
        }

        #endregion

        #endregion
    }
}
