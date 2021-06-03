using LoanApplication.Model;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Extensions.ExtensionClasses;
using TenantConfig.Common;
using TenantConfig.Common.DistributedCache;
using TenantConfig.Data;
using TenantConfig.Entity.Models;
using URF.Core.Abstractions;

namespace LoanApplication.Service
{
    public class InfoService : ServiceBase<TenantConfigContext, Branch>, IInfoService
    {
        private readonly IUnitOfWork<LoanApplicationContext> _loanUow;
        private readonly IDbFunctionService _dbFunctionService;
        public InfoService(IUnitOfWork<TenantConfigContext> previousUow, IUnitOfWork<LoanApplicationContext> loanUow, IServiceProvider services, IDbFunctionService dbFunctionService) : base(previousUow, services)
        {
            _loanUow = loanUow;
            _dbFunctionService = dbFunctionService;
        }

        public List<PropertyUsageModel> GetAllPropertyUsages(TenantModel tenant, int? sectionId)
        {
            //return await _loanUow.DataContext.UdfPropertyUsage(tenant.Id).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder).Select(x => new PropertyUsageModel { Id = x.Id, Name = x.Name, Image = CommonHelper.GenerateGlobalCdnUrl(tenant, x.Image) }).ToListAsync();
            return _dbFunctionService.UdfPropertyUsage(tenant.Id,sectionId).Select(x => new PropertyUsageModel { Id = x.Id, Name = x.Name, Image = CommonHelper.GenerateGlobalCdnUrl(tenant, x.Image) }).ToList();
        }
        public List<PropertyTypeModel> GetAllPropertyTypes(TenantModel tenant)
        {
            //return await _loanUow.DataContext.UdfPropertyType(tenant.Id).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder).Select(x => new PropertyTypeModel { Id = x.Id, Name = x.Name, Image = CommonHelper.GenerateGlobalCdnUrl(tenant, x.Image) }).ToListAsync();
            return _dbFunctionService.UdfPropertyType(tenant.Id).Select(x => new PropertyTypeModel { Id = x.Id, Name = x.Name, Image = CommonHelper.GenerateGlobalCdnUrl(tenant, x.Image) }).ToList();
        }
        public List<PropertyTypeModel> GetAllPropertyTypes(TenantModel tenant, int? sectionId)
        {
            //return await _loanUow.DataContext.UdfPropertyType(tenant.Id).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder).Select(x => new PropertyTypeModel { Id = x.Id, Name = x.Name, Image = CommonHelper.GenerateGlobalCdnUrl(tenant, x.Image) }).ToListAsync();
            return _dbFunctionService.UdfPropertyType(tenant.Id, sectionId).Select(x => new PropertyTypeModel { Id = x.Id, Name = x.Name, Image = CommonHelper.GenerateGlobalCdnUrl(tenant, x.Image) }).ToList();
        }
        public LoanApplicationDb.Data.LocationSearch GetLocationSearchByZipCodeCityState(string cityName, string stateName, string countyName, string zipCode)
        {
            //return (await _loanUow.DataContext.LocationSearchByCityCountyStateZipCode(cityName,stateName,countyName,zipCode).ToListAsync()).FirstOrDefault();
            return (_dbFunctionService.LocationSearchByCityCountyStateZipCode(cityName, stateName, countyName, zipCode).ToList()).FirstOrDefault();
        }
        public async Task<ZipcodeModel> GetZipCodesByStateCityAndCounty(int stateId, int cityId, int countyId)
        {
            if (countyId != 0 && cityId != 0)
                return await _loanUow.Repository<ZipCode>()
                    .Query(x => x.StateId == stateId && x.CountyId == countyId && x.CityId == cityId && x.IsActive != false)
                    .Select(x => new ZipcodeModel { ZipPostalCode = x.ZipPostalCode }).FirstOrDefaultAsync();
            else
                return await _loanUow.Repository<ZipCode>()
                .Query(x => x.StateId == stateId && x.IsActive != false)
                .Select(x => new ZipcodeModel { ZipPostalCode = x.ZipPostalCode }).FirstOrDefaultAsync();
        }
        public List<LoanApplicationDb.Data.CustomerSearch> GetSearchByString(string searchKey)
        {
            //return await _loanUow.DataContext.CustomerSearchByString(searchKey).ToListAsync();
            return _dbFunctionService.CustomerSearchByString(searchKey).ToList();
        }
        public List<LoanApplicationDb.Data.CustomerSearch> GetSearchByZipcode(int zipCode)
        {
            //return await _loanUow.DataContext.CustomerSearchByZipcode(zipCode).ToListAsync();
            return _dbFunctionService.CustomerSearchByZipcode(zipCode).ToList();
        }
        public List<LoanGoalModel> GetAllLoanGoal(TenantModel tenant, int purposeId)
        {
            //return await _loanUow.DataContext.UdfLoanGoal(tenant.Id).Where(x => x.LoanPurposeId == purposeId && x.IsActive).OrderBy(x => x.DisplayOrder).Select(x => new LoanGoalModel { Id = x.Id, Name = x.Name, Image = CommonHelper.GenerateGlobalCdnUrl(tenant, x.Image) }).ToListAsync();
            return _dbFunctionService.UdfLoanGoal(tenant.Id).Where(x => x.LoanPurposeId == purposeId).Select(x => new LoanGoalModel { Id = x.Id, Name = x.Name, Image = CommonHelper.GenerateGlobalCdnUrl(tenant, x.Image) }).ToList();
        }
        public async Task<LoInfo> GetLoInfo(int loId, TenantModel tenant)
        {
            return await Uow.Repository<Employee>().Query(x => x.Id == loId)
                .Include(x => x.EmployeeEmailBinders).ThenInclude(x => x.EmailAccount)
                .Include(x => x.EmployeePhoneBinders).ThenInclude(x => x.CompanyPhoneInfo)
                .Include(x => x.Contact)
                .Select(x => new LoInfo
                {
                    IsLoanOfficer = true,
                    Name = x.Contact.NickName,
                    Email = x.EmployeeEmailBinders.First(x => x.TypeId == (int)EmailType.Primary).EmailAccount.Email,
                    Phone = PhoneHelper.Mask(x.EmployeePhoneBinders.First(x => x.TypeId == (int)PhoneType.Work).CompanyPhoneInfo.Phone),
                    Image = CommonHelper.GenerateCdnUrl(tenant, x.Photo),
                    Url = x.Url,
                    NmlsNo = x.NmlsNo
                }).SingleAsync();
        }
        public async Task<LoInfo> GetBranchInfo(int branchId, TenantModel tenant)
        {
            return await Repository.Query(x => x.Id == branchId)
                .Include(x => x.BranchEmailBinders).ThenInclude(x => x.EmailAccount)
                .Include(x => x.BranchPhoneBinders).ThenInclude(x => x.CompanyPhoneInfo)
                .Select(x => new LoInfo
                {
                    IsLoanOfficer = false,
                    Name = x.Name,
                    Email = x.BranchEmailBinders.First(x => x.TypeId == (int)EmailType.Primary).EmailAccount.Email,
                    Phone = PhoneHelper.Mask(x.BranchPhoneBinders.First(x => x.TypeId == (int)PhoneType.Work).CompanyPhoneInfo.Phone),
                    Image = CommonHelper.GenerateCdnUrl(tenant, x.Image),
                    Url = x.Url,
                    NmlsNo = x.NmlsNo
                }).SingleAsync();
        }

