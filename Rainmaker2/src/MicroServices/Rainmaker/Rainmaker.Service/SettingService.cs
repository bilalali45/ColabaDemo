using Microsoft.EntityFrameworkCore;
using RainMaker.Common;
using RainMaker.Data;
using RainMaker.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Rainmaker.Model;
using Rainmaker.Service;
using URF.Core.Abstractions;
using URF.Core.EF;
using TrackableEntities.Common.Core;
using System.Net.Http;
using RainMaker.Common.Extensions;

namespace RainMaker.Service
{
    public class SettingService : ServiceBase<RainMakerContext, Setting>, ISettingService
    {
        private readonly IOpportunityService opportunityService;
        private readonly IUserProfileService userProfileService;
        private readonly ILoanApplicationService loanApplicationService;
        public SettingService(IOpportunityService opportunityService, IUserProfileService userProfileService, ILoanApplicationService loanApplicationService, IUnitOfWork<RainMakerContext> previousUow, IServiceProvider services)
            : base(previousUow, services)
        {
            this.opportunityService = opportunityService;
            this.userProfileService = userProfileService;
            this.loanApplicationService = loanApplicationService;
        }

        public async Task<(IEnumerable<Setting> collection, int totalRecords)> GetPagedListAsync(int pageNumber, int pageSize, DynamicLinQFilter gridfilter)
        {
            string order = gridfilter.Sort.Split('-')[0];
            string member = gridfilter.Sort.Split('-')[1];


            if (order == "Des")
            {
                member += " descending";
            }
            var query = Uow.Repository<Setting>()
                .Query(gridfilter, x => x.IsDeleted != true)
                .OrderBy(member);


            var settings = await query.SelectPageAsync(pageNumber, pageSize);

            return settings;

        }
        public override async Task<Setting> GetByIdWithDetailAsync(int id)
        {
            return (await Uow.Repository<Setting>().Query(a => a.Id == id && a.IsDeleted != true).ToListAsync()).FirstOrDefault();
        }
        public async Task<Setting> GetSettingAsync(string key)
        {
            return (await Uow.Repository<Setting>().Query(a => a.Name == key && a.IsActive != false && a.IsDeleted != true).ToListAsync()).FirstOrDefault();
        }

        public async Task<IList<Setting>> GetAllSettingAsync(int? businessUnit = null)
        {
            return (await Uow.Repository<Setting>().Query(x => x.BusinessUnitId == null || x.BusinessUnitId == businessUnit).ToListAsync()).OrderByDescending(x => x.BusinessUnitId).ToList();
        }

        public async Task<string> GetSettingValueAsync(string keyName)
        {
            string settingValue = string.Empty;
            var setting = await GetSettingAsync(keyName);
            if (setting != null)
            {
                settingValue = setting.Value;
            }

            return settingValue;
        }

        public async Task<bool> IsExistsAsync(string name)
        {

            return (await Uow.Repository<Setting>()
                      .Query(x => x.IsDeleted != true && x.Name.ToLower().Trim() == name.ToLower().Trim()).ToListAsync())
                      .Any();

        }
        public async Task<IEnumerable<Setting>> GetGroupSettingsAsync(int gId, int buId)
        {

            var dynamicLinQFilter = new DynamicLinQFilter
            {
                Filter = (buId == 0) ? "BusinessUnitId != @0" : "BusinessUnitId = @0"
            };
            dynamicLinQFilter.Predicates.Add(buId);

            if (gId != 0)
            {
                dynamicLinQFilter.Filter += (dynamicLinQFilter.Filter.Length > 10) ? " And " : "";
                dynamicLinQFilter.Filter += "SettingGroupBinders.Any(GroupId= @1)";
                dynamicLinQFilter.Predicates.Add(gId);
            }



            var query = Uow.Repository<Setting>().Query(dynamicLinQFilter, x => x.SettingGroupBinders.Any(gb => gb.GroupId > 0) && x.IsDeleted != true)
                        .Include(x => x.SettingGroupBinders).ThenInclude(c => c.SettingGroup)
                        .Include(x => x.BusinessUnit);

            var ret = await query.ToListAsync();

            return ret;


        }

