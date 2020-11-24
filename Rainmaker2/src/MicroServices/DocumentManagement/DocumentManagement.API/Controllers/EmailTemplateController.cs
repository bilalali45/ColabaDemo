using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.Model;
using DocumentManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentManagement.API.Controllers
{
    [Route("api/DocumentManagement/[controller]")]
    [ApiController]
    [Authorize(Roles = "MCU")]
    public class EmailTemplateController : ControllerBase
    {
        #region Private Variables
        private readonly IEmailTemplateService emailTemplateService;
        #endregion

        #region Constructors

        public EmailTemplateController(IEmailTemplateService emailTemplateService)
        {
            this.emailTemplateService = emailTemplateService;
        }

        #endregion

        #region Action Methods

        #region GetMethods

        [HttpGet(template: "GetTokens")]
        public async Task<IActionResult> GetTokens()
        {
            var toeknQuery = await emailTemplateService.GetTokens();
            return Ok(value: toeknQuery);
        }

        [HttpGet(template: "GetEmailTemplates")]
        public async Task<IActionResult> GetEmailTemplates()
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var templateQuery = await emailTemplateService.GetEmailTemplates(tenantId: tenantId);
            return Ok(value: templateQuery);
        }

        [HttpGet(template: "GetEmailTemplateById")]
        public async Task<IActionResult> GetEmailTemplateById([FromQuery] EmailTemplateIdModel templateIdModel)
        {
            var templateQuery = await emailTemplateService.GetEmailTemplateById(id: templateIdModel.id);
            return Ok(value: templateQuery);
        }

        [HttpGet(template: "GetRenderEmailTemplateById")]
        public async Task<IActionResult> GetRenderEmailTemplateById([FromQuery] RenderTemplateIdModel templateIdModel)
        {
            var templateQuery = await emailTemplateService.GetRenderEmailTemplateById(id: templateIdModel.id, loanApplicationId: templateIdModel.loanApplicationId, Request.Headers["Authorization"].Select(x => x.ToString()));
            return Ok(value: templateQuery);
        }
        [HttpGet(template: "GetDraftEmailTemplateById")]
        public async Task<IActionResult> GetDraftEmailTemplateById([FromQuery] RenderTemplateIdModel templateIdModel)
        {
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var templateQuery = await emailTemplateService.GetDraftEmailTemplateById(id: templateIdModel.id, loanApplicationId: templateIdModel.loanApplicationId,tenantId:tenantId, Request.Headers["Authorization"].Select(x => x.ToString()));
            return Ok(value: templateQuery);
        }
        #endregion
        #region PostMethods
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> InsertEmailTemplate(InsertEmailTemplateModel insertEmailTemplateModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var emailTemplateQuery = await emailTemplateService.InsertEmailTemplate(tenantId: tenantId,
                                                                                    templateName: insertEmailTemplateModel.templateName,
                                                                                    templateDescription: insertEmailTemplateModel.templateDescription,
                                                                                    fromAddress: insertEmailTemplateModel.fromAddress,
                                                                                    toAddress: insertEmailTemplateModel.toAddress,
                                                                                    CCAddress: insertEmailTemplateModel.CCAddress,
                                                                                    subject: insertEmailTemplateModel.subject,
                                                                                    emailBody: insertEmailTemplateModel.emailBody,
                                                                                    userProfileId: userProfileId);

            return Ok(value: emailTemplateQuery);
        }

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> UpdateEmailTemplate(UpdateEmailTemplateModel updateEmailTemplateModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var docQuery = await emailTemplateService.UpdateEmailTemplate(id: updateEmailTemplateModel.id,
                                                                        templateName: updateEmailTemplateModel.templateName,
                                                                        templateDescription: updateEmailTemplateModel.templateDescription,
                                                                        fromAddress: updateEmailTemplateModel.fromAddress,
                                                                        CCAddress: updateEmailTemplateModel.CCAddress,
                                                                        subject: updateEmailTemplateModel.subject,
                                                                        emailBody: updateEmailTemplateModel.emailBody,
                                                                        userProfileId: userProfileId);
            if (docQuery)
                return Ok();
            return NotFound();
        }

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> UpdateSortOrder(List<TemplateSortModel> lstEmailTemplates)
        {
            await emailTemplateService.UpdateSortOrder(lstEmailTemplates: lstEmailTemplates);

            return Ok();
        }
        #endregion
        #region DeleteMethods

        [HttpDelete(template: "[action]")]
        public async Task<IActionResult> DeleteEmailTemplate(DeleteEmailTemplateModel deleteTemplateModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);

           var docQuery = await emailTemplateService.DeleteEmailTemplate(id: deleteTemplateModel.id, userProfileId: userProfileId);

            if (docQuery)
                return Ok();
            return NotFound();
        }

        #endregion

        #endregion
    }
}
