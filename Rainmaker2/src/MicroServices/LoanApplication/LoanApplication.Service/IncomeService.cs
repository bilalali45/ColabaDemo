using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanApplication.Model;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;
using IncomeCategory = LoanApplicationDb.Entity.Models.IncomeCategory;

namespace LoanApplication.Service
{
    public interface IIncomeService : IServiceBase<IncomeCategory>
    {
        List<IncomeCategoryGetModel> GetAllIncomeCategories(TenantModel tenant);


        Task<int> AddOrUpdateRetirementIncomeInfo(RetirementIncomeInfoModel model,
                                                  TenantModel tenant,
                                                  int userId);


        List<IncomeGroupModel> GetAllIncomeGroupsWithOtherIncomeTypes(TenantModel tenant);


        Task<int> AddOrUpdateMilitaryIncome(TenantModel tenant,
                                            int userId,
                                            MilitaryIncomeModel model);


        Task<MilitaryIncomeModel> GetMilitaryIncome(TenantModel tenant,
                                                    int userId,
                                                    int borrowerId,
                                                    int incomeInfoId);


        Task<BusinessModel> GetBusinessIncome(TenantModel tenant,
                                              int borrowerId,
                                              int incomeInfoId,
                                              int userId);


        Task<int> AddOrUpdateBusiness(TenantModel tenant,
                                      BusinessModel model,
                                      int userId);


        Task<IncomeInfoViewModel> GetRetirementIncomeInfo(int incomeInfoId,
                                                          int borrowerId,
                                                          int userId,
                                                          TenantModel tenant);


        Task<int> AddOrUpdateSelfBusiness(TenantModel tenant,
                                          SelfBusinessModel model,
                                          int userId);


        Task<SelfBusinessModel> GetSelfBusinessIncome(TenantModel tenant,
                                                      int borrowerId,
                                                      int incomeInfoId,
                                                      int userId);


        Task<int> AddOrUpdateEmployerOtherMonthyIncome(TenantModel tenant,
                                                       int userId,
                                                       AddOrUpdateOtherIncomeModel model);


        //Task<int> AddOrUpdateEmployerMonthlyIncomeWithDescription(TenantModel tenant,
        //                                                         int userId,
        //                                                         UpdateOtherMonthlyIncomeWithDescriptionModel model);


        //Task<int> AddOrUpdateEmployerYearlyIncome(TenantModel tenant,
        //                                          int userId,
        //                                          UpdateOtherAnnualIncomeModel model);


        //Task<int> AddOrUpdateOtherAnnualIncomeWithDescription(TenantModel tenant,
        //                                                      int userId,
        //                                                      UpdateOtherAnnualIncomeWithDescriptionModel model);


        Task<List<EmployerDetailBaseModels>> GetSummary(TenantModel tenant,
                                                        int userId,
                                                        int loanApplicationId);


        Task<GetAllIncomesForHomeScreenModel> GetAllIncomesForHomeScreen(TenantModel tenant,
                                                                         int userId,
                                                                         int loanApplicationId);


        Task<GetOtherIncomeInfoModel> GetOtherIncomeInfo(TenantModel tenant,
                                                         int userId,
                                                         int incomeInfoId);

        Task<GetBorrowerIncomesDataModel> GetBorrowerIncomes(TenantModel tenant, int userId,
            GetBorrowerIncomesModel model);
    }

    public class IncomeService : ServiceBase<LoanApplicationContext, IncomeCategory>, IIncomeService
    {
        private readonly IDbFunctionService _dbFunctionService;
        private readonly ILogger<IncomeService> _logger;


        public IncomeService(IUnitOfWork<LoanApplicationContext> uow,
                             IServiceProvider services,
                             ILogger<IncomeService> logger,
                             IDbFunctionService dbFunctionService) : base(previousUow: uow,
                                                                          services: services)
        {
            _logger = logger;
            _dbFunctionService = dbFunctionService;
        }


        public async Task<SelfBusinessModel> GetSelfBusinessIncome(TenantModel tenant,
                                                                   int borrowerId,
                                                                   int incomeInfoId,
                                                                   int userId)
        {
            return await Uow.Repository<IncomeInfo>()
                            .Query(query: x => x.TenantId == tenant.Id && x.Borrower.LoanApplication.UserId == userId && x.Id == incomeInfoId && x.BorrowerId == borrowerId)
                            .Select(selector: x => new SelfBusinessModel
                            {
                                Id = x.Id,
                                BorrowerId = x.BorrowerId,
                                BusinessName = x.EmployerName,
                                JobTitle = x.JobTitle,
                                StartDate = x.StartDate.Value,
                                BusinessPhone = x.EmployeeNumber,
                                LoanApplicationId = x.Borrower.LoanApplicationId.Value,
                                AnnualIncome = x.AnnualBaseIncome,
                                Address = new GenericAddressModel
                                {
                                    Unit = x.AddressInfo.UnitNo,
                                    City = x.AddressInfo.CityName,
                                    ZipCode = x.AddressInfo.ZipCode,
                                    StateId = x.AddressInfo.StateId,
                                    CountryId = x.AddressInfo.CountryId,
                                    Street = x.AddressInfo.StreetAddress,
                                    StateName = x.AddressInfo.StateName,
                                    CountryName = x.AddressInfo.CountryName
                                }
                            })
                            .SingleAsync();
        }