        public async Task<bool> IsUniqueSettingAsync(string name, int? businessUnitId, int id)
        {
            return (await Uow.Repository<Setting>().Query(x => x.Name == name && x.Id != id && x.BusinessUnitId == businessUnitId && x.IsDeleted != true).ToListAsync()).Any();
        }

        public async Task<Setting> GetSettingByKeyAsync(string settingKey, int? businessUnit = null)
        {
            return (await Uow.Repository<Setting>().Query(x => x.Name == settingKey && (x.BusinessUnitId == null || x.BusinessUnitId == businessUnit)).ToListAsync()).FirstOrDefault();
        }

        public async Task<IList<Setting>> GetSettingsForAllBusinessUnitsAsync()
        {
            return (await Uow.Repository<Setting>().Query(x => x.IsDifferentForBusinessUnit && x.BusinessUnitId == null).ToListAsync()).OrderBy(o => o.Name).ToList();
        }

        public async Task<Setting> GetSettingByKeyNameAsync(string keyName, int? businessUnitId)
        {
            return (await Uow.Repository<Setting>().Query(x => x.Name == keyName && (x.BusinessUnitId == null || x.BusinessUnitId == businessUnitId) && x.IsActive != false && x.IsDeleted != true).ToListAsync()).FirstOrDefault();
        }

