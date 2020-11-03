using Microsoft.EntityFrameworkCore;
using Rainmaker.Model;
using RainMaker.Common;
using RainMaker.Common.Extensions;
using RainMaker.Data;
using RainMaker.Entity.Models;
using RainMaker.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URF.Core.Abstractions;

namespace Rainmaker.Service
{
    public class LoanApplicationService : ServiceBase<RainMakerContext, LoanApplication>, ILoanApplicationService
    {
        [Flags]
        public enum RelatedEntities : long
        {
            Borrower = (long)1 << 0,
            PropertyInfo = (long)1 << 1,
            Borrower_LoanContact = (long)1 << 2,
            Borrower_EmploymentInfoes = (long)1 << 3,
            Borrower_EmploymentInfoes_OtherEmploymentIncomes = (long)1 << 4,
            Borrower_OtherIncomes_IncomeType = (long)1 << 5,
            Borrower_BorrowerResidences = (long)1 << 6,
            LoanGoal = (long)1 << 7,
            Borrower_BorrowerResidences_OwnershipType = (long)1 << 8,
            Borrower_BorrowerResidences_LoanAddress = (long)1 << 9,
            Borrower_FamilyRelationType = (long)1 << 10,
            PropertyInfo_PropertyType = (long)1 << 11,
            PropertyInfo_PropertyUsage = (long)1 << 12,
            Borrower_PropertyInfo = (long)1 << 13,
            Borrower_PropertyInfo_AddressInfo = (long)1 << 14,
            Borrower_BorrowerAccount = (long)1 << 15,
            Borrower_BorrowerAccount_AccountType = (long)1 << 16,
            Borrower_EmploymentInfoes_OtherEmploymentIncomes_IncomeType = (long)1 << 17,
            Borrower_EmploymentInfoes_AddressInfo = (long)1 << 18,
            Borrower_BorrowerQuestionResponses = (long)1 << 19,
            Borrower_BorrowerQuestionResponses_Question = (long)1 << 20,
            Borrower_BorrowerQuestionResponses_QuestionResponse = (long)1 << 21,
            Borrower_LoanContact_Gender = (long)1 << 22,
            PropertyInfo_PropertyTaxEscrows = (long)1 << 23,
            Borrower_LoanContact_Ethnicity = (long)1 << 24,
            Borrower_LoanContact_Race = (long)1 << 25,
            Borrower_Bankruptcies = (long)1 << 26,
            Borrower_LoanContact_ResidencyType = (long)1 << 27,
            Borrower_LoanContact_ResidencyState = (long)1 << 28,
            Borrower_BorrowerAssets = (long)1 << 29,
            Borrower_EmploymentInfoes_JobType = (long)1 << 30,
            PropertyInfo_AddressInfo = (long)1 << 31,
            PropertyInfo_MortgageOnProperties = (long)1 << 32,
            Borrower_OwnType = (long)1 << 33,
            LoanPurpose = (long)1 << 34,
            Borrower_Consent_ConsentLog = (long)1 << 35,
            Borrower_Liability = (long)1 << 36,
            Borrower_SupportPayments = (long)1 << 37,
            Borrower_OwnerShipInterests = (long)1 << 38,
            Borrower_VaDetails = (long)1 << 39,
            BusinessUnit = (long)1 << 40,
            Opportunity = (long)1 << 41,
            Opportunity_UserProfile = 1L << 42,
            Opportunity_LoanRequest = 1L << 43,
            LosSyncLog = 1L << 44,
            Opportunity_Employee_UserProfile = 1L << 45,
            Opportunity_Employee_Contact = 1L << 46,
            Opportunity_Employee_CompanyPhoneInfo = 1L << 47,
            Opportunity_Employee_EmailAccount = 1L << 48,
            BusinessUnit_LeadSource = 1L << 49,
        }