        public List<LoanPurposeModel> GetAllLoanPurpose(TenantModel tenant)
        {
            //return await _loanUow.DataContext.UdfLoanPurpose(tenant.Id).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder).Select(x => new LoanPurposeModel { Id = x.Id, Name = x.Name, Image = CommonHelper.GenerateGlobalCdnUrl(tenant, x.Image) }).ToListAsync();
            return _dbFunctionService.UdfLoanPurpose(tenantId: tenant.Id).Select(x => new LoanPurposeModel { Id = x.Id, Name = x.Name, Image = CommonHelper.GenerateGlobalCdnUrl(tenant, x.Image) }).ToList();
        }
        public async Task<List<CountryModel>> GetAllCountry(TenantModel tenant)
        {
            return await _loanUow.Repository<LoanApplicationDb.Entity.Models.Country>().Query(x => x.IsActive).OrderBy(x => x.DisplayOrder).Select(x => new CountryModel { Id = x.Id, Name = x.Name, ShortCode = x.TwoLetterIsoCode }).ToListAsync();
        }
        public List<StateModel> GetTenantState(TenantModel tenant)
        {
            //return await _loanUow.DataContext.UdfState(tenant.Id).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder).Select(x => new StateModel { Id = x.Id, Name = x.Name, ShortCode = x.Abbreviation, CountryId = x.CountryId }).ToListAsync();
            return _dbFunctionService.UdfState(tenant.Id).Select(x => new StateModel { Id = x.Id, Name = x.Name, ShortCode = x.Abbreviation, CountryId = x.CountryId }).ToList();
        }
        public async Task<List<StateModel>> GetAllState(TenantModel tenant, int? countryId)
        {
            if (countryId.HasValue)
                return await _loanUow.Repository<LoanApplicationDb.Entity.Models.State>().Query(x => x.IsActive).Where(x => x.CountryId == countryId.Value).OrderBy(x => x.DisplayOrder).Select(x => new StateModel { Id = x.Id, Name = x.Name, ShortCode = x.Abbreviation, CountryId = x.CountryId }).ToListAsync();
            else
                return await _loanUow.Repository<LoanApplicationDb.Entity.Models.State>().Query(x => x.IsActive).OrderBy(x => x.DisplayOrder).Select(x => new StateModel { Id = x.Id, Name = x.Name, ShortCode = x.Abbreviation, CountryId = x.CountryId }).ToListAsync();
        }
        public List<OwnershipTypeModel> GetAllOwnershipType(TenantModel tenant)
        {
            //return await _loanUow.DataContext.UdfOwnershipType(tenant.Id).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder).Select(x => new OwnershipTypeModel { Id = x.Id, Name = x.Name }).ToListAsync();
            return _dbFunctionService.UdfOwnershipType(tenant.Id).Select(x => new OwnershipTypeModel { Id = x.Id, Name = x.Name }).ToList();
        }

