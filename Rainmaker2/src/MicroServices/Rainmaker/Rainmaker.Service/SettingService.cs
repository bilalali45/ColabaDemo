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

            var relatedEntities = LoanApplicationService.RelatedEntities.Borrower_EmploymentInfoes_AddressInfo |
                                LoanApplicationService.RelatedEntities.Borrower_EmploymentInfoes_OtherEmploymentIncomes |
                                LoanApplicationService.RelatedEntities.Borrower_BorrowerResidences_LoanAddress |
                                LoanApplicationService.RelatedEntities.Borrower_BorrowerAccount_AccountType |
                                LoanApplicationService.RelatedEntities.Borrower_LoanContact_Ethnicity |
                                LoanApplicationService.RelatedEntities.Borrower_LoanContact_Race |
                                LoanApplicationService.RelatedEntities.Borrower_LoanContact_Gender |
                                LoanApplicationService.RelatedEntities.Borrower_Liability |
                                LoanApplicationService.RelatedEntities.Borrower_PropertyInfo_AddressInfo |
                                LoanApplicationService.RelatedEntities.PropertyInfo_PropertyTaxEscrows |
                                LoanApplicationService.RelatedEntities.PropertyInfo_AddressInfo |
                                LoanApplicationService.RelatedEntities.PropertyInfo_MortgageOnProperties |
                                LoanApplicationService.RelatedEntities.BusinessUnit_LeadSource |
                                LoanApplicationService.RelatedEntities.LoanGoal |
                                LoanApplicationService.RelatedEntities.Opportunity_Employee_CompanyPhoneInfo |
                                LoanApplicationService.RelatedEntities.Opportunity_Employee_EmailAccount |
                                LoanApplicationService.RelatedEntities.Opportunity_Employee_UserProfile |
                                LoanApplicationService.RelatedEntities.Opportunity_Employee_Contact;

            var loanApplication = loanApplicationService.GetLoanApplicationWithDetails(id: loanApplicationId, includes: relatedEntities).SingleOrDefault();



            //var opportunityId = await Uow.Repository<LoanApplication>().Query(x => x.IsDeleted == false && x.Id == loanApplicationId).Select(x => x.OpportunityId).FirstOrDefaultAsync();

            //if (opportunityId != null)
            //{
            //var opportunity = await opportunityService.GetSingleOpportunity(opportunityId);
            var borrowerChunks = loanApplication.Borrowers.ToList().ChunkBy(chunkSize: 2);
            List<Borrower> borrowers = borrowerChunks[0];
            var rmBorrower = borrowers[index: 0];
            var rmCoBorrower = borrowers.Count > 1 ? borrowers[index: 1] : null;
           
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
                            token.value = rmBorrower.LoanContact.EmailAddress;
                        }
                        break;
                    case TokenKey.PrimaryBorrowerFirstName:
                        {
                            token.value = rmBorrower.LoanContact.FirstName;
                        }
                        break;
                    case TokenKey.PrimaryBorrowerLastName:
                        {
                            token.value = rmBorrower.LoanContact.LastName;
                        }
                        break;
                    case TokenKey.CoBorrowerEmailAddress:
                        {
                            token.value = rmCoBorrower.LoanContact.EmailAddress;
                        }
                        break;
                    case TokenKey.CoBorrowerFirstName:
                        {
                            token.value = rmCoBorrower.LoanContact.FirstName;
                        }
                        break;
                    case TokenKey.CoBorrowerLastName:
                        {
                            token.value = rmCoBorrower.LoanContact.LastName;
                        }
                        break;
                    case TokenKey.EmailTag:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.LoanPortalUrl:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.LoanStatus:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.SubjectPropertyAddress:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.SubjectPropertyState:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.SubjectPropertyStateAbbreviation:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.SubjectPropertyCounty:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.SubjectPropertyCity:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.SubjectPropertyZipCode:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.LoanPurpose:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.LoanAmount:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.PropertyValue:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.PropertyType:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.PropertyUsage:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.ResidencyType:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.BranchNmlsNo:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.BusinessUnitName:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.BusinessUnitPhoneNumber:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.BusinessUnitWebSiteUrl:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.LoanApplicationLoginLink:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.LoanOfficerPageUrl:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.LoanOfficerFirstName:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.LoanOfficerLastName:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.RequestDocumentList:
                        {
                            token.value = "";
                        }
                        break;
                    case TokenKey.RequestorUserEmail:
                        {
                            token.value = "";
                        }
                        break;
                }
                //if (fromAddess.Contains(token.symbol))
                //{
                //    string value = token.symbol;

                //    switch (token.key)
                //    {
                //        case TokenKey.LoginUserEmail:
                //            {
                //                string userEmail = await GetLoginUserEmail(loanApplicationId, userProfileId);

                //                value = userEmail;
                //            }
                //            break;
                //    }

                //    fromAddess = fromAddess.Replace(token.symbol,
                //                                    value);

                //}
                //if (ccAddess.Contains(token.symbol))
                //{
                //    string value = token.symbol;

                //    switch (token.key)
                //    {
                //        case TokenKey.LoginUserEmail:
                //            {
                //                string userEmail = await GetLoginUserEmail(loanApplicationId, userProfileId);

                //                value = userEmail;
                //            }
                //            break;
                //    }

                //    ccAddess = ccAddess.Replace(token.symbol,
                //                                    value);

                //}
                //if (subject.Contains(token.symbol))
                //{
                //    string value = token.symbol;

                //    switch (token.key)
                //    {
                //        case TokenKey.LoginUserEmail:
                //            {
                //                string userEmail = await GetLoginUserEmail(loanApplicationId, userProfileId);

                //                value = userEmail;
                //            }
                //            break;
                //        case TokenKey.CustomerFirstName:
                //            {
                //                value = GetCustomerFirstName(opportunity);
                //            }
                //            break;
                //        case TokenKey.BusinessUnitName:
                //            {
                //                value = GetBusinessUnitName(loanApplicationId);
                //            }
                //            break;
                //    }

                //    subject = subject.Replace(token.symbol,
                //                              value);

                //}
                //if (emailBody.Contains(token.symbol))
                //{
                //    string value = token.symbol;

                //    switch (token.key)
                //    {
                //        case TokenKey.LoginUserEmail:
                //            {
                //                string userEmail = await GetLoginUserEmail(loanApplicationId, userProfileId);

                //                value = userEmail;
                //            }
                //            break;
                //        case TokenKey.CustomerFirstName:
                //            {
                //                value = GetCustomerFirstName(opportunity);
                //            }
                //            break;
                //        case TokenKey.BusinessUnitName:
                //            {
                //                value = GetBusinessUnitName(loanApplicationId);
                //            }
                //            break;
                //    }

                //    emailBody = emailBody.Replace(token.symbol,
                //                                  value);
                //}
            }
            // Get Customer Email Address
            //var customerEmailAddess = opportunity.OpportunityLeadBinders.FirstOrDefault(s => s.Customer != null && s.Customer.Contact != null && s.Customer.Contact.ContactEmailInfoes != null && s.OwnTypeId == 1 && s.Customer.Contact.ContactEmailInfoes.Any(a => a.IsPrimary == true && a.ValidityId != 3));

            emailTemplate.id = id;
            emailTemplate.fromAddress = fromAddess;
            emailTemplate.toAddress = "";// customerEmailAddess.Customer.Contact.ContactEmailInfoes.FirstOrDefault().Email;
            emailTemplate.subject = subject;
            emailTemplate.emailBody = emailBody;
            //}
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
        private string GetBusinessUnitName(int loanApplicationId)
        {
            return Uow.Repository<LoanApplication>().Query(x => x.IsDeleted == false && x.Id == loanApplicationId).Include(x => x.BusinessUnit).Select(x => x.BusinessUnit.Name).FirstOrDefault();
        }
        private string GetCustomerFirstName(Opportunity opportunity)
        {
            var customer = opportunity.OpportunityLeadBinders.FirstOrDefault(s => s.Customer != null && s.Customer.Contact != null);
            return customer != null ? customer.Customer.Contact.FirstName : string.Empty;
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
    }
}
