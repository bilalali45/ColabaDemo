using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Setting.Data;
using Setting.Entity.Models;
using Setting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;
using EmailReminderLog = Setting.Entity.Models.EmailReminderLog;

namespace Setting.Service
{
    public class EmailReminderService : ServiceBase<SettingContext, EmailReminderLog>, IEmailReminderService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public EmailReminderService(IUnitOfWork<SettingContext> previousUow,
                                    IServiceProvider services, HttpClient _httpClient, IConfiguration _configuration) : base(previousUow: previousUow,
                                                                                                                             services: services)
        {
            this._httpClient = _httpClient;
            this._configuration = _configuration;
        }
        public async Task<bool> InsertEmailReminderLog(int tenantId, int jobTypeId, int loanApplicationId, IEnumerable<string> authHeader)
        {
            // Mark isActive false of all pending trigers
            await this.EnableDisableEmailRemindersbyLAId(new List<int> { loanApplicationId }, false);

            var jobType = await Uow.Repository<Entity.Models.JobType>().Query(x => x.Id == jobTypeId && x.IsActive == true).FirstOrDefaultAsync();

            if (jobType != null)
            {
                var request = new HttpRequestMessage()
                {
                    RequestUri = new Uri(_configuration[key: "DocumentManagement:Url"] + "/api/DocumentManagement/EmailReminder/GetEmailReminders"),
                    Method = HttpMethod.Get,
                    Content = new StringContent("", encoding: Encoding.UTF8, mediaType: "application/json")
                };
                request.Headers.Add("Authorization", authHeader);
                var response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                var lstEmailReminders = Newtonsoft.Json.JsonConvert.DeserializeObject<Model.EmailReminderModel>(await response.Content.ReadAsStringAsync());

                if (lstEmailReminders.emailReminders.Count > 0)
                {
                    DateTime requestDate = DateTime.UtcNow;
                    foreach (var item in lstEmailReminders.emailReminders)
                    {
                        DateTime recurringDate = GetRecurringDate(requestDate, item.noOfDays, item.recurringTime);

                        EmailReminderLog emailReminderLog = new EmailReminderLog();

                        emailReminderLog.TenantId = tenantId;
                        emailReminderLog.JobTypeId = jobTypeId;
                        emailReminderLog.RequestDate = Convert.ToDateTime(requestDate.ToString("yyyy-MM-ddTHH:mm:00"));
                        emailReminderLog.LoanApplicationId = loanApplicationId;
                        emailReminderLog.RecurringDate = Convert.ToDateTime(recurringDate.ToString("yyyy-MM-ddTHH:mm:00"));
                        emailReminderLog.ReminderId = item.id;
                        emailReminderLog.IsActive = true;
                        emailReminderLog.IsDeleted = false;
                        emailReminderLog.IsEmailSent = false;
                        emailReminderLog.CreatedDate = DateTime.UtcNow;
                        emailReminderLog.TrackingState = TrackingState.Added;

                        Uow.Repository<EmailReminderLog>().Insert(emailReminderLog);
                        await Uow.SaveChangesAsync();
                    }
                }
                return true;
            }
            return false;
        }

        public async Task<bool> InsertLoanStatusLog(int tenantId, int jobTypeId, int loanApplicationId, int? statusId, IEnumerable<string> authHeader)
        {

            var jobType = await Uow.Repository<Entity.Models.JobType>().Query(x => x.Id == jobTypeId && x.IsActive == true).FirstOrDefaultAsync();

            if (jobType != null)
            {
                
                DateTime requestDate = DateTime.UtcNow;
                    
                DateTime recurringDate = GetRecurringDate(requestDate, 1, Convert.ToDateTime(requestDate.ToString("yyyy-MM-ddT06:00:00")));

                EmailReminderLog emailReminderLog = new EmailReminderLog();

                emailReminderLog.TenantId = tenantId;
                emailReminderLog.JobTypeId = jobTypeId;
                emailReminderLog.RequestDate = Convert.ToDateTime(requestDate.ToString("yyyy-MM-ddTHH:mm:00"));
                emailReminderLog.LoanApplicationId = loanApplicationId;
                emailReminderLog.RecurringDate = Convert.ToDateTime(recurringDate.ToString("yyyy-MM-ddTHH:mm:00"));
                emailReminderLog.LoanStatusId = statusId;
                emailReminderLog.IsActive = true;
                emailReminderLog.IsDeleted = false;
                emailReminderLog.IsEmailSent = false;
                emailReminderLog.CreatedDate = DateTime.UtcNow;
                emailReminderLog.TrackingState = TrackingState.Added;

                Uow.Repository<EmailReminderLog>().Insert(emailReminderLog);
                await Uow.SaveChangesAsync();
                    
                return true;
            }
            return false;
        }
        public async Task<int> InsertEmailReminderLogResponse(int emailReminderLogId, DateTime modifiedDate, bool isEmailSent, string response)
        {

            var emailReminderLog = await Uow.Repository<EmailReminderLog>().Query(x => x.Id == emailReminderLogId).FirstOrDefaultAsync();
            emailReminderLog.Id = emailReminderLogId;
            emailReminderLog.IsEmailSent = isEmailSent;
            emailReminderLog.Response = response;
            emailReminderLog.ModifiedDate = modifiedDate;
            emailReminderLog.TrackingState = TrackingState.Modified;
            Uow.Repository<EmailReminderLog>().Update(emailReminderLog);
            return await Uow.SaveChangesAsync();
        }

