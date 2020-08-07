using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RainMaker.Common;
using RainMaker.Common.Extensions;
using RainMaker.Data;
using RainMaker.Entity.Models;
using Rainmaker.Model;
using RainMaker.Service;
using URF.Core.Abstractions;

namespace Rainmaker.Service
{
    public class LoanApplicationService : ServiceBase<RainMakerContext, LoanApplication>, ILoanApplicationService
    {
        [Flags]
        public enum RelatedEntity
        {
            Borrowers = 1 << 0
        }


        public LoanApplicationService(IUnitOfWork<RainMakerContext> previousUow,
                                      IServiceProvider services) : base(previousUow: previousUow,
                                                                        services: services)
        {
        }


        public async Task<LoanSummary> GetLoanSummary(int loanApplicationId,
                                                      int userProfileId)
        {
            return await Repository
                         .Query(query: x =>
                                    x.Opportunity.OpportunityLeadBinders
                                     .Where(y => y.OwnTypeId == (int) OwnTypeEnum.PrimaryContact).FirstOrDefault().Customer
                                     .UserId == userProfileId && x.Id == loanApplicationId)
                         .Include(navigationPropertyPath: x => x.PropertyInfo)
                         .ThenInclude(navigationPropertyPath: x => x.PropertyType)
                         .Include(navigationPropertyPath: x => x.PropertyInfo)
                         .ThenInclude(navigationPropertyPath: x => x.AddressInfo)
                         .ThenInclude(navigationPropertyPath: x => x.State)
                         .Include(navigationPropertyPath: x => x.LoanPurpose)
                         .Include(navigationPropertyPath: x => x.StatusList)
                         .Include(navigationPropertyPath: x => x.Opportunity)
                         .ThenInclude(navigationPropertyPath: x => x.OpportunityLeadBinders)
                         .ThenInclude(navigationPropertyPath: x => x.Customer)
                         .Select(selector: x => new LoanSummary
                                                {
                                                    CityName = x.PropertyInfo.AddressInfo.CityName,
                                                    CountyName = x.PropertyInfo.AddressInfo.CountyName,
                                                    LoanAmount = x.LoanAmount,
                                                    LoanPurpose = x.LoanPurpose.Description,
                                                    PropertyType = x.PropertyInfo.PropertyType.Description,
                                                    StateName =
                                                        x.PropertyInfo.AddressInfo.StateId == null ||
                                                        x.PropertyInfo.AddressInfo.StateId == 0
                                                            ? x.PropertyInfo.AddressInfo.StateName
                                                            : x.PropertyInfo.AddressInfo.State.Abbreviation,
                                                    StreetAddress = x.PropertyInfo.AddressInfo.StreetAddress,
                                                    ZipCode = x.PropertyInfo.AddressInfo.ZipCode,
                                                    CountryName = x.PropertyInfo.AddressInfo.CountryName,
                                                    UnitNumber = x.PropertyInfo.AddressInfo.UnitNo
                                                }).FirstOrDefaultAsync();
        }


