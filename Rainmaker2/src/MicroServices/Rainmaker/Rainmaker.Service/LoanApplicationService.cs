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
        public enum RelatedEntity
        {
            Borrowers = 1 << 0
        }

        private readonly ICommonService commonService;
        public LoanApplicationService(IUnitOfWork<RainMakerContext> previousUow,
                                      IServiceProvider services,ICommonService commonService) : base(previousUow: previousUow,
                                                                        services: services)
        {
            this.commonService = commonService;
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