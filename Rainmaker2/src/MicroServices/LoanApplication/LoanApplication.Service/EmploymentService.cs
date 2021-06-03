using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Extensions.ExtensionClasses;
using LoanApplication.Model;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TenantConfig.Common.DistributedCache;
using TrackableEntities.Common.Core;
using URF.Core.Abstractions;

namespace LoanApplication.Service
{
    public interface IEmploymentService : IServiceBase<IncomeInfo>
    {

        Dictionary<int, string> ErrorMessages { get; }
        List<EmploymentOtherIncome> GetEmploymentOtherDefaultIncomeTypes(TenantModel tenant);


        Task<int> AddOrUpdateCurrentEmploymentDetail(TenantModel tenant,
                                              int userId,
                                              AddOrUpdateEmploymentDetailModel model);

        Task<int> AddOrUpdatePreviousEmploymentDetail(TenantModel tenant,
            int userId,
            AddOrUpdatePreviousEmploymentDetailModel model);


        Task<EmploymentDetailBaseModel> GetEmploymentDetail(TenantModel tenant,
                                                            int userId,
                                                            int loanApplicationId,
                                                            int borrowerId,
                                                            int incomeInfoId);

        Task<int> DeleteIncomeDetail(TenantModel tenant, int userId, CurrentEmploymentDeleteModel model);

        Task<List<EmploymentDetailWithBorrower>> GetAllBorrowerWithIncome(TenantModel tenant, int userId, int loanApplicationId);
        Task<EmploymentHistoryModel> GetEmploymentHistory(TenantModel tenant, int userId, int loanApplicationId);

        Task<GetBorrowerWithIncomesForReviewModel> GetBorrowerIncomesForReview(TenantModel tenant, int userId,
            int loanApplicationId);
    }

    public class EmploymentService : ServiceBase<LoanApplicationContext, IncomeInfo>, IEmploymentService
    {
        private readonly IDbFunctionService _dbFunctionService;
        private readonly ILogger<EmploymentService> _logger;

        public const int LoanApplicationNotFound = -1;
        public const int BorrowerDetailNotFound = -2;
        public const int EmploymentInfoIsRequired = -3;
        public const int EmploymentEndDateMissing = -4;
        public const int BorrowerIncomeDetailNotFound = -5;

        public Dictionary<int, string> ErrorMessages { get; } = null;

        public EmploymentService(IUnitOfWork<LoanApplicationContext> uow,
                                 IServiceProvider services,
                                 ILogger<EmploymentService> logger,
                                 IDbFunctionService dbFunctionService) : base(previousUow: uow,
                                                                              services: services)
        {
            _logger = logger;
            _dbFunctionService = dbFunctionService;
            ErrorMessages = new Dictionary<int, string>()
            {
                {LoanApplicationNotFound, "Loan application not found"},
                {BorrowerDetailNotFound, "Borrower detail not found"},
                {EmploymentInfoIsRequired, "Employment data is required"},
                {EmploymentEndDateMissing, "Employment end date is missing"},
                {BorrowerIncomeDetailNotFound, "Borrower income detail not found"}
            };
        }


        //public async Task<int> AddOrUpdateCurrentEmployer(TenantModel tenant, int userId, EmploymentInfoBaseModel model)
        //{
        //    var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
        //        .Query(x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
        //        .Include(x => x.Borrowers).ThenInclude(b => b.IncomeInfoes)
        //        .FirstOrDefaultAsync();
        //    if (loanApplication == null)
        //    {
        //        _logger.LogWarning($"Loan application not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
        //        return -1;
        //    }
        //    var borrowerInfo = loanApplication.Borrowers
        //        .FirstOrDefault(b => b.Id == model.BorrowerId);
        //    if (borrowerInfo == null)
        //    {
        //        _logger.LogWarning($"Borrower not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
        //        return -1;
        //    }

        //    var incomeInfo = borrowerInfo.IncomeInfoes
        //        .FirstOrDefault(ii => ii.Id == model.IncomeInfoId);
        //    if (incomeInfo == null)
        //    {
        //        incomeInfo = new IncomeInfo() { TrackingState = TrackingState.Added, BorrowerId = model.BorrowerId };
        //        incomeInfo = this.SetEmploymentInfoData(incomeInfo, model);
        //        Uow.Repository<IncomeInfo>().Insert(incomeInfo);
        //    }
        //    else
        //    {
        //        incomeInfo = this.SetEmploymentInfoData(incomeInfo, model);
        //        incomeInfo.TrackingState = TrackingState.Modified;
        //    }

        //    loanApplication.State = model.State;
        //    Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
        //    await Uow.SaveChangesAsync();
        //    return incomeInfo.Id;
        //}

        //public async Task<EmploymentInfoGetModel> GetEmployerInfo(TenantModel tenant, int userId, int loanApplicationId, int borrowerId, int? incomeInfoId)
        //{
        //    EmploymentInfoGetModel modelToReturn = null;
        //    var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
        //        .Query(x => x.Id == loanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
        //        .Include(x => x.Borrowers).ThenInclude(b => b.IncomeInfoes)
        //        .FirstOrDefaultAsync();
        //    if (loanApplication == null)
        //    {
        //        _logger.LogWarning($"Loan application not found. Tenant ID {tenant.Id} Borrower ID : {borrowerId}");
        //        return null;
        //    }
        //    var borrowerInfo = loanApplication.Borrowers
        //        .FirstOrDefault(b => b.Id == borrowerId);
        //    if (borrowerInfo == null)
        //    {
        //        _logger.LogWarning($"Borrower not found. Tenant ID {tenant.Id} Borrower ID : {borrowerId}");
        //        return null;
        //    }

        //    var incomeInfo = borrowerInfo.IncomeInfoes
        //        .FirstOrDefault(ii => ii.Id == incomeInfoId);
        //    if (incomeInfo != null)
        //    {
        //        modelToReturn = new EmploymentInfoGetModel()
        //        {
        //            BorrowerId = incomeInfo.BorrowerId.Value,
        //            StartDate = incomeInfo.StartDate,
        //            OwnershipInterest = incomeInfo.OwnershipPercentage,
        //            IncomeInfoId = incomeInfoId,
        //            EmployedByFamilyOrParty = incomeInfo.IsEmployedByPartyInTransaction,
        //            EmployerName = incomeInfo.EmployerName,
        //            EmployerPhoneNumber = incomeInfo.Phone,
        //            EndDate = incomeInfo.EndDate,
        //            HasOwnershipInterest = incomeInfo.OwnershipPercentage.HasValue,
        //            JobTitle = incomeInfo.JobTitle,
        //            YearsInProfession = incomeInfo.YearsInProfession
        //        };
        //    }
        //    return modelToReturn;
        //}

