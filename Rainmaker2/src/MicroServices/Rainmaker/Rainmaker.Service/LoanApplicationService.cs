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
using TrackableEntities.Common.Core;
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
            LoanApplication_Status = 1L << 50,
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
        public async Task SetBothLosAndMilestoneId(int loanApplicationId, int milestoneId, int losMilestoneId)
        {
            LoanApplication loanApplication = await this.GetByIdAsync(loanApplicationId);
            loanApplication.MilestoneId = milestoneId;
            loanApplication.LosMilestoneId = losMilestoneId;
            Repository.Update(loanApplication);
            await Uow.SaveChangesAsync();
        }
        public async Task<BothLosMilestoneModel> GetBothLosAndMilestoneId(int loanApplicationId)
        {
            BothLosMilestoneModel model = new BothLosMilestoneModel();
            var loanApplication = await Query(x => x.Id == loanApplicationId).FirstOrDefaultAsync();
            if(loanApplication!=null)
            {
                model.milestoneId = loanApplication.MilestoneId;
                model.losMilestoneId = loanApplication.LosMilestoneId;
            }
            return model;
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
            //if (includes.HasFlag(RelatedEntities.LosSyncLog)) query = query.Include(x => x.LosSyncLogs);
            if (includes.HasFlag(RelatedEntities.Opportunity_UserProfile)) query = query.Include(l => l.Opportunity.OpportunityLeadBinders).ThenInclude(olb => olb.Customer);
            if (includes.HasFlag(RelatedEntities.Opportunity_LoanRequest)) query = query.Include(l => l.Opportunity.LoanRequest);
            if (includes.HasFlag(RelatedEntities.Opportunity_Employee_UserProfile)) query = query.Include(l => l.Opportunity.Owner.UserProfile);
            if (includes.HasFlag(RelatedEntities.Opportunity_Employee_Contact)) query = query.Include(l => l.Opportunity.Owner.Contact);
            if (includes.HasFlag(RelatedEntities.Opportunity_Employee_CompanyPhoneInfo)) query = query.Include(l => l.Opportunity.Owner.EmployeePhoneBinders).ThenInclude(employeePhoneBinder => employeePhoneBinder.CompanyPhoneInfo);
            if (includes.HasFlag(RelatedEntities.Opportunity_Employee_EmailAccount)) query = query.Include(l => l.Opportunity.Owner.EmployeeBusinessUnitEmails).ThenInclude(employeeBusinessUnitEmails => employeeBusinessUnitEmails.EmailAccount);

            if (includes.HasFlag(RelatedEntities.LoanGoal)) query = query.Include(l => l.LoanGoal);
            if (includes.HasFlag(RelatedEntities.LoanPurpose)) query = query.Include(l => l.LoanPurpose);

            if (includes.HasFlag(RelatedEntities.PropertyInfo)) query = query.Include(l => l.PropertyInfo);
            if (includes.HasFlag(RelatedEntities.PropertyInfo_AddressInfo))
            {
                query = query.Include(l => l.PropertyInfo.AddressInfo);
                query = query.Include(l => l.PropertyInfo.AddressInfo.State);
                query = query.Include(l => l.PropertyInfo.AddressInfo.County);
                query = query.Include(l => l.PropertyInfo.AddressInfo.City);
            }

            if (includes.HasFlag(RelatedEntities.PropertyInfo_MortgageOnProperties)) query = query.Include(l => l.PropertyInfo.MortgageOnProperties);
            if (includes.HasFlag(RelatedEntities.PropertyInfo_PropertyType)) query = query.Include(l => l.PropertyInfo.PropertyType);
            if (includes.HasFlag(RelatedEntities.PropertyInfo_PropertyUsage)) query = query.Include(l => l.PropertyInfo.PropertyUsage);
            if (includes.HasFlag(RelatedEntities.PropertyInfo_PropertyTaxEscrows)) query = query.Include(l => l.PropertyInfo.PropertyTaxEscrows).ThenInclude(pte => pte.EscrowEntityType);

            if (includes.HasFlag(RelatedEntities.Borrower)) query = query.Include(l => l.Borrowers);
            if (includes.HasFlag(RelatedEntities.Borrower_OwnType)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.OwnType);
            if (includes.HasFlag(RelatedEntities.Borrower_BorrowerResidences)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerResidences);
            if (includes.HasFlag(RelatedEntities.Borrower_BorrowerResidences_OwnershipType)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerResidences).ThenInclude(br => br.OwnershipType);
            if (includes.HasFlag(RelatedEntities.Borrower_BorrowerResidences_LoanAddress))
            {
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerResidences).ThenInclude(br => br.LoanAddress);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerResidences).ThenInclude(br => br.LoanAddress.City);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerResidences).ThenInclude(br => br.LoanAddress.County);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerResidences).ThenInclude(br => br.LoanAddress.State);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerResidences).ThenInclude(br => br.LoanAddress.Country);

            }
            if (includes.HasFlag(RelatedEntities.Borrower_Bankruptcies)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerBankRuptcies);
            if (includes.HasFlag(RelatedEntities.Borrower_BorrowerAssets)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.AssetBorrowerBinders).ThenInclude(abb => abb.BorrowerAsset.AssetType);
            if (includes.HasFlag(RelatedEntities.Borrower_PropertyInfo))
            {
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerProperties).ThenInclude(bp => bp.PropertyInfo);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerProperties).ThenInclude(bp => bp.PropertyInfo.PropertyUsage);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerProperties).ThenInclude(bp => bp.PropertyInfo.PropertyType);
            }

            if (includes.HasFlag(RelatedEntities.Borrower_PropertyInfo_AddressInfo))
            {
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerProperties).ThenInclude(bp => bp.PropertyInfo.AddressInfo);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerProperties).ThenInclude(bp => bp.PropertyInfo.AddressInfo.City);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerProperties).ThenInclude(bp => bp.PropertyInfo.AddressInfo.County);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerProperties).ThenInclude(bp => bp.PropertyInfo.AddressInfo.State);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerProperties).ThenInclude(bp => bp.PropertyInfo.AddressInfo.Country);
            }

            if (includes.HasFlag(RelatedEntities.Borrower_BorrowerAccount)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerAccountBinders).ThenInclude(bab => bab.BorrowerAccount);
            if (includes.HasFlag(RelatedEntities.Borrower_BorrowerAccount_AccountType)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerAccountBinders).ThenInclude(bab => bab.BorrowerAccount.AccountType);
            if (includes.HasFlag(RelatedEntities.Borrower_LoanContact))
            {
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.LoanContact);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.LoanContact.MaritalStatusList);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.LoanContact.Contact);
            }
            if (includes.HasFlag(RelatedEntities.Borrower_LoanContact_Gender)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.LoanContact.Gender);
            if (includes.HasFlag(RelatedEntities.Borrower_LoanContact_Ethnicity))
            {
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.LoanContact.LoanContactEthnicityBinders).ThenInclude(eb => eb.Ethnicity);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.LoanContact.LoanContactEthnicityBinders).ThenInclude(eb => eb.EthnicityDetail);

            }
            if (includes.HasFlag(RelatedEntities.Borrower_LoanContact_Race))
            {
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.LoanContact.LoanContactRaceBinders).ThenInclude(rb => rb.Race);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.LoanContact.LoanContactRaceBinders).ThenInclude(rb => rb.RaceDetail);
            }
            if (includes.HasFlag(RelatedEntities.Borrower_LoanContact_ResidencyType)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.LoanContact.ResidencyType);
            if (includes.HasFlag(RelatedEntities.Borrower_LoanContact_ResidencyState)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.LoanContact.ResidencyState);

            if (includes.HasFlag(RelatedEntities.Borrower_EmploymentInfoes)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.EmploymentInfoes);
            if (includes.HasFlag(RelatedEntities.Borrower_EmploymentInfoes_JobType)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.EmploymentInfoes).ThenInclude(ei => ei.JobType);
            if (includes.HasFlag(RelatedEntities.Borrower_EmploymentInfoes_AddressInfo))
            {
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.EmploymentInfoes).ThenInclude(e => e.AddressInfo);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.EmploymentInfoes).ThenInclude(e => e.AddressInfo.City);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.EmploymentInfoes).ThenInclude(e => e.AddressInfo.County);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.EmploymentInfoes).ThenInclude(e => e.AddressInfo.State);
                query = query.Include(l => l.Borrowers).ThenInclude(b => b.EmploymentInfoes).ThenInclude(e => e.AddressInfo.Country);
            }
            if (includes.HasFlag(RelatedEntities.Borrower_EmploymentInfoes_OtherEmploymentIncomes)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.EmploymentInfoes).ThenInclude(e => e.OtherEmploymentIncomes).ThenInclude(oei => oei.OtherEmploymentIncomeHistories);
            if (includes.HasFlag(RelatedEntities.Borrower_EmploymentInfoes_OtherEmploymentIncomes_IncomeType)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.EmploymentInfoes).ThenInclude(ei => ei.OtherEmploymentIncomes).ThenInclude(oei => oei.OtherEmploymentIncomeType);
            if (includes.HasFlag(RelatedEntities.Borrower_OtherIncomes_IncomeType)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.OtherIncomes).ThenInclude(o => o.IncomeType);
            if (includes.HasFlag(RelatedEntities.Borrower_BorrowerQuestionResponses)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerQuestionResponses);
            if (includes.HasFlag(RelatedEntities.Borrower_BorrowerQuestionResponses_Question)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerQuestionResponses).ThenInclude(bqr => bqr.Question);
            if (includes.HasFlag(RelatedEntities.Borrower_BorrowerQuestionResponses_QuestionResponse)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerQuestionResponses).ThenInclude(bqr => bqr.QuestionResponse);
            if (includes.HasFlag(RelatedEntities.Borrower_Consent_ConsentLog)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerConsents).ThenInclude(bc => bc.BorrowerConsentLogs);
            if (includes.HasFlag(RelatedEntities.Borrower_Liability)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerLiabilities).ThenInclude(bl => bl.LiabilityType);
            if (includes.HasFlag(RelatedEntities.Borrower_SupportPayments)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.BorrowerSupportPayments);
            if (includes.HasFlag(RelatedEntities.Borrower_OwnerShipInterests)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.OwnerShipInterests);
            if (includes.HasFlag(RelatedEntities.Borrower_VaDetails)) query = query.Include(l => l.Borrowers).ThenInclude(b => b.VaDetails);
            if (includes.HasFlag(RelatedEntities.Borrower_FamilyRelationType)) query = query.Include(l => l.Borrowers).ThenInclude(x => x.FamilyRelationType);

            if (includes.HasFlag(RelatedEntities.BusinessUnit)) query = query.Include(l => l.BusinessUnit);
            if (includes.HasFlag(RelatedEntities.BusinessUnit_LeadSource)) query = query.Include(l => l.BusinessUnit.LeadSource);
            if (includes.HasFlag(RelatedEntities.Opportunity)) query = query.Include(l => l.Opportunity);
            if (includes.HasFlag(RelatedEntities.LoanApplication_Status)) query = query.Include(l => l.StatusList);

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

        public async Task UpdateLoanApplication(LoanApplication loanApplicationRequest)
        {
            var loanApplication = await Uow.Repository<LoanApplication>().Query(query: application => application.Id == loanApplicationRequest.Id).Include(application => application.Opportunity).SingleAsync();
            loanApplication.ByteFileName = loanApplicationRequest.ByteFileName;
            loanApplication.ByteLoanNumber = loanApplicationRequest.ByteLoanNumber;
            loanApplication.BytePostDateUtc = loanApplicationRequest.BytePostDateUtc;
            loanApplication.Opportunity.TpId = loanApplicationRequest.ByteFileName;
            loanApplication.TrackingState = TrackingState.Modified;
            await SaveChangesAsync();
        }
    }
}