        private readonly ICommonService commonService;
        public LoanApplicationService(IUnitOfWork<RainMakerContext> previousUow,
                                      IServiceProvider services,ICommonService commonService) : base(previousUow: previousUow,
                                                                        services: services)
        {
            this.commonService = commonService;
        }
        public async Task<int> GetLoanApplicationId(string loanId, short losId)
        {
            var binder = (await Uow.Repository<LosLoanApplicationBinder>().Query(x => x.LosLoanAplicationNumber == loanId && x.LosId == losId).FirstOrDefaultAsync())?.LoanApplicationId;
            return binder ?? -1;
        }
        public async Task<int> GetMilestoneId(int loanApplicationId)
        {
            return await Query(x => x.Id == loanApplicationId).Select(x => x.MilestoneId ?? -1).FirstAsync();
        }

        public async Task SetMilestoneId(int loanApplicationId, int milestoneId)
        {
            LoanApplication loanApplication = await this.GetByIdAsync(loanApplicationId);
            loanApplication.MilestoneId = milestoneId;
            Repository.Update(loanApplication);
            await Uow.SaveChangesAsync();
        }

        public async Task<LoanSummary> GetLoanSummary(int loanApplicationId,
                                                      int userProfileId)
        {
            string url = await commonService.GetSettingFreshValueByKeyAsync<string>(SystemSettingKeys.AdminDomainUrl);
            return await Repository
                         .Query(query: x =>
                                    x.Opportunity.OpportunityLeadBinders
                                     .FirstOrDefault(y => y.OwnTypeId == (int) OwnTypeEnum.PrimaryContact).Customer
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
                         .Include(navigationPropertyPath: x => x.Borrowers)
                         .ThenInclude(navigationPropertyPath: x => x.LoanContact)
                         .Select(selector: x => new LoanSummary
                                                {
                                                    Url = url,
                                                    Name = x
                                                        .Borrowers.Where(y => y.OwnTypeId==(int)OwnTypeEnum.PrimaryContact)
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
                                                        .FirstOrDefault(),
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
                                                              LoanNumber = x.ByteFileName,
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
                                                                   RelatedEntities? includes = null)
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
                                                            RelatedEntities includes)
        {
            // @formatter:off 
            if (includes.HasFlag(flag: RelatedEntities.Borrower)) query = query.Include(navigationPropertyPath: loanApplication => loanApplication.Borrowers);
            // @formatter:on 
            return query;
        }


        public async Task<LoanOfficer> GetLOInfo(int loanApplicationId,
                                                 int businessUnitId,
                                                 int userProfileId)
        {
            return await Repository.Query(query: x =>
                                              x.Opportunity.OpportunityLeadBinders
                                               .FirstOrDefault(y => y.OwnTypeId == (int) OwnTypeEnum.PrimaryContact).Customer.UserId == userProfileId && x.Id == loanApplicationId &&
                                              x.BusinessUnit.Id == businessUnitId)
                                   .Include(navigationPropertyPath: x => x.Opportunity)
                                   .ThenInclude(navigationPropertyPath: x => x.Owner)
                                   .ThenInclude(navigationPropertyPath: x => x.Contact)
                                   .Include(navigationPropertyPath: x => x.Opportunity)
                                   .ThenInclude(navigationPropertyPath: x => x.Owner)
                                   .ThenInclude(navigationPropertyPath: x => x.EmployeeBusinessUnitEmails)
                                   .ThenInclude(navigationPropertyPath: x => x.EmailAccount)
                                   .Include(navigationPropertyPath: x => x.Opportunity)
                                   .ThenInclude(navigationPropertyPath: x => x.OpportunityLeadBinders)
                                   .ThenInclude(navigationPropertyPath: x => x.Customer)
                                   .Include(navigationPropertyPath: x => x.BusinessUnit)
                                   .Include(navigationPropertyPath: x => x.Opportunity)
                                   .ThenInclude(navigationPropertyPath: x => x.Owner)
                                   .ThenInclude(navigationPropertyPath: x => x.EmployeePhoneBinders)
                                   .ThenInclude(navigationPropertyPath: x => x.CompanyPhoneInfo)
                                   .Select(selector: x =>
                                               new LoanOfficer
                                               {
                                                   Email = x.Opportunity.Owner.EmployeeBusinessUnitEmails
                                                            .FirstOrDefault(y => y.BusinessUnitId == businessUnitId).EmailAccount.Email,
                                                   FirstName = x.Opportunity.Owner.Contact.FirstName,
                                                   LastName = x.Opportunity.Owner.Contact.LastName,
                                                   NMLS = x.Opportunity.Owner.NmlsNo,
                                                   Phone = x.Opportunity.Owner.EmployeePhoneBinders
                                                            .FirstOrDefault(y => y.TypeId == 3).CompanyPhoneInfo.Phone,
                                                   Photo = x.Opportunity.Owner.Photo,
                                                   WebUrl = x.BusinessUnit.WebUrl + "/lo/" +
                                                            x.Opportunity.Owner.CmsName
                                               }
                                          ).FirstOrDefaultAsync();
        }


        public async Task<LoanOfficer> GetDbaInfo(int businessUnitId)
        {
            var businessUnit = await Uow.Repository<BusinessUnit>()
                                        .Query(query: x =>
                                                   x.Id == businessUnitId &&
                                                   x.BusinessUnitPhones.Any(y => y.TypeId == 3))
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
                                                                          .First(y => y.OwnTypeId ==
                                                                                      (int) OwnTypeEnum
                                                                                          .PrimaryContact).Customer.UserId,
                                                                userName =
                                                                    x.Opportunity.OpportunityLeadBinders
                                                                     .First(y => y.OwnTypeId ==
                                                                                 (int) OwnTypeEnum.PrimaryContact).Customer.Contact.FirstName + " " +
                                                                    x.Opportunity.OpportunityLeadBinders
                                                                     .First(y => y.OwnTypeId ==
                                                                                 (int) OwnTypeEnum.PrimaryContact).Customer.Contact.LastName
                                                            }).FirstOrDefaultAsync();
            return postModel;
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

        public async Task UpdateLoanInfo(UpdateLoanInfo updateLoanInfo)
        {
            LoanDocumentPipeLine loanDocumentPipeLine = await Uow.Repository<LoanDocumentPipeLine>().Query(x => x.LoanApplicationId == updateLoanInfo.loanApplicationId.Value).FirstOrDefaultAsync();

            if(loanDocumentPipeLine == null)
            {
                loanDocumentPipeLine = new LoanDocumentPipeLine();
                loanDocumentPipeLine.TrackingState = TrackableEntities.Common.Core.TrackingState.Added;
                loanDocumentPipeLine.LoanApplicationId = updateLoanInfo.loanApplicationId.Value;
                Uow.Repository<LoanDocumentPipeLine>().Insert(loanDocumentPipeLine);
            }
            else
            {
                loanDocumentPipeLine.TrackingState = TrackableEntities.Common.Core.TrackingState.Modified;
                Uow.Repository<LoanDocumentPipeLine>().Update(loanDocumentPipeLine);
            }

            loanDocumentPipeLine.DocumentUploadDateUtc = updateLoanInfo.lastDocUploadDate;
            loanDocumentPipeLine.DocumentRequestSentDateUtc = updateLoanInfo.lastDocRequestSentDate;
            loanDocumentPipeLine.DocumentRemaining = updateLoanInfo.remainingDocuments;
            loanDocumentPipeLine.DocumentOutstanding = updateLoanInfo.outstandingDocuments;
            loanDocumentPipeLine.DocumentCompleted = updateLoanInfo.completedDocuments;
            loanDocumentPipeLine.ModifiedOnUtc = DateTime.UtcNow;
          
            await Uow.SaveChangesAsync();
            
        }

        public async Task<string> GetBanner(int loanApplicationId)
        {
            return await Repository.Query(x => x.Id == loanApplicationId).Include(x => x.BusinessUnit)
                .Select(x => x.BusinessUnit.Banner).FirstAsync();
        }

        public async Task<string> GetFavIcon(int loanApplicationId)
        {
            return await Repository.Query(x => x.Id == loanApplicationId).Include(x => x.BusinessUnit)
                .Select(x => x.BusinessUnit.FavIcon).FirstAsync();
        }
    }
}