        //public async Task<int> AddAddOrUpdateEmployerAddress(TenantModel tenant, int userId, EmployerAddressModel model)
        //{
        //    var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
        //        .Query(x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
        //        .Include(x => x.Borrowers).ThenInclude(b => b.IncomeInfoes).ThenInclude(ii => ii.AddressInfo)
        //        .FirstOrDefaultAsync();
        //    if (loanApplication == null)
        //    {
        //        _logger.LogWarning($"Loan application not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
        //        return -1;
        //    }
        //    var borrowerInfo = loanApplication.Borrowers
        //        .FirstOrDefault(b => b.Id == model.BorrowerId);
        //    if (borrowerInfo == null)
        //    {
        //        _logger.LogWarning($"Borrower not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
        //        return -2;
        //    }

        //    var incomeInfo = borrowerInfo.IncomeInfoes
        //        .FirstOrDefault(ii => ii.Id == model.IncomeInfoId);
        //    if (incomeInfo == null)
        //    {
        //        _logger.LogWarning($"Borrower income detail not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
        //        return -3;
        //    }

        //    AddressInfo employerAddress = incomeInfo.AddressInfo;
        //    if (employerAddress == null)
        //    {
        //        employerAddress = new AddressInfo() { TrackingState = TrackingState.Added };
        //        employerAddress = this.SetEmployerAddressInfo(employerAddress, model);
        //        Uow.Repository<AddressInfo>().Insert(employerAddress);
        //        await Uow.SaveChangesAsync();
        //        incomeInfo.EmployerAddressId = employerAddress.Id;
        //        incomeInfo.TrackingState = TrackingState.Modified;
        //    }
        //    else
        //    {
        //        employerAddress = this.SetEmployerAddressInfo(employerAddress, model);
        //        employerAddress.TrackingState = TrackingState.Modified;
        //    }
        //    loanApplication.State = model.State;
        //    Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
        //    await Uow.SaveChangesAsync();
        //    return employerAddress.Id;

        //}

        //public async Task<EmployerAddressBaseModel> GetEmployerAddress(TenantModel tenant, int userId, int loanApplicationId, int borrowerId, int incomeInfoId)
        //{
        //    EmployerAddressBaseModel modelToReturn = null;
        //    var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
        //        .Query(x => x.Id == loanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
        //        .Include(x => x.Borrowers).ThenInclude(b => b.IncomeInfoes).ThenInclude(ii => ii.AddressInfo)
        //        .FirstOrDefaultAsync();
        //    if (loanApplication == null)
        //    {
        //        _logger.LogWarning($"Loan application not found. Tenant ID {tenant.Id} Borrower ID : {borrowerId}");
        //        return null;
        //    }
        //    var borrowerInfo = loanApplication.Borrowers
        //        .FirstOrDefault(b => b.Id == borrowerId);
        //    if (borrowerInfo == null)
        //    {
        //        _logger.LogWarning($"Borrower not found. Tenant ID {tenant.Id} Borrower ID : {borrowerId}");
        //        return null;
        //    }

        //    var incomeInfo = borrowerInfo.IncomeInfoes
        //        .FirstOrDefault(ii => ii.Id == incomeInfoId);
        //    if (incomeInfo == null)
        //    {
        //        _logger.LogWarning($"Borrower income detail not found. Tenant ID {tenant.Id} Borrower ID : {borrowerId} Income ID : {incomeInfo}");
        //        return null;
        //    }
        //    if (incomeInfo.AddressInfo != null)
        //    {
        //        modelToReturn = new EmployerAddressBaseModel()
        //        {
        //            BorrowerId = borrowerId,
        //            IncomeInfoId = incomeInfoId,
        //            StreetAddress = incomeInfo.AddressInfo.StreetAddress,
        //            UnitNo = incomeInfo.AddressInfo.UnitNo,
        //            CityId = incomeInfo.AddressInfo.CityId,
        //            StateId = incomeInfo.AddressInfo.StateId,
        //            StateName = incomeInfo.AddressInfo.StateName,
        //            ZipCode = incomeInfo.AddressInfo.ZipCode
        //    };
        //    }
        //    return modelToReturn;
        //}

        //public async Task<int> UpdateEmployerBaseSalary(TenantModel tenant, int userId, EmployerAnnualSalaryModel model)
        //{
        //    var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
        //        .Query(x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
        //        .Include(x => x.Borrowers).ThenInclude(b => b.IncomeInfoes).ThenInclude(ii => ii.AddressInfo)
        //        .FirstOrDefaultAsync();
        //    if (loanApplication == null)
        //    {
        //        _logger.LogWarning($"Loan application not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
        //        return -1;
        //    }
        //    var borrowerInfo = loanApplication.Borrowers
        //        .FirstOrDefault(b => b.Id == model.BorrowerId);
        //    if (borrowerInfo == null)
        //    {
        //        _logger.LogWarning($"Borrower not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
        //        return -2;
        //    }

        //    var incomeInfo = borrowerInfo.IncomeInfoes
        //        .FirstOrDefault(ii => ii.Id == model.IncomeInfoId);
        //    if (incomeInfo == null)
        //    {
        //        _logger.LogWarning($"Borrower income detail not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
        //        return -3;
        //    }

        //    if (model.AnnualBaseSalary >= 0 && (model.MonthlyBaseSalary == null || model.MonthlyBaseSalary <= 0))
        //    {
        //        model.MonthlyBaseSalary = model.AnnualBaseSalary / 12;
        //    }

        //    if (model.MonthlyBaseSalary >= 0 && (model.AnnualBaseSalary == null || model.AnnualBaseSalary <= 0))
        //    {
        //        model.AnnualBaseSalary = model.MonthlyBaseSalary * 12;
        //    }

        //    incomeInfo.AnnualBaseIncome = model.AnnualBaseSalary;
        //    incomeInfo.MonthlyBaseIncome = model.MonthlyBaseSalary;
        //    incomeInfo.IsHourlyPayment = false;
        //    incomeInfo.HourlyRate = null;
        //    incomeInfo.HoursPerWeek = null;

        //    loanApplication.State = model.State;
        //    Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
        //    await Uow.SaveChangesAsync();
        //    return incomeInfo.Id;

        //}

        //public async Task<EmployerSalaryGetModel> GetEmployerIncomeDetail(TenantModel tenant, int userId, int loanApplicationId, int borrowerId, int incomeInfoId)
        //{
        //    EmployerSalaryGetModel modelToReturn = null;
        //    var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
        //        .Query(x => x.Id == loanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
        //        .Include(x => x.Borrowers).ThenInclude(b => b.IncomeInfoes).ThenInclude(ii => ii.AddressInfo)
        //        .FirstOrDefaultAsync();
        //    if (loanApplication == null)
        //    {
        //        _logger.LogWarning($"Loan application not found. Tenant ID {tenant.Id} Borrower ID : {borrowerId}");
        //        return null;
        //    }
        //    var borrowerInfo = loanApplication.Borrowers
        //        .FirstOrDefault(b => b.Id == borrowerId);
        //    if (borrowerInfo == null)
        //    {
        //        _logger.LogWarning($"Borrower not found. Tenant ID {tenant.Id} Borrower ID : {borrowerId}");
        //        return null;
        //    }

