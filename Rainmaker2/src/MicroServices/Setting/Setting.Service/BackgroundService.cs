using Hangfire;
using Setting.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Extensions.ExtensionClasses;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;

namespace Setting.Service
{
    public class HangfireBackgroundService : IBackgroundService
    {
        private readonly IEmailReminderService emailReminderService;
        private readonly IConfiguration _configuration;
        private readonly IEmailTemplateService emailTemplateService;
        private readonly IRainmakerService rainmakerService;
        private readonly ILogger<HangfireBackgroundService> _logger;
        private readonly HttpClient _httpClient;
        private string authToken;
        private JwtSecurityToken jwtSecurityToken;
        //public HangfireBackgroundService()
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    jwtSecurityToken = handler.ReadToken(authToken) as JwtSecurityToken;
        //}
        public HangfireBackgroundService(HttpClient _httpClient, IEmailReminderService emailReminderService, IConfiguration _configuration, IEmailTemplateService emailTemplateService, IRainmakerService rainmakerService, ILogger<HangfireBackgroundService> logger)
        {
            this._httpClient = _httpClient;
            this.emailReminderService = emailReminderService;
            this._configuration = _configuration;
            this.emailTemplateService = emailTemplateService;
            this.rainmakerService = rainmakerService;
            _logger = logger;
        }
        public void RegisterJob()
        {
            RecurringJob.AddOrUpdate("EmailReminderJob", () => EmailReminderJob(), "*/5 * * * *");
            //RecurringJob.AddOrUpdate("LoanStatusJob", () => LoanStatusJob(), "0 6 * * *");
            RecurringJob.AddOrUpdate("LoanStatusJob", () => LoanStatusJob(), "*/5 * * * *");
        }
        public async Task EmailReminderJob()
        {
            int tenantId = await BeforeJob();
            DateTime dateTime = Convert.ToDateTime(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:00"));

            var jobType = await emailReminderService.GetJobType(JobType.EmailReminder, tenantId);

            if (jobType != null && !jobType.isActive) {
                _logger.LogInformation("Email Reminder Job is disabled"); return;
            }

            List<EmailReminderLogModel> emailReminderLogModels = emailReminderService.GetEmailreminderLogByDate(dateTime, tenantId, JobType.EmailReminder).Result;

            if (emailReminderLogModels == null || emailReminderLogModels.Count == 0) { _logger.LogInformation("emailReminderLogModels not found"); return; }

            List<RemainingDocuments> remainingDocuments = emailReminderLogModels.Select(x => new RemainingDocuments() { loanApplicationId = x.loanApplicationId }).ToList();
            RemainingDocumentsModel docModel = new RemainingDocumentsModel() { remainingDocuments = remainingDocuments };
            bool isEmailSent = false;
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "DocumentManagement:Url"] + "/api/Documentmanagement/emailreminder/GetDocumentStatusByLoanIds"),
                Method = HttpMethod.Get,
                Content = new StringContent(content: docModel.ToJson(),
                                                          encoding: Encoding.UTF8,
                                                          mediaType: "application/json")
            };
            request.Headers.Add("Authorization", "Bearer " + authToken);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var remainingDocumentslist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.RemainingDocuments>>(await response.Content.ReadAsStringAsync());
            List<RemainingDocuments> remaining = remainingDocumentslist.FindAll(x => x.isDocumentRemaining == true);
            // Mark all loan application active false, document uploaded already
            List<RemainingDocuments> documentCompleted = remainingDocumentslist.FindAll(x => x.isDocumentRemaining == false);
            if (documentCompleted != null && documentCompleted.Count > 0) {
                await emailReminderService.EnableDisableEmailRemindersbyLAId(documentCompleted.Select( x => x.loanApplicationId).ToList(), false);
            }
            if (remaining == null || remaining.Count == 0) {
                _logger.LogInformation("remaining document not found"); 
                return; 
            }
            string[] reminderIds = remaining.Select(b => emailReminderLogModels.FirstOrDefault(a => a.loanApplicationId == b.loanApplicationId).ReminderId).ToArray();
            var requestTemplate = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "DocumentManagement:Url"] + "/api/Documentmanagement/emailreminder/GetEmailReminderByIds"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: new RequestEmailRemidersByIds() { id = reminderIds }.ToJson(),
                                                        encoding: Encoding.UTF8,
                                                        mediaType: "application/json")
            };
            requestTemplate.Headers.Add("Authorization", "Bearer " + authToken);
            var responseTemplate = await _httpClient.SendAsync(requestTemplate);

            responseTemplate.EnsureSuccessStatusCode();
            var emailReminderList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.EmailReminder>>(await responseTemplate.Content.ReadAsStringAsync());
            if (emailReminderList == null || emailReminderList.Count == 0) { _logger.LogInformation("GetEmailReminderByIds not found"); return; }
            List<EmailReminder> updatedEmailReminderList = (from item1 in emailReminderLogModels
                                                            join item2 in emailReminderList
                                                       on item1.ReminderId equals item2.id
                                                            select new EmailReminder
                                                            {
                                                                EmailReminderLogId = item1.id,
                                                                LoanApplicationId = item1.loanApplicationId,
                                                                email = item2.email,
                                                                id = item2.id,
                                                                isActive = item2.isActive,
                                                                noOfDays = item2.noOfDays,
                                                                recurringTime = item2.recurringTime
                                                            }).ToList();
            foreach (var emailReminder in updatedEmailReminderList)
            {

                EmailTemplate template = await emailTemplateService.GetEmailReplacedToken(emailReminder, authToken);
                List<DashboardDTO> allDocuments = await GetPendingDocuments(emailReminder.LoanApplicationId,authToken);
                template = ReplaceDocListToken(allDocuments, template);

                try
                {
                    await rainmakerService.SendBorrowerEmail(emailReminder.LoanApplicationId, template.toAddress, template.CCAddress, template.fromAddress, template.subject, template.emailBody, (int)ActivityForType.LoanApplicationDocumentRequestActivity, 0, null, authToken);
                    isEmailSent = true;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Email couldn't sent" + ex.Message);
                    await emailReminderService.InsertEmailReminderLogResponse(emailReminder.EmailReminderLogId, DateTime.UtcNow, false, ex.Message);
                }
                if (isEmailSent)
                {
                    await emailReminderService.InsertEmailReminderLogResponse(emailReminder.EmailReminderLogId, DateTime.UtcNow, true, "");
                    _logger.LogInformation("Email Sent");
                }

            }



        }
        private EmailTemplate ReplaceDocListToken(List<DashboardDTO> allDocuments,EmailTemplate template)
        {
            string names = "<ul>";
            for (int i = 0; i < allDocuments.Count; i++)
            {
                names += "<li style='padding-bottom: 10px;'>" + allDocuments[i].docName + "</li>";
                if (i != allDocuments.Count - 1)
                    names = names + "\n";
            }
            names += "</ul>";
            template.emailBody = template.emailBody.Replace("###RequestDocumentList###", names);
            return template;
        }
        public async Task<List<DashboardDTO>> GetPendingDocuments(int loanApplicationId,string authToken) 
        {
            var requestTemplate = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "DocumentManagement:Url"] + $"/api/Documentmanagement/dashboardMCU/GetPendingDocuments?loanApplicationId={loanApplicationId}"),
                Method = HttpMethod.Get,
                Content = new StringContent(content:     "",
                                                         encoding: Encoding.UTF8,
                                                         mediaType: "application/json")
            };
            requestTemplate.Headers.Add("Authorization", "Bearer " + authToken);
            var responseTemplate = await _httpClient.SendAsync(requestTemplate);
            responseTemplate.EnsureSuccessStatusCode();
            var pendingDocuments = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DashboardDTO>>(await responseTemplate.Content.ReadAsStringAsync());
            return pendingDocuments;
        }
        public async Task<string> GetTokenAsync()
        {
            _logger.LogInformation("HangfireBackgroundService GetTokenAsync() Start");
            HttpResponseMessage backgrounduserResponse = await _httpClient.GetAsync(requestUri: $"{_configuration[key: "KeyStore:Url"]}/api/keystore/keystore?key=BackgroundJobUser");
            backgrounduserResponse.EnsureSuccessStatusCode();
            var backgroeunduser = await backgrounduserResponse.Content.ReadAsStringAsync();
            _logger.LogInformation("backgroeunduser found" + backgroeunduser);
            HttpResponseMessage backgrounduserPwdResponse = await _httpClient.GetAsync(requestUri: $"{_configuration[key: "KeyStore:Url"]}/api/keystore/keystore?key=BackgroundJobPwd");
            backgrounduserPwdResponse.EnsureSuccessStatusCode();
            var backgroeunduserPwd = await backgrounduserPwdResponse.Content.ReadAsStringAsync();
            _logger.LogInformation("backgroeunduserPwd found");
            var requestTemplate = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "Identity:Url"] + "/api/Identity/token/authorize"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: new AuthorizeRequest() { UserName = backgroeunduser, Password = backgroeunduserPwd, Employee = true }.ToJson(),
                                                           encoding: Encoding.UTF8,
                                                           mediaType: "application/json")
            };
            var responseTemplate = await _httpClient.SendAsync(requestTemplate);
            _logger.LogInformation("authorize StatusCode" + responseTemplate.StatusCode);
            responseTemplate.EnsureSuccessStatusCode();
            var authResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<AuthorizeResponse>(await responseTemplate.Content.ReadAsStringAsync());
            _logger.LogInformation("HangfireBackgroundService GetTokenAsync() End");
            return authResponse.data.token;
        }

        public async Task LoanStatusJob()
        {
            bool isEmailSent = false;
            int tenantId = await BeforeJob();
            DateTime dateTime = Convert.ToDateTime(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:00"));

            var jobType = await emailReminderService.GetJobType(JobType.LoanStatusUpdate, tenantId);

            if (jobType != null && !jobType.isActive)
            {
                _logger.LogInformation("Email Reminder Job is disabled"); return;
            }

            List<EmailReminderLogModel> emailReminderLogModels = emailReminderService.GetEmailreminderLogByDate(dateTime, tenantId, JobType.LoanStatusUpdate).Result;

            if (emailReminderLogModels == null || emailReminderLogModels.Count == 0) { _logger.LogInformation("emailReminderLogModels not found"); return; }

            int[] loanStatusIds = emailReminderLogModels.Select(a=>a.loanStatusId.Value).ToArray();
            var requestTemplate = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "Milestone:Url"] + "/api/Milestone/Setting/GetEmailConfigurationsByIds"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: new RequestLoanStatusByIds() { id = loanStatusIds }.ToJson(),
                                                        encoding: Encoding.UTF8,
                                                        mediaType: "application/json")
            };
            requestTemplate.Headers.Add("Authorization", "Bearer " + authToken);
            var responseTemplate = await _httpClient.SendAsync(requestTemplate);

            responseTemplate.EnsureSuccessStatusCode();
            var loanStatusEmailTemplates = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Model.ResponseLoanStatus>>(await responseTemplate.Content.ReadAsStringAsync());
            if (loanStatusEmailTemplates == null || loanStatusEmailTemplates.Count == 0) { _logger.LogInformation("GetEmailConfigurationsByIds not found"); return; }
            List<EmailReminder> updatedLoanStatsusList = (from item1 in emailReminderLogModels
                                                            join item2 in loanStatusEmailTemplates
                                                            on item1.loanStatusId equals item2.statusUpdateId
                                                            select new EmailReminder
                                                            {
                                                                EmailReminderLogId = item1.id,
                                                                LoanApplicationId = item1.loanApplicationId,
                                                                email = new EmailDetail() { emailBody = item2.body,ccAddress = item2.ccAddress,subject = item2.subject,fromAddress = item2.fromAddress}
                                                            }).ToList();
            foreach (var emailReminder in updatedLoanStatsusList)
            {

                EmailTemplate template = await emailTemplateService.GetEmailReplacedToken(emailReminder, authToken);
                //List<DashboardDTO> allDocuments = await GetPendingDocuments(emailReminder.LoanApplicationId, authToken);
                //template = ReplaceDocListToken(allDocuments, template);

                try
                {
                    await rainmakerService.SendBorrowerEmail(emailReminder.LoanApplicationId, template.toAddress, template.CCAddress, template.fromAddress, template.subject, template.emailBody, (int)ActivityForType.LoanApplicationDocumentRequestActivity, 0, null, authToken);
                    isEmailSent = true;
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Email couldn't sent" + ex.Message);
                    await emailReminderService.InsertEmailReminderLogResponse(emailReminder.EmailReminderLogId, DateTime.UtcNow, false, ex.Message);
                }
                if (isEmailSent)
                {
                    await emailReminderService.InsertEmailReminderLogResponse(emailReminder.EmailReminderLogId, DateTime.UtcNow, true, "");
                    _logger.LogInformation("Email Sent");
                }

            }



        }
        private  async Task<int> BeforeJob()
        {
            authToken = await GetTokenAsync();
            var handler = new JwtSecurityTokenHandler();
            jwtSecurityToken = handler.ReadToken(authToken) as JwtSecurityToken;
            var tenantId = int.Parse(jwtSecurityToken.Claims.First(claim => claim.Type == "TenantId").Value);
            return tenantId;
        }
    }
}
