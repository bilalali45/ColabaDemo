using Microsoft.EntityFrameworkCore;
using Setting.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Setting.Entity.Models;
using Setting.Model;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;
using EmailTemplate = Setting.Entity.Models.EmailTemplate;
using System.Net.Http;
using System.Text;
using Extensions.ExtensionClasses;
using Microsoft.Extensions.Configuration;

namespace Setting.Service
{
    public class EmailTemplateService : ServiceBase<SettingContext, EmailTemplate>, IEmailTemplateService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public EmailTemplateService(IUnitOfWork<SettingContext> previousUow,
                                    IServiceProvider services, HttpClient _httpClient, IConfiguration _configuration) : base(previousUow: previousUow,
                                                                                                                             services: services)
        {
            this._httpClient = _httpClient;
            this._configuration = _configuration;
        }
        public async Task<List<Model.EmailTemplate>> GetEmailTemplates(int templateTypeId, int tenantId, int userProfileId)
        {
            return await Uow.Repository<EmailTemplate>().Query(x=>!x.IsDeleted == true
                                                               && x.CreatedBy == userProfileId
                                                               && x.TenantId == tenantId
                                                               && x.TemplateTypeId == templateTypeId).Select(x => new Model.EmailTemplate()
                                                                                                                  {
                                                                                                                      id = x.Id,
                                                                                                                      templateTypeId = x.TemplateTypeId,
                                                                                                                      tenantId = x.TenantId,
                                                                                                                      templateName = x.TemplateName,
                                                                                                                      templateDescription = x.TemplateDescription,
                                                                                                                      fromAddress = x.FromAddress,
                                                                                                                      toAddress = x.ToAddress,
                                                                                                                      CCAddress = x.CcAddress,
                                                                                                                      emailBody = x.EmailBody,
                                                                                                                      subject = x.Subject,
                                                                                                                      sortOrder = x.SortOrder
                                                                                                                  }).OrderBy(x=>x.sortOrder).ToListAsync();


        }
        public async Task<long> InsertEmailTemplate(int templateTypeId,
                                                      int tennantId,
                                                      string templateName,
                                                      string templateDescription,
                                                      string fromAddress,
                                                      string toAddress,
                                                      string CCAddress,
                                                      string subject,
                                                      string emailBody,
                                                      int userProfileId)
        {
            var maxSortOrder = await Uow.Repository<EmailTemplate>().Query(x => !x.IsDeleted).MaxAsync(x=>x.SortOrder);

            EmailTemplate emailTemplate = new EmailTemplate();
            
            emailTemplate.TemplateTypeId = templateTypeId;
            emailTemplate.TenantId = tennantId;
            emailTemplate.TemplateName = templateName;
            emailTemplate.TemplateDescription = templateDescription;
            emailTemplate.FromAddress = fromAddress;
            emailTemplate.ToAddress = toAddress;
            emailTemplate.CcAddress = CCAddress;
            emailTemplate.Subject = subject;
            emailTemplate.EmailBody = emailBody;
            emailTemplate.CreatedOn = DateTime.UtcNow;
            emailTemplate.CreatedBy = userProfileId;
            emailTemplate.SortOrder =  maxSortOrder + 1;
            emailTemplate.TrackingState = TrackingState.Added;

            Uow.Repository<EmailTemplate>().Insert(emailTemplate);
            await Uow.SaveChangesAsync();

            return emailTemplate.Id;
        }
        public async Task DeleteEmailTemplate(int id,int userProfileId)
        {
            var result = await Uow.Repository<EmailTemplate>().Query(x => x.Id == id).FirstOrDefaultAsync();
            result.ModifiedBy = userProfileId;
            result.ModifiedOn = DateTime.UtcNow;
            result.TrackingState = TrackingState.Deleted;

            Uow.Repository<EmailTemplate>().Delete(result);

            await Uow.SaveChangesAsync();
        }

        public async Task UpdateSortOrder(List<Model.EmailTemplate> lstEmailTemplates,
                                          int userProfileId)
        {
            foreach (var item in lstEmailTemplates)
            {
                var result = await Uow.Repository<EmailTemplate>().Query(x => x.Id == item.id).FirstOrDefaultAsync();

                result.SortOrder = item.sortOrder;
                result.ModifiedBy = userProfileId;
                result.ModifiedOn = DateTime.UtcNow;
                result.TrackingState = TrackingState.Modified;

                Uow.Repository<EmailTemplate>().Update(result);

                await Uow.SaveChangesAsync();
            }
        }

        public async Task<List<TokenModel>> GetTokens()
        {
            return await Uow.Repository<TokenParam>().Query().Select(x=> new TokenModel(){
                                                                                             id = x.Id,
                                                                                             name = x.Name,
                                                                                             symbol = x.Symbol,
                                                                                             description = x.Description,
                                                                                             key = x.Key

            }).ToListAsync();
        }