        private DateTime GetRecurringDate(DateTime requestDate, int noOfDays, DateTime recurringTime)
        {
            requestDate = requestDate.AddDays(noOfDays);

            var time = recurringTime.TimeOfDay;

            var date = new DateTime(requestDate.Year, requestDate.Month, requestDate.Day, time.Hours, time.Minutes, time.Seconds);

            return date;
        }
        public async Task<List<EmailReminderLogModel>> GetEmailreminderLogByDate(DateTime recurringTime, int tenantId, int jobTypeId)
        {
            return Uow.Repository<EmailReminderLog>().Query(x => !x.IsDeleted == true
                                                   && !x.IsEmailSent == true
                                                   && x.RecurringDate == recurringTime
                                                   && x.TenantId == tenantId
                                                   && x.JobTypeId == jobTypeId
                                                   && x.IsActive == true).Select(x => new Model.EmailReminderLogModel()
                                                   {
                                                       id = x.Id,
                                                       tenantId = x.TenantId,
                                                       jobTypeId = x.JobTypeId,
                                                       RecurringDate = x.RecurringDate,
                                                       loanApplicationId = x.LoanApplicationId,
                                                       IsActive = x.IsActive,
                                                       IsDeleted = x.IsDeleted,
                                                       ReminderId = x.ReminderId,
                                                       loanStatusId = x.LoanStatusId
                                                   }).ToList();
        }

        public void JobTrigger()
        {
            RecurringJob.AddOrUpdate("TestJobHourly", () => Console.WriteLine(), Cron.Hourly);
            RecurringJob.AddOrUpdate("TestJobMinuteInterval", () => Console.WriteLine(), Cron.MinuteInterval(30));
            RecurringJob.AddOrUpdate("TestJobMinutely", () => Console.WriteLine(), "*/5 * * * *");
        }

        public async Task DeleteEmailReminder(string id)
        {
            var emailReminderLogIds = Uow.Repository<EmailReminderLog>().Query(x => !x.IsDeleted == true
                                                    && x.RecurringDate >= DateTime.UtcNow
                                                    && x.ReminderId == id).Select(x => new Model.EmailReminderLogModel()
                                                    {
                                                        id = x.Id
                                                    }).ToList();

            if (emailReminderLogIds.Count > 0)
            {
                foreach (var emailReminderId in emailReminderLogIds)
                {
                    var emailReminderLog = await Uow.Repository<EmailReminderLog>().Query(x => x.Id == emailReminderId.id).FirstOrDefaultAsync();
                    emailReminderLog.IsDeleted = true;
                    emailReminderLog.ModifiedDate = DateTime.UtcNow;

                    emailReminderLog.TrackingState = TrackingState.Modified;

                    Uow.Repository<EmailReminderLog>().Update(emailReminderLog);
                    await Uow.SaveChangesAsync();
                }
            }
        }

        public async Task UpdateEmailReminder(string id, int noOfDays, DateTime recurringTime)
        {
            var lstEmailReminderLog = Uow.Repository<EmailReminderLog>().Query(x => !x.IsDeleted == true
                                                    && x.IsActive == true
                                                    && !x.IsEmailSent == true
                                                    && x.ReminderId == id).Select(x => new Model.EmailReminderLogModel()
                                                    {
                                                        id = x.Id,
                                                        requestDate = x.RequestDate,
                                                        RecurringDate = x.RecurringDate
                                                    }).ToList();

            if (lstEmailReminderLog.Count > 0)
            {
                foreach (var emailReminderLog in lstEmailReminderLog)
                {
                    DateTime recurringDate = Convert.ToDateTime(GetRecurringDate(emailReminderLog.requestDate, noOfDays, recurringTime).ToString("yyyy-MM-ddTHH:mm:00")).ToUniversalTime();
                    if (recurringDate > DateTime.UtcNow)
                    {
                        var emailReminder = await Uow.Repository<EmailReminderLog>().Query(x => x.Id == emailReminderLog.id).FirstOrDefaultAsync();
                        emailReminder.RecurringDate = recurringDate;
                        emailReminder.ModifiedDate = DateTime.UtcNow;

                        emailReminder.TrackingState = TrackingState.Modified;

                        Uow.Repository<EmailReminderLog>().Update(emailReminder);
                        await Uow.SaveChangesAsync();
                    }
                }
            }
        }