        //    var incomeInfo = borrowerInfo.IncomeInfoes
        //        .FirstOrDefault(ii => ii.Id == incomeInfoId);
        //    if (incomeInfo == null)
        //    {
        //        _logger.LogWarning($"Borrower income detail not found. Tenant ID {tenant.Id} Borrower ID : {borrowerId} Income ID : {incomeInfo}");
        //        return null;
        //    }

        //    modelToReturn = new EmployerSalaryGetModel()
        //    {
        //        MonthlyBaseSalary = incomeInfo.MonthlyBaseIncome,
        //        AnnualBaseSalary = incomeInfo.AnnualBaseIncome,
        //        BorrowerId = borrowerId,
        //        HourlyRate = incomeInfo.HourlyRate,
        //        HoursPerWeek = incomeInfo.HoursPerWeek,
        //        IncomeInfoId = incomeInfoId,
        //        IsHourlyBase = incomeInfo.IsHourlyPayment,
        //        LoanApplicationId = loanApplication.Id
        //    };
        //    return modelToReturn;
        //}

        //public async Task<int> UpdateEmployerHourlyIncome(TenantModel tenant, int userId, EmployerHourlyModel model)
        //{
        //    var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
        //        .Query(x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
        //        .Include(x => x.Borrowers).ThenInclude(b => b.IncomeInfoes).ThenInclude(ii => ii.AddressInfo)
        //        .FirstOrDefaultAsync();
        //    if (loanApplication == null)
        //    {
        //        _logger.LogWarning($"Loan application not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
        //        return -1;
        //    }
        //    var borrowerInfo = loanApplication.Borrowers
        //        .FirstOrDefault(b => b.Id == model.BorrowerId);
        //    if (borrowerInfo == null)
        //    {
        //        _logger.LogWarning($"Borrower not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
        //        return -2;
        //    }

        //    var incomeInfo = borrowerInfo.IncomeInfoes
        //        .FirstOrDefault(ii => ii.Id == model.IncomeInfoId);
        //    if (incomeInfo == null)
        //    {
        //        _logger.LogWarning($"Borrower income detail not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
        //        return -3;
        //    }

        //    incomeInfo.IsHourlyPayment = true;
        //    incomeInfo.HourlyRate = model.HourlyRate;
        //    incomeInfo.HoursPerWeek = model.HoursPerWeek;
        //    incomeInfo.MonthlyBaseIncome = null;
        //    incomeInfo.AnnualBaseIncome = null;

        //    loanApplication.State = model.State;
        //    Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(loanApplication);
        //    await Uow.SaveChangesAsync();
        //    return incomeInfo.Id;

        //}


        public List<EmploymentOtherIncome> GetEmploymentOtherDefaultIncomeTypes(TenantModel tenant) //todo dani need for this function
        {
            var otherIncomesToReturn = new List<EmploymentOtherIncomeType>
                                       {
                                           EmploymentOtherIncomeType.Bonus,
                                           EmploymentOtherIncomeType.Commission,
                                           EmploymentOtherIncomeType.Overtime
                                       };

            var results = _dbFunctionService.UdfOtherIncomeType(tenantId: tenant.Id)
                                            .Where(predicate: otherIncomeType => otherIncomeType.IsActive && !otherIncomeType.IsDeleted
                                                                                                          && otherIncomesToReturn.Contains((EmploymentOtherIncomeType) otherIncomeType.Id))
                                            .OrderBy(keySelector: otherIncomeType => otherIncomeType.DisplayOrder)
                                            .ThenBy(keySelector: otherIncomeType => otherIncomeType.Name)
                                            .Select(selector: otherIncomeType => new EmploymentOtherIncome
                                                                                 {
                                                                                     Id = otherIncomeType.Id,
                                                                                     Name = otherIncomeType.Name,
                                                                                     DisplayName = otherIncomeType.DisplayName,
                                                                                 }).ToList();

            return results;
        }


        public async Task<int> AddOrUpdateCurrentEmploymentDetail(TenantModel tenant,
                                                           int userId,
                                                           AddOrUpdateEmploymentDetailModel model)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                                           .Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
                                           .Include(navigationPropertyPath: x => x.Borrowers).ThenInclude(navigationPropertyPath: b => b.IncomeInfoes).ThenInclude(navigationPropertyPath: ii => ii.AddressInfo)
                                           .Include(navigationPropertyPath: x => x.Borrowers).ThenInclude(navigationPropertyPath: b => b.IncomeInfoes).ThenInclude(navigationPropertyPath: ii => ii.OtherIncomeInfoes)
                                           .FirstOrDefaultAsync();