        public async Task<GetAllIncomesForHomeScreenModel> GetAllIncomesForHomeScreen(TenantModel tenant,
                                                                                      int userId,
                                                                                      int loanApplicationId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                                           .Query(query: application => application.Id == loanApplicationId && application.UserId == userId && application.TenantId == tenant.Id)
                                           //.Include(navigationPropertyPath: application => application.Borrowers).ThenInclude<LoanApplicationDb.Entity.Models.LoanApplication, Borrower, ICollection<IncomeInfo>>(navigationPropertyPath: borrower => borrower.IncomeInfoes)
                                           .Include(app => app.Borrowers).ThenInclude(b => b.IncomeInfoes)
                                           .Include(app => app.Borrowers).ThenInclude(b => b.LoanContact_LoanContactId)
                                           .SingleAsync();

            var model = new GetAllIncomesForHomeScreenModel();
            model.Borrowers = new List<GetAllIncomesForHomeScreenModel.BorrowerIncome>();

            var tenantOwnTypes = _dbFunctionService.UdfOwnType(tenant.Id).ToList();
            var tenantIncomeTypes = _dbFunctionService.UdfIncomeType(tenant.Id);
            var tenantIncomeCategories = _dbFunctionService.UdfIncomeCategory(tenant.Id);

            foreach (var borrower in loanApplication.Borrowers)
            {
                borrower.OwnType = tenantOwnTypes.FirstOrDefault(x => x.Id == borrower.OwnTypeId);
                var borrowerIncome = new GetAllIncomesForHomeScreenModel.BorrowerIncome();
                model.Borrowers.Add(item: borrowerIncome);
                borrowerIncome.BorrowerName = $"{borrower.LoanContact_LoanContactId.FirstName}";
                borrowerIncome.BorrowerId = borrower.Id;
                borrowerIncome.OwnTypeId = borrower.OwnTypeId;
                borrowerIncome.OwnTypeName = borrower.OwnType?.Name;
                borrowerIncome.OwnTypeDisplayName = borrower.OwnType?.DisplayName;
                borrowerIncome.Incomes = new List<GetAllIncomesForHomeScreenModel.Income>();

                borrower.IncomeInfoes = borrower.IncomeInfoes.Where(income => income.IsCurrentJob == true).ToList();

                foreach (var incomeInfo in borrower.IncomeInfoes)
                {
                    var income = new GetAllIncomesForHomeScreenModel.Income();
                    income.IncomeName = incomeInfo.EmployerName ?? (tenantIncomeTypes.Single(it => it.Id == incomeInfo.IncomeTypeId).Name);
                    income.IncomeId = incomeInfo.Id;
                    income.IncomeTypeId = incomeInfo.IncomeTypeId;
                    income.IsCurrentIncome = incomeInfo.EndDate == null;
                    var incomeType = tenantIncomeTypes.FirstOrDefault(type => type.Id == incomeInfo.IncomeTypeId);
                    if (incomeType != null)
                    {
                        income.IncomeTypeDisplayName = incomeType.DisplayName;
                        var incomeCategory = tenantIncomeCategories.FirstOrDefault(cat => cat.Id == incomeType.IncomeCategoryId);
                        if(incomeCategory != null)
                        {
                            income.EmploymentCategory = new EmploymentCategory()
                            {
                                CategoryDisplayName = incomeCategory.DisplayName,
                                CategoryId = incomeCategory.Id,
                                CategoryName = incomeCategory.Name
                            };
                        }
                    }
                    income.IncomeValue = incomeInfo.MonthlyBaseIncome + incomeInfo.OtherIncomeInfoes.Sum(selector: otherIncomeInfo => otherIncomeInfo.MonthlyIncome);
                    borrowerIncome.Incomes.Add(income);
                }
            }

            model.Borrowers.ForEach(action: borrowerIncome =>
            {
                borrowerIncome.MonthlyIncome = borrowerIncome.Incomes.Sum(selector: income => income.IncomeValue);
            });
            model.TotalMonthlyQualifyingIncome = model.Borrowers.Sum(selector: borrowerIncome => borrowerIncome.MonthlyIncome);

            return model;
        }


        public async Task<BusinessModel> GetBusinessIncome(TenantModel tenant,
                                                           int borrowerId,
                                                           int incomeInfoId,
                                                           int userId)
        {
            return await Uow.Repository<IncomeInfo>()
                            .Query(query: x => x.TenantId == tenant.Id && x.Borrower.LoanApplication.UserId == userId && x.Id == incomeInfoId && x.BorrowerId == borrowerId)
                            .Select(selector: x => new BusinessModel
                            {
                                Id = x.Id,
                                BorrowerId = x.BorrowerId,
                                BusinessName = x.EmployerName,
                                JobTitle = x.JobTitle,
                                StartDate = x.StartDate.Value,
                                BusinessPhone = x.EmployeeNumber,
                                IncomeTypeId = x.IncomeTypeId,
                                LoanApplicationId = x.Borrower.LoanApplicationId.Value,
                                AnnualIncome = x.AnnualBaseIncome,
                                OwnershipPercentage = x.OwnershipPercentage.Value,
                                Address = new GenericAddressModel
                                {
                                    Unit = x.AddressInfo.UnitNo,
                                    City = x.AddressInfo.CityName,
                                    ZipCode = x.AddressInfo.ZipCode,
                                    StateId = x.AddressInfo.StateId,
                                    CountryId = x.AddressInfo.CountryId,
                                    Street = x.AddressInfo.StreetAddress,
                                    StateName = x.AddressInfo.StateName,
                                    CountryName = x.AddressInfo.CountryName
                                }
                            })
                            .SingleAsync();
        }


