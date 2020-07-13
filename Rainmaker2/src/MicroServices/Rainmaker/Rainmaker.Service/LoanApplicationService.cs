using Microsoft.EntityFrameworkCore;
using Rainmaker.Model;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using URF.Core.Abstractions;
using System.Linq;
using RainMaker.Common;
using RainMaker.Common.Extensions;

namespace Rainmaker.Service
{
    public class LoanApplicationService : ServiceBase<RainMakerContext,LoanApplication>, ILoanApplicationService
    {
        public LoanApplicationService(IUnitOfWork<RainMakerContext> previousUow, IServiceProvider services) : base(previousUow,services)
        {
        }

        public async Task<LoanSummary> GetLoanSummary(int loanApplicationId, int userProfileId)
        {
            return await Repository.Query(x => x.Opportunity.OpportunityLeadBinders.Where(y=>y.OwnTypeId==(int)OwnTypeEnum.PrimaryContact).First().Customer.UserId==userProfileId && x.Id == loanApplicationId).Include(x => x.PropertyInfo).ThenInclude(x => x.PropertyType)
                .Include(x => x.PropertyInfo).ThenInclude(x => x.AddressInfo).ThenInclude(x=>x.State)
                .Include(x => x.LoanPurpose)
                .Include(x => x.StatusList)
                .Include(x=>x.Opportunity).ThenInclude(x=>x.OpportunityLeadBinders).ThenInclude(x=>x.Customer)
                .Select(x => new LoanSummary{
                    CityName = x.PropertyInfo.AddressInfo.CityName,
                    CountyName = x.PropertyInfo.AddressInfo.CountyName,
                    LoanAmount = x.LoanAmount,
                    LoanPurpose = x.LoanPurpose.Description,
                    PropertyType = x.PropertyInfo.PropertyType.Description,
                    StateName = (x.PropertyInfo.AddressInfo.StateId==null || x.PropertyInfo.AddressInfo.StateId==0) ? x.PropertyInfo.AddressInfo.StateName : x.PropertyInfo.AddressInfo.State.Abbreviation,
                    StreetAddress = x.PropertyInfo.AddressInfo.StreetAddress,
                    ZipCode = x.PropertyInfo.AddressInfo.ZipCode,
                    CountryName = x.PropertyInfo.AddressInfo.CountryName,
                    UnitNumber = x.PropertyInfo.AddressInfo.UnitNo
                }).FirstOrDefaultAsync();
        }


        public async Task<AdminLoanSummary> GetAdminLoanSummary(int loanApplicationId)
        {
            return await Repository.Query(x => x.Id == loanApplicationId)
                .Include(x => x.PropertyInfo).ThenInclude(x => x.PropertyType)
                .Include(x => x.PropertyInfo).ThenInclude(x => x.AddressInfo).ThenInclude(x => x.State)
                .Include(x => x.LoanPurpose)
                .Include(x => x.StatusList)
                .Include(x=>x.Borrowers).ThenInclude(x=>x.LoanContact)
                .Include(x=>x.Product)
                .Include(x=>x.Opportunity).ThenInclude(x=>x.OpportunityLockStatusLogs).ThenInclude(x=>x.LockStatusList)
                .Select(x => new AdminLoanSummary
                {
                    CityName = x.PropertyInfo.AddressInfo.CityName,
                    CountyName = x.PropertyInfo.AddressInfo.CountyName,
                    LoanAmount = x.LoanAmount,
                    LoanPurpose = x.LoanPurpose.Description,
                    PropertyType = x.PropertyInfo.PropertyType.Description,
                    StateName = (x.PropertyInfo.AddressInfo.StateId == null || x.PropertyInfo.AddressInfo.StateId == 0) ? x.PropertyInfo.AddressInfo.StateName : x.PropertyInfo.AddressInfo.State.Abbreviation,
                    StreetAddress = x.PropertyInfo.AddressInfo.StreetAddress,
                    ZipCode = x.PropertyInfo.AddressInfo.ZipCode,
                    CountryName = x.PropertyInfo.AddressInfo.CountryName,
                    UnitNumber = x.PropertyInfo.AddressInfo.UnitNo,
                    Status = x.StatusList.Name,
                    Borrowers = x.Borrowers.OrderBy(y=>y.OwnTypeId).Select(y=>(string.IsNullOrEmpty(y.LoanContact.FirstName)? "" : y.LoanContact.FirstName)+" "+(string.IsNullOrEmpty(y.LoanContact.LastName) ? "" : y.LoanContact.LastName)).ToList(),
                    LoanNumber = x.LoanNumber,
                    ExpectedClosingDate = x.ExpectedClosingDate,
                    PopertyValue = x.PropertyInfo.PropertyValue,
                    LoanProgram = x.Product.AliasName,
                    Rate = null,
                    LockStatus=x.Opportunity.OpportunityLockStatusLogs.OrderByDescending(y=>y.Id).First().LockStatusList.Name,
                    LockDate= x.Opportunity.OpportunityLockStatusLogs.OrderByDescending(y => y.Id).First().CreatedOnUtc.SpecifyKind(DateTimeKind.Utc),
                    ExpirationDate=null
                }).FirstOrDefaultAsync();
        }


