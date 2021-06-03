using LoanApplicationDb.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoanApplicationDb.Data
{
     
    public partial class LoanApplicationContext : DbContext
    {
        public IQueryable<AssetCategory> UdfAssetCategory(int tenantId, int? sectionId = null) => Set<AssetCategory>().FromSqlInterpolated($"select * from udfAssetCategory ({tenantId},{sectionId})");
        public IQueryable<AssetType> UdfAssetType(int tenantId, int? sectionId = null) => Set<AssetType>().FromSqlInterpolated($"select * from udfAssetType ({tenantId},{sectionId})");
        public IQueryable<AssetTypeGiftSourceBinder> UdfAssetTypeGiftSourceBinder(int tenantId, int? sectionId = null) => Set<AssetTypeGiftSourceBinder>().FromSqlInterpolated($"select * from udfAssetTypeGiftSourceBinder ({tenantId},{sectionId})");
        public IQueryable<BankRuptcy> UdfBankRuptcy(int tenantId, int? sectionId = null) => Set<BankRuptcy>().FromSqlInterpolated($"select * from udfBankRuptcy ({tenantId},{sectionId})");
        public IQueryable<City> UdfCity(int tenantId, int? sectionId = null) => Set<City>().FromSqlInterpolated($"select * from udfCity ({tenantId},{sectionId})");
        public IQueryable<CollateralAssetType> UdfCollateralAssetType(int tenantId, int? sectionId = null) => Set<CollateralAssetType>().FromSqlInterpolated($"select * from udfCollateralAssetType ({tenantId},{sectionId})");
        public IQueryable<Config> UdfConfig(int tenantId, int? sectionId = null) => Set<Config>().FromSqlInterpolated($"select * from udfConfig ({tenantId},{sectionId})");
        public IQueryable<ConfigSelectionItem> UdfConfigSelectionItem(int tenantId, int? sectionId = null) => Set<ConfigSelectionItem>().FromSqlInterpolated($"select * from udfConfigSelectionItem ({tenantId},{sectionId})");
        public IQueryable<ConsentType> UdfConsentType(int tenantId, int? sectionId = null) => Set<ConsentType>().FromSqlInterpolated($"select * from udfConsentType ({tenantId},{sectionId})");
        public IQueryable<Country> UdfCountry(int tenantId, int? sectionId = null) => Set<Country>().FromSqlInterpolated($"select * from udfCountry ({tenantId},{sectionId})");
        public IQueryable<County> UdfCounty(int tenantId, int? sectionId = null) => Set<County>().FromSqlInterpolated($"select * from udfCounty ({tenantId},{sectionId})");
        public IQueryable<CountyType> UdfCountyType(int tenantId, int? sectionId = null) => Set<CountyType>().FromSqlInterpolated($"select * from udfCountyType ({tenantId},{sectionId})");
        public IQueryable<EntityType> UdfEntityType(int tenantId, int? sectionId = null) => Set<EntityType>().FromSqlInterpolated($"select * from udfEntityType ({tenantId},{sectionId})");
        public IQueryable<EscrowEntityType> UdfEscrowEntityType(int tenantId, int? sectionId = null) => Set<EscrowEntityType>().FromSqlInterpolated($"select * from udfEscrowEntityType ({tenantId},{sectionId})");
        public IQueryable<Ethnicity> UdfEthnicity(int tenantId, int? sectionId = null) => Set<Ethnicity>().FromSqlInterpolated($"select * from udfEthnicity ({tenantId},{sectionId})");
        public IQueryable<EthnicityDetail> UdfEthnicityDetail(int tenantId, int? sectionId = null) => Set<EthnicityDetail>().FromSqlInterpolated($"select * from udfEthnicityDetail ({tenantId},{sectionId})");
        public IQueryable<FamilyRelationType> UdfFamilyRelationType(int tenantId, int? sectionId = null) => Set<FamilyRelationType>().FromSqlInterpolated($"select * from udfFamilyRelationType ({tenantId},{sectionId})");
        public IQueryable<Gender> UdfGender(int tenantId, int? sectionId = null) => Set<Gender>().FromSqlInterpolated($"select * from udfGender ({tenantId},{sectionId})");
        public IQueryable<GiftSource> UdfGiftSource(int tenantId, int? sectionId = null) => Set<GiftSource>().FromSqlInterpolated($"select * from udfGiftSource ({tenantId},{sectionId})");
        public IQueryable<IncomeCategory> UdfIncomeCategory(int tenantId, int? sectionId = null) => Set<IncomeCategory>().FromSqlInterpolated($"select * from udfIncomeCategory ({tenantId},{sectionId})");
        public IQueryable<IncomeGroup> UdfIncomeGroup(int tenantId, int? sectionId = null) => Set<IncomeGroup>().FromSqlInterpolated($"select * from udfIncomeGroup ({tenantId},{sectionId})");
        public IQueryable<IncomeType> UdfIncomeType(int tenantId, int? sectionId = null) => Set<IncomeType>().FromSqlInterpolated($"select * from udfIncomeType ({tenantId},{sectionId})");
        public IQueryable<JobType> UdfJobType(int tenantId, int? sectionId = null) => Set<JobType>().FromSqlInterpolated($"select * from udfJobType ({tenantId},{sectionId})");
        public IQueryable<LiabilityType> UdfLiabilityType(int tenantId, int? sectionId = null) => Set<LiabilityType>().FromSqlInterpolated($"select * from udfLiabilityType ({tenantId},{sectionId})");
        public IQueryable<LoanGoal> UdfLoanGoal(int tenantId, int? sectionId = null) => Set<LoanGoal>().FromSqlInterpolated($"select * from udfLoanGoal ({tenantId},{sectionId})");
        public IQueryable<LoanPurpose> UdfLoanPurpose(int tenantId, int? sectionId = null) => Set<LoanPurpose>().FromSqlInterpolated($"select * from udfLoanPurpose ({tenantId},{sectionId})");
        public IQueryable<LoanPurposeProgram> UdfLoanPurposeProgram(int tenantId, int? sectionId = null) => Set<LoanPurposeProgram>().FromSqlInterpolated($"select * from udfLoanPurposeProgram ({tenantId},{sectionId})");
        public IQueryable<LoanType> UdfLoanType(int tenantId, int? sectionId = null) => Set<LoanType>().FromSqlInterpolated($"select * from udfLoanType ({tenantId},{sectionId})");
        public IQueryable<MaritalStatusList> UdfMaritalStatusList(int tenantId, int? sectionId = null) => Set<MaritalStatusList>().FromSqlInterpolated($"select * from udfMaritalStatusList ({tenantId},{sectionId})");
        public IQueryable<MaritalStatusType> UdfMaritalStatusType(int tenantId, int? sectionId = null) => Set<MaritalStatusType>().FromSqlInterpolated($"select * from udfMaritalStatusType ({tenantId},{sectionId})");
        public IQueryable<MilitaryAffiliation> UdfMilitaryAffiliation(int tenantId, int? sectionId = null) => Set<MilitaryAffiliation>().FromSqlInterpolated($"select * from udfMilitaryAffiliation ({tenantId},{sectionId})");
        public IQueryable<MilitaryBranch> UdfMilitaryBranch(int tenantId, int? sectionId = null) => Set<MilitaryBranch>().FromSqlInterpolated($"select * from udfMilitaryBranch ({tenantId},{sectionId})");
        public IQueryable<MilitaryStatusList> UdfMilitaryStatusList(int tenantId, int? sectionId = null) => Set<MilitaryStatusList>().FromSqlInterpolated($"select * from udfMilitaryStatusList ({tenantId},{sectionId})");
        public IQueryable<OtherIncomeType> UdfOtherIncomeType(int tenantId, int? sectionId = null) => Set<OtherIncomeType>().FromSqlInterpolated($"select * from udfOtherIncomeType ({tenantId},{sectionId})");
        public IQueryable<OwnershipType> UdfOwnershipType(int tenantId, int? sectionId = null) => Set<OwnershipType>().FromSqlInterpolated($"select * from udfOwnershipType ({tenantId},{sectionId})");
        public IQueryable<OwnType> UdfOwnType(int tenantId, int? sectionId = null) => Set<OwnType>().FromSqlInterpolated($"select * from udfOwnType ({tenantId},{sectionId})");
        public IQueryable<PaidBy> UdfPaidBy(int tenantId, int? sectionId = null) => Set<PaidBy>().FromSqlInterpolated($"select * from udfPaidBy ({tenantId},{sectionId})");
        public IQueryable<ProductAmortizationType> UdfProductAmortizationType(int tenantId, int? sectionId = null) => Set<ProductAmortizationType>().FromSqlInterpolated($"select * from udfProductAmortizationType ({tenantId},{sectionId})");
        public IQueryable<ProductFamily> UdfProductFamily(int tenantId, int? sectionId = null) => Set<ProductFamily>().FromSqlInterpolated($"select * from udfProductFamily ({tenantId},{sectionId})");
        public IQueryable<ProjectType> UdfProjectType(int tenantId, int? sectionId = null) => Set<ProjectType>().FromSqlInterpolated($"select * from udfProjectType ({tenantId},{sectionId})");
        public IQueryable<PropertyType> UdfPropertyType(int tenantId, int? sectionId = null) => Set<PropertyType>().FromSqlInterpolated($"select * from udfPropertyType ({tenantId},{sectionId})");
        public IQueryable<PropertyUsage> UdfPropertyUsage(int tenantId, int? sectionId = null) => Set<PropertyUsage>().FromSqlInterpolated($"select * from udfPropertyUsage ({tenantId},{sectionId})");
        public IQueryable<Question> UdfQuestion(int tenantId, int? sectionId = null) => Set<Question>().FromSqlInterpolated($"select * from udfQuestion ({tenantId},{sectionId})");
        public IQueryable<QuestionSection> UdfQuestionSection(int tenantId, int? sectionId = null) => Set<QuestionSection>().FromSqlInterpolated($"select * from udfQuestionSection ({tenantId},{sectionId})");
        public IQueryable<QuestionType> UdfQuestionType(int tenantId, int? sectionId = null) => Set<QuestionType>().FromSqlInterpolated($"select * from udfQuestionType ({tenantId},{sectionId})");
        public IQueryable<Race> UdfRace(int tenantId, int? sectionId = null) => Set<Race>().FromSqlInterpolated($"select * from udfRace ({tenantId},{sectionId})");
        public IQueryable<RaceDetail> UdfRaceDetail(int tenantId, int? sectionId = null) => Set<RaceDetail>().FromSqlInterpolated($"select * from udfRaceDetail ({tenantId},{sectionId})");
        public IQueryable<ResidencyState> UdfResidencyState(int tenantId, int? sectionId = null) => Set<ResidencyState>().FromSqlInterpolated($"select * from udfResidencyState ({tenantId},{sectionId})");
        public IQueryable<ResidencyType> UdfResidencyType(int tenantId, int? sectionId = null) => Set<ResidencyType>().FromSqlInterpolated($"select * from udfResidencyType ({tenantId},{sectionId})");
        public IQueryable<MortgageType> UdfMortgageType(int tenantId, int? sectionId = null) => Set<MortgageType>().FromSqlInterpolated($"select * from udfMortgageType ({tenantId},{sectionId})");
        public IQueryable<State> UdfState(int tenantId, int? sectionId = null) => Set<State>().FromSqlInterpolated($"select * from udfState ({tenantId},{sectionId})");
        public IQueryable<TitleEstate> UdfTitleEstate(int tenantId, int? sectionId = null) => Set<TitleEstate>().FromSqlInterpolated($"select * from udfTitleEstate ({tenantId},{sectionId})");
        public IQueryable<TitleHeldWith> UdfTitleHeldWith(int tenantId, int? sectionId = null) => Set<TitleHeldWith>().FromSqlInterpolated($"select * from udfTitleHeldWith ({tenantId},{sectionId})");
        public IQueryable<TitleLandTenure> UdfTitleLandTenure(int tenantId, int? sectionId = null) => Set<TitleLandTenure>().FromSqlInterpolated($"select * from udfTitleLandTenure ({tenantId},{sectionId})");
        public IQueryable<TitleManner> UdfTitleManner(int tenantId, int? sectionId = null) => Set<TitleManner>().FromSqlInterpolated($"select * from udfTitleManner ({tenantId},{sectionId})");
        public IQueryable<TitleTrustInfo> UdfTitleTrustInfo(int tenantId, int? sectionId = null) => Set<TitleTrustInfo>().FromSqlInterpolated($"select * from udfTitleTrustInfo ({tenantId},{sectionId})");





    }
}
