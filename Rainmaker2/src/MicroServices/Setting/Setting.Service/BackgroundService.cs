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
using URF.Core.Abstractions;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using TenantConfig.Common;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Newtonsoft.Json;
using Notification.Model;
using TenantConfig.Common.DistributedCache;

namespace Setting.Service
{
    public class HangfireBackgroundService : IBackgroundService
    {
        //private readonly IEmailReminderService emailReminderService;
        //private readonly IConfiguration _configuration;
        //private readonly IEmailTemplateService emailTemplateService;
        //private readonly IRainmakerService rainmakerService;
        //private readonly ILogger<HangfireBackgroundService> _logger;
        //private readonly HttpClient _httpClient;
        //private readonly IUnitOfWork<TenantConfigContext> _tenantConfigUow;
        //private readonly ISettingService _settingService;
        //private readonly IKeyStoreService _keyStoreService;
        //private readonly ISmtpService _smtpService;
        private string authToken;
        private JwtSecurityToken jwtSecurityToken;
        private readonly IServiceProvider _serviceProvider;
        //private readonly INotificationService notificationService;
        //private readonly IConnectionMultiplexer connection;
        private static readonly string NotificationKey = "NotificationQueue";
        //public HangfireBackgroundService()
        //{
        //    var handler = new JwtSecurityTokenHandler();
        //    jwtSecurityToken = handler.ReadToken(authToken) as JwtSecurityToken;
        //}
        public HangfireBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public void RegisterJob()
        {
            RecurringJob.AddOrUpdate("EmailReminderJob", () => EmailReminderJob(), "*/5 * * * *");
            RecurringJob.AddOrUpdate("DispatchEmailJob", () => DispatchEmailJob(), "*/1 * * * *");
            RecurringJob.AddOrUpdate("LoanStatusJob", () => LoanStatusJob(), "*/5 * * * *");
            RecurringJob.AddOrUpdate("PollAndSendNotification", () => PollAndSendNotification(), "*/5 * * * *");
            RecurringJob.AddOrUpdate("UpdateTenantBranchCache", () => UpdateTenantBranchCache(), "*/5 * * * *");
            RecurringJob.AddOrUpdate("UpdateSettingCache", () => UpdateSettingCache(), "*/5 * * * *");
            RecurringJob.AddOrUpdate("UpdateStringResourceCache", () => UpdateStringResourceCache(), "*/5 * * * *");
        }
        [DisableConcurrentExecution(60 * 60 * 24)]
        public async Task UpdateTenantBranchCache()
        {
            using var scope = _serviceProvider.CreateScope();
            var connection = scope.ServiceProvider.GetRequiredService<IConnectionMultiplexer>();
            var _tenantConfigUow = scope.ServiceProvider.GetRequiredService<IUnitOfWork<TenantConfigContext>>();
            IDatabaseAsync database = connection.GetDatabase();

            await database.KeyDeleteAsync(Constants.TENANTS);

            var tenants = await _tenantConfigUow.Repository<Tenant>().Query(x => x.IsActive == true).Include(x => x.TenantUrls)
                .Include(x => x.Branches).ThenInclude(x => x.BranchLoanOfficerBinders).ThenInclude(x => x.Employee)
                .Select(x => new TenantModel
                {
                    Id = x.Id,
                    Urls = x.TenantUrls.Select(y => new UrlModel { Url = y.Url.ToLower(), Type = (TenantUrlType)y.TypeId }).ToList(),
                    Code = x.Code.ToLower(),
                    Branches = x.Branches.Where(x => x.IsActive == true).Select(y => new BranchModel
                    {
                        Id = y.Id,
                        Code = y.Code.ToLower(),
                        IsCorporate = y.IsCorporate,
                        LoanOfficers = y.BranchLoanOfficerBinders.Where(z => z.Employee.IsActive == true && z.Employee.IsLoanOfficer == true)
                                .Select(z => new LoanOfficerModel
                                {
                                    Id = z.Employee.Id,
                                    Code = z.Employee.Code.ToLower()
                                }).ToList()
                    }).ToList()
                }).ToListAsync();

            foreach (var tenant in tenants)
            {
                foreach (var url in tenant.Urls)
                {
                    await database.HashSetAsync(Constants.TENANTS, Constants.URL_PREFIX + url.Url, JsonConvert.SerializeObject(tenant));
                }
            }
        }
        [DisableConcurrentExecution(60 * 60 * 24)]
        public async Task UpdateSettingCache()
        {
            using var scope = _serviceProvider.CreateScope();
            var connection = scope.ServiceProvider.GetRequiredService<IConnectionMultiplexer>();
            var _tenantConfigUow = scope.ServiceProvider.GetRequiredService<IUnitOfWork<TenantConfigContext>>();
            IDatabaseAsync database = connection.GetDatabase();

            await database.KeyDeleteAsync(SettingService.SettingPrefix);
            var branches = await _tenantConfigUow.Repository<Branch>().Query().ToListAsync();

            foreach (var branch in branches)
            {
                await database.KeyDeleteAsync(SettingService.SettingPrefix + SettingService.TenantPrefix + branch.TenantId);
                await database.KeyDeleteAsync(SettingService.SettingPrefix + SettingService.BranchPrefix + branch.Id);
            }
            var settings = await _tenantConfigUow.Repository<TenantConfig.Entity.Models.Setting>().Query(x => x.IsActive == true).ToListAsync();
            foreach (var setting in settings)
            {
                if (setting.BranchId != null)
                {
                    await database.HashSetAsync(SettingService.SettingPrefix + SettingService.BranchPrefix + setting.BranchId, setting.Name, Newtonsoft.Json.JsonConvert.SerializeObject(setting.Value));
                }
                else if (setting.TenantId != null)
                {
                    await database.HashSetAsync(SettingService.SettingPrefix + SettingService.TenantPrefix + setting.TenantId, setting.Name, Newtonsoft.Json.JsonConvert.SerializeObject(setting.Value));
                }
                else
                {
                    await database.HashSetAsync(SettingService.SettingPrefix, setting.Name, Newtonsoft.Json.JsonConvert.SerializeObject(setting.Value));
                }
            }

        }
        [DisableConcurrentExecution(60 * 60 * 24)]
        public async Task UpdateStringResourceCache()
        {
            using var scope = _serviceProvider.CreateScope();
            var connection = scope.ServiceProvider.GetRequiredService<IConnectionMultiplexer>();
            var _tenantConfigUow = scope.ServiceProvider.GetRequiredService<IUnitOfWork<TenantConfigContext>>();
            IDatabaseAsync database = connection.GetDatabase();

            await database.KeyDeleteAsync(StringResourceService.StringResourcePrefix);
            var branches = await _tenantConfigUow.Repository<Branch>().Query().ToListAsync();

            foreach (var branch in branches)
            {
                await database.KeyDeleteAsync(StringResourceService.StringResourcePrefix + StringResourceService.TenantPrefix + branch.TenantId);
                await database.KeyDeleteAsync(StringResourceService.StringResourcePrefix + StringResourceService.BranchPrefix + branch.Id);
            }
            var stringResources = await _tenantConfigUow.Repository<StringResource>().Query(x => x.IsActive == true).ToListAsync();
            foreach (var stringResource in stringResources)
            {
                if (stringResource.BranchId != null)
                {
                    await database.HashSetAsync(StringResourceService.StringResourcePrefix + StringResourceService.BranchPrefix + stringResource.BranchId, stringResource.Name, Newtonsoft.Json.JsonConvert.SerializeObject(stringResource.Value));
                }
                else if (stringResource.TenantId != null)
                {
                    await database.HashSetAsync(StringResourceService.StringResourcePrefix + StringResourceService.TenantPrefix + stringResource.TenantId, stringResource.Name, Newtonsoft.Json.JsonConvert.SerializeObject(stringResource.Value));
                }
                else
                {
                    await database.HashSetAsync(StringResourceService.StringResourcePrefix, stringResource.Name, Newtonsoft.Json.JsonConvert.SerializeObject(stringResource.Value));
                }
            }
        }
        [DisableConcurrentExecution(60 * 60 * 24)]
        public async Task PollAndSendNotification()
        {
            using var scope = _serviceProvider.CreateScope();
            var connection = scope.ServiceProvider.GetRequiredService<IConnectionMultiplexer>();
            var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
            IDatabaseAsync database = connection.GetDatabase();
            long count = await database.ListLengthAsync(NotificationKey);
            List<NotificationModel> list = new List<NotificationModel>();
            for (int i = 0; i < count; i++)
            {
                RedisValue value = await database.ListGetByIndexAsync(NotificationKey, i);
                if (!value.IsNullOrEmpty)
                {
                    NotificationModel m =
                        JsonConvert.DeserializeObject<NotificationModel>(value.ToString());
                    NotificationModel c =
                        JsonConvert.DeserializeObject<NotificationModel>(value.ToString());
                    if (await notificationService.SendNotification(m))
                        list.Add(c);
                }
            }
            foreach (var item in list)
            {
                await database.ListRemoveAsync(NotificationKey, JsonConvert.SerializeObject(item));
            }
        }
        [DisableConcurrentExecution(60*60*24)]
        public async Task DispatchEmailJob()
        {
            using var scope = _serviceProvider.CreateScope();
            var _keyStoreService = scope.ServiceProvider.GetRequiredService<IKeyStoreService>();
            var _tenantConfigUow = scope.ServiceProvider.GetRequiredService<IUnitOfWork<TenantConfigContext>>();
            var _configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var _logger = scope.ServiceProvider.GetRequiredService<ILogger<HangfireBackgroundService>>();
            var _settingService = scope.ServiceProvider.GetRequiredService<ISettingService>();
            var _smtpService = scope.ServiceProvider.GetRequiredService<ISmtpService>();
            //_logger.LogInformation("Dispatch email job started");
            var key = await _keyStoreService.GetFtpKey(_configuration["KeyStore:Url"]);
            var workItems = await _tenantConfigUow.Repository<WorkQueue>().Query(x => x.ScheduleDate <= DateTime.UtcNow && 
            x.EndDate == null && (x.RetryCount == null || x.RetryCount < x.Retries) && 
            x.IsActive == true && x.Activity.ActivityTypeId==(int)TenantConfig.Common.ActivityType.Email)
                .Include(x => x.WorkQueueTokens)
                .Include(x => x.Activity).ThenInclude(x => x.Template)
                .Include(x => x.Activity).ThenInclude(x => x.ActivityTenantBinders).OrderByDescending(x=>x.Priority).ThenBy(x=>x.RetryCount).ToListAsync();
            if(workItems.Count>0)
                _logger.LogInformation($"Fetched {workItems.Count} work items");
            foreach(var workItem in workItems)
            {
                _logger.LogInformation($"Started working on {workItem.Id}, tenantId {workItem.TenantId}, branchId {workItem.BranchId}");
                if(!workItem.Activity.ActivityTenantBinders.Any(x=>x.TenantId==workItem.TenantId))
                {
                    _logger.LogInformation($"Activity {workItem.Activity.Name} is not enabled for Tenant {workItem.TenantId}");
                    workItem.IsActive = false;
                    workItem.Response = $"Activity {workItem.Activity.Name} is not enabled for Tenant {workItem.TenantId}";
                    workItem.Status = "Disabled";
                    workItem.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
                    _tenantConfigUow.Repository<WorkQueue>().Update(workItem);
                    await _tenantConfigUow.SaveChangesAsync();
                    return;
                }
                DateTime startDate = DateTime.UtcNow;
                try
                {
                    string body = workItem.Activity.Template.Body;
                    string subject = workItem.Subject??"";

                    foreach (var token in workItem.WorkQueueTokens)
                    {
                        body = body.Replace(token.Key, token.Value == null ? "" : token.Value);
                        subject = subject.Replace(token.Key, token.Value == null ? "" : token.Value);
                    }
                    
                    MailAddress from = new MailAddress(workItem.WorkQueueTokens.First(x => x.Key == EmailTokens.FROM_EMAIL).Value, workItem.WorkQueueTokens.FirstOrDefault(x=>x.Key==EmailTokens.FROM_EMAIL_DISPLAY)?.Value);
                    MailAddress[] to = workItem.To.Split(",").Select(x => new MailAddress(x)).ToArray();
                    MailAddress[] cc = null;
                    MailAddress[] bcc = null;
                    if(!string.IsNullOrEmpty(workItem.Cc))
                    {
                        cc= workItem.Cc.Split(",").Select(x => new MailAddress(x)).ToArray();
                    }
                    if (!string.IsNullOrEmpty(workItem.Bcc))
                    {
                        bcc = workItem.Bcc.Split(",").Select(x => new MailAddress(x)).ToArray();
                    }
                    using SmtpClient client = new SmtpClient();
                    using MailMessage message = new MailMessage();
                    message.From = from;
                    foreach (var mailAddress in to)
                    {
                        message.To.Add(mailAddress);
                    }

                    if (cc != null)
                        foreach (var mailAddress in cc)
                        {
                            message.CC.Add(mailAddress);
                        }

                    if (bcc != null)
                        foreach (var mailAddress in bcc)
                        {
                            message.Bcc.Add(mailAddress);
                        }
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;
                    client.Host = await _settingService.GetSetting<string>(Settings.EmailHost);
                    client.Port = await _settingService.GetSetting<int>(Settings.EmailPort);
                    client.EnableSsl = await _settingService.GetSetting<bool>(Settings.EmailEnableSsl);
                    client.Credentials = new NetworkCredential(await _settingService.GetSetting<string>(Settings.EmailUser), AesCryptography.Decrypt(await _settingService.GetSetting<string>(Settings.EmailPassword),key));
                    await _smtpService.Send(client,message);
                    workItem.StartDate = workItem.StartDate ?? startDate;
                    workItem.EndDate = DateTime.UtcNow;
                    workItem.Response = "Email send sucessfully";
                    workItem.RetryCount = workItem.RetryCount == null ? 1 : workItem.RetryCount + 1;
                    workItem.Status = "Success";
                    workItem.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
                    _tenantConfigUow.Repository<WorkQueue>().Update(workItem);
                    await _tenantConfigUow.SaveChangesAsync();
                }
                catch(Exception e)
                {
                    _logger.LogError(e, "Error occoured");
                    workItem.StartDate = workItem.StartDate ?? startDate;
                    workItem.EndDate = null;
                    workItem.Response = $"Email send failed: error {e}";
                    workItem.RetryCount = workItem.RetryCount == null ? 1 : workItem.RetryCount + 1;
                    workItem.Status = "Failure";
                    workItem.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
                    _tenantConfigUow.Repository<WorkQueue>().Update(workItem);
                    await _tenantConfigUow.SaveChangesAsync();
                }
                _logger.LogInformation($"Finished working on {workItem.Id}, tenantId {workItem.TenantId}, branchId {workItem.BranchId}");
            }
            //_logger.LogInformation("Dispatch email job ended");
        }
        [DisableConcurrentExecution(60 * 60 * 24)]
        public async Task EmailReminderJob()
        {
            using var scope = _serviceProvider.CreateScope();
            var emailReminderService = scope.ServiceProvider.GetRequiredService<IEmailReminderService>();
            var _logger = scope.ServiceProvider.GetRequiredService<ILogger<HangfireBackgroundService>>();
            var _configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var _httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();
            var emailTemplateService = scope.ServiceProvider.GetRequiredService<IEmailTemplateService>();
            var rainmakerService = scope.ServiceProvider.GetRequiredService<IRainmakerService>();
            int tenantId = await BeforeJob(_httpClient, _configuration, _logger);
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
                List<DashboardDTO> allDocuments = await GetPendingDocuments(emailReminder.LoanApplicationId,authToken,_configuration,_httpClient);
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
        public async Task<List<DashboardDTO>> GetPendingDocuments(int loanApplicationId,string authToken,IConfiguration _configuration, HttpClient _httpClient) 
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
        public async Task<string> GetTokenAsync(HttpClient _httpClient, IConfiguration _configuration, ILogger<HangfireBackgroundService> _logger)
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
        [DisableConcurrentExecution(60 * 60 * 24)]
        public async Task LoanStatusJob()
        {
            using var scope = _serviceProvider.CreateScope();
            var emailReminderService = scope.ServiceProvider.GetRequiredService<IEmailReminderService>();
            var _logger = scope.ServiceProvider.GetRequiredService<ILogger<HangfireBackgroundService>>();
            var _configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var _httpClient = scope.ServiceProvider.GetRequiredService<HttpClient>();
            var emailTemplateService = scope.ServiceProvider.GetRequiredService<IEmailTemplateService>();
            var rainmakerService = scope.ServiceProvider.GetRequiredService<IRainmakerService>();

            bool isEmailSent = false;
            int tenantId = await BeforeJob(_httpClient, _configuration, _logger);
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
        private  async Task<int> BeforeJob(HttpClient _httpClient, IConfiguration _configuration, ILogger<HangfireBackgroundService> _logger)
        {
            authToken = await GetTokenAsync(_httpClient,_configuration,_logger);
            var handler = new JwtSecurityTokenHandler();
            jwtSecurityToken = handler.ReadToken(authToken) as JwtSecurityToken;
            var tenantId = int.Parse(jwtSecurityToken.Claims.First(claim => claim.Type == "TenantId").Value);
            return tenantId;
        }
    }
}