        public async Task<int> AddOrUpdateSelfBusiness(TenantModel tenant,
                                                       SelfBusinessModel model,
                                                       int userId)
        {
            IncomeInfo incomeInfo;

            if (!model.Id.HasValue)
            {
                incomeInfo = new IncomeInfo
                {
                    TenantId = tenant.Id,
                    BorrowerId = model.BorrowerId,
                    TrackingState = TrackingState.Added
                };
                incomeInfo.AddressInfo = new AddressInfo
                {
                    TenantId = tenant.Id,
                    TrackingState = TrackingState.Added
                };
                Uow.Repository<IncomeInfo>().Insert(item: incomeInfo);
            }
            else
            {
                incomeInfo = await Uow.Repository<IncomeInfo>()
                                      .Query(query: x => x.TenantId == tenant.Id && x.Borrower.LoanApplicationId == model.LoanApplicationId && x.Borrower.LoanApplication.UserId == userId && x.Id == model.Id && x.BorrowerId == model.BorrowerId)
                                      .Include(navigationPropertyPath: x => x.AddressInfo)
                                      .SingleAsync();
                incomeInfo.TrackingState = TrackingState.Modified;
                incomeInfo.AddressInfo.TrackingState = TrackingState.Modified;
            }

            incomeInfo.IncomeTypeId = (int)IncomeTypes.SelfEmployment;
            incomeInfo.EmployerName = model.BusinessName;
            incomeInfo.EmployeeNumber = model.BusinessPhone;
            incomeInfo.JobTitle = model.JobTitle;
            incomeInfo.StartDate = model.StartDate;
            incomeInfo.MonthlyBaseIncome = model.AnnualIncome / 12;
            incomeInfo.AnnualBaseIncome = model.AnnualIncome;

            incomeInfo.AddressInfo.UnitNo = model.Address.Unit;
            incomeInfo.AddressInfo.StreetAddress = model.Address.Street;
            incomeInfo.AddressInfo.StateId = model.Address.StateId;
            incomeInfo.AddressInfo.StateName = model.Address.StateName;
            incomeInfo.AddressInfo.ZipCode = model.Address.ZipCode;
            incomeInfo.AddressInfo.CityName = model.Address.City;
            incomeInfo.AddressInfo.CountryId = model.Address.CountryId;
            incomeInfo.AddressInfo.CountryName = model.Address.CountryName;

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();

            return incomeInfo.Id;
        }


        public async Task<int> AddOrUpdateBusiness(TenantModel tenant,
                                                   BusinessModel model,
                                                   int userId)
        {

            IncomeInfo incomeInfo;

            if (!model.Id.HasValue)
            {
                incomeInfo = new IncomeInfo
                {
                    TenantId = tenant.Id,
                    BorrowerId = model.BorrowerId,
                    TrackingState = TrackingState.Added
                };
                incomeInfo.AddressInfo = new AddressInfo
                {
                    TenantId = tenant.Id,
                    TrackingState = TrackingState.Added
                };
                Uow.Repository<IncomeInfo>().Insert(item: incomeInfo);
            }
            else
            {
                incomeInfo = await Uow.Repository<IncomeInfo>()
                                      .Query(query: x => x.TenantId == tenant.Id && x.Borrower.LoanApplicationId == model.LoanApplicationId && x.Borrower.LoanApplication.UserId == userId && x.Id == model.Id && x.BorrowerId == model.BorrowerId)
                                      .Include(navigationPropertyPath: x => x.AddressInfo)
                                      .SingleAsync();
                incomeInfo.TrackingState = TrackingState.Modified;
                incomeInfo.AddressInfo.TrackingState = TrackingState.Modified;
            }

            incomeInfo.IncomeTypeId = model.IncomeTypeId;
            incomeInfo.EmployerName = model.BusinessName;
            incomeInfo.EmployeeNumber = model.BusinessPhone;
            incomeInfo.JobTitle = model.JobTitle;
            incomeInfo.StartDate = model.StartDate;
            incomeInfo.OwnershipPercentage = model.OwnershipPercentage;
            incomeInfo.MonthlyBaseIncome = model.AnnualIncome / 12;
            incomeInfo.AnnualBaseIncome = model.AnnualIncome;

            incomeInfo.AddressInfo.UnitNo = model.Address.Unit;
            incomeInfo.AddressInfo.StreetAddress = model.Address.Street;
            incomeInfo.AddressInfo.StateId = model.Address.StateId;
            incomeInfo.AddressInfo.StateName = model.Address.StateName;
            incomeInfo.AddressInfo.ZipCode = model.Address.ZipCode;
            incomeInfo.AddressInfo.CityName = model.Address.City;
            incomeInfo.AddressInfo.CountryId = model.Address.CountryId;
            incomeInfo.AddressInfo.CountryName = model.Address.CountryName;

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();

            return incomeInfo.Id;
        }


        public List<IncomeCategoryGetModel> GetAllIncomeCategories(TenantModel tenant)
        {
            var incomeCategories = _dbFunctionService.UdfIncomeCategory(tenantId: tenant.Id)
                                                     .Where(predicate: incomeCategory => incomeCategory.IsActive && !incomeCategory.IsDeleted)
                                                     .ToList();

            var incomeTypes = _dbFunctionService.UdfIncomeType(tenantId: tenant.Id)
                                                .Where(predicate: ig => ig.IsActive && !ig.IsDeleted)
                                                .ToList();

            var incomeCategoriesList = incomeCategories.Where(predicate: ic => incomeTypes.Any(predicate: it => it.IncomeCategoryId == ic.Id))
                                                       .OrderBy(keySelector: incomeCategory => incomeCategory.DisplayOrder)
                                                       .Select(selector: incomeCategory => new IncomeCategoryGetModel
                                                       {
                                                           Id = incomeCategory.Id,
                                                           Name = incomeCategory.Name,
                                                           DisplayName = incomeCategory.DisplayName
                                                       }).ToList();

            return incomeCategoriesList;
        }


