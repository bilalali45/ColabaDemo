using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setting.Model;
using Setting.Service;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Setting.API.Controllers
{
    [Route("api/Setting/[controller]")]
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
     
        [HttpGet(template: "GetEmailTemplates")]
        public async Task<IActionResult> GetEmailTemplates()
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var templateQuery = await emailTemplateService.GetEmailTemplates(templateTypeId:1,tenantId:tenantId,userProfileId);
            return Ok(value: templateQuery);
        }

        [HttpGet(template: "GetTokens")]
        public async Task<IActionResult> GetTokens()
        {
            var toeknQuery = await emailTemplateService.GetTokens();
            return Ok(value: toeknQuery);
        }

        [HttpGet(template: "GetEmailTemplateById")]
        public async Task<IActionResult> GetEmailTemplateById(TemplateIdModel templateIdModel)
        {
            var templateQuery = await emailTemplateService.GetEmailTemplateById(id: templateIdModel.id);
            return Ok(value: templateQuery);
        }

        [HttpGet(template: "GetRenderEmailTemplateById")]
        public async Task<IActionResult> GetRenderEmailTemplateById(TemplateIdModel templateIdModel)
        {
            var templateQuery = await emailTemplateService.GetRenderEmailTemplateById(id: templateIdModel.id,loanApplicationId:templateIdModel.loanApplicationId, Request.Headers["Authorization"].Select(x => x.ToString()));
            return Ok(value: templateQuery);
        }

        #endregion

        #region PostMethods
        [HttpPost(template: "[action]")]
        public async Task<IActionResult> InsertEmailTemplate(InsertEmailTemplateModel insertEmailTemplateModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            var tenantId = int.Parse(s: User.FindFirst(type: "TenantId").Value);
            var emailTemplateQuery = await emailTemplateService.InsertEmailTemplate(templateTypeId:insertEmailTemplateModel.templateTypeId,
                                                                          tenantId: tenantId,
                                                                          templateName: insertEmailTemplateModel.templateName,
                                                                          templateDescription:insertEmailTemplateModel.templateDescription,
                                                                          fromAddress:insertEmailTemplateModel.fromAddress,
                                                                          toAddress:insertEmailTemplateModel.toAddress,
                                                                          CCAddress:insertEmailTemplateModel.CCAddress,
                                                                          subject:insertEmailTemplateModel.subject,
                                                                          emailBody:insertEmailTemplateModel.emailBody,
                                                                          userProfileId:userProfileId);

            return Ok(value: emailTemplateQuery);
        }

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> UpdateEmailTemplate(UpdateEmailTemplateModel updateEmailTemplateModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);
            await emailTemplateService.UpdateEmailTemplate(id: updateEmailTemplateModel.id,
                                                                                    templateName: updateEmailTemplateModel.templateName,
                                                                                    templateDescription: updateEmailTemplateModel.templateDescription,
                                                                                    fromAddress: updateEmailTemplateModel.fromAddress,
                                                                                    toAddress: updateEmailTemplateModel.toAddress,
                                                                                    CCAddress: updateEmailTemplateModel.CCAddress,
                                                                                    subject: updateEmailTemplateModel.subject,
                                                                                    emailBody: updateEmailTemplateModel.emailBody,
                                                                                    userProfileId: userProfileId);

            return Ok();
        }

        [HttpPost(template: "[action]")]
        public async Task<IActionResult> UpdateSortOrder(List<Model.EmailTemplate> lstEmailTemplates)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);

            await emailTemplateService.UpdateSortOrder(lstEmailTemplates: lstEmailTemplates,
                                                                                userProfileId: userProfileId);

            return Ok();
        }
        #endregion

        #region Delete

        [HttpDelete(template: "[action]")]
        public async Task<IActionResult> DeleteEmailTemplate(DeleteTemplateModel deleteTemplateModel)
        {
            var userProfileId = int.Parse(s: User.FindFirst(type: "UserProfileId").Value);

            await emailTemplateService.DeleteEmailTemplate(id: deleteTemplateModel.id,userProfileId:userProfileId);
         
            return Ok();
        }

        #endregion

        #endregion
    }
}