        public async Task<Model.EmailTemplate> GetEmailTemplateById(int id)
        {
            return await Uow.Repository<EmailTemplate>().Query(x => !x.IsDeleted
                                                              && x.Id == id).Select(x => new Model.EmailTemplate()
                                                                                         {
                                                                                             id = x.Id,
                                                                                             templateTypeId = x.TemplateTypeId,
                                                                                             tenantId = x.TenantId,
                                                                                             templateName = x.TemplateName,
                                                                                             templateDescription = x.TemplateDescription,
                                                                                             fromAddress = x.FromAddress,
                                                                                             toAddress = x.ToAddress,
                                                                                             CCAddress = x.CcAddress,
                                                                                             emailBody = x.EmailBody,
                                                                                             subject = x.Subject,
                                                                                             sortOrder = x.SortOrder
                                                                                         }).FirstOrDefaultAsync();
        }

        public async Task<Model.EmailTemplate> GetRenderEmailTemplateById(int id,int loanApplicationId, IEnumerable<string> authHeader)
        {
            var template = await Uow.Repository<EmailTemplate>().Query(x => !x.IsDeleted
                                                                    && x.Id == id).Select(x => new Model.EmailTemplate()
                                                                                               {
                                                                                                   id = x.Id,
                                                                                                   templateTypeId = x.TemplateTypeId,
                                                                                                   tenantId = x.TenantId,
                                                                                                   templateName = x.TemplateName,
                                                                                                   templateDescription = x.TemplateDescription,
                                                                                                   fromAddress = x.FromAddress,
                                                                                                   toAddress = x.ToAddress,
                                                                                                   CCAddress = x.CcAddress,
                                                                                                   emailBody = x.EmailBody,
                                                                                                   subject = x.Subject,
                                                                                                   sortOrder = x.SortOrder
                                                                                               }).FirstOrDefaultAsync();
            var tokens = GetTokens();

            EmailTemplateModel emailTemplateModel = new EmailTemplateModel();
            emailTemplateModel.id = id;
            emailTemplateModel.loanApplicationId = loanApplicationId;
            emailTemplateModel.fromAddress = template.fromAddress;
            emailTemplateModel.subject = template.subject;
            emailTemplateModel.emailBody = template.emailBody;
            emailTemplateModel.lstTokens = tokens.Result;

            var request = new HttpRequestMessage()
                          {
                              RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/RainMaker/Setting/RenderEmailTokens"),
                              Method = HttpMethod.Get,
                              Content = new StringContent(content: emailTemplateModel.ToJson(),
                                                          encoding: Encoding.UTF8,
                                                          mediaType: "application/json")
                          };
            request.Headers.Add("Authorization", authHeader);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.EmailTemplate>(await response.Content.ReadAsStringAsync());

            return result;
        }
        public async Task<Model.EmailTemplate> GetEmailReplacedToken(EmailReminder template,string authHeader)
        {
            var requestTokens = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "DocumentManagement:Url"] + "/api/Documentmanagement/EmailTemplate/GetTokens"),
                Method = HttpMethod.Get,
                Content = new StringContent(content: "",
                                                        encoding: Encoding.UTF8,
                                                        mediaType: "application/json")
            };
            requestTokens.Headers.Add("Authorization","Bearer " + authHeader);
            var responseTokens = await _httpClient.SendAsync(requestTokens);

            responseTokens.EnsureSuccessStatusCode();
            var tokens = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.TokenModelEmailReminder>>(await responseTokens.Content.ReadAsStringAsync());
            EmailReminderTemplateModel emailTemplateModel = new EmailReminderTemplateModel();
            //emailTemplateModel.id = id;
            emailTemplateModel.loanApplicationId = template.LoanApplicationId;
            emailTemplateModel.fromAddress = template.email.fromAddress;
            emailTemplateModel.subject = template.email.subject;
            emailTemplateModel.emailBody = template.email.emailBody;
            emailTemplateModel.lstTokens = tokens;

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "RainMaker:Url"] + "/api/RainMaker/Setting/RenderEmailTokens"),
                Method = HttpMethod.Get,
                Content = new StringContent(content: emailTemplateModel.ToJson(),
                                                          encoding: Encoding.UTF8,
                                                          mediaType: "application/json")
            };
            request.Headers.Add("Authorization", "Bearer " + authHeader);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.EmailTemplate>(await response.Content.ReadAsStringAsync());

            return result;
        }
        public async Task UpdateEmailTemplate(int id,
                                              string templateName,
                                              string templateDescription,
                                              string fromAddress,
                                              string toAddress,
                                              string CCAddress,
                                              string subject,
                                              string emailBody,
                                              int userProfileId)
        {
            var result = await Uow.Repository<EmailTemplate>().Query(x => x.Id == id).FirstOrDefaultAsync();
            result.TemplateName = templateName;
            result.TemplateDescription = templateDescription;
            result.FromAddress = fromAddress;
            result.ToAddress = toAddress;
            result.CcAddress = CCAddress;
            result.Subject = subject;
            result.EmailBody = emailBody;
            result.ModifiedBy = userProfileId;
            result.ModifiedOn = DateTime.UtcNow;
            result.TrackingState = TrackingState.Modified;

            Uow.Repository<EmailTemplate>().Update(result);

            await Uow.SaveChangesAsync();
        }
    }
}