        public async Task<AdminLoanSummary> GetAdminLoanSummary(int loanApplicationId)
        {
            return await Repository.Query(query: x => x.Id == loanApplicationId)
                                   .Include(navigationPropertyPath: x => x.PropertyInfo)
                                   .ThenInclude(navigationPropertyPath: x => x.PropertyType)
                                   .Include(navigationPropertyPath: x => x.PropertyInfo)
                                   .ThenInclude(navigationPropertyPath: x => x.AddressInfo)
                                   .ThenInclude(navigationPropertyPath: x => x.State)
                                   .Include(navigationPropertyPath: x => x.LoanPurpose)
                                   .Include(navigationPropertyPath: x => x.StatusList)
                                   .Include(navigationPropertyPath: x => x.Borrowers)
                                   .ThenInclude(navigationPropertyPath: x => x.LoanContact)
                                   .Include(navigationPropertyPath: x => x.Product)
                                   .Include(navigationPropertyPath: x => x.Opportunity)
                                   .ThenInclude(navigationPropertyPath: x => x.OpportunityLockStatusLogs)
                                   .ThenInclude(navigationPropertyPath: x => x.LockStatusList)
                                   .Include(navigationPropertyPath: x => x.Opportunity)
                                   .ThenInclude(navigationPropertyPath: x => x.StatusList)
                                   .Select(selector: x => new AdminLoanSummary
                                                          {
                                                              CityName = x.PropertyInfo.AddressInfo.CityName,
                                                              CountyName = x.PropertyInfo.AddressInfo.CountyName,
                                                              LoanAmount = x.LoanAmount,
                                                              LoanPurpose = x.LoanPurpose.Description,
                                                              PropertyType = x.PropertyInfo.PropertyType.Description,
                                                              StateName =
                                                                  x.PropertyInfo.AddressInfo.StateId == null ||
                                                                  x.PropertyInfo.AddressInfo.StateId == 0
                                                                      ? x.PropertyInfo.AddressInfo.StateName
                                                                      : x.PropertyInfo.AddressInfo.State.Abbreviation,
                                                              StreetAddress = x.PropertyInfo.AddressInfo.StreetAddress,
                                                              ZipCode = x.PropertyInfo.AddressInfo.ZipCode,
                                                              CountryName = x.PropertyInfo.AddressInfo.CountryName,
                                                              UnitNumber = x.PropertyInfo.AddressInfo.UnitNo,
                                                              Status = x.Opportunity.StatusList.Name,
                                                              Borrowers = x
                                                                          .Borrowers.OrderBy(y => y.OwnTypeId)
                                                                          .Select(y =>
                                                                                      (string
                                                                                          .IsNullOrEmpty(y.LoanContact
                                                                                                          .FirstName)
                                                                                          ? ""
                                                                                          : y.LoanContact.FirstName) +
                                                                                      " " +
                                                                                      (string
                                                                                          .IsNullOrEmpty(y.LoanContact
                                                                                                          .LastName)
                                                                                          ? ""
                                                                                          : y.LoanContact.LastName))
                                                                          .ToList(),
                                                              LoanNumber = x.LoanNumber,
                                                              ExpectedClosingDate = x.ExpectedClosingDate,
                                                              PopertyValue = x.PropertyInfo.PropertyValue,
                                                              LoanProgram = x.Product.AliasName,
                                                              Rate = null,
                                                              LockStatus =
                                                                  x.Opportunity.OpportunityLockStatusLogs
                                                                   .OrderByDescending(y => y.Id).FirstOrDefault()
                                                                   .LockStatusList.Name,
                                                              LockDate = x
                                                                         .Opportunity.OpportunityLockStatusLogs
                                                                         .OrderByDescending(y => y.Id).FirstOrDefault()
                                                                         .CreatedOnUtc.SpecifyKind(DateTimeKind.Utc),
                                                              ExpirationDate = null
                                                          }).FirstOrDefaultAsync();
        }


        public List<LoanApplication> GetLoanApplicationWithDetails(int? id = null,
                                                                   string encompassNumber = "",
                                                                   RelatedEntity? includes = null)
        {
            var loanApplications = Repository.Query().AsQueryable();

            // @formatter:off 

            if (id.HasValue()) loanApplications = loanApplications.Where(predicate: loanApplication => loanApplication.Id == id);
            if (encompassNumber.HasValue()) loanApplications = loanApplications.Where(predicate: loanApplication =>loanApplication.EncompassNumber == encompassNumber);

            // @formatter:on 

            if (includes.HasValue)
                loanApplications = ProcessIncludes(query: loanApplications,
                                                   includes: includes.Value);

            return loanApplications.ToList();
        }


        private IQueryable<LoanApplication> ProcessIncludes(IQueryable<LoanApplication> query,
                                                            RelatedEntity includes)
        {
            // @formatter:off 
            if (includes.HasFlag(flag: RelatedEntity.Borrowers)) query = query.Include(navigationPropertyPath: loanApplication => loanApplication.Borrowers);
            // @formatter:on 
            return query;
        }