        public async Task EnableDisableEmailReminders(List<string> id, bool isActive)
        {
            for (int i = 0; i < id.Count; i++)
            {
                var emailReminderLogIds = Uow.Repository<EmailReminderLog>().Query(x => !x.IsDeleted == true
                                                   && x.RecurringDate >= DateTime.UtcNow
                                                   && x.ReminderId == id[i]).Select(x => new Model.EmailReminderLogModel()
                                                   {
                                                       id = x.Id
                                                   }).ToList();

                if (emailReminderLogIds.Count > 0)
                {
                    foreach (var emailReminderId in emailReminderLogIds)
                    {
                        var emailReminderLog = await Uow.Repository<EmailReminderLog>().Query(x => x.Id == emailReminderId.id).FirstOrDefaultAsync();
                        emailReminderLog.IsActive = isActive;
                        emailReminderLog.ModifiedDate = DateTime.UtcNow;

                        emailReminderLog.TrackingState = TrackingState.Modified;

                        Uow.Repository<EmailReminderLog>().Update(emailReminderLog);
                        await Uow.SaveChangesAsync();
                    }
                }
            }
        }
        public async Task EnableDisableEmailReminders(List<int> id, bool isActive)
        {
            for (int i = 0; i < id.Count; i++)
            {
                var emailReminderLogIds = Uow.Repository<EmailReminderLog>().Query(x => !x.IsDeleted == true
                                                   && x.RecurringDate >= DateTime.UtcNow
                                                   && x.LoanStatusId == id[i]).Select(x => new Model.EmailReminderLogModel()
                                                   {
                                                       id = x.Id
                                                   }).ToList();

                if (emailReminderLogIds.Count > 0)
                {
                    foreach (var emailReminderId in emailReminderLogIds)
                    {
                        var emailReminderLog = await Uow.Repository<EmailReminderLog>().Query(x => x.Id == emailReminderId.id).FirstOrDefaultAsync();
                        emailReminderLog.IsActive = isActive;
                        emailReminderLog.ModifiedDate = DateTime.UtcNow;

                        emailReminderLog.TrackingState = TrackingState.Modified;

                        Uow.Repository<EmailReminderLog>().Update(emailReminderLog);
                        await Uow.SaveChangesAsync();
                    }
                }
            }
        }
        public async Task EnableDisableAllEmailReminders(bool isActive, int jobTypeId)
        {
            var jobType = await Uow.Repository<Entity.Models.JobType>().Query(x => x.Id == jobTypeId).FirstOrDefaultAsync();

            jobType.IsActive = isActive;
            Uow.Repository<Entity.Models.JobType>().Update(jobType);
            await Uow.SaveChangesAsync();
        }

        public async Task<JobTypeModel> GetJobType(int id, int tenantId)
        {
             return Uow.Repository<Entity.Models.JobType>().Query(x => x.Id == id && x.TenantId == tenantId).Select(x => new JobTypeModel()
                                                   {
                                                       id = x.Id,
                                                       name = x.Name,
                                                       isActive = x.IsActive
                                                   }).FirstOrDefault();
        }

        public async Task EnableDisableEmailRemindersbyLAId(List<int> laIds, bool isActive)
        {
            for (int i = 0; i < laIds.Count; i++)
            {
                var emailReminderLogIds = Uow.Repository<EmailReminderLog>().Query(x => !x.IsDeleted == true
                                                   && x.RecurringDate >= DateTime.UtcNow
                                                   && x.LoanApplicationId == laIds[i]).Select(x => new Model.EmailReminderLogModel()
                                                   {
                                                       id = x.Id
                                                   }).ToList();

                if (emailReminderLogIds.Count > 0)
                {
                    foreach (var emailReminderId in emailReminderLogIds)
                    {
                        var emailReminderLog = await Uow.Repository<EmailReminderLog>().Query(x => x.Id == emailReminderId.id).FirstOrDefaultAsync();
                        emailReminderLog.IsActive = isActive;
                        emailReminderLog.ModifiedDate = DateTime.UtcNow;

                        emailReminderLog.TrackingState = TrackingState.Modified;

                        Uow.Repository<EmailReminderLog>().Update(emailReminderLog);
                        await Uow.SaveChangesAsync();
                    }
                }
            }
        }
    }
}
