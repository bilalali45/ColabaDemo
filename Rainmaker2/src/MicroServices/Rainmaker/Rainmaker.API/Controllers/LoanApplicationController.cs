using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Rainmaker.Model;
using Rainmaker.Service;
using Rainmaker.Service.Helpers;
using RainMaker.Common;
using RainMaker.Common.FTP;
using RainMaker.Entity.Models;
using RainMaker.Service;
using RainMaker.Common.Extensions;
using Rainmaker.Model.LoanApplication;
using TrackableEntities.Common.Core;


namespace Rainmaker.API.Controllers
{
    [ApiController]
    [Route("api/RainMaker/[controller]")]
    public class LoanApplicationController : Controller
    {
        private readonly ILoanApplicationService loanApplicationService;
        private readonly ICommonService commonService;
        private readonly IFtpHelper ftp;
        private readonly IOpportunityService opportunityService;
        private readonly IActivityService activityService;
        private readonly IWorkQueueService workQueueService;
        public LoanApplicationController(ILoanApplicationService loanApplicationService,ICommonService commonService, IFtpHelper ftp, IOpportunityService opportunityService, IActivityService activityService, IWorkQueueService workQueueService)
        {
            this.loanApplicationService = loanApplicationService;
            this.commonService = commonService;
            this.ftp = ftp;
            this.opportunityService = opportunityService;
            this.activityService = activityService;
            this.workQueueService = workQueueService;
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLoanInfo(int loanApplicationId)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            var loanApplication = await loanApplicationService.GetLoanSummary(loanApplicationId, userProfileId);
            return Ok(loanApplication);
        }
        [Authorize(Roles = "Customer")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLOInfo(int loanApplicationId)
        {   
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            int businessUnitId = (await loanApplicationService.GetByLoanApplicationId(loanApplicationId)).BusinessUnitId.Value;
            Rainmaker.Model.LoanOfficer lo = await loanApplicationService.GetLOInfo(loanApplicationId,businessUnitId,userProfileId);
            if(lo==null || lo.FirstName==null)
            {
                lo = await loanApplicationService.GetDbaInfo(businessUnitId);
            }
            return Ok(lo);
        }
        [Authorize(Roles = "Customer")]
        [HttpPost("[action]")]
        public async Task<IActionResult> GetByLoanApplicationId([FromBody] GetLoanApplicationModel model)
        {
            return Ok(await loanApplicationService.GetByLoanApplicationId(model.loanApplicationId));
        }
        [Authorize(Roles = "MCU")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetLoanApplication([FromQuery] string encompassNumber)
        {
           var loanApplication =   loanApplicationService.GetLoanApplicationWithDetails(encompassNumber: encompassNumber).SingleOrDefault();
            return Ok(loanApplication);
        }

        [Authorize(Roles = "Customer")]
        [HttpGet("[action]")]
        public async Task<string> GetPhoto(string photo, int loanApplicationId)
        {
            int businessUnitId = (await loanApplicationService.GetByLoanApplicationId(loanApplicationId)).BusinessUnitId.Value;
            var remoteFilePath = await commonService.GetSettingValueByKeyAsync<string>(SystemSettingKeys.FtpEmployeePhotoFolder, businessUnitId) + "/" + photo;
            Stream imageData = null;
            try
            {
                imageData = await ftp.DownloadStream(remoteFilePath);
            }
            catch
            {
            }
            if (imageData == null)
            {
                imageData = new FileStream("Content\\images\\default-LO.jpg", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            }
            using MemoryStream ms = new MemoryStream();
            imageData.CopyTo(ms);
            imageData.Close();
            return Convert.ToBase64String(ms.ToArray());
        }

        [Authorize(Roles = "MCU")]
        [HttpPost("[action]")]
        public async Task<IActionResult> PostLoanApplication([FromBody]PostLoanApplicationModel model)
        {
            int userProfileId = int.Parse(User.FindFirst("UserProfileId").Value.ToString());
            return Ok(await loanApplicationService.PostLoanApplication(model.loanApplicationId, model.isDraft, userProfileId,opportunityService));
        }

        [Authorize(Roles = "MCU")]
        [HttpPost("[action]")]
        public async Task<IActionResult> SendBorrowerEmail([FromBody]SendBorrowerEmailModel model)
        {
            var loanApplication = await loanApplicationService.GetByLoanApplicationId(model.loanApplicationId);
            var activityEnumType = (ActivityForType)model.activityForId;

            var data = new Dictionary<FillKey, string>();
            data.Add(FillKey.CustomEmailHeader, "");
            data.Add(FillKey.CustomEmailFooter, "");
            data.Add(FillKey.EmailBody, model.emailBody.Replace(Environment.NewLine, "<br/>"));

            await SendLoanApplicationActivityEmail(data, loanApplication.OpportunityId.ToInt(), loanApplication.LoanRequestId.ToInt(), loanApplication.BusinessUnitId.ToInt(), activityEnumType);
            return Ok();
        }

        private async Task SendLoanApplicationActivityEmail(Dictionary<FillKey, string> data, int opportunityId, int loanRequestId, int businessUnitId, ActivityForType emailtype)
        {
            var activity = await activityService.GetCustomerActivity(businessUnitId, emailtype);
            if (activity != null)
            {
                var rnd = new Random();

                var random = rnd.Next(100, 1000);
                
                var witem = new WorkQueue
                {
                    CampaignId = null,
                    ActivityId = activity.Id,
                    ActivityTypeId = activity.ActivityTypeId,
                    CreatedBy = 1,//todo: employee userid
                    CreatedOnUtc = DateTime.UtcNow,
                    EntityRefId = opportunityId,
                    EntityRefTypeId = Constants.GetEntityType(typeof(Opportunity)),
                    EntityTypeId = Constants.GetEntityType(typeof(WorkQueue)),
                    IsActive = true,
                    IsDeleted = false,
                    RandomNo = random,
                    LoanRequestId = loanRequestId,
                    Code = activity.Id.ToString(System.Globalization.CultureInfo.InvariantCulture).Encrypt(random.ToString(System.Globalization.CultureInfo.InvariantCulture)),
                    ScheduleDateUtc = DateTime.UtcNow,
                    IsCustom = false

                };
                foreach (var key in data)
                {
                    var keySymbol = EmailTemplateKeys.GetKeySymbol(key.Key);

                    if (!string.IsNullOrEmpty(keySymbol))
                        witem.WorkQueueKeyValues.Add(new WorkQueueKeyValue { KeyName = keySymbol, Value = key.Value, TrackingState = TrackingState.Added});
                }
                workQueueService.Insert(witem);
                await workQueueService.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Activity not found for Business unit");
            }
        }
    }
}