        public async Task<int> AddOrUpdateRetirementIncomeInfo(RetirementIncomeInfoModel model,
                                                               TenantModel tenant,
                                                               int userId)
        {
            var incomeInfo = default(IncomeInfo);
            if (model.IncomeInfoId.HasValue)
            {
                incomeInfo = new IncomeInfo();
                incomeInfo = await Uow.Repository<IncomeInfo>()
                                      .Query(query: x => x.Id == model.IncomeInfoId.Value && x.Borrower.LoanApplicationId == model.LoanApplicationId
                                                                                          && x.BorrowerId == model.BorrowerId && x.Borrower.LoanApplication.UserId == userId && x.TenantId == tenant.Id)
                                      .SingleAsync();
                incomeInfo.TrackingState = TrackingState.Modified;
            }
            else
            {
                incomeInfo = new IncomeInfo
                {
                    BorrowerId = model.BorrowerId,
                    TenantId = tenant.Id,
                    TrackingState = TrackingState.Added
                };
                Uow.Repository<IncomeInfo>().Insert(item: incomeInfo);
            }

            var tenantIncomes = _dbFunctionService.UdfIncomeType(tenant.Id).ToList();

            var incomeTypeInfo = tenantIncomes.FirstOrDefault(inc => inc.Id == model.IncomeTypeId);

            incomeInfo.IncomeTypeId = model.IncomeTypeId;
            if (model.IncomeTypeId == (int)IncomeTypes.SocialSecurity)
            {
                incomeInfo.MonthlyBaseIncome = model.MonthlyBaseIncome;
                incomeInfo.AnnualBaseIncome = model.MonthlyBaseIncome * 12;
                if(incomeTypeInfo != null)
                {
                    incomeInfo.EmployerName = incomeTypeInfo.DisplayName;
                }
            }
            else if (model.IncomeTypeId == (int)IncomeTypes.Pension)
            {
                incomeInfo.EmployerName = model.EmployerName;
                incomeInfo.MonthlyBaseIncome = model.MonthlyBaseIncome;
                incomeInfo.AnnualBaseIncome = model.MonthlyBaseIncome * 12;
            }
            else if (model.IncomeTypeId == (int)IncomeTypes.Ira401K)
            {
                incomeInfo.MonthlyBaseIncome = model.MonthlyBaseIncome;
                incomeInfo.AnnualBaseIncome = model.MonthlyBaseIncome * 12;
                if (incomeTypeInfo != null)
                {
                    incomeInfo.EmployerName = incomeTypeInfo.DisplayName;
                }
            }
            else if (model.IncomeTypeId == (int)IncomeTypes.OtherRetirement)
            {
                incomeInfo.Description = model.Description;
                incomeInfo.MonthlyBaseIncome = model.MonthlyBaseIncome;
                incomeInfo.AnnualBaseIncome = model.MonthlyBaseIncome * 12;
            }

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                                       .Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
                                       .SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await Uow.SaveChangesAsync();

            return incomeInfo.Id;
        }


        public List<IncomeGroupModel> GetAllIncomeGroupsWithOtherIncomeTypes(TenantModel tenant)
        {
            var incomeGroups = _dbFunctionService.UdfIncomeGroup(tenantId: tenant.Id)
                                                 .Where(predicate: incomeGroup => incomeGroup.IsActive && !incomeGroup.IsDeleted).ToList();

            var incomeTypes = _dbFunctionService.UdfIncomeType(tenantId: tenant.Id)
                                                .Where(predicate: incomeType => incomeType.IsActive && !incomeType.IsDeleted).ToList();

            var incomeGroupsList = incomeGroups.Select(selector: incomeGroup =>
            {
                incomeGroup.IncomeTypes = incomeTypes.Where(predicate: incomeType => incomeType.IncomeGroupId == incomeGroup.Id).Select(selector: incomeType => incomeType).ToList();
                return incomeGroup;
            });

            incomeGroupsList = incomeGroupsList.Where(predicate: incomeGroup => incomeGroup.IncomeTypes.Any()).ToList();



            return incomeGroupsList.Select(selector: incomeGroup => new IncomeGroupModel
            {
                ImageUrl = incomeGroup.Image,
                IncomeGroupId = incomeGroup.Id,
                IncomeGroupName = incomeGroup.Name,
                IncomeGroupDisplayName = incomeGroup.DisplayName,
                IncomeGroupDescription = incomeGroup.Description,
                IncomeGroupDisplayOrder = incomeGroup.DisplayOrder,
                IncomeTypes = incomeGroup.IncomeTypes.Select(selector: incomeType => new IncomeGroupModel.IncomeTypeModel
                {
                    Id = incomeType.Id,
                    Name = incomeType.Name,
                    FieldsInfo = incomeType.FieldsInfo,
                })
                                                                                                 .ToList()
            })
                                   .ToList();
        }