        public List<MilitaryAffiliationModel> GetAllMilitaryAffiliation(TenantModel tenant)
        {
            //return await _loanUow.DataContext.UdfMilitaryAffiliation(tenant.Id).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder).Select(militaryAffiliation => new MilitaryAffiliationModel { Id = militaryAffiliation.Id, Name = militaryAffiliation.Name }).ToListAsync();
            return _dbFunctionService.UdfMilitaryAffiliation(tenant.Id).Select(militaryAffiliation => new MilitaryAffiliationModel { Id = militaryAffiliation.Id, Name = militaryAffiliation.Name }).ToList();
        }

        public List<MaritalStatusModel> GetAllMaritalStatus(TenantModel tenant)
        {
            //return await _loanUow.DataContext.UdfMaritalStatusList(tenant.Id).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder).Select(statusList => new MaritalStatusModel() { Id = statusList.Id, Name = statusList.Name }).ToListAsync();
            return _dbFunctionService.UdfMaritalStatusList(tenant.Id).Select(statusList => new MaritalStatusModel() { Id = statusList.Id, Name = statusList.Name }).ToList();
        }
        public async Task<List<BusinessTypeModel>> GetAllBusinessTypes(TenantModel tenant)
        {
            return await _dbFunctionService.UdfIncomeType(tenant.Id).Where(x => x.IncomeCategoryId == (int)Model.IncomeCategory.Business).Select(x => new BusinessTypeModel { Id = x.Id, Name = x.Name }).ToListAsync();
        }

        public async Task<List<IncomeTypeModel>> GetIncomeTypes(TenantModel tenant,Model.IncomeCategory incomeCategory)
        {
            return await _dbFunctionService.UdfIncomeType(tenant.Id).Where(x => x.IncomeCategoryId == incomeCategory.ToInt()).Select(x => new IncomeTypeModel { Id = x.Id, Name = x.Name , FieldsInfo = x.FieldsInfo}).ToListAsync();
        }

        public async Task<List<RetirementIncomeTypeModel>> GetRetirementIncomeTypes(TenantModel tenant)
        {
            return await _dbFunctionService.UdfIncomeType(tenant.Id).Where(x => x.IncomeCategoryId == (int)Model.IncomeCategory.Retirement).Select(x => new RetirementIncomeTypeModel { Id = x.Id, Name = x.Name }).ToListAsync();
        }


    }
}
