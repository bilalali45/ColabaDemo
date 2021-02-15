using Extensions.ExtensionClasses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Milestone.Data;
using Milestone.Entity.Models;
using Milestone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using URF.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TrackableEntities.Common.Core;

namespace Milestone.Service
{
    public class SettingService : ServiceBase<MilestoneContext, Entity.Models.Milestone>, ISettingService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<SettingService> _logger;

        public SettingService(IUnitOfWork<MilestoneContext> previousUow, IServiceProvider services, HttpClient httpClient, IConfiguration configuration, ILogger<SettingService> logger) : base(previousUow: previousUow,
           services: services)
        {
            this._httpClient = httpClient;
            this._configuration = configuration;
            this._logger = logger;
        }
        public async Task<StatusConfigurationModel> GetLoanStatuses(int tenantId, IEnumerable<string> authHeader)
        {
            var lstStatus = Uow.Repository<Entity.Models.Milestone>().Query().Include(x => x.MilestoneStatusConfigurations).ThenInclude(x => x.MilestoneEmailConfigurations);
            StatusConfigurationModel statusConfigurationModel = new StatusConfigurationModel();
            statusConfigurationModel.loanStatuses = new List<LoanStatus>();

            GetJobTypeModel getJobTypeModel = new GetJobTypeModel();
            getJobTypeModel.jobTypeId = JobType.LoanStatusUpdate;

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "Setting:Url"] + "/api/Setting/EmailReminder/GetJobType"),
                Method = HttpMethod.Get,
                Content = new StringContent(content: JsonConvert.SerializeObject(getJobTypeModel),
                                                         encoding: Encoding.UTF8,
                                                         mediaType: "application/json")
            };

            request.Headers.Add("Authorization", authHeader);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            var jobTypeModel = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.JobTypeModel>(await response.Content.ReadAsStringAsync());

            statusConfigurationModel.isActive = jobTypeModel.isActive;

            lstStatus.OrderBy(x => x.Order);

            if (lstStatus != null)
            {
                foreach (var status in lstStatus)
                {
                    LoanStatus loanStatus = new LoanStatus();
                    loanStatus.id = status.Id;
                    loanStatus.mcuName = status.McuName;
                    if (status.MilestoneStatusConfigurations.Count > 0)
                    {
                        loanStatus.statusId = status.MilestoneStatusConfigurations.FirstOrDefault().Id;
                        loanStatus.tenantId = status.MilestoneStatusConfigurations.FirstOrDefault().TenantId;
                        loanStatus.fromAddress = status.MilestoneStatusConfigurations.FirstOrDefault().Milestone.McuName;
                        loanStatus.fromStatusId = (int)status.MilestoneStatusConfigurations.FirstOrDefault().FromStatus;
                        loanStatus.fromStatus = status.MilestoneStatusConfigurations.FirstOrDefault().Milestone.McuName;
                        loanStatus.toStatusId = (int)status.MilestoneStatusConfigurations.FirstOrDefault().ToStatus;
                        loanStatus.noofDays = (short)status.MilestoneStatusConfigurations.FirstOrDefault().NoofDays;
                        loanStatus.recurringTime = (DateTime)status.MilestoneStatusConfigurations.FirstOrDefault().RecurringTime;
                        loanStatus.isActive = (bool)status.MilestoneStatusConfigurations.FirstOrDefault().IsActive;
                        loanStatus.emailId = status.MilestoneStatusConfigurations.FirstOrDefault().MilestoneEmailConfigurations.FirstOrDefault().Id;
                        loanStatus.fromAddress = status.MilestoneStatusConfigurations.FirstOrDefault().MilestoneEmailConfigurations.FirstOrDefault().FromAddress;
                        loanStatus.ccAddress = status.MilestoneStatusConfigurations.FirstOrDefault().MilestoneEmailConfigurations.FirstOrDefault().CcAddress;
                        loanStatus.subject = status.MilestoneStatusConfigurations.FirstOrDefault().MilestoneEmailConfigurations.FirstOrDefault().Subject;
                        loanStatus.body = status.MilestoneStatusConfigurations.FirstOrDefault().MilestoneEmailConfigurations.FirstOrDefault().Body;
                    }
                    if (loanStatus.toStatusId != 0)
                    {
                        loanStatus.toStatus = lstStatus.Where(x => x.Id == loanStatus.toStatusId).Select(x => x.McuName).FirstOrDefault();
                    }
                    statusConfigurationModel.loanStatuses.Add(loanStatus);
                }
            }
            return statusConfigurationModel;
        }
        public async Task<bool> SendEmailReminderLog(int loanApplicationId, int toStatus,int tenantId, IEnumerable<string> authHeader)
        {
            try
            {
                MilestoneLog milestoneLog = await Uow.Repository<MilestoneLog>()
               .Query(x => x.LoanApplicationId == loanApplicationId)
               .OrderByDescending(x => x.CreatedDateUtc).FirstOrDefaultAsync();

                EmailStatusConfiguration emailStatusConfiguration = await GetEmailStatusConfiguration(milestoneLog.MilestoneId, toStatus, tenantId);
                if (emailStatusConfiguration != null)
                {
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri(_configuration[key: "Setting:Url"] + "/api/Setting/EmailReminder/InsertLoanStatusLog"),
                        Method = HttpMethod.Post,
                        Content = new StringContent(content: new EmailReminderLogModel()
                        {
                            RecurringDate = emailStatusConfiguration.recurringTime,
                            tenantId = emailStatusConfiguration.tenantId.Value,
                            loanStatusId = emailStatusConfiguration.id,
                            IsActive = true,
                            jobTypeId = JobType.LoanStatusUpdate,
                            requestDate = DateTime.UtcNow,
                            loanApplicationId = loanApplicationId,

                        }.ToJson(),
                                                          encoding: Encoding.UTF8,
                                                          mediaType: "application/json")
                    };
                    request.Headers.Add("Authorization", authHeader);
                    var response = await _httpClient.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return false;
        }
        public async Task<EmailStatusConfiguration> GetEmailStatusConfiguration(int fromStatus, int toStatus, int tenantId)
        {
            return await Uow.Repository<MilestoneStatusConfiguration>().Query(x => x.FromStatus == fromStatus && x.ToStatus == toStatus && x.TenantId == tenantId && x.IsActive == true && x.IsDeleted == false)
                 .Select(x => new EmailStatusConfiguration()
                 {
                     id = x.Id,
                     fromStatus = x.FromStatus,
                     tenantId = x.TenantId,
                     toStatus = x.ToStatus,
                     noOfDays = x.NoofDays,
                     recurringTime = x.RecurringTime
                 }).FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateLoanStatuses(StatusConfigurationModel statusConfigurationModel, int tenantId)
        {

            var lstStatusConfiguration = Uow.Repository<Entity.Models.MilestoneStatusConfiguration>().Query().ToList();

            foreach (var status in statusConfigurationModel.loanStatuses)
            {

                if (status.fromStatusId != 0)
                {
                    var loanStatus = Uow.Repository<Entity.Models.Milestone>().Query().Include(x => x.MilestoneStatusConfigurations).ThenInclude(x => x.MilestoneEmailConfigurations).Where(x => x.Id == status.id).FirstOrDefaultAsync().Result;

                    Entity.Models.MilestoneStatusConfiguration statusConfiguration = new Entity.Models.MilestoneStatusConfiguration();
                    if (loanStatus.MilestoneStatusConfigurations != null && loanStatus.MilestoneStatusConfigurations.Count > 0)
                    {
                        statusConfiguration = loanStatus.MilestoneStatusConfigurations.FirstOrDefault();
                        statusConfiguration.TrackingState = TrackingState.Modified;
                    }
                    else
                    {
                        statusConfiguration.TrackingState = TrackingState.Added;
                        loanStatus.MilestoneStatusConfigurations.Add(statusConfiguration);
                    }
                    statusConfiguration.TenantId = tenantId;
                    statusConfiguration.FromStatus = status.fromStatusId;
                    statusConfiguration.ToStatus = status.toStatusId;
                    statusConfiguration.NoofDays = status.noofDays;
                    statusConfiguration.RecurringTime = status.recurringTime;
                    statusConfiguration.IsActive = status.isActive;
                    statusConfiguration.IsDeleted = false;


                    Entity.Models.MilestoneEmailConfiguration milestoneEmailConfiguration = new Entity.Models.MilestoneEmailConfiguration();
                    if (statusConfiguration.MilestoneEmailConfigurations != null && statusConfiguration.MilestoneEmailConfigurations.Count > 0)
                    {
                        milestoneEmailConfiguration = statusConfiguration.MilestoneEmailConfigurations.FirstOrDefault();
                        milestoneEmailConfiguration.TrackingState = TrackingState.Modified;
                    }
                    else
                    {
                        milestoneEmailConfiguration.TrackingState = TrackingState.Added;
                        statusConfiguration.MilestoneEmailConfigurations.Add(milestoneEmailConfiguration);
                    }

                    milestoneEmailConfiguration.FromAddress = status.fromAddress;
                    milestoneEmailConfiguration.CcAddress = status.ccAddress;
                    milestoneEmailConfiguration.Subject = status.subject;
                    milestoneEmailConfiguration.Body = status.body;

                    loanStatus.TrackingState = TrackingState.Modified;

                    Uow.Repository<Entity.Models.Milestone>().Update(loanStatus);
                    await Uow.SaveChangesAsync();

                }

            }
            return true;
        }

        public async Task<List<EmailConfigurationModel>> GetEmailConfigurations(List<int> emailIds)
        {
            List<EmailConfigurationModel> lstEmailConfiguration = new List<EmailConfigurationModel>();

            var lstEmailConfigruation = Uow.Repository<Entity.Models.MilestoneEmailConfiguration>().Query().Where(t => emailIds.Contains((int)t.StatusUpdateId)).ToList();

            foreach (var email in lstEmailConfigruation)
            {
                EmailConfigurationModel emailConfigurationModel = new EmailConfigurationModel();
                emailConfigurationModel.id = email.Id;
                emailConfigurationModel.statusUpdateId = (int)email.StatusUpdateId;
                emailConfigurationModel.fromAddress = email.FromAddress;
                emailConfigurationModel.ccAddress = email.CcAddress;
                emailConfigurationModel.subject = email.Subject;
                emailConfigurationModel.body = email.Body;
                lstEmailConfiguration.Add(emailConfigurationModel);
            }
            return lstEmailConfiguration;
        }
        public async Task<bool> EnableDisableEmailReminder(int id, bool isActive,
                                                   int userProfileId, IEnumerable<string> authHeader)
        {
            var statusConfiguration = Uow.Repository<Entity.Models.MilestoneStatusConfiguration>().Query().Where(x => x.Id == id).FirstOrDefaultAsync().Result;

            statusConfiguration.IsActive = isActive;

            statusConfiguration.TrackingState = TrackingState.Modified;

            Uow.Repository<Entity.Models.MilestoneStatusConfiguration>().Update(statusConfiguration);
            await Uow.SaveChangesAsync();

            var model = new
            {
                id = new[] { id },
                isActive
            };

            var content = JsonConvert.SerializeObject(model);

            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "Setting:Url"] + "/api/Setting/emailreminder/EnableDisableEmailRemindersByStatusUpdateId"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: content,
                                                        encoding: Encoding.UTF8,
                                                        mediaType: "application/json")
            };

            request.Headers.Add("Authorization", authHeader);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();

            return true;
        }
        public async Task EnableDisableAllEmailReminders(bool isActive, int userProfileId, IEnumerable<string> authHeader)
        {
            int jobTypeId = JobType.LoanStatusUpdate;

            var model = new
            {
                isActive,
                jobTypeId
            };
            var content = JsonConvert.SerializeObject(model);
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(_configuration[key: "Setting:Url"] + "/api/Setting/emailreminder/EnableDisableAllEmailReminders"),
                Method = HttpMethod.Post,
                Content = new StringContent(content: content,
                                                        encoding: Encoding.UTF8,
                                                        mediaType: "application/json")
            };

            request.Headers.Add("Authorization", authHeader);
            var response = await _httpClient.SendAsync(request);

            response.EnsureSuccessStatusCode();
        }
    }
}