        public async Task<int> AddOrUpdateMilitaryIncome(TenantModel tenant,
                                                         int userId,
                                                         MilitaryIncomeModel model)
        {
        
            IncomeInfo incomeInfo;

            if (!model.Id.HasValue)
            {
                incomeInfo = new IncomeInfo
                {
                    TenantId = tenant.Id,
                    BorrowerId = model.BorrowerId,
                    IncomeTypeId = (int)IncomeTypes.MilitaryPay,
                    TrackingState = TrackingState.Added
                };

                incomeInfo.OtherIncomeInfoes.Add(item: new OtherIncomeInfo
                {
                    TenantId = tenant.Id,
                    OtherIncomeTypeId = (int)OtherIncomeTypes.MilitaryEntitlements,
                    TrackingState = TrackingState.Added
                });

                incomeInfo.AddressInfo = new AddressInfo
                {
                    TenantId = tenant.Id,
                    TrackingState = TrackingState.Added
                };

                Uow.Repository<IncomeInfo>().Insert(item: incomeInfo);
            }
            else
            {
                incomeInfo = await Uow.Repository<IncomeInfo>()
                                      .Query(query: x => x.TenantId == tenant.Id && x.Borrower.LoanApplicationId == model.LoanApplicationId && x.Borrower.LoanApplication.UserId == userId && x.Id == model.Id && x.BorrowerId == model.BorrowerId)
                                      .Include(navigationPropertyPath: x => x.OtherIncomeInfoes)
                                      .Include(navigationPropertyPath: x => x.AddressInfo)
                                      .SingleAsync();
                incomeInfo.TrackingState = TrackingState.Modified;
                incomeInfo.AddressInfo.TrackingState = TrackingState.Modified;
            }

            incomeInfo.EmployerName = model.EmployerName;
            incomeInfo.JobTitle = model.JobTitle;
            incomeInfo.StartDate = model.StartDate;
            incomeInfo.YearsInProfession = model.YearsInProfession;
            incomeInfo.MonthlyBaseIncome = model.MonthlyBaseSalary;
            incomeInfo.AnnualBaseIncome = model.MonthlyBaseSalary * 12;

            incomeInfo.AddressInfo.UnitNo = model.Address.Unit;
            incomeInfo.AddressInfo.StreetAddress = model.Address.Street;
            incomeInfo.AddressInfo.StateId = model.Address.StateId;
            incomeInfo.AddressInfo.StateName = model.Address.StateName;
            incomeInfo.AddressInfo.ZipCode = model.Address.ZipCode;
            incomeInfo.AddressInfo.CityName = model.Address.City;
            incomeInfo.AddressInfo.CountryId = model.Address.CountryId;
            incomeInfo.AddressInfo.CountryName = model.Address.CountryName;

            var militaryOtherIncome = incomeInfo.OtherIncomeInfoes.Single(predicate: x => x.OtherIncomeTypeId == (int)OtherIncomeTypes.MilitaryEntitlements);

            militaryOtherIncome.MonthlyIncome = model.MilitaryEntitlements;
            militaryOtherIncome.AnnualIncome = model.MilitaryEntitlements * 12;
            militaryOtherIncome.TrackingState = model.Id.HasValue ? TrackingState.Modified : TrackingState.Added;

            var application = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId).SingleAsync();
            application.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: application);

            await SaveChangesAsync();