        public async Task<LoanOfficer> GetLOInfo(int loanApplicationId, int businessUnitId, int userProfileId)
        {
            return await Repository.Query(x => x.Opportunity.OpportunityLeadBinders.Where(y => y.OwnTypeId == (int)OwnTypeEnum.PrimaryContact).First().Customer.UserId == userProfileId && x.Id == loanApplicationId && x.BusinessUnit.Id == businessUnitId)
                .Include(x => x.Opportunity).ThenInclude(x => x.Employee).ThenInclude(x=>x.Contact)
                .Include(x => x.Opportunity).ThenInclude(x => x.Employee)
                .ThenInclude(x => x.EmployeeBusinessUnitEmails).ThenInclude(x => x.EmailAccount)
                .Include(x => x.Opportunity).ThenInclude(x => x.OpportunityLeadBinders).ThenInclude(x => x.Customer)
                .Include(x => x.BusinessUnit)
                .Include(x => x.Opportunity).ThenInclude(x => x.Employee)
                .ThenInclude(x => x.EmployeePhoneBinders).ThenInclude(x => x.CompanyPhoneInfo)
                .Select(x =>
                    new LoanOfficer()
                    {
                        Email = x.Opportunity.Employee.EmployeeBusinessUnitEmails.Where(y=>y.BusinessUnitId==businessUnitId).First().EmailAccount.Email,
                        FirstName = x.Opportunity.Employee.Contact.FirstName,
                        LastName = x.Opportunity.Employee.Contact.LastName,
                        NMLS = x.Opportunity.Employee.NmlsNo,
                        Phone = x.Opportunity.Employee.EmployeePhoneBinders.Where(y=>y.TypeId==3).First().CompanyPhoneInfo.Phone,
                        Photo = x.Opportunity.Employee.Photo,
                        WebUrl = x.BusinessUnit.WebUrl+"/lo/"+x.Opportunity.Employee.CmsName
                    }
                ).FirstOrDefaultAsync();
        }

        public async Task<LoanOfficer> GetDbaInfo(int businessUnitId)
        {
            var businessUnit = await Uow.Repository<BusinessUnit>().Query(x => x.Id == businessUnitId && x.BusinessUnitPhones.Where(y => y.TypeId == 3).Count() > 0)
                .Include(x => x.EmailAccount)
                .Include(x => x.BusinessUnitPhones).ThenInclude(x => x.CompanyPhoneInfo)
                .Select(x => new
                {
                    x.Name,
                    x.BusinessUnitPhones.FirstOrDefault().CompanyPhoneInfo.Phone,
                    x.EmailAccount.Email,
                    x.WebUrl,
                    x.Logo
                })
                .FirstOrDefaultAsync();
            //var nmls = (await Uow.Repository<Branch>().Query(x => x.Id == 1).FirstOrDefaultAsync()).NmlsNo;
            return new LoanOfficer()
            {
                Email = businessUnit.Email,
                FirstName = businessUnit.Name,
                LastName = string.Empty,
                NMLS = null,//nmls,
                Phone = businessUnit.Phone,
                Photo = businessUnit.Logo,
                WebUrl = businessUnit.WebUrl
            };
        }