        public async Task<LoanOfficer> GetLOInfo(int loanApplicationId,
                                                 int businessUnitId,
                                                 int userProfileId)
        {
            return await Repository.Query(query: x =>
                                              x.Opportunity.OpportunityLeadBinders
                                               .Where(y => y.OwnTypeId == (int) OwnTypeEnum.PrimaryContact).FirstOrDefault()
                                               .Customer.UserId == userProfileId && x.Id == loanApplicationId &&
                                              x.BusinessUnit.Id == businessUnitId)
                                   .Include(navigationPropertyPath: x => x.Opportunity)
                                   .ThenInclude(navigationPropertyPath: x => x.Employee)
                                   .ThenInclude(navigationPropertyPath: x => x.Contact)
                                   .Include(navigationPropertyPath: x => x.Opportunity)
                                   .ThenInclude(navigationPropertyPath: x => x.Employee)
                                   .ThenInclude(navigationPropertyPath: x => x.EmployeeBusinessUnitEmails)
                                   .ThenInclude(navigationPropertyPath: x => x.EmailAccount)
                                   .Include(navigationPropertyPath: x => x.Opportunity)
                                   .ThenInclude(navigationPropertyPath: x => x.OpportunityLeadBinders)
                                   .ThenInclude(navigationPropertyPath: x => x.Customer)
                                   .Include(navigationPropertyPath: x => x.BusinessUnit)
                                   .Include(navigationPropertyPath: x => x.Opportunity)
                                   .ThenInclude(navigationPropertyPath: x => x.Employee)
                                   .ThenInclude(navigationPropertyPath: x => x.EmployeePhoneBinders)
                                   .ThenInclude(navigationPropertyPath: x => x.CompanyPhoneInfo)
                                   .Select(selector: x =>
                                               new LoanOfficer
                                               {
                                                   Email = x.Opportunity.Employee.EmployeeBusinessUnitEmails
                                                            .Where(y => y.BusinessUnitId == businessUnitId).FirstOrDefault()
                                                            .EmailAccount.Email,
                                                   FirstName = x.Opportunity.Employee.Contact.FirstName,
                                                   LastName = x.Opportunity.Employee.Contact.LastName,
                                                   NMLS = x.Opportunity.Employee.NmlsNo,
                                                   Phone = x.Opportunity.Employee.EmployeePhoneBinders
                                                            .Where(y => y.TypeId == 3).FirstOrDefault().CompanyPhoneInfo.Phone,
                                                   Photo = x.Opportunity.Employee.Photo,
                                                   WebUrl = x.BusinessUnit.WebUrl + "/lo/" +
                                                            x.Opportunity.Employee.CmsName
                                               }
                                          ).FirstOrDefaultAsync();
        }


        public async Task<LoanOfficer> GetDbaInfo(int businessUnitId)
        {
            var businessUnit = await Uow.Repository<BusinessUnit>()
                                        .Query(query: x =>
                                                   x.Id == businessUnitId &&
                                                   x.BusinessUnitPhones.Where(y => y.TypeId == 3).Count() > 0)
                                        .Include(navigationPropertyPath: x => x.EmailAccount)
                                        .Include(navigationPropertyPath: x => x.BusinessUnitPhones)
                                        .ThenInclude(navigationPropertyPath: x => x.CompanyPhoneInfo)
                                        .Select(selector: x => new
                                                               {
                                                                   x.Name,
                                                                   x.BusinessUnitPhones.FirstOrDefault()
                                                                    .CompanyPhoneInfo.Phone,
                                                                   x.EmailAccount.Email,
                                                                   x.WebUrl,
                                                                   x.Logo
                                                               })
                                        .FirstOrDefaultAsync();
            //var nmls = (await Uow.Repository<Branch>().Query(x => x.Id == 1).FirstOrDefaultAsync()).NmlsNo;
            return new LoanOfficer
                   {
                       Email = businessUnit.Email,
                       FirstName = businessUnit.Name,
                       LastName = string.Empty,
                       NMLS = null, //nmls,
                       Phone = businessUnit.Phone,
                       Photo = businessUnit.Logo,
                       WebUrl = businessUnit.WebUrl
                   };
        }


