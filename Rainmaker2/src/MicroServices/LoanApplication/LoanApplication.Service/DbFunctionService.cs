using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoanApplicationDb.Data;
using LoanApplicationDb.Entity.Models;
using URF.Core.Abstractions;

namespace LoanApplication.Service
{
    public interface IDbFunctionService : IServiceBase<LoanApplicationDb.Entity.Models.LoanApplication>
    {
       

        IQueryable<AssetCategory> UdfAssetCategory(int tenantId, int? sectionId = null);
        IQueryable<AssetType> UdfAssetType(int tenantId, int? sectionId = null);
        IQueryable<AssetTypeGiftSourceBinder> UdfAssetTypeGiftSourceBinder(int tenantId, int? sectionId = null);
        IQueryable<BankRuptcy> UdfBankRuptcy(int tenantId, int? sectionId = null);
        IQueryable<City> UdfCity(int tenantId, int? sectionId = null);
        IQueryable<CollateralAssetType> UdfCollateralAssetType(int tenantId, int? sectionId = null);
        IQueryable<ConsentType> UdfConsentType(int tenantId, int? sectionId = null);
        IQueryable<Country> UdfCountry(int tenantId, int? sectionId = null);
        IQueryable<County> UdfCounty(int tenantId, int? sectionId = null);
        IQueryable<CountyType> UdfCountyType(int tenantId, int? sectionId = null);
        IQueryable<EscrowEntityType> UdfEscrowEntityType(int tenantId, int? sectionId = null);
        IQueryable<Ethnicity> UdfEthnicity(int tenantId, int? sectionId = null);
        IQueryable<EthnicityDetail> UdfEthnicityDetail(int tenantId, int? sectionId = null);
        IQueryable<FamilyRelationType> UdfFamilyRelationType(int tenantId, int? sectionId = null);
        IQueryable<Gender> UdfGender(int tenantId, int? sectionId = null);
        IQueryable<GiftSource> UdfGiftSource(int tenantId, int? sectionId = null);
        IQueryable<IncomeCategory> UdfIncomeCategory(int tenantId, int? sectionId = null);
        IQueryable<IncomeGroup> UdfIncomeGroup(int tenantId, int? sectionId = null);
        IQueryable<IncomeType> UdfIncomeType(int tenantId, int? sectionId = null);
        IQueryable<JobType> UdfJobType(int tenantId, int? sectionId = null);
        IQueryable<LiabilityType> UdfLiabilityType(int tenantId, int? sectionId = null);
        IQueryable<LoanGoal> UdfLoanGoal(int tenantId, int? sectionId = null);
        IQueryable<LoanPurpose> UdfLoanPurpose(int tenantId, int? sectionId = null);
        IQueryable<LoanPurposeProgram> UdfLoanPurposeProgram(int tenantId, int? sectionId = null);
        IQueryable<LoanType> UdfLoanType(int tenantId, int? sectionId = null);
        IQueryable<MaritalStatusList> UdfMaritalStatusList(int tenantId, int? sectionId = null);
        IQueryable<MaritalStatusType> UdfMaritalStatusType(int tenantId, int? sectionId = null);
        IQueryable<MilitaryAffiliation> UdfMilitaryAffiliation(int tenantId, int? sectionId = null);
        IQueryable<MilitaryBranch> UdfMilitaryBranch(int tenantId, int? sectionId = null);
        IQueryable<MilitaryStatusList> UdfMilitaryStatusList(int tenantId, int? sectionId = null);
        IQueryable<OtherIncomeType> UdfOtherIncomeType(int tenantId, int? sectionId = null);
        IQueryable<OwnershipType> UdfOwnershipType(int tenantId, int? sectionId = null);
        IQueryable<OwnType> UdfOwnType(int tenantId, int? sectionId = null);
        IQueryable<PaidBy> UdfPaidBy(int tenantId, int? sectionId = null);
        IQueryable<ProductAmortizationType> UdfProductAmortizationType(int tenantId, int? sectionId = null);
        IQueryable<ProductFamily> UdfProductFamily(int tenantId, int? sectionId = null);
        IQueryable<ProjectType> UdfProjectType(int tenantId, int? sectionId = null);
        IQueryable<PropertyType> UdfPropertyType(int tenantId, int? sectionId = null);
        IQueryable<PropertyUsage> UdfPropertyUsage(int tenantId, int? sectionId = null);
        IQueryable<Question> UdfQuestion(int tenantId, int? sectionId = null);
        IQueryable<QuestionSection> UdfQuestionSection(int tenantId, int? sectionId = null);
        IQueryable<QuestionType> UdfQuestionType(int tenantId, int? sectionId = null);
        IQueryable<Race> UdfRace(int tenantId, int? sectionId = null);
        IQueryable<RaceDetail> UdfRaceDetail(int tenantId, int? sectionId = null);
        IQueryable<ResidencyState> UdfResidencyState(int tenantId, int? sectionId = null);
        IQueryable<ResidencyType> UdfResidencyType(int tenantId, int? sectionId = null);
        IQueryable<MortgageType> UdfMortgageType(int tenantId, int? sectionId = null);
        IQueryable<State> UdfState(int tenantId, int? sectionId = null);
        IQueryable<TitleEstate> UdfTitleEstate(int tenantId, int? sectionId = null);
        IQueryable<TitleHeldWith> UdfTitleHeldWith(int tenantId, int? sectionId = null);
        IQueryable<TitleLandTenure> UdfTitleLandTenure(int tenantId, int? sectionId = null);
        IQueryable<TitleManner> UdfTitleManner(int tenantId, int? sectionId = null);
        IQueryable<TitleTrustInfo> UdfTitleTrustInfo(int tenantId, int? sectionId = null);