            return incomeInfo.Id;
        }


        public async Task<MilitaryIncomeModel> GetMilitaryIncome(TenantModel tenant,
                                                                 int userId,
                                                                 int borrowerId,
                                                                 int incomeInfoId)
        {
            return await Uow.Repository<IncomeInfo>()
                            .Query(query: x => x.TenantId == tenant.Id && x.Borrower.LoanApplication.UserId == userId && x.Id == incomeInfoId && x.BorrowerId == borrowerId)
                            .Select(selector: x => new MilitaryIncomeModel
                            {
                                Id = x.Id,
                                LoanApplicationId = x.Borrower.LoanApplicationId,
                                BorrowerId = x.BorrowerId,
                                EmployerName = x.EmployerName,
                                JobTitle = x.JobTitle,
                                StartDate = x.StartDate,
                                YearsInProfession = x.YearsInProfession,
                                MonthlyBaseSalary = x.MonthlyBaseIncome,
                                MilitaryEntitlements = x.OtherIncomeInfoes.Single(y => y.OtherIncomeTypeId == (int)OtherIncomeTypes.MilitaryEntitlements).MonthlyIncome,
                                Address = new GenericAddressModel
                                {
                                    Unit = x.AddressInfo.UnitNo,
                                    City = x.AddressInfo.CityName,
                                    ZipCode = x.AddressInfo.ZipCode,
                                    StateId = x.AddressInfo.StateId,
                                    CountryId = x.AddressInfo.CountryId,
                                    Street = x.AddressInfo.StreetAddress,
                                    StateName = x.AddressInfo.StateName,
                                    CountryName = x.AddressInfo.CountryName
                                }
                            })
                            .SingleAsync();
        }


        public async Task<IncomeInfoViewModel> GetRetirementIncomeInfo(int incomeInfoId,
                                                                       int borrowerId,
                                                                       int userId,
                                                                       TenantModel tenant)
        {
            return await Uow.Repository<IncomeInfo>().Query(query: x => x.Id == incomeInfoId && x.BorrowerId == borrowerId && x.Borrower.LoanApplication.UserId == userId && x.TenantId == tenant.Id)
                            .Select(selector: x => new IncomeInfoViewModel
                            {
                                IncomeInfoId = x.Id,
                                BorrowerId = x.BorrowerId,
                                IncomeTypeId = x.IncomeTypeId,
                                MonthlyBaseIncome = x.MonthlyBaseIncome,
                                EmployerName = x.EmployerName,
                                Description = x.Description,
                            }).SingleAsync();
        }


        public async Task<GetOtherIncomeInfoModel> GetOtherIncomeInfo(TenantModel tenant,
                                                                      int userId,
                                                                      int incomeInfoId)
        {
            var incomeInfo = await Uow.Repository<IncomeInfo>()
                                      .Query(query: incomeInfo => incomeInfo.Id == incomeInfoId && incomeInfo.Borrower.LoanApplication.UserId == userId && incomeInfo.TenantId == tenant.Id)
                                      .FirstOrDefaultAsync();
           // var obj = _dbFunctionService.UdfIncomeType(tenant.Id).FirstOrDefault(incometype=>incometype.Id== incomeInfo.IncomeTypeId);

            incomeInfo.IncomeType = _dbFunctionService.UdfIncomeType(tenant.Id).Single(incomeType => incomeType.Id == incomeInfo.IncomeTypeId);
            // Replacing Single with FirstOrDefault because there is no income group in migrated data
            incomeInfo.IncomeType.IncomeGroup = _dbFunctionService.UdfIncomeGroup(tenant.Id).FirstOrDefault(incomeGroup => incomeGroup.Id == incomeInfo.IncomeType.IncomeGroupId);


            var models = new GetOtherIncomeInfoModel
            {
                IncomeInfoId = incomeInfo.Id,
                IncomeTypeId = incomeInfo.IncomeType.Id,
                IncomeTypeName = incomeInfo.IncomeType.Name,
                //IncomeGroupId = incomeInfo.IncomeType.IncomeGroup.Id,
                //IncomeGroupName = incomeInfo.IncomeType.IncomeGroup.Name,
                MonthlyBaseIncome = incomeInfo.MonthlyBaseIncome,
                AnnualBaseIncome = incomeInfo.AnnualBaseIncome,
                Description = incomeInfo.Description,
                FieldInfo = incomeInfo.IncomeType.FieldsInfo
            };
            if (incomeInfo.IncomeType.IncomeGroup != null)
            {
                models.IncomeGroupId = incomeInfo.IncomeType.IncomeGroup.Id;
                models.IncomeGroupName = incomeInfo.IncomeType.IncomeGroup.Name;
            }

        

            return models;
        }


        public async Task<int> AddOrUpdateEmployerOtherMonthyIncome(TenantModel tenant,
                                                                    int userId,
                                                                    AddOrUpdateOtherIncomeModel model)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                                           .Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
                                           .Include(navigationPropertyPath: x => x.Borrowers).ThenInclude<LoanApplicationDb.Entity.Models.LoanApplication, Borrower, ICollection<IncomeInfo>>(navigationPropertyPath: b => b.IncomeInfoes)
                                           .FirstOrDefaultAsync();
            if (loanApplication == null)
            {
                _logger.LogWarning(message: $"Loan application not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
                return -1;
            }

            var borrowerInfo = loanApplication.Borrowers
                                              .FirstOrDefault(predicate: b => b.Id == model.BorrowerId);
            if (borrowerInfo == null)
            {
                _logger.LogWarning(message: $"Borrower not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
                return -1;
            }

            var incomeInfo = borrowerInfo.IncomeInfoes
                                         .FirstOrDefault(predicate: ii => ii.Id == model.IncomeInfoId);
            if (incomeInfo == null)
            {
                incomeInfo = new IncomeInfo
                {
                    TrackingState = TrackingState.Added,
                    EmployerName = Convert.ToString(model.IncomeTypeId),
                    BorrowerId = model.BorrowerId,
                    TenantId = tenant.Id
                };

                Uow.Repository<IncomeInfo>().Insert(item: incomeInfo);
            }
            else
            {
                incomeInfo.TrackingState = TrackingState.Modified;
            }

            incomeInfo.MonthlyBaseIncome = (decimal)(model.MonthlyBaseIncome ?? model.AnnualBaseIncome / 12);
            incomeInfo.AnnualBaseIncome = (decimal)(model.AnnualBaseIncome ?? model.MonthlyBaseIncome * 12);
            incomeInfo.Description = model.Description;
            incomeInfo.IncomeTypeId = (int)model.IncomeTypeId;
            loanApplication.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: loanApplication);
            await Uow.SaveChangesAsync();
            return incomeInfo.Id;
        }


        //public async Task<int> AddOrUpdateEmployerMonthlyIncomeWithDescription(TenantModel tenant,
        //                                                                      int userId,
        //                                                                      UpdateOtherMonthlyIncomeWithDescriptionModel model)
        //{
        //    var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
        //                                   .Query(query: x => x.TenantId == tenant.Id && x.UserId == userId)
        //                                   .Include(navigationPropertyPath: x => x.Borrowers).ThenInclude<LoanApplicationDb.Entity.Models.LoanApplication, Borrower, ICollection<IncomeInfo>>(navigationPropertyPath: b => b.IncomeInfoes)
        //                                   .FirstOrDefaultAsync();
        //    if (loanApplication == null)
        //    {
        //        _logger.LogWarning(message: $"Loan application not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
        //        return -1;
        //    }

        //    var borrowerInfo = loanApplication.Borrowers
        //                                      .FirstOrDefault(predicate: b => b.Id == model.BorrowerId);
        //    if (borrowerInfo == null)
        //    {
        //        _logger.LogWarning(message: $"Borrower not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
        //        return -1;
        //    }

        //    var incomeInfo = borrowerInfo.IncomeInfoes
        //                                 .FirstOrDefault(predicate: ii => ii.Id == model.IncomeInfoId);
        //    if (incomeInfo == null)
        //    {
        //        incomeInfo = new IncomeInfo
        //                     {
        //                         TrackingState = TrackingState.Added,
        //                         EmployerName = Convert.ToString(model.IncomeTypeId),
        //                         BorrowerId = model.BorrowerId,
        //                         TenantId = tenant.Id
        //                     };

        //        Uow.Repository<IncomeInfo>().Insert(item: incomeInfo);
        //    }
        //    else
        //    {
        //        incomeInfo.TrackingState = TrackingState.Modified;
        //    }

        //    incomeInfo.MonthlyBaseIncome = model.MonthlyBaseIncome;
        //    incomeInfo.AnnualBaseIncome = model.MonthlyBaseIncome * 12;
        //    incomeInfo.Description = model.Description;
        //    incomeInfo.IncomeTypeId = (int) model.IncomeTypeId;
        //    loanApplication.State = model.State;
        //    Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: loanApplication);
        //    await Uow.SaveChangesAsync();
        //    return incomeInfo.Id;
        //}


        //public async Task<int> AddOrUpdateEmployerYearlyIncome(TenantModel tenant,
        //                                                       int userId,
        //                                                       UpdateOtherAnnualIncomeModel model)
        //{
        //    var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
        //                                   .Query(query: x => x.TenantId == tenant.Id && x.UserId == userId)
        //                                   .Include(navigationPropertyPath: x => x.Borrowers).ThenInclude<LoanApplicationDb.Entity.Models.LoanApplication, Borrower, ICollection<IncomeInfo>>(navigationPropertyPath: b => b.IncomeInfoes)
        //                                   .FirstOrDefaultAsync();
        //    if (loanApplication == null)
        //    {
        //        _logger.LogWarning(message: $"Loan application not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
        //        return -1;
        //    }

        //    var borrowerInfo = loanApplication.Borrowers
        //                                      .FirstOrDefault(predicate: b => b.Id == model.BorrowerId);
        //    if (borrowerInfo == null)
        //    {
        //        _logger.LogWarning(message: $"Borrower not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
        //        return -1;
        //    }

        //    var incomeInfo = borrowerInfo.IncomeInfoes
        //                                 .FirstOrDefault(predicate: ii => ii.Id == model.IncomeInfoId);
        //    if (incomeInfo == null)
        //    {
        //        incomeInfo = new IncomeInfo
        //        {
        //            TrackingState = TrackingState.Added,
        //            BorrowerId = model.BorrowerId,
        //            EmployerName = Convert.ToString(model.IncomeTypeId)
        //                     };

        //        Uow.Repository<IncomeInfo>().Insert(item: incomeInfo);
        //    }
        //    else
        //    {
        //        incomeInfo.TrackingState = TrackingState.Modified;
        //    }

        //    incomeInfo.MonthlyBaseIncome = model.AnnualBaseIncome / 12;
        //    incomeInfo.AnnualBaseIncome = model.AnnualBaseIncome;

        //    incomeInfo.IncomeTypeId = (int) model.IncomeTypeId;
        //    loanApplication.State = model.State;
        //    Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: loanApplication);
        //    await Uow.SaveChangesAsync();
        //    return incomeInfo.Id;
        //}


        //public async Task<int> AddOrUpdateOtherAnnualIncomeWithDescription(TenantModel tenant,
        //                                                                   int userId,
        //                                                                   UpdateOtherAnnualIncomeWithDescriptionModel model)
        //{
        //    var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
        //                                   .Query(query: x => x.TenantId == tenant.Id && x.UserId == userId)
        //                                   .Include(navigationPropertyPath: x => x.Borrowers).ThenInclude<LoanApplicationDb.Entity.Models.LoanApplication, Borrower, ICollection<IncomeInfo>>(navigationPropertyPath: b => b.IncomeInfoes)
        //                                   .FirstOrDefaultAsync();
        //    if (loanApplication == null)
        //    {
        //        _logger.LogWarning(message: $"Loan application not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
        //        return -1;
        //    }

        //    var borrowerInfo = loanApplication.Borrowers
        //                                      .FirstOrDefault(predicate: b => b.Id == model.BorrowerId);
        //    if (borrowerInfo == null)
        //    {
        //        _logger.LogWarning(message: $"Borrower not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
        //        return -1;
        //    }

        //    var incomeInfo = borrowerInfo.IncomeInfoes
        //                                 .FirstOrDefault(predicate: ii => ii.Id == model.IncomeInfoId);
        //    if (incomeInfo == null)
        //    {
        //        incomeInfo = new IncomeInfo
        //                     {
        //                         TrackingState = TrackingState.Added,
        //                         BorrowerId = model.BorrowerId,
        //                         EmployerName=Convert.ToString(model.IncomeTypeId)
        //                     };

        //        Uow.Repository<IncomeInfo>().Insert(item: incomeInfo);
        //    }
        //    else
        //    {
        //        incomeInfo.TrackingState = TrackingState.Modified;
        //    }

        //    incomeInfo.MonthlyBaseIncome = model.AnnualBaseIncome / 12;
        //    incomeInfo.AnnualBaseIncome = model.AnnualBaseIncome;
        //    incomeInfo.Description = model.Description;
        //    incomeInfo.IncomeTypeId = (int) model.IncomeTypeId;
        //    loanApplication.State = model.State;
        //    Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: loanApplication);
        //    await Uow.SaveChangesAsync();
        //    return incomeInfo.Id;
        //}


        public async Task<List<EmployerDetailBaseModels>> GetSummary(TenantModel tenant,
                                                                     int userId,
                                                                     int loanApplicationId)
        {
            var model = new List<EmployerDetailBaseModels>();

            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                                           .Query(query: x => x.Id == loanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
                                           .Include(navigationPropertyPath: x => x.Borrowers).ThenInclude<LoanApplicationDb.Entity.Models.LoanApplication, Borrower, ICollection<IncomeInfo>>(navigationPropertyPath: b => b.IncomeInfoes).ThenInclude<LoanApplicationDb.Entity.Models.LoanApplication, IncomeInfo, AddressInfo>(navigationPropertyPath: ii => ii.AddressInfo)
                                           .Include(navigationPropertyPath: x => x.Borrowers).ThenInclude<LoanApplicationDb.Entity.Models.LoanApplication, Borrower, ICollection<IncomeInfo>>(navigationPropertyPath: b => b.IncomeInfoes).ThenInclude<LoanApplicationDb.Entity.Models.LoanApplication, IncomeInfo, ICollection<OtherIncomeInfo>>(navigationPropertyPath: ii => ii.OtherIncomeInfoes)
                                           .FirstOrDefaultAsync();

            if (loanApplication == null)
            {
                _logger.LogWarning(message: $"LoanApplication not found. Loan Application ID : {loanApplicationId} User ID : {userId}");
                var objEmployerDeatil = new EmployerDetailBaseModels();
                objEmployerDeatil.ErrorMessage = "LoanApplication not found";
                model.Add(item: objEmployerDeatil);
                return model;
            }

            var borrowerInfo = loanApplication.Borrowers.ToList();

            if (borrowerInfo.Count <= 0)
            {
                _logger.LogWarning(message: $"Borrower detail not found. Loan Application ID : {loanApplicationId} User ID : {userId}");
                var employerDetailBaseModels = new EmployerDetailBaseModels();
                employerDetailBaseModels.ErrorMessage = "Borrower detail not found";
                model.Add(item: employerDetailBaseModels);
                return model;
            }

            var incomeInfo = borrowerInfo.Select(selector: x => x.IncomeInfoes).ToList();
            //.FirstOrDefault(ii => ii.Id == incomeInfoId);
            if (incomeInfo.Count <= 0)
            {
                _logger.LogWarning(message: $"Borrowers not found. Loan Application ID : {loanApplicationId} User ID : {userId} ");
                var objEmployerDeatil = new EmployerDetailBaseModels();
                objEmployerDeatil.ErrorMessage = "Borrower income detail not found";

                model.Add(item: objEmployerDeatil);
                return model;
            }

            var modelToReturn = new List<EmployerDetailBaseModels>();

            foreach (var borrower in loanApplication.Borrowers)
            {
                modelToReturn.Add(item: new EmployerDetailBaseModels
                {
                    BorrowerId = borrower.Id,
                    OwnerTypeId = borrower.OwnTypeId,
                    IncomeInfos = borrower.IncomeInfoes.Select(selector: x => new IncomeInfos
                    {
                        BorrowerId = x.BorrowerId,
                        EmployerName = x.EmployerName,
                        EmployedByFamilyOrParty = x.IsEmployedByPartyInTransaction,
                        EmployerPhoneNumber = x.Phone,
                        EndDate = x.EndDate,
                        HasOwnershipInterest = x.OwnershipPercentage.HasValue,
                        IncomeInfoId = x.Id,
                        JobTitle = x.JobTitle,
                        OwnershipInterest = x.OwnershipPercentage,
                        StartDate = x.StartDate,
                        YearsInProfession = x.YearsInProfession,
                        OtherIncomes = x.OtherIncomeInfoes.Select(selector: otherIncomeInfo => new OtherIncome
                        {
                            MonthlyIncome = otherIncomeInfo.MonthlyIncome,
                            AnnualIncome = otherIncomeInfo.AnnualIncome,
                            IncomeTypeId = otherIncomeInfo.OtherIncomeTypeId
                        }).ToList()
                    }).ToList()
                });
                ;
            }

            return modelToReturn;
        }

        public async Task<GetBorrowerIncomesDataModel> GetBorrowerIncomes(TenantModel tenant, int userId, GetBorrowerIncomesModel model)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == model.LoanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(app => app.Borrowers).ThenInclude(b => b.LoanContact_LoanContactId)
                .Include(app => app.Borrowers).ThenInclude(b => b.IncomeInfoes)
                .FirstOrDefaultAsync();

            if(loanApplication == null)
            {
                _logger.LogWarning(message: $"LoanApplication not found. Loan Application ID : {model.LoanApplicationId} User ID : {userId}");
                return new GetBorrowerIncomesDataModel()
                {
                    ErrorMessage = "Loan application not found"
                };
            }

            var borrower = loanApplication.Borrowers.FirstOrDefault(b => b.Id == model.BorrowerId);
            if(borrower == null)
            {
                _logger.LogWarning(message: $"Borrower detail not found. Loan Application ID : {model.LoanApplicationId} User ID : {userId} Borrower ID : {model.BorrowerId}");
                return new GetBorrowerIncomesDataModel()
                {
                    ErrorMessage = "Loan application not found"
                };
            }

            GetBorrowerIncomesDataModel modelToReturn = new GetBorrowerIncomesDataModel()
            {
                LoanApplicationId = model.LoanApplicationId,
                BorrowerId = model.BorrowerId,
                ErrorMessage = null,
                BorrowerName = $"{borrower.LoanContact_LoanContactId.FirstName} {borrower.LoanContact_LoanContactId.LastName}",
                BorrowerIncomes = new List<GetBorrowerIncomesIncomeDataModel>()
            };
            var tenantOwnTypes = _dbFunctionService.UdfOwnType(tenant.Id);
            var ownTypeToSet = tenantOwnTypes.FirstOrDefault(own => own.Id == borrower.OwnTypeId);
            if (ownTypeToSet != null)
            {
                modelToReturn.OwnType = new GetBorrowerIncomesOwnTypeDataModel()
                {
                    OwnTypeDisplayName = ownTypeToSet.DisplayName,
                    OwnTypeId = borrower.OwnTypeId,
                    Name = ownTypeToSet.Name
                };
            }

            var tenantIncomeTypes = _dbFunctionService.UdfIncomeType(tenant.Id).ToList();
            var tenantIncomeCategoryTypes = _dbFunctionService.UdfIncomeCategory(tenant.Id).ToList();

            foreach (var income in borrower.IncomeInfoes)
            {
                var incomeToAdd = new GetBorrowerIncomesIncomeDataModel()
                {
                    //EmployerName = income.EmployerName,
                    EmployerName = income.EmployerName ?? (tenantIncomeTypes.Single(it => it.Id == income.IncomeTypeId).Name),
                    EndDate = income.EndDate,
                    IncomeInfoId = income.Id,
                    IsCurrentEmployment = income.EndDate == null,
                    StartDate = income.StartDate
                };
                var incomeType = tenantIncomeTypes.FirstOrDefault(type => type.Id == income.IncomeTypeId);
                if(incomeType != null)
                {
                    incomeToAdd.IncomeType = new GetBorrowerIncomesIncomeTypeDataModel()
                    {
                        DisplayName = incomeType.DisplayName,
                        IncomeTypeId = income.IncomeTypeId,
                        Name = incomeType.Name
                    };
                    var category =
                        tenantIncomeCategoryTypes.FirstOrDefault(cat => cat.Id == incomeType.IncomeCategoryId);
                    if(category != null)
                    {
                        incomeToAdd.EmploymentCategory = new GetBorrowerIncomeTypeCategoryDataModel()
                        {
                            CategoryDisplayName = category.DisplayName,
                            CategoryId = incomeType.IncomeCategoryId,
                            CategoryName = category.Name
                        };
                    }
                }
                modelToReturn.BorrowerIncomes.Add(incomeToAdd);
            }

            return modelToReturn;
        }
    }
}