        public async Task<PostModel> PostLoanApplication(int loanApplicationId,
                                                         bool isDraft,
                                                         int userProfileId,
                                                         IOpportunityService opportunityService)
        {
            var postModel = await Uow.Repository<LoanApplication>().Query(query: x => x.Id == loanApplicationId)
                                     .Include(navigationPropertyPath: x => x.Opportunity)
                                     .ThenInclude(navigationPropertyPath: x => x.OpportunityLeadBinders)
                                     .ThenInclude(navigationPropertyPath: x => x.Customer)
                                     .ThenInclude(navigationPropertyPath: x => x.Contact)
                                     .Select(selector: x => new PostModel
                                                            {
                                                                userId = x.Opportunity.OpportunityLeadBinders
                                                                          .Where(y => y.OwnTypeId ==
                                                                                      (int) OwnTypeEnum
                                                                                          .PrimaryContact).First()
                                                                          .Customer.UserId,
                                                                userName =
                                                                    x.Opportunity.OpportunityLeadBinders
                                                                     .Where(y => y.OwnTypeId ==
                                                                                 (int) OwnTypeEnum.PrimaryContact)
                                                                     .First().Customer.Contact.FirstName + " " +
                                                                    x.Opportunity.OpportunityLeadBinders
                                                                     .Where(y => y.OwnTypeId ==
                                                                                 (int) OwnTypeEnum.PrimaryContact)
                                                                     .First().Customer.Contact.LastName
                                                            }).FirstOrDefaultAsync();
            if (!isDraft)
                await ChangeStatus(loanApplicationId: loanApplicationId,
                                   userProfileId: userProfileId,
                                   _opportunityservice: opportunityService);
            return postModel;
        }


        private async Task ChangeStatus(int loanApplicationId,
                                        int userProfileId,
                                        IOpportunityService _opportunityservice)
        {
            var loanApplication = await Uow.Repository<LoanApplication>().Query(query: x => x.Id == loanApplicationId)
                                           .FirstOrDefaultAsync();

            var lockStatusId = EnumLockStatusList.Float.ToInt();
            var statusId = StatusListEnum.DocumentUpload.ToInt();

            var opportunity = await _opportunityservice.GetByIdAsync(id: loanApplication.OpportunityId.Value);
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
                                    EntityTypeId = Constants.GetEntityType(t: typeof(OpportunityStatusLog))
                                };
                    _opportunityservice.InsertOpportunityStatusLog(opportunityStatusLog: statuslog);
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
                                        EntityTypeId = Constants.GetEntityType(t: typeof(OpportunityLockStatusLog))
                                    };
                    _opportunityservice.InsertOpportunityLockStatusLog(opportunityLockStatusLog: lockStatusLog);
                }

                opportunity.StatusId = statusId;
                opportunity.StatusCauseId = null;
                opportunity.LockStatusId = lockStatusId;
                opportunity.LockCauseId = null;
                opportunity.ModifiedBy = CurrentUserId;
                opportunity.ModifiedOnUtc = DateTime.UtcNow;
                opportunity.TpId = null;
                _opportunityservice.Update(item: opportunity);
                await _opportunityservice.SaveChangesAsync();
            }
        }


        public async Task<LoanApplicationModel> GetByLoanApplicationId(int loanId)
        {
            return await Repository.Query(query: x => x.Id == loanId)
                                   .Include(navigationPropertyPath: x => x.Opportunity)
                                   .Select(selector: x => new LoanApplicationModel
                                                          {
                                                              OpportunityId = x.OpportunityId,
                                                              LoanRequestId = x.Opportunity.LoanRequestId,
                                                              BusinessUnitId = x.Opportunity.BusinessUnitId
                                                          }).FirstOrDefaultAsync();
        }
    }
}