            if (loanApplication == null)
            {
                _logger.LogWarning(message: $"Loan application not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
                return LoanApplicationNotFound;
            }

            var borrowerInfo = loanApplication.Borrowers
                                              .FirstOrDefault(predicate: b => b.Id == model.BorrowerId);
            if (borrowerInfo == null)
            {
                _logger.LogWarning(message: $"Borrower not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
                return BorrowerDetailNotFound;
            }

            if (model.EmploymentInfo == null)
            {
                _logger.LogWarning(message: $"Incoming employment data is missing. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
                return -3;
            }

            model.EmploymentInfo.EndDate = null;
            

            var incomeInfo = borrowerInfo.IncomeInfoes
                                         .FirstOrDefault(predicate: info => info.Id == model.EmploymentInfo.IncomeInfoId);

            var defaultIncomeTypes = GetEmploymentOtherDefaultIncomeTypes(tenant: tenant);

            if (incomeInfo == null)
            {
                incomeInfo = new IncomeInfo
                             {
                                 TrackingState = TrackingState.Added,
                                 AddressInfo = new AddressInfo
                                               {
                                                   TrackingState = TrackingState.Added
                                               },
                                 IncomeTypeId = (int)BorrowerIncomeTypes.EmploymentInfo,
                                 TenantId = tenant.Id
                };
                borrowerInfo.IncomeInfoes.Add(item: incomeInfo);
                borrowerInfo.TrackingState = TrackingState.Modified;
                incomeInfo = SetEmploymentInfo(incomeInfo: incomeInfo, employmentInfo: model.EmploymentInfo, true);
                if (model.EmployerAddress == null)
                {
                    _logger.LogWarning($"Employment address is missing. Loan Application ID : {model.LoanApplicationId} Borrower ID : {model.BorrowerId}");
                }
                else
                {
                    incomeInfo.AddressInfo = SetEmployerAddressInfo(employerAddressInfo: incomeInfo.AddressInfo, model: model.EmployerAddress);
                }
                if(model.WayOfIncome != null)
                {
                    incomeInfo = SetWayOfIncomeInfo(incomeInfo: incomeInfo, wayOfIncome: model.WayOfIncome);
                }

                if (model.EmploymentOtherIncomes != null)
                {
                    foreach (var otherIncome in model.EmploymentOtherIncomes)
                    {
                        var otherIncomeInfo = new OtherIncomeInfo
                        {
                            TrackingState = TrackingState.Added
                        };
                        otherIncomeInfo = SetEmploymentOtherIncome(otherIncomeInfo: otherIncomeInfo,
                            employmentOtherAnnualIncome: otherIncome,
                            tenantId: tenant.Id);
                        otherIncomeInfo.Description = defaultIncomeTypes.FirstOrDefault(predicate: x =>  x.Id == (int)otherIncome.IncomeTypeId)?.Name;
                        incomeInfo.OtherIncomeInfoes.Add(item: otherIncomeInfo);
                    }
                }

                Uow.Repository<IncomeInfo>().Insert(item: incomeInfo);
            }
            else
            {
                incomeInfo.TrackingState = TrackingState.Modified;
                incomeInfo.AddressInfo.TrackingState = TrackingState.Modified;
                incomeInfo = SetEmploymentInfo(incomeInfo: incomeInfo, employmentInfo: model.EmploymentInfo, true);
                incomeInfo.AddressInfo = SetEmployerAddressInfo(employerAddressInfo: incomeInfo.AddressInfo,
                                                                model: model.EmployerAddress);
                incomeInfo = SetWayOfIncomeInfo(incomeInfo: incomeInfo,
                                                wayOfIncome: model.WayOfIncome);

                foreach (var existingOtherIncomeItem in incomeInfo.OtherIncomeInfoes)
                {
                    existingOtherIncomeItem.TrackingState = TrackingState.Deleted;
                    Uow.Repository<OtherIncomeInfo>().Delete(item: existingOtherIncomeItem);
                }

                if (model.EmploymentOtherIncomes == null)
                {
                    _logger.LogWarning(message: $"SKIPPING OTHER INCOME. Other employment income should be empty array.  Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
                }
                else
                {
                    foreach (var otherIncome in model.EmploymentOtherIncomes)
                    {
                        var itemToAdd = new OtherIncomeInfo
                        {
                            TrackingState = TrackingState.Added
                        };
                        itemToAdd = SetEmploymentOtherIncome(otherIncomeInfo: itemToAdd,
                            employmentOtherAnnualIncome: otherIncome,
                            tenantId: tenant.Id);
                        itemToAdd.Description = defaultIncomeTypes.FirstOrDefault(predicate: employmentOtherIncome => employmentOtherIncome.Id == (int)otherIncome.IncomeTypeId)?.Name;
                        incomeInfo.OtherIncomeInfoes.Add(item: itemToAdd);
                        //Uow.Repository<OtherIncomeInfo>().Insert(itemToAdd);
                    }
                }

                Uow.Repository<IncomeInfo>().Update(item: incomeInfo);
            }

            loanApplication.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: loanApplication);
            await Uow.SaveChangesAsync();
            return incomeInfo.Id;
        }

        public async Task<int> AddOrUpdatePreviousEmploymentDetail(TenantModel tenant,
                                                           int userId,
                                                           AddOrUpdatePreviousEmploymentDetailModel model)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                                           .Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
                                           .Include(navigationPropertyPath: x => x.Borrowers).ThenInclude(navigationPropertyPath: b => b.IncomeInfoes).ThenInclude(navigationPropertyPath: ii => ii.AddressInfo)
                                           .Include(navigationPropertyPath: x => x.Borrowers).ThenInclude(navigationPropertyPath: b => b.IncomeInfoes).ThenInclude(navigationPropertyPath: ii => ii.OtherIncomeInfoes)
                                           .FirstOrDefaultAsync();

            if (loanApplication == null)
            {
                _logger.LogWarning(message: $"Loan application not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
                return LoanApplicationNotFound;
            }

            var borrowerInfo = loanApplication.Borrowers
                                              .FirstOrDefault(predicate: b => b.Id == model.BorrowerId);
            if (borrowerInfo == null)
            {
                _logger.LogWarning(message: $"Borrower not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
                return BorrowerDetailNotFound;
            }

            if (model.EmploymentInfo == null)
            {
                _logger.LogWarning(message: $"Incoming employment data is missing. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
                return -3;
            }

            if (model.EmploymentInfo.EndDate == null)
            {
                _logger.LogWarning(message: $"End date for previous employment is null. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
                return EmploymentEndDateMissing;
            }


            var incomeInfo = borrowerInfo.IncomeInfoes
                                         .FirstOrDefault(predicate: info => info.Id == model.EmploymentInfo.IncomeInfoId);

            var defaultIncomeTypes = GetEmploymentOtherDefaultIncomeTypes(tenant: tenant);

            if (incomeInfo == null)
            {
                incomeInfo = new IncomeInfo
                {
                    TrackingState = TrackingState.Added,
                    AddressInfo = new AddressInfo
                    {
                        TrackingState = TrackingState.Added
                    },
                    IncomeTypeId = (int)BorrowerIncomeTypes.EmploymentInfo,
                    TenantId = tenant.Id
                };
                borrowerInfo.IncomeInfoes.Add(item: incomeInfo);
                borrowerInfo.TrackingState = TrackingState.Modified;
                incomeInfo = SetEmploymentInfo(incomeInfo: incomeInfo, employmentInfo: model.EmploymentInfo, false);
                if (model.EmployerAddress == null)
                {
                    _logger.LogWarning($"Employment address is missing. Loan Application ID : {model.LoanApplicationId} Borrower ID : {model.BorrowerId}");
                }
                else
                {
                    incomeInfo.AddressInfo = SetEmployerAddressInfo(employerAddressInfo: incomeInfo.AddressInfo, model: model.EmployerAddress);
                }
                if (model.WayOfIncome != null)
                {
                    incomeInfo.AnnualBaseIncome = model.WayOfIncome.EmployerAnnualSalary;
                    incomeInfo.MonthlyBaseIncome = model.WayOfIncome.EmployerAnnualSalary / 12;
                }

                Uow.Repository<IncomeInfo>().Insert(item: incomeInfo);
            }
            else
            {
                incomeInfo.TrackingState = TrackingState.Modified;
                incomeInfo.AddressInfo.TrackingState = TrackingState.Modified;
                incomeInfo = SetEmploymentInfo(incomeInfo: incomeInfo, employmentInfo: model.EmploymentInfo, false);
                incomeInfo.AddressInfo = SetEmployerAddressInfo(employerAddressInfo: incomeInfo.AddressInfo,
                                                                model: model.EmployerAddress);
                incomeInfo.AnnualBaseIncome = model.WayOfIncome.EmployerAnnualSalary;
                incomeInfo.MonthlyBaseIncome = model.WayOfIncome.EmployerAnnualSalary / 12;

                Uow.Repository<IncomeInfo>().Update(item: incomeInfo);
            }

            loanApplication.State = model.State;
            Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>().Update(item: loanApplication);
            await Uow.SaveChangesAsync();
            return incomeInfo.Id;
        }


        public async Task<EmploymentDetailBaseModel> GetEmploymentDetail(TenantModel tenant,
                                                                         int userId,
                                                                         int loanApplicationId,
                                                                         int borrowerId,
                                                                         int incomeInfoId)
        {
            var model = new EmploymentDetailBaseModel();

            var applicationInfo = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                                           .Query(query: x => x.Id == loanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
                                           .Include(navigationPropertyPath: loanApplication => loanApplication.Borrowers).ThenInclude(navigationPropertyPath: borrower => borrower.IncomeInfoes).ThenInclude(navigationPropertyPath: incomeInfo1 => incomeInfo1.AddressInfo)
                                           .Include(navigationPropertyPath: loanApplication => loanApplication.Borrowers).ThenInclude(navigationPropertyPath: borrower => borrower.IncomeInfoes).ThenInclude(navigationPropertyPath: incomeInfo1 => incomeInfo1.OtherIncomeInfoes)
                                           .FirstOrDefaultAsync();

            if (applicationInfo == null)
            {
                _logger.LogWarning(message: $"Application detail not found. Loan Application ID : {loanApplicationId} User ID : {userId}");
                model.ErrorMessage = ErrorMessages[LoanApplicationNotFound];
                return model;
            }

            var borrowerInfo = applicationInfo.Borrowers
                                              .FirstOrDefault(predicate: b => b.Id == borrowerId);
            if (borrowerInfo == null)
            {
                _logger.LogWarning(message: $"Borrower detail not found. Loan Application ID : {loanApplicationId} User ID : {userId} Borrower ID {borrowerId}");
                model.ErrorMessage = ErrorMessages[BorrowerDetailNotFound];
                return model;
            }

            var incomeInfo = borrowerInfo.IncomeInfoes
                                         .FirstOrDefault(predicate: ii => ii.Id == incomeInfoId);
            if (incomeInfo == null)
            {
                _logger.LogWarning(message: $"Borrower income detail not found. Loan Application ID : {loanApplicationId} User ID : {userId} Borrower ID {borrowerId} Income Info ID : {incomeInfoId}");
                model.ErrorMessage = ErrorMessages[BorrowerIncomeDetailNotFound];
                return model;
            }

            model.BorrowerId = borrowerId;
            model.LoanApplicationId = loanApplicationId;
            model.EmploymentInfo = new EmploymentInfo
                                   {
                                       BorrowerId = borrowerId,
                                       EmployerName = incomeInfo.EmployerName,
                                       EmployedByFamilyOrParty = incomeInfo.IsEmployedByPartyInTransaction,
                                       EmployerPhoneNumber = incomeInfo.Phone,
                                       EndDate = incomeInfo.EndDate,
                                       HasOwnershipInterest = incomeInfo.OwnershipPercentage.HasValue,
                                       IncomeInfoId = incomeInfo.Id,
                                       JobTitle = incomeInfo.JobTitle,
                                       OwnershipInterest = incomeInfo.OwnershipPercentage,
                                       StartDate = incomeInfo.StartDate,
                                       YearsInProfession = incomeInfo.YearsInProfession
                                   };
            if (incomeInfo.AddressInfo != null)
                model.EmployerAddress = new EmployerAddressBaseModel
                                        {
                                            BorrowerId = borrowerId,
                                            CityId = incomeInfo.AddressInfo.CityId,
                                            CityName = incomeInfo.AddressInfo.CityName,
                                            CountryId = incomeInfo.AddressInfo.CountryId,
                                            IncomeInfoId = incomeInfoId,
                                            StateId = incomeInfo.AddressInfo.StateId,
                                            StateName = incomeInfo.AddressInfo.StateName,
                                            StreetAddress = incomeInfo.AddressInfo.StreetAddress,
                                            UnitNo = incomeInfo.AddressInfo.UnitNo,
                                            ZipCode = incomeInfo.AddressInfo.ZipCode
                                        };

            model.WayOfIncome = new WayOfIncome
                                {
                                    HourlyRate = incomeInfo.HourlyRate,
                                    HoursPerWeek = incomeInfo.HoursPerWeek,
                                    EmployerAnnualSalary = incomeInfo.AnnualBaseIncome,
                                    IsPaidByMonthlySalary = incomeInfo.IsHourlyPayment == false
                                };
            if (incomeInfo.OtherIncomeInfoes != null)
            {
                model.EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>();
                foreach (var otherIncome in incomeInfo.OtherIncomeInfoes)
                    model.EmploymentOtherIncomes.Add(item: new EmploymentOtherAnnualIncome
                                                           {
                                                               AnnualIncome = otherIncome.AnnualIncome,
                                                               IncomeTypeId = (EmploymentOtherIncomeType) otherIncome.OtherIncomeTypeId
                                                           });
            }

            return model;
        }


        public async Task<int> DeleteIncomeDetail(TenantModel tenant, int userId, CurrentEmploymentDeleteModel model)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(query: x => x.Id == model.LoanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
                .Include(navigationPropertyPath: x => x.Borrowers).ThenInclude(navigationPropertyPath: b => b.IncomeInfoes).ThenInclude(navigationPropertyPath: ii => ii.AddressInfo)
                .Include(navigationPropertyPath: x => x.Borrowers).ThenInclude(navigationPropertyPath: b => b.IncomeInfoes).ThenInclude(navigationPropertyPath: ii => ii.OtherIncomeInfoes)
                .FirstOrDefaultAsync();

            if (loanApplication == null)
            {
                _logger.LogWarning(message: $"Loan application not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
                return LoanApplicationNotFound;
            }

            var borrowerInfo = loanApplication.Borrowers
                .FirstOrDefault(predicate: b => b.Id == model.BorrowerId);
            if (borrowerInfo == null)
            {
                _logger.LogWarning(message: $"Borrower not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
                return BorrowerDetailNotFound;
            }

            if (borrowerInfo.IncomeInfoes == null || borrowerInfo.IncomeInfoes.Count == 0)
            {
                _logger.LogWarning(message: $"No income is associates with borrower. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId}");
                return 0;
            }

            var incomeToDelete = borrowerInfo.IncomeInfoes.FirstOrDefault(x => x.Id == model.IncomeInfoId);
            if (incomeToDelete == null)
            {
                _logger.LogWarning(message: $"Borrower income detail not found. Tenant ID {tenant.Id} Borrower ID : {model.BorrowerId} Income Info ID : {model.IncomeInfoId}");
                return BorrowerIncomeDetailNotFound;
            }

            incomeToDelete.TrackingState = TrackingState.Deleted;
            if (incomeToDelete.OtherIncomeInfoes != null)
            {
                incomeToDelete.OtherIncomeInfoes.ForEach(otherIncome =>
                {
                    Uow.Repository<OtherIncomeInfo>().Delete(otherIncome); 
                });
            }

            if(incomeToDelete.AddressInfo != null)
            {
                //incomeToDelete.AddressInfo.TrackingState = TrackingState.Deleted;
                Uow.Repository<AddressInfo>().Delete(incomeToDelete.AddressInfo);
            }

            Uow.Repository<IncomeInfo>().Delete(incomeToDelete);
            await Uow.SaveChangesAsync();
            return 1;
        }

        public async Task<List<EmploymentDetailWithBorrower>> GetAllBorrowerWithIncome(TenantModel tenant, int userId, int loanApplicationId)
        {
            List<EmploymentDetailWithBorrower> modelToReturn = new List<EmploymentDetailWithBorrower>();
            var applicationInfo = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(query: x => x.Id == loanApplicationId && x.TenantId == tenant.Id && x.UserId == userId)
                .Include(loanApplication => loanApplication.Borrowers).ThenInclude(borrower => borrower.LoanContact_LoanContactId)
                .Include(navigationPropertyPath: loanApplication => loanApplication.Borrowers).ThenInclude(navigationPropertyPath: borrower => borrower.IncomeInfoes).ThenInclude(navigationPropertyPath: incomeInfo1 => incomeInfo1.AddressInfo)
                .Include(navigationPropertyPath: loanApplication => loanApplication.Borrowers).ThenInclude(navigationPropertyPath: borrower => borrower.IncomeInfoes).ThenInclude(navigationPropertyPath: incomeInfo1 => incomeInfo1.OtherIncomeInfoes)
                .FirstOrDefaultAsync();

            if (applicationInfo == null)
            {
                _logger.LogWarning(message: $"Application detail not found. Loan Application ID : {loanApplicationId} User ID : {userId}");
                return null;
            }

            var tenantOtherIncomes = _dbFunctionService.UdfOtherIncomeType(tenant.Id).ToList();

            var tenantIncomeTypes = _dbFunctionService.UdfIncomeType(tenant.Id);
            var tenantEmploymentCategories = _dbFunctionService.UdfIncomeCategory(tenant.Id);
            var tenantOwnTypes = _dbFunctionService.UdfOwnType(tenant.Id);

            foreach (var borrower in applicationInfo.Borrowers)
            {
                var ownType = tenantOwnTypes.FirstOrDefault(own => own.Id == borrower.OwnTypeId);

                EmploymentDetailWithBorrower itemToAdd = new EmploymentDetailWithBorrower()
                {
                    BorrowerId = borrower.Id,
                    BorrowerName = $"{borrower.LoanContact_LoanContactId.FirstName} {borrower.LoanContact_LoanContactId.LastName}",
                    IncomeList = new List<EmploymentDetailBaseModel>()
                };
                if (ownType != null)
                {
                    itemToAdd.OwnType = new EmploymentDetailWithBorrowerOwnType()
                    {
                        DisplayName = ownType.DisplayName,
                        Name = ownType.Name,
                        OwnTypeId = borrower.OwnTypeId
                    };
                }
                var borrowerIncomes = borrower.IncomeInfoes.OrderBy(ii => ii.StartDate).ToList();
                foreach (var income in borrowerIncomes)
                {
                    EmploymentDetailBaseModel incomeItemToAdd = new EmploymentDetailBaseModel()
                    {
                        BorrowerId = borrower.Id,
                        EmployerAddress = new EmployerAddressBaseModel()
                        {
                            LoanApplicationId = loanApplicationId,
                            BorrowerId = borrower.Id,
                            StateId = income.AddressInfo?.StateId,
                            CityId = income.AddressInfo?.CityId,
                            CityName = income.AddressInfo?.CityName,
                            CountryId = income.AddressInfo?.CountryId,
                            IncomeInfoId = income.Id,
                            StateName = income.AddressInfo?.StateName,
                            StreetAddress = income.AddressInfo?.StreetAddress,
                            UnitNo = income.AddressInfo?.UnitNo,
                            ZipCode = income.AddressInfo?.ZipCode
                        },
                        EmploymentInfo = new EmploymentInfo()
                        {
                            IncomeInfoId = income.Id,
                            BorrowerId = borrower.Id,
                            EndDate = income.EndDate,
                            StartDate = income.StartDate,
                            EmployedByFamilyOrParty = income.IsEmployedByPartyInTransaction,
                            EmployerName = income.EmployerName,
                            EmployerPhoneNumber = income.Phone,
                            HasOwnershipInterest = income.OwnershipPercentage > 0,
                            JobTitle = income.JobTitle,
                            OwnershipInterest = income.OwnershipPercentage,
                            YearsInProfession = income.YearsInProfession,
                            IsCurrentIncome = income.EndDate == null
                        },
                        EmploymentOtherIncomes = new List<EmploymentOtherAnnualIncome>(),
                        LoanApplicationId = loanApplicationId,
                        WayOfIncome = new WayOfIncome()
                        {
                            HourlyRate = income.HourlyRate,
                            HoursPerWeek = income.HoursPerWeek,
                            EmployerAnnualSalary = income.AnnualBaseIncome,
                            IsPaidByMonthlySalary = income.IsHourlyPayment == false
                        }
                    };
                    var incomeType = tenantIncomeTypes.FirstOrDefault(type => type.Id == income.IncomeTypeId);
                    if (incomeType != null)
                    {
                        var categoryToSet =
                            tenantEmploymentCategories.FirstOrDefault(cat => cat.Id == incomeType.IncomeCategoryId);
                        if (categoryToSet != null)
                        {
                            incomeItemToAdd.EmploymentInfo.EmploymentCategory = new EmploymentCategory()
                            {
                                CategoryDisplayName = categoryToSet.DisplayName,
                                CategoryId = categoryToSet.Id,
                                CategoryName = categoryToSet.Name
                            };
                        }
                    }
                    foreach (var otherIncome in income.OtherIncomeInfoes)
                    {
                        var otherIncomeToAdd = new EmploymentOtherAnnualIncome()
                        {
                            AnnualIncome = otherIncome.AnnualIncome,
                            IncomeTypeId = (EmploymentOtherIncomeType) otherIncome.OtherIncomeTypeId
                        };
                        var otherIncomeInfo =
                            tenantOtherIncomes.FirstOrDefault(oi => oi.Id == otherIncome.OtherIncomeTypeId);
                        if(otherIncomeInfo != null)
                        {
                            otherIncomeToAdd.Name = otherIncomeInfo.Name;
                            otherIncomeToAdd.DisplayName = otherIncomeInfo.DisplayName;
                        }
                        incomeItemToAdd.EmploymentOtherIncomes.Add(otherIncomeToAdd);
                    }
                    itemToAdd.IncomeList.Add(incomeItemToAdd);
                }
                modelToReturn.Add(itemToAdd);
            }

            return modelToReturn;
        }

        public async Task<EmploymentHistoryModel> GetEmploymentHistory(TenantModel tenant, int userId, int loanApplicationId)
        {
            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == loanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(x => x.Borrowers).ThenInclude(b => b.LoanContact_LoanContactId)
                //.Include(x => x.Borrowers).ThenInclude(b => b.IncomeInfoes).ThenInclude(ii => ii.IncomeType)
                .Include(x => x.Borrowers).ThenInclude(b => b.IncomeInfoes)
                .FirstOrDefaultAsync();



            //var incomeTypes = _dbFunctionService.UdfIncomeType(tenant.Id);
            //var incomeGroups = _dbFunctionService.UdfIncomeGroup(tenant.Id);
            //incomeTypes.ForEach(it => { it.IncomeGroup = incomeGroups.Single(ig => ig.Id == it.IncomeGroupId);});
            //loanApplication.Borrowers.SelectMany(b => b.IncomeInfoes).ForEach(incomeInfo=> { incomeInfo.IncomeType = incomeTypes.Single(it => it.Id == incomeInfo.IncomeTypeId); });
            

            if (loanApplication == null)
            {
                _logger.LogWarning($"Loan application not found. Loan Application ID {loanApplicationId} User ID : {userId}");
                return new EmploymentHistoryModel()
                {
                    ErrorMessage = "Loan application not found"
                };
            }

            var customIncomeTypes = _dbFunctionService.UdfIncomeType(tenant.Id).ToList();
            var customIncomeCategories = _dbFunctionService.UdfIncomeCategory(tenant.Id).ToList();

            EmploymentHistoryModel model = new EmploymentHistoryModel()
            {
                LoanApplicationId = loanApplicationId
            };

            List<BorrowerEmploymentHistory> borrowerEmploymentHistory = new List<BorrowerEmploymentHistory>();
            DateTime yearsBack = DateTime.UtcNow.AddYears(-2);
            foreach (var borrower in loanApplication.Borrowers)
            {
                if (borrower.IncomeInfoes == null || borrower.IncomeInfoes.Count == 0)
                {
                    borrowerEmploymentHistory.Add(new BorrowerEmploymentHistory()
                    {
                        BorrowerName = $"{borrower.LoanContact_LoanContactId.FirstName} {borrower.LoanContact_LoanContactId.LastName}",
                        BorrowerId = borrower.Id,
                        EmploymentHistory = new List<EmploymentHistory>()
                    });
                }
                else
                {
                    foreach (var ii in borrower.IncomeInfoes)
                    {
                        var incomeTypeToSet = customIncomeTypes.FirstOrDefault(it => it.Id == ii.IncomeTypeId);
                        var incomeCategoryToSet =
                            customIncomeCategories.FirstOrDefault(ic => ic.Id == incomeTypeToSet.IncomeCategoryId);
                        incomeTypeToSet.IncomeCategory = incomeCategoryToSet;
                        ii.IncomeType = incomeTypeToSet;
                    }
                    var historyExists = borrower.IncomeInfoes.Any(ii => ii.StartDate <= yearsBack && ii?.IncomeType?.IncomeCategoryId == (int)BorrowerIncomeCategory.Employment);
                    if (!historyExists)
                    {
                        BorrowerEmploymentHistory borrowerToAdd = new BorrowerEmploymentHistory()
                        {
                            BorrowerId = borrower.Id,
                            BorrowerName = $"{borrower.LoanContact_LoanContactId.FirstName} {borrower.LoanContact_LoanContactId.LastName}",
                            EmploymentHistory = borrower.IncomeInfoes.Select(x => new EmploymentHistory()
                            {
                                EmployerName = x.EmployerName,
                                EndDate = x.EndDate,
                                StartDate = x.StartDate,
                                IsCurrentEmployment = !x.EndDate.HasValue(),
                                IncomeInfoId = x.Id
                            }).ToList()
                        };
                        borrowerEmploymentHistory.Add(borrowerToAdd);
                    }
                }
            }

            model.BorrowerEmploymentHistory = borrowerEmploymentHistory;
            model.RequiresHistory = borrowerEmploymentHistory.Count > 0;
            return model;
        }

        public async Task<GetBorrowerWithIncomesForReviewModel> GetBorrowerIncomesForReview(TenantModel tenant, int userId, int loanApplicationId)
        {
            

            var loanApplication = await Uow.Repository<LoanApplicationDb.Entity.Models.LoanApplication>()
                .Query(x => x.Id == loanApplicationId && x.UserId == userId && x.TenantId == tenant.Id)
                .Include(la => la.Borrowers).ThenInclude(b => b.LoanContact_LoanContactId)
                .Include(la => la.Borrowers).ThenInclude(b => b.IncomeInfoes).ThenInclude(income => income.AddressInfo)
                .FirstOrDefaultAsync();

            if(loanApplication == null)
            {
                return new GetBorrowerWithIncomesForReviewModel()
                {
                    ErrorMessage = ErrorMessages[LoanApplicationNotFound]
                };
            }

            List<int> borrowerIncomeTypes = new List<int>();
            var tenantIncomeTypes = _dbFunctionService.UdfIncomeType(tenant.Id);
            var tenantIncomeCategories = _dbFunctionService.UdfIncomeCategory(tenant.Id);
            var tenantOwnTypes = _dbFunctionService.UdfOwnType(tenant.Id);

            foreach (var borrower in loanApplication.Borrowers)
            {
                borrower.OwnType = tenantOwnTypes.FirstOrDefault(ownType => ownType.Id == borrower.OwnTypeId);
                foreach (var incomeInfo in borrower.IncomeInfoes)
                {
                    incomeInfo.IncomeType =
                        tenantIncomeTypes.FirstOrDefault(type => type.Id == incomeInfo.IncomeTypeId);
                    if (incomeInfo.IncomeType != null && (!borrowerIncomeTypes.Contains(incomeInfo.IncomeType.Id)))
                    {
                        borrowerIncomeTypes.Add(incomeInfo.IncomeType.Id);
                    }

                }
            }

            GetBorrowerWithIncomesForReviewModel modelToReturn = new GetBorrowerWithIncomesForReviewModel()
            {
                Borrowers = new List<IncomeReviewBorrowerModel>()
            };
            foreach(var borrower in loanApplication.Borrowers)
            {
                IncomeReviewBorrowerModel borrowerToAdd = new IncomeReviewBorrowerModel()
                {
                    BorrowerId = borrower.Id,
                    BorrowerName = $"{borrower.LoanContact_LoanContactId.FirstName} {borrower.LoanContact_LoanContactId.LastName}",
                    OwnType = new IncomeReviewOwnType()
                    {
                        DisplayName = borrower.OwnType.DisplayName,
                        Id = borrower.OwnType.Id,
                        Name = borrower.OwnType.DisplayName
                    }
                };
                modelToReturn.Borrowers.Add(borrowerToAdd);
                foreach(var incomeTypeId in borrowerIncomeTypes)
                {
                    var borrowerIncomes = borrower.IncomeInfoes.Where(income => income.IncomeTypeId == incomeTypeId)
                        .ToList(); 
                    if(borrowerIncomes.Count > 0)
                    {
                        var incomeTypeInfo = tenantIncomeTypes.FirstOrDefault(type => type.Id == incomeTypeId); 
                        var incomeCategory =
                            tenantIncomeCategories.FirstOrDefault(cat => cat.Id == incomeTypeInfo.IncomeCategoryId);
                        IncomeReviewIncomeType typeToAdd = new IncomeReviewIncomeType()
                        {
                            DisplayName = incomeTypeInfo.DisplayName,
                            Id = incomeTypeInfo.Id,
                            Name = incomeTypeInfo.Name,
                            IncomeCategory = new IncomeReviewIncomeCategory()
                            {
                                DisplayName = incomeCategory.DisplayName,
                                Id = incomeCategory.Id,
                                Name = incomeCategory.Name
                            },
                            IncomeList = new List<IncomeReviewList>()
                        };
                        if(borrowerToAdd.IncomeTypes == null)
                        {
                            borrowerToAdd.IncomeTypes = new List<IncomeReviewIncomeType>();
                        }
                        borrowerToAdd.IncomeTypes.Add(typeToAdd);

                        foreach (var incomeInfoWithType in borrowerIncomes)
                        {
                            var incomeToAdd = new IncomeReviewList();

                            incomeToAdd.IncomeInfo = new IncomeInfoForReview()
                            {
                                EmployerName = incomeInfoWithType.EmployerName,
                                JobTitle = incomeInfoWithType.JobTitle,
                                StartDate = incomeInfoWithType.StartDate,
                                EndDate = incomeInfoWithType.EndDate,
                                IncomeInfoId = incomeInfoWithType.Id,
                                IsCurrentIncome = incomeInfoWithType.EndDate == null,
                                EmployedByFamilyOrParty = incomeInfoWithType.IsEmployedByPartyInTransaction,
                                EmployerPhoneNumber = incomeInfoWithType.Phone,
                                HasOwnershipInterest = incomeInfoWithType.OwnershipPercentage > 0,
                                OwnershipInterest = incomeInfoWithType.OwnershipPercentage,
                                YearsInProfession = incomeInfoWithType.YearsInProfession
                            };

                            if(incomeInfoWithType.AddressInfo != null)
                            {
                                incomeToAdd.IncomeInfo.IncomeAddress = new EmployerAddressForIncomeReview()
                                {
                                    CityId = incomeInfoWithType.AddressInfo.CityId,
                                    CityName = incomeInfoWithType.AddressInfo.CityName,
                                    CountryId = incomeInfoWithType.AddressInfo.CountryId,
                                    StateId = incomeInfoWithType.AddressInfo.StateId,
                                    StateName = incomeInfoWithType.AddressInfo.StateName,
                                    StreetAddress = incomeInfoWithType.AddressInfo.StreetAddress,
                                    UnitNo = incomeInfoWithType.AddressInfo.UnitNo,
                                    ZipCode = incomeInfoWithType.AddressInfo.ZipCode
                                };
                            }

                            if(incomeInfoWithType.IsHourlyPayment == true)
                            {
                                incomeToAdd.IncomeInfo.WayOfIncome = new WayOfIncomeForReview()
                                {
                                    AnnualSalary = null,
                                    MonthlySalary = null,
                                    HourlyRate = incomeInfoWithType.HourlyRate,
                                    HoursPerWeek = incomeInfoWithType.HoursPerWeek,
                                    IsPaidByMonthlySalary = false
                                };
                            }
                            else
                            {
                                incomeToAdd.IncomeInfo.WayOfIncome = new WayOfIncomeForReview()
                                {
                                    MonthlySalary = incomeInfoWithType.MonthlyBaseIncome,
                                    HourlyRate = null,
                                    HoursPerWeek = null,
                                    AnnualSalary = incomeInfoWithType.AnnualBaseIncome,
                                    IsPaidByMonthlySalary = true
                                };
                            }
                            typeToAdd.IncomeList.Add(incomeToAdd);
                        }
                    }
                }
            }

            return modelToReturn;
        }

        private IncomeInfo SetEmploymentInfo(IncomeInfo incomeInfo, EmploymentInfo employmentInfo, bool isCurrentEmployer)
        {
            incomeInfo.EmployerName = employmentInfo.EmployerName;
            incomeInfo.JobTitle = employmentInfo.JobTitle;
            incomeInfo.StartDate = employmentInfo.StartDate;
            incomeInfo.EndDate = employmentInfo.EndDate;
            incomeInfo.YearsInProfession = employmentInfo.YearsInProfession;
            incomeInfo.Phone = employmentInfo.EmployerPhoneNumber;
            incomeInfo.IsEmployedByPartyInTransaction = null;
            incomeInfo.OwnershipPercentage = employmentInfo.OwnershipInterest;
            if (isCurrentEmployer)
            {
                incomeInfo.EndDate = null;
                incomeInfo.IsEmployedByPartyInTransaction = employmentInfo.EmployedByFamilyOrParty;
            }

            return incomeInfo;
        }


        private AddressInfo SetEmployerAddressInfo(AddressInfo employerAddressInfo,
                                                   EmployerAddressBaseModel model)
        {
            if (employerAddressInfo != null)
            {
                employerAddressInfo.StreetAddress = model.StreetAddress;
                employerAddressInfo.UnitNo = model.UnitNo;
                employerAddressInfo.CityId = model.CityId;
                employerAddressInfo.CityName = model.CityName;
                employerAddressInfo.StateId = model.StateId;
                employerAddressInfo.CountryId = model.CountryId;
                employerAddressInfo.StateName = model.StateName;
                employerAddressInfo.ZipCode = model.ZipCode;
                return employerAddressInfo;
            }

            return null;
        }


        private IncomeInfo SetWayOfIncomeInfo(IncomeInfo incomeInfo,
                                              WayOfIncome wayOfIncome)
        {
            switch (wayOfIncome.IsPaidByMonthlySalary)
            {
                case true:
                    incomeInfo.AnnualBaseIncome = wayOfIncome.EmployerAnnualSalary;
                    incomeInfo.MonthlyBaseIncome = wayOfIncome.EmployerAnnualSalary / 12;
                    incomeInfo.IsHourlyPayment = false;
                    incomeInfo.HourlyRate = null;
                    incomeInfo.HoursPerWeek = null;
                    break;

                case false:
                    incomeInfo.AnnualBaseIncome = 0; //todo dani calculate value based on hourly rate and no of hours perweek
                    incomeInfo.MonthlyBaseIncome = 0; //todo dani calculate value based on hourly rate and no of hours perweek
                    incomeInfo.IsHourlyPayment = true;
                    incomeInfo.HourlyRate = wayOfIncome.HourlyRate;
                    incomeInfo.HoursPerWeek = wayOfIncome.HoursPerWeek;
                    break;
            }

            return incomeInfo;
        }


        private IncomeInfo SetEmploymentInfoData(IncomeInfo incomeInfo,
                                                 EmploymentInfoBaseModel model)
        {
            incomeInfo.EmployerName = model.EmployerName;
            incomeInfo.JobTitle = model.JobTitle;
            incomeInfo.StartDate = model.StartDate;
            incomeInfo.YearsInProfession = model.YearsInProfession;
            incomeInfo.Phone = model.EmployerPhoneNumber;
            incomeInfo.IsEmployedByPartyInTransaction = model.EmployedByFamilyOrParty;
            incomeInfo.OwnershipPercentage = model.OwnershipInterest;
            return incomeInfo;
        }


        private OtherIncomeInfo SetEmploymentOtherIncome(OtherIncomeInfo otherIncomeInfo,
                                                         EmploymentOtherAnnualIncome employmentOtherAnnualIncome,
                                                         int tenantId)
        {
            otherIncomeInfo.OtherIncomeTypeId = (int) employmentOtherAnnualIncome.IncomeTypeId;
            otherIncomeInfo.MonthlyIncome = employmentOtherAnnualIncome.AnnualIncome / 12;
            otherIncomeInfo.TenantId = tenantId;
            otherIncomeInfo.AnnualIncome = employmentOtherAnnualIncome.AnnualIncome;
            return otherIncomeInfo;
        }
    }
}