        public async Task<PostModel> PostLoanApplication(int loanApplicationId, bool isDraft, int userProfileId, IOpportunityService opportunityService)
        {
            var postModel = await Uow.Repository<LoanApplication>().Query(x => x.Id == loanApplicationId)
                .Include(x => x.Opportunity).ThenInclude(x => x.OpportunityLeadBinders).ThenInclude(x => x.Customer)
                .ThenInclude(x => x.Contact)
                .Select(x => new PostModel()
                {
                    userId = x.Opportunity.OpportunityLeadBinders.Where(y => y.OwnTypeId == (int)OwnTypeEnum.PrimaryContact).First().Customer.UserId,
                    userName = x.Opportunity.OpportunityLeadBinders.Where(y => y.OwnTypeId == (int)OwnTypeEnum.PrimaryContact).First().Customer.Contact.FirstName + " " + x.Opportunity.OpportunityLeadBinders.Where(y => y.OwnTypeId == (int)OwnTypeEnum.PrimaryContact).First().Customer.Contact.LastName
                }).FirstOrDefaultAsync();
            if (!isDraft)
            {
                await ChangeStatus(loanApplicationId,userProfileId,opportunityService);
            }
            return postModel;
        }

        private async Task ChangeStatus(int loanApplicationId, int userProfileId, IOpportunityService _opportunityservice)
        {
            var loanApplication = await Uow.Repository<LoanApplication>().Query(x => x.Id == loanApplicationId).FirstOrDefaultAsync();

            int lockStatusId = EnumLockStatusList.Float.ToInt();
            int statusId = StatusListEnum.DocumentUpload.ToInt();

            var opportunity = await _opportunityservice.GetByIdAsync(loanApplication.OpportunityId.Value);
            OpportunityStatusLog statuslog = null;
            OpportunityLockStatusLog lockStatusLog = null;

            if (opportunity != null)
            {
                //status log
                if (statusId != opportunity.StatusId)
                {
                    statuslog = new OpportunityStatusLog
                    {
                        StatusId = statusId,
                        DatetimeUtc = DateTime.UtcNow,
                        OpportunityId = opportunity.Id,
                        IsActive = true,
                        StatusCauseId = null,
                        ModifiedBy = userProfileId,
                        ModifiedOnUtc = DateTime.UtcNow,
                        CreatedBy = userProfileId,
                        CreatedOnUtc = DateTime.UtcNow,
                        EntityTypeId = Constants.GetEntityType(typeof(OpportunityStatusLog))
                    };
                    _opportunityservice.InsertOpportunityStatusLog(statuslog);
                }

                //Lock Status Log
                if (lockStatusId != opportunity.LockStatusId)
                {
                    lockStatusLog = new OpportunityLockStatusLog
                    {
                        LockStatusId = lockStatusId,
                        LockCauseId = null,
                        DatetimeUtc = DateTime.UtcNow,
                        OpportunityId = opportunity.Id,
                        IsActive = true,
                        ModifiedBy = userProfileId,
                        ModifiedOnUtc = DateTime.UtcNow,
                        CreatedBy = userProfileId,
                        CreatedOnUtc = DateTime.UtcNow,
                        EntityTypeId = Constants.GetEntityType(typeof(OpportunityLockStatusLog))

                    };
                    _opportunityservice.InsertOpportunityLockStatusLog(lockStatusLog);
                }

                opportunity.StatusId = statusId;
                opportunity.StatusCauseId = null;
                opportunity.LockStatusId = lockStatusId;
                opportunity.LockCauseId = null;
                opportunity.ModifiedBy = CurrentUserId;
                opportunity.ModifiedOnUtc = DateTime.UtcNow;
                opportunity.TpId = null;
                _opportunityservice.Update(opportunity);
                await _opportunityservice.SaveChangesAsync();
            }
        }
    }
}
