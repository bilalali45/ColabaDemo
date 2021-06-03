using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using URF.Core.Abstractions;

namespace LoanApplication.Service
{
    public class LoanService : ServiceBase<LoanApplicationContext, LoanApplicationDb.Entity.Models.LoanApplication>, ILoanService
    {
        private readonly IDbFunctionService _dbFunctionService;
        public LoanService(IUnitOfWork<LoanApplicationContext> previousUow,
                           IServiceProvider services, IDbFunctionService dbFunctionService) : base(previousUow: previousUow,
                                                             services: services)
        {
            _dbFunctionService = dbFunctionService;
        }

        public async Task<List<LoanSummary>> GetDashboardLoanInfo(TenantModel tenant, int userId)
        {
            return await Repository.Query(x => x.UserId == userId && x.TenantId == tenant.Id && x.BranchId == tenant.Branches[0].Id)
                .Include(x => x.LoanPurpose)
                .OrderByDescending(x => x.CreatedOnUtc)
                .Select(x => new LoanSummary
                {
                    Id = x.Id,
                    CityName = "Dallas",
                    CountryName = "US",
                    CountyName = "Harris",
                    LoanAmount = 1000,
                    LoanPurpose = x.LoanPurpose.Name,
                    MileStone = "Application Started",
                    MileStoneId = x.MilestoneId.Value,
                    StateName = "Tx",
                    StreetAddress = "Downtown Abby",
                    UnitNumber = "A-230",
                    ZipCode = "75490"
                }).ToListAsync();
        }
        public async Task<PendingLoanApplicationModel> GetPendingLoanApplication(TenantModel tenant,
                                                                                 int userId,
                                                                                 int? loanApplicationId)
        {
            LoanApplicationDb.Entity.Models.LoanApplication application;
            if (loanApplicationId.HasValue)
                application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.MilestoneId == 1 && x.Id == loanApplicationId.Value && x.IsActive == true && x.UserId == userId && x.TenantId == tenant.Id).SingleAsync();
            else
                application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.MilestoneId == 1 && x.IsActive == true && x.UserId == userId && x.TenantId == tenant.Id).FirstOrDefaultAsync();
            var restart = true;
            var settings = await Uow.Repository<Config>().Query()
                                    .Include(navigationPropertyPath: x => x.ConfigSelections).OrderBy(keySelector: x => x.Name).Select(selector: x => new TenantSetting
                                    {
                                        Name = x.Name,
                                        Value = x.ConfigSelections.Any(y => y.TenantId == tenant.Id) ? (int)x.ConfigSelections.First(y => y.TenantId == tenant.Id).SelectionId : 1
                                    }).ToListAsync();
            var settingHash = JsonConvert.SerializeObject(value: settings).ToSha(rounds: 1);
            if (application?.SettingHash == settingHash) restart = false;
            return new PendingLoanApplicationModel
            {
                LoanApplicationId = application?.Id,
                RestartLoanApplication = restart,
                Setting = settings,
                State = application?.State
            };
        }

        [ExcludeFromCodeCoverage]
        public async Task<LoanApplicationReview> GetLoanApplicationForBorrowersInfoSectionReview(TenantModel tenant,
                                                                             int userId,
                                                                             int? loanApplicationId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(query: x => x.Id == loanApplicationId.Value &&
                    x.IsActive == true &&
                    x.UserId == userId &&
                    x.TenantId == tenant.Id)
                .Include(app => app.Borrowers)
                //.Include(app => app.Borrowers.Select(borrower => borrower.LoanContact))
                .ThenInclude(borrower => borrower.LoanContact_LoanContactId)
                .ThenInclude(borrower => borrower.BorrowerResidences)
                .SingleAsync();

            var loanApplicationReview = new LoanApplicationReview
            {
                LoanPurpose = loanApplication.LoanPurposeId,
                LoanGoal = loanApplication.LoanGoalId,
                ApplyingWithSomeone = loanApplication.Borrowers.Count > 1,
                BorrowerReviews = new List<BorrowerReview>()
            };

            foreach (var borrower in loanApplication.Borrowers)
            {
                var borrowerReview = new BorrowerReview();
                loanApplicationReview.BorrowerReviews.Add(item: borrowerReview);

                var borrowerResidence = borrower.BorrowerResidences.FirstOrDefault();
                borrowerReview.BorrowerId = borrower.Id;
                borrowerReview.FirstName = borrower.LoanContact_LoanContactId.FirstName;
                borrowerReview.MiddleName = borrower.LoanContact_LoanContactId.MiddleName;
                borrowerReview.LastName = borrower.LoanContact_LoanContactId.LastName;
                borrowerReview.OwnTypeId = borrower.OwnTypeId;
                borrowerReview.EmailAddress = borrower.LoanContact_LoanContactId.EmailAddress;
                borrowerReview.HomePhone = borrower.LoanContact_LoanContactId.HomePhone;
                borrowerReview.WorkPhone = borrower.LoanContact_LoanContactId.WorkPhone;
                borrowerReview.WorkPhoneExt = borrower.LoanContact_LoanContactId.WorkPhoneExt;
                borrowerReview.CellPhone = borrower.LoanContact_LoanContactId.CellPhone;
                borrowerReview.IsVaEligible = borrower.IsVaEligible;
                borrowerReview.MaritalStatusId = borrower.LoanContact_LoanContactId.MaritalStatusId;
                if (borrowerResidence != null)
                {
                    borrowerReview.BorrowerAddress.CountryId = borrowerResidence.LoanAddress.CountryId;
                    borrowerReview.BorrowerAddress.CountryName = borrowerResidence.LoanAddress.CountryName;
                    borrowerReview.BorrowerAddress.StateId = borrowerResidence.LoanAddress.StateId;
                    borrowerReview.BorrowerAddress.StateName = borrowerResidence.LoanAddress.StateName;
                    borrowerReview.BorrowerAddress.CountyId = borrowerResidence.LoanAddress.CountyId;
                    borrowerReview.BorrowerAddress.CountyName = borrowerResidence.LoanAddress.CountyName;
                    borrowerReview.BorrowerAddress.CityId = borrowerResidence.LoanAddress.CityId;
                    borrowerReview.BorrowerAddress.CityName = borrowerResidence.LoanAddress.CityName;
                    borrowerReview.BorrowerAddress.StreetAddress = borrowerResidence.LoanAddress.StreetAddress;
                    borrowerReview.BorrowerAddress.ZipCode = borrowerResidence.LoanAddress.ZipCode;
                    borrowerReview.BorrowerAddress.UnitNo = borrowerResidence.LoanAddress.UnitNo;
                }
            }

            return loanApplicationReview;
        }

        public async Task<LoanApplicationFirstReview> GetLoanApplicationForFirstReview(TenantModel tenant, int userId, int? loanApplicationId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(query: x => x.Id == loanApplicationId.Value &&
                    x.IsActive == true &&
                    x.UserId == userId &&
                    x.TenantId == tenant.Id)
                .Include(app => app.Borrowers).ThenInclude(b => b.BorrowerResidences).ThenInclude(borrowerResidences => borrowerResidences.LoanAddress)
                .Include(app => app.Borrowers).ThenInclude(b => b.LoanContact_LoanContactId)
                //.ThenInclude(borrower => borrower.LoanContact)
                //.ThenInclude(borrower => borrower.BorrowerResidences)
                .SingleAsync();

            var loanApplicationReview = new LoanApplicationFirstReview
            {
                LoanPurpose = loanApplication.LoanPurposeId,
                LoanGoal = loanApplication.LoanGoalId,
                ApplyingWithSomeone = loanApplication.Borrowers.Count > 1,
                BorrowerReviews = new List<BorrowerReview>()
            };

            foreach (var borrower in loanApplication.Borrowers)
            {
                var borrowerReview = new BorrowerReview();
                loanApplicationReview.BorrowerReviews.Add(item: borrowerReview);

                var borrowerResidence = borrower.BorrowerResidences.FirstOrDefault();
                borrowerReview.BorrowerId = borrower.Id;
                borrowerReview.FirstName = borrower.LoanContact_LoanContactId.FirstName;
                borrowerReview.MiddleName = borrower.LoanContact_LoanContactId.MiddleName;
                borrowerReview.LastName = borrower.LoanContact_LoanContactId.LastName;
                borrowerReview.OwnTypeId = borrower.OwnTypeId;
                borrowerReview.EmailAddress = borrower.LoanContact_LoanContactId.EmailAddress;
                borrowerReview.HomePhone = borrower.LoanContact_LoanContactId.HomePhone;
                borrowerReview.WorkPhone = borrower.LoanContact_LoanContactId.WorkPhone;
                borrowerReview.WorkPhoneExt = borrower.LoanContact_LoanContactId.WorkPhoneExt;
                borrowerReview.CellPhone = borrower.LoanContact_LoanContactId.CellPhone;
                borrowerReview.IsVaEligible = borrower.IsVaEligible;
                borrowerReview.MaritalStatusId = borrower.LoanContact_LoanContactId.MaritalStatusId;
                if (borrowerResidence != null)
                {
                    borrowerReview.BorrowerAddress = new BorrowerAddress();
                    borrowerReview.BorrowerAddress.CountryId = borrowerResidence.LoanAddress.CountryId;
                    borrowerReview.BorrowerAddress.CountryName = borrowerResidence.LoanAddress.CountryName;
                    borrowerReview.BorrowerAddress.StateId = borrowerResidence.LoanAddress.StateId;
                    borrowerReview.BorrowerAddress.StateName = borrowerResidence.LoanAddress.StateName;
                    borrowerReview.BorrowerAddress.CountyId = borrowerResidence.LoanAddress.CountyId;
                    borrowerReview.BorrowerAddress.CountyName = borrowerResidence.LoanAddress.CountyName;
                    borrowerReview.BorrowerAddress.CityId = borrowerResidence.LoanAddress.CityId;
                    borrowerReview.BorrowerAddress.CityName = borrowerResidence.LoanAddress.CityName;
                    borrowerReview.BorrowerAddress.StreetAddress = borrowerResidence.LoanAddress.StreetAddress;
                    borrowerReview.BorrowerAddress.ZipCode = borrowerResidence.LoanAddress.ZipCode;
                    borrowerReview.BorrowerAddress.UnitNo = borrowerResidence.LoanAddress.UnitNo;
                }
            }

            return loanApplicationReview;
        }

        public async Task<LoanApplicationSecondReview> GetLoanApplicationForSecondReview(TenantModel tenant,
                                                                           int userId,
                                                                           int? loanApplicationId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(query: x => x.Id == loanApplicationId.Value &&
                    x.IsActive == true &&
                    x.UserId == userId &&
                    x.TenantId == tenant.Id)
                .Include(app => app.Borrowers).ThenInclude(b => b.BorrowerResidences).ThenInclude(borrowerResidences => borrowerResidences.LoanAddress)
                .Include(app => app.Borrowers).ThenInclude(b => b.LoanContact_LoanContactId)
                //.Include(app => app.Borrowers).ThenInclude(b => b.OwnType)
                .Include(app => app.PropertyInfo).ThenInclude(propertyInfo => propertyInfo.AddressInfo)
                //.Include(app => app.PropertyInfo).ThenInclude(propertyInfo => propertyInfo.PropertyType)
                //.Include(app => app.PropertyInfo).ThenInclude(propertyInfo => propertyInfo.PropertyUsage)
                //.Include(app => app.LoanGoal)
                .SingleAsync();

            var customOwnTypes = _dbFunctionService.UdfOwnType(tenant.Id).ToList();
            var customPropertyTypes = _dbFunctionService.UdfPropertyType(tenant.Id).ToList();
            var customPropertyUsages = _dbFunctionService.UdfPropertyUsage(tenant.Id).ToList();
            var customLoanGoal = _dbFunctionService.UdfLoanGoal(tenant.Id).ToList();
            
            foreach (var loanApplicationBorrower in loanApplication.Borrowers)
            {
                loanApplicationBorrower.OwnType =
                    customOwnTypes.FirstOrDefault(ot => ot.Id == loanApplicationBorrower.OwnTypeId);
            }

            loanApplication.PropertyInfo.PropertyType = customPropertyTypes.FirstOrDefault(pt => pt.Id == loanApplication.PropertyInfo.PropertyTypeId);
            loanApplication.PropertyInfo.PropertyUsage = customPropertyUsages.FirstOrDefault(pu => pu.Id == loanApplication.PropertyInfo.PropertyUsageId);
            loanApplication.LoanGoal = customLoanGoal.FirstOrDefault(lg => lg.Id == loanApplication.LoanGoalId);

            var loanApplicationReview = new LoanApplicationSecondReview
            {
                LoanPurpose = loanApplication.LoanPurposeId,
                LoanGoal = loanApplication.LoanGoalId,
                LoanGoalName = loanApplication.LoanGoal?.Name,
                ApplyingWithSomeone = loanApplication.Borrowers.Count > 1,
                PropertyIdentified = loanApplication.IsPropertyIdentified,
                BorrowerReviews = new List<BorrowerReview>(),
                PropertyInfo = new PropertyInfoReview
                {
                    PropertyUsageId = loanApplication.PropertyInfo.PropertyUsageId,
                    PropertyTypeId = loanApplication.PropertyInfo.PropertyTypeId,
                    PropertyTypeName = loanApplication.PropertyInfo.PropertyType?.Name,
                    PropertyUsageName = loanApplication.PropertyInfo.PropertyUsage?.Name,
                    AddressInfo = new PropertyAddress
                    {
                        StateName = loanApplication.PropertyInfo.AddressInfo.StateName,
                        StreetAddress = loanApplication.PropertyInfo.AddressInfo.StreetAddress,
                        CountryId = loanApplication.PropertyInfo.AddressInfo.CountryId,
                        CountryName = loanApplication.PropertyInfo.AddressInfo.CountryName,
                        StateId = loanApplication.PropertyInfo.AddressInfo.StateId,
                        CityId = loanApplication.PropertyInfo.AddressInfo.CityId,
                        CityName = loanApplication.PropertyInfo.AddressInfo.CityName,
                        ZipCode = loanApplication.PropertyInfo.AddressInfo.ZipCode,
                        UnitNo = loanApplication.PropertyInfo.AddressInfo.UnitNo

                    },

                   
                }
            };

            var tenantOwnTypes = _dbFunctionService.UdfOwnType(tenant.Id);

            foreach (var borrower in loanApplication.Borrowers)
            {
                var borrowerReview = new BorrowerReview();
                loanApplicationReview.BorrowerReviews.Add(item: borrowerReview);

                var borrowerResidence = borrower.BorrowerResidences.FirstOrDefault();
                borrowerReview.BorrowerId = borrower.Id;
                borrowerReview.FirstName = borrower.LoanContact_LoanContactId.FirstName;
                borrowerReview.MiddleName = borrower.LoanContact_LoanContactId.MiddleName;
                borrowerReview.LastName = borrower.LoanContact_LoanContactId.LastName;
                borrowerReview.OwnTypeId = borrower.OwnTypeId;
                borrower.OwnType = tenantOwnTypes.FirstOrDefault(ot => ot.Id == borrower.OwnTypeId);
                borrowerReview.OwnTypeName = borrower.OwnType.Name;
                borrowerReview.EmailAddress = borrower.LoanContact_LoanContactId.EmailAddress;
                borrowerReview.HomePhone = borrower.LoanContact_LoanContactId.HomePhone;
                borrowerReview.WorkPhone = borrower.LoanContact_LoanContactId.WorkPhone;
                borrowerReview.WorkPhoneExt = borrower.LoanContact_LoanContactId.WorkPhoneExt;
                borrowerReview.CellPhone = borrower.LoanContact_LoanContactId.CellPhone;
                borrowerReview.IsVaEligible = borrower.IsVaEligible;
                borrowerReview.MaritalStatusId = borrower.LoanContact_LoanContactId.MaritalStatusId;
                if (borrowerResidence != null)
                {
                    borrowerReview.BorrowerAddress = new BorrowerAddress();
                    borrowerReview.BorrowerAddress.CountryId = borrowerResidence.LoanAddress.CountryId;
                    borrowerReview.BorrowerAddress.CountryName = borrowerResidence.LoanAddress.CountryName;
                    borrowerReview.BorrowerAddress.StateId = borrowerResidence.LoanAddress.StateId;
                    borrowerReview.BorrowerAddress.StateName = borrowerResidence.LoanAddress.StateName;
                    borrowerReview.BorrowerAddress.CountyId = borrowerResidence.LoanAddress.CountyId;
                    borrowerReview.BorrowerAddress.CountyName = borrowerResidence.LoanAddress.CountyName;
                    borrowerReview.BorrowerAddress.CityId = borrowerResidence.LoanAddress.CityId;
                    borrowerReview.BorrowerAddress.CityName = borrowerResidence.LoanAddress.CityName;
                    borrowerReview.BorrowerAddress.StreetAddress = borrowerResidence.LoanAddress.StreetAddress;
                    borrowerReview.BorrowerAddress.ZipCode = borrowerResidence.LoanAddress.ZipCode;
                    borrowerReview.BorrowerAddress.UnitNo = borrowerResidence.LoanAddress.UnitNo;
                }
            }

            return loanApplicationReview;
        }


        public async Task<int> UpdateState(TenantModel tenant, int userId, UpdateStateModel model)
        {
            LoanApplicationDb.Entity.Models.LoanApplication application;
            application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId &&
                                                                                                    x.IsActive == true &&
                                                                                                    x.UserId == userId &&
                                                                                                    x.TenantId == tenant.Id).SingleOrDefaultAsync();
            application.State = model.State;
            await Uow.SaveChangesAsync();
            return application.Id;
        }
        public async Task<object> SubmitLoanApplication(TenantModel tenant, int userId, LoanCommentsModel model)
        {
            LoanApplicationDb.Entity.Models.LoanApplication application;
            application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(x => x.Id == model.LoanApplicationId &&
                                                                                                    x.IsActive == true &&
                                                                                                    x.UserId == userId &&
                                                                                                    x.TenantId == tenant.Id).SingleOrDefaultAsync();
            application.Comments = model.Comments;
            application.State = model.State;
            await Uow.SaveChangesAsync();
            return application.Id;
        }
    }
}