        public async Task<EmailTemplate> RenderEmailTokens(int id, int loanApplicationId, int userProfileId, string fromAddess, string ccAddess, string subject, string emailBody, List<TokenModel> lsTokenModels)
        {
            EmailTemplate emailTemplate = new EmailTemplate();

            var relatedEntities = LoanApplicationService.RelatedEntities.LoanPurpose |
                                LoanApplicationService.RelatedEntities.BusinessUnit |
                                LoanApplicationService.RelatedEntities.LoanApplication_Status |
                                LoanApplicationService.RelatedEntities.PropertyInfo_PropertyType |
                                LoanApplicationService.RelatedEntities.PropertyInfo_PropertyUsage |
                                LoanApplicationService.RelatedEntities.Borrower_LoanContact |
                                LoanApplicationService.RelatedEntities.Borrower_LoanContact_ResidencyState |
                                LoanApplicationService.RelatedEntities.Borrower_BorrowerResidences_LoanAddress |
                                LoanApplicationService.RelatedEntities.PropertyInfo_AddressInfo |
                                LoanApplicationService.RelatedEntities.Opportunity_Employee_CompanyPhoneInfo |
                                LoanApplicationService.RelatedEntities.Opportunity_Employee_Contact|
                                LoanApplicationService.RelatedEntities.Opportunity_Branch |
                                LoanApplicationService.RelatedEntities.Opportunity_Employee_EmailAccount|
                                LoanApplicationService.RelatedEntities.Opportunity_Employee_Contact_ContactPhoneInfoes;

           var loanApplication = loanApplicationService.GetLoanApplicationWithDetails(id: loanApplicationId, includes: relatedEntities).SingleOrDefault();

            lsTokenModels = await SetTokenValues(loanApplication, lsTokenModels, userProfileId, emailTemplate);

            foreach (var token in lsTokenModels)
            {
                if (fromAddess.Contains(token.symbol))
                {
                    fromAddess = fromAddess.Replace(token.symbol,
                                                    token.value);
                }
                if (ccAddess.Contains(token.symbol))
                {
                    ccAddess = ccAddess.Replace(token.symbol,
                                                    token.value);
                }
                if (subject.Contains(token.symbol))
                {
                    subject = subject.Replace(token.symbol,
                                                    token.value);
                }
                if (emailBody.Contains(token.symbol))
                {
                    emailBody = emailBody.Replace(token.symbol,
                                                    token.value);
                }
            }

            emailTemplate.id = id;
            emailTemplate.fromAddress = fromAddess;
            emailTemplate.CCAddress = ccAddess;
            emailTemplate.subject = subject;
            emailTemplate.emailBody = emailBody;

            return emailTemplate;
        }
        public async Task<List<ByteUserNameModel>> GetLoanOfficers()
        {
            var result = await Uow.Repository<UserProfile>().Query(query: x => x.IsActive
                                                                          && !x.IsDeleted)
                .Include(e => e.Employees)
                .ThenInclude(c => c.Contact)
                .Include(u => u.UserInRoles).ToListAsync();

            //Filter Loan Officers
            var loanOfficers = result.Where(c => c.UserInRoles.Any(x => x.RoleId == 12)).ToList();

            return loanOfficers.Select(x => new ByteUserNameModel()
            {
                userId = x.Id,
                userName = x.UserName,
                byteUserName = x.ByteUserName,
                fullName = x.Employees.FirstOrDefault().Contact.FirstName + " " + x.Employees.FirstOrDefault().Contact.LastName
            }).ToList();
        }
        public async Task UpdateByteUserName(List<ByteUserNameModel> byteUserNameModel, int userId)
        {
            foreach (var item in byteUserNameModel)
            {
                var userProfile = await Uow.Repository<UserProfile>().Query(x => x.Id == item.userId).FirstOrDefaultAsync();
                userProfile.ByteUserName = item.byteUserName;
                userProfile.ModifiedBy = userId;
                userProfile.ModifiedOnUtc = DateTime.UtcNow;

                userProfile.TrackingState = TrackingState.Modified;

                Uow.Repository<UserProfile>().Update(userProfile);
                await Uow.SaveChangesAsync();
            }
        }
        public async Task<List<ByteBusinessUnitModel>> GetBusinessUnits()
        {
            var result = await Uow.Repository<BusinessUnit>().Query(query: x => x.IsActive
                                                                          && !x.IsDeleted).ToListAsync();

            return result.Select(x => new ByteBusinessUnitModel()
            {
                id = x.Id,
                name = x.Name,
                byteOrganizationCode = x.ByteOrganizationCode
            }).ToList();
        }
        public async Task UpdateByteOrganizationCode(List<ByteBusinessUnitModel> byteBusinessUnitModel, int userId)
        {
            foreach (var item in byteBusinessUnitModel)
            {
                var businessUnit = await Uow.Repository<BusinessUnit>().Query(x => x.Id == item.id).FirstOrDefaultAsync();
                businessUnit.ByteOrganizationCode = item.byteOrganizationCode;
                businessUnit.ModifiedBy = userId;
                businessUnit.ModifiedOnUtc = DateTime.UtcNow;

                businessUnit.TrackingState = TrackingState.Modified;

                Uow.Repository<BusinessUnit>().Update(businessUnit);
                await Uow.SaveChangesAsync();
            }
        }
        private async Task<string> GetLoginUserEmail(int loanApplicationId, int userProfileId)
        {
            int? busnessUnitId = Uow.Repository<LoanApplication>().Query(x => x.IsDeleted == false && x.Id == loanApplicationId).Select(x => x.BusinessUnitId).FirstOrDefault();

            var userProfile = await userProfileService.GetUserProfileEmployeeDetail(userProfileId, UserProfileService.RelatedEntities.Employees_EmployeeBusinessUnitEmails_EmailAccount);

            if (userProfile != null && userProfile.Employees.SingleOrDefault().EmployeeBusinessUnitEmails.Any())
            {
                var emailAccounts = userProfile.Employees.SingleOrDefault().EmployeeBusinessUnitEmails.Where(e => e.BusinessUnitId == busnessUnitId || e.BusinessUnitId == null).OrderByDescending(e => e.BusinessUnitId);
                return emailAccounts.Any() ? emailAccounts.FirstOrDefault().EmailAccount.Email : "";
            }
            return "";
        }
        private async Task<List<TokenModel>> SetTokenValues(LoanApplication loanApplication, List<TokenModel> lsTokenModels, int userProfileId, EmailTemplate emailTemplate)
        {
            var borrowerChunks = loanApplication.Borrowers.ToList().ChunkBy(chunkSize: 2);
            List<Borrower> borrowers = borrowerChunks[0];
            var rmBorrower = borrowers[index: 0];
            var rmCoBorrower = borrowers.Count > 1 ? borrowers[index: 1] : null;
            int? busnessUnitId = Uow.Repository<LoanApplication>().Query(x => x.IsDeleted == false && x.Id == loanApplication.Id).Select(x => x.BusinessUnitId).FirstOrDefault();

            foreach (var token in lsTokenModels)
            {
                switch (token.key)
                {
                    case TokenKey.Date:
                        {
                            token.value = DateTime.UtcNow.ToString();
                        }
                        break;
                    case TokenKey.PrimaryBorrowerEmailAddress:
                        {
                            token.value = rmBorrower?.LoanContact?.EmailAddress ?? "";
                        }
                        break;
                    case TokenKey.PrimaryBorrowerFirstName:
                        {
                            token.value = rmBorrower?.LoanContact?.FirstName ?? "";
                        }
                        break;
                    case TokenKey.PrimaryBorrowerLastName:
                        {
                            token.value = rmBorrower?.LoanContact?.LastName ?? "";
                        }
                        break;
                    case TokenKey.CoBorrowerEmailAddress:
                        {
                            token.value = rmCoBorrower?.LoanContact?.EmailAddress ?? "";
                        }
                        break;
                    case TokenKey.CoBorrowerFirstName:
                        {
                            token.value = rmCoBorrower?.LoanContact?.FirstName ?? "";
                        }
                        break;
                    case TokenKey.CoBorrowerLastName:
                        {
                            token.value = rmCoBorrower?.LoanContact?.LastName ?? "";
                        }
                        break;
                    case TokenKey.EmailTag:
                        {
                            token.value = await GetLoginUserEmail(loanApplication.Id, userProfileId);
                        }
                        break;
                    case TokenKey.LoanPortalUrl:
                        {
                            token.value = loanApplication.BusinessUnit?.LoanUrl ?? "";
                        }
                        break;
                    case TokenKey.LoanStatus:
                        {
                            token.value = loanApplication.StatusList?.Name ?? "";
                        }
                        break;
                    case TokenKey.SubjectPropertyAddress:
                        {
                            token.value = loanApplication.PropertyInfo?.AddressInfo?.StreetAddress ?? "" + " " + loanApplication.PropertyInfo?.AddressInfo?.UnitNo ?? "";
                        }
                        break;
                    case TokenKey.SubjectPropertyState:
                        {
                            token.value = loanApplication.PropertyInfo?.AddressInfo?.State?.Name ?? "";
                        }
                        break;
                    case TokenKey.SubjectPropertyStateAbbreviation:
                        {
                            token.value = loanApplication.PropertyInfo?.AddressInfo?.State?.Abbreviation ?? "";
                        }
                        break;
                    case TokenKey.SubjectPropertyCounty:
                        {
                            token.value = loanApplication.PropertyInfo?.AddressInfo?.CountyName ?? "";

                        }
                        break;
                    case TokenKey.SubjectPropertyCity:
                        {
                            token.value = loanApplication.PropertyInfo?.AddressInfo?.CityName ?? "";
                        }
                        break;
                    case TokenKey.SubjectPropertyZipCode:
                        {
                            token.value = loanApplication.PropertyInfo?.AddressInfo?.ZipCode ?? "";
                        }
                        break;
                    case TokenKey.LoanPurpose:
                        {
                            token.value = loanApplication.LoanPurpose?.Name ?? "";
                        }
                        break;
                    case TokenKey.LoanAmount:
                        {
                            token.value = loanApplication.LoanAmount?.ToString() ?? "";
                        }
                        break;
                    case TokenKey.PropertyValue:
                        {
                            token.value = loanApplication.PropertyInfo?.PropertyValue?.ToString() ?? "";
                        }
                        break;
                    case TokenKey.PropertyType:
                        {
                            token.value = loanApplication.PropertyInfo?.PropertyType?.Name ?? "";
                        }
                        break;
                    case TokenKey.PropertyUsage:
                        {
                            token.value = loanApplication.PropertyInfo?.PropertyUsage?.Name ?? "";
                        }
                        break;
                    case TokenKey.ResidencyType:
                        {
                            token.value = rmBorrower.LoanContact?.ResidencyState?.Name ?? "";
                        }
                        break;
                    case TokenKey.BranchNmlsNo:
                        {
                            token.value = loanApplication.Opportunity?.Owner?.NmlsNo ?? "";
                        }
                        break;
                    case TokenKey.BusinessUnitName:
                        {
                            token.value = loanApplication.BusinessUnit?.Name ?? "";
                        }
                        break;
                    case TokenKey.BusinessUnitPhoneNumber:
                        {
                            token.value = loanApplication.Opportunity?.Owner?.EmployeePhoneBinders?.FirstOrDefault()?.CompanyPhoneInfo?.Phone ?? "";
                        }
                        break;
                    case TokenKey.BusinessUnitWebSiteUrl:
                        {
                            token.value = loanApplication.BusinessUnit?.WebUrl ?? "";
                        }
                        break;
                    case TokenKey.LoanApplicationLoginLink:
                        {
                            token.value = loanApplication.BusinessUnit?.LoanLoginUrl ?? "";
                        }
                        break;
                    case TokenKey.LoanOfficerPageUrl:
                        {
                            token.value =  loanApplication.BusinessUnit?.WebUrl != null  ? loanApplication.BusinessUnit?.WebUrl + "/lo/" + loanApplication.Opportunity?.Owner?.CmsName ?? "" : "";
                        }
                        break;
                    case TokenKey.LoanOfficerFirstName:
                        {
                            token.value = loanApplication.Opportunity?.Owner?.Contact.FirstName ?? "";
                        }
                        break;
                    case TokenKey.LoanOfficerLastName:
                        {
                            token.value = loanApplication.Opportunity?.Owner?.Contact.LastName ?? "";
                        }
                        break;
                    case TokenKey.RequestDocumentList:
                        {
                            token.value = token.symbol;
                        }
                        break;
                    case TokenKey.RequestorUserEmail:
                        {
                            token.value = await GetLoginUserEmail(loanApplication.Id, userProfileId);
                        }
                        break;
                    case TokenKey.CompanyNMLSNo:
                        {
                            token.value = loanApplication.Opportunity?.Branch?.NmlsNo ?? "";
                        }
                        break;
                    case TokenKey.LoanOfficerEmailAddress:
                        {
                            token.value = loanApplication.Opportunity?.Owner?.EmployeeBusinessUnitEmails?.Where(e => e.BusinessUnitId == busnessUnitId || e.BusinessUnitId == null).OrderByDescending(e => e.BusinessUnitId).FirstOrDefault()?.EmailAccount?.Email ?? "";
                        }
                        break;
                    case TokenKey.LoanOfficerOfficePhoneNumber:
                        {
                            token.value = loanApplication.Opportunity?.Owner?.EmployeePhoneBinders?.FirstOrDefault()?.CompanyPhoneInfo?.Phone ?? "";
                        }
                        break;
                    case TokenKey.LoanOfficerCellPhoneNumber:
                        {
                            token.value = loanApplication.Opportunity?.Owner?.Contact?.ContactPhoneInfoes?.FirstOrDefault()?.Phone ?? "";
                        }
                        break;
                    case TokenKey.PrimaryBorrowerPresentStreetAddress:
                        {
                            token.value = rmBorrower.BorrowerResidences?.FirstOrDefault()?.LoanAddress?.StreetAddress ?? "" ;
                        }
                        break;
                    case TokenKey.PrimaryBorrowerPresentUnitNo:
                        {
                            token.value = rmBorrower.BorrowerResidences?.FirstOrDefault()?.LoanAddress?.UnitNo ?? "";
                        }
                        break;
                    case TokenKey.PrimaryBorrowerPresentCity:
                        {
                            token.value = rmBorrower.BorrowerResidences?.FirstOrDefault()?.LoanAddress?.CityName ?? "";
                        }
                        break;
                    case TokenKey.PrimaryBorrowerPresentState:
                        {
                            token.value = rmBorrower.BorrowerResidences?.FirstOrDefault()?.LoanAddress?.StateName ?? "";
                        }
                        break;
                    case TokenKey.PrimaryBorrowerPresentStateAbbreviation:
                        {
                            token.value = rmBorrower.BorrowerResidences?.FirstOrDefault()?.LoanAddress?.State?.Abbreviation ?? "";
                        }
                        break;
                    case TokenKey.PrimaryBorrowerPresentZipCode:
                        {
                            token.value = rmBorrower.BorrowerResidences?.FirstOrDefault()?.LoanAddress?.ZipCode ?? "";
                        }
                        break;
                    case TokenKey.CoBorrowerPresentStreetAddress:
                        {
                            token.value = rmCoBorrower?.BorrowerResidences?.FirstOrDefault()?.LoanAddress?.StreetAddress ?? "";
                        }
                        break;
                    case TokenKey.CoBorrowerPresentUnitNo:
                        {
                            token.value = rmCoBorrower?.BorrowerResidences?.FirstOrDefault()?.LoanAddress?.UnitNo ?? "";
                        }
                        break;
                    case TokenKey.CoBorrowerPresentCity:
                        {
                            token.value = rmCoBorrower?.BorrowerResidences?.FirstOrDefault()?.LoanAddress?.CityName ?? "";
                        }
                        break;
                    case TokenKey.CoBorrowerPresentState:
                        {
                            token.value = rmCoBorrower?.BorrowerResidences?.FirstOrDefault()?.LoanAddress?.StateName ?? "";
                        }
                        break;
                    case TokenKey.CoBorrowerPresentStateAbbreviation:
                        {
                            token.value = rmCoBorrower?.BorrowerResidences?.FirstOrDefault()?.LoanAddress?.State?.Abbreviation ?? "";
                        }
                        break;
                    case TokenKey.CoBorrowerPresentZipCode:
                        {
                            token.value = rmCoBorrower?.BorrowerResidences?.FirstOrDefault()?.LoanAddress?.ZipCode ?? "";
                        }
                        break;
                    case TokenKey.DocumentUploadButton:
                        {
                            token.value = token.symbol;
                        }
                        break;
                    case TokenKey.LoanPortalHomeButton:
                        {
                            token.value = token.symbol;
                        }
                        break;
                    case TokenKey.DocumentsPageButton:
                        {
                            token.value = token.symbol;
                        }
                        break;
                    //case TokenKey.DocumentUploadButton:
                    //    {
                    //        token.value = token.symbol;
                    //    }
                    //    break;
                    //case TokenKey.LoanPortalHomeButton:
                    //    {
                    //        token.value = token.symbol;
                    //    }
                    //    break;
                    //case TokenKey.DocumentsPageButton:
                    //    {
                    //        token.value = token.symbol;
                    //    }
                    //    break;
                }
            }
            emailTemplate.toAddress = rmBorrower?.LoanContact?.EmailAddress ?? "";
            return lsTokenModels;
        }
    }
}