        #region SP Wrappers

        IQueryable<CustomerSearch> CustomerSearchByZipcode(int zipCode);
        IQueryable<CustomerSearch> CustomerSearchByString(string searchKey);
        IQueryable<LocationSearch> LocationSearchByCityCountyStateZipCode(string cityName, string stateName, string countyName, string zipCode);

        #endregion

    }

    [ExcludeFromCodeCoverage]
    public class DbFunctionService : ServiceBase<LoanApplicationContext, LoanApplicationDb.Entity.Models.LoanApplication>, IDbFunctionService
    {
        public DbFunctionService(IUnitOfWork<LoanApplicationContext> loanUow, IServiceProvider services) : base(loanUow, services)
        {

        }



        public IQueryable<AssetCategory> UdfAssetCategory(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfAssetCategory(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<AssetType> UdfAssetType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfAssetType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<AssetTypeGiftSourceBinder> UdfAssetTypeGiftSourceBinder(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfAssetTypeGiftSourceBinder(tenantId, sectionId); }
        public IQueryable<BankRuptcy> UdfBankRuptcy(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfBankRuptcy(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<City> UdfCity(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfCity(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<CollateralAssetType> UdfCollateralAssetType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfCollateralAssetType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<Config> UdfConfig(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfConfig(tenantId, sectionId); }
        public IQueryable<ConsentType> UdfConsentType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfConsentType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<Country> UdfCountry(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfCountry(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<County> UdfCounty(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfCounty(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<CountyType> UdfCountyType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfCountyType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        //public IQueryable<Entity> UdfEntity(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfEntity(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        //public IQueryable<EntityType> UdfEntityType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfEntityType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<EscrowEntityType> UdfEscrowEntityType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfEscrowEntityType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<Ethnicity> UdfEthnicity(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfEthnicity(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<EthnicityDetail> UdfEthnicityDetail(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfEthnicityDetail(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<FamilyRelationType> UdfFamilyRelationType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfFamilyRelationType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<Gender> UdfGender(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfGender(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<GiftSource> UdfGiftSource(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfGiftSource(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<IncomeCategory> UdfIncomeCategory(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfIncomeCategory(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<IncomeGroup> UdfIncomeGroup(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfIncomeGroup(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<IncomeType> UdfIncomeType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfIncomeType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<JobType> UdfJobType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfJobType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<LiabilityType> UdfLiabilityType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfLiabilityType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<LoanGoal> UdfLoanGoal(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfLoanGoal(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<LoanPurpose> UdfLoanPurpose(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfLoanPurpose(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<LoanPurposeProgram> UdfLoanPurposeProgram(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfLoanPurposeProgram(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<LoanType> UdfLoanType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfLoanType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<MaritalStatusList> UdfMaritalStatusList(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfMaritalStatusList(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<MaritalStatusType> UdfMaritalStatusType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfMaritalStatusType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<MilitaryAffiliation> UdfMilitaryAffiliation(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfMilitaryAffiliation(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<MilitaryBranch> UdfMilitaryBranch(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfMilitaryBranch(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<MilitaryStatusList> UdfMilitaryStatusList(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfMilitaryStatusList(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<OtherIncomeType> UdfOtherIncomeType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfOtherIncomeType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<OwnershipType> UdfOwnershipType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfOwnershipType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<OwnType> UdfOwnType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfOwnType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<PaidBy> UdfPaidBy(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfPaidBy(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<ProductAmortizationType> UdfProductAmortizationType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfProductAmortizationType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<ProductFamily> UdfProductFamily(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfProductFamily(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<ProjectType> UdfProjectType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfProjectType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<PropertyType> UdfPropertyType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfPropertyType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<PropertyUsage> UdfPropertyUsage(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfPropertyUsage(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<Question> UdfQuestion(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfQuestion(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<QuestionSection> UdfQuestionSection(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfQuestionSection(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<QuestionType> UdfQuestionType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfQuestionType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<Race> UdfRace(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfRace(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<RaceDetail> UdfRaceDetail(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfRaceDetail(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<ResidencyState> UdfResidencyState(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfResidencyState(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<ResidencyType> UdfResidencyType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfResidencyType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<MortgageType> UdfMortgageType(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfMortgageType(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<State> UdfState(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfState(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<TitleEstate> UdfTitleEstate(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfTitleEstate(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<TitleHeldWith> UdfTitleHeldWith(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfTitleHeldWith(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<TitleLandTenure> UdfTitleLandTenure(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfTitleLandTenure(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<TitleManner> UdfTitleManner(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfTitleManner(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }
        public IQueryable<TitleTrustInfo> UdfTitleTrustInfo(int tenantId, int? sectionId = null) { return Uow.DataContext.UdfTitleTrustInfo(tenantId, sectionId).Where(x => x.IsActive).OrderBy(x => x.DisplayOrder); }



        #region SP Wrappers

        public IQueryable<CustomerSearch> CustomerSearchByZipcode(int zipCode)
        {
            return Uow.DataContext.CustomerSearchByZipcode(zipCode);
        }
        public IQueryable<CustomerSearch> CustomerSearchByString(string searchKey)
        {
            return Uow.DataContext.CustomerSearchByString(searchKey);
        }
        public IQueryable<LocationSearch> LocationSearchByCityCountyStateZipCode(string cityName, string stateName, string countyName, string zipCode)
        {
            return Uow.DataContext.LocationSearchByCityCountyStateZipCode(cityName, stateName, countyName, zipCode);
        }

        #endregion

    }
}
