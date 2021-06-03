using LoanApplication.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Entity.Models;

namespace LoanApplication.Service
{
    public interface IInfoService : IServiceBase<Branch>
    {
        Task<LoInfo> GetLoInfo(int loId, TenantModel tenant);
        Task<LoInfo> GetBranchInfo(int branchId, TenantModel tenant);
        List<LoanGoalModel> GetAllLoanGoal(TenantModel tenant, int purposeId);
        List<LoanPurposeModel> GetAllLoanPurpose(TenantModel tenant);
        Task<List<CountryModel>> GetAllCountry(TenantModel tenant);
        Task<List<StateModel>> GetAllState(TenantModel tenant, int? countryId);
        List<OwnershipTypeModel> GetAllOwnershipType(TenantModel tenant);
        List<MilitaryAffiliationModel> GetAllMilitaryAffiliation(TenantModel tenant);
        Task<ZipcodeModel> GetZipCodesByStateCityAndCounty(int stateId, int cityId, int countyId);
        List<PropertyTypeModel> GetAllPropertyTypes(TenantModel tenant, int? sectionId);
        List<LoanApplicationDb.Data.CustomerSearch> GetSearchByString(string searchKey);
        Task<List<BusinessTypeModel>> GetAllBusinessTypes(TenantModel tenant);
        List<MaritalStatusModel> GetAllMaritalStatus(TenantModel tenant);
        LoanApplicationDb.Data.LocationSearch GetLocationSearchByZipCodeCityState(string cityName, string stateName, string countyName, string zipCode);
        List<LoanApplicationDb.Data.CustomerSearch> GetSearchByZipcode(int zipCode);
        List<PropertyUsageModel> GetAllPropertyUsages(TenantModel tenant, int? sectionId);
        List<StateModel> GetTenantState(TenantModel tenant);
        Task<List<RetirementIncomeTypeModel>> GetRetirementIncomeTypes(TenantModel tenant);

        
        Task<List<IncomeTypeModel>> GetIncomeTypes(TenantModel tenant,Model.IncomeCategory incomeCategory);
    }
}