using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace ByteWebConnector.SDK.Models
{
    [XmlRoot(ElementName = "ABOUT_VERSION", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class ABOUT_VERSION
    {
        [XmlElement(ElementName = "CreatedDatetime", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public DateTime? CreatedDatetime { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public int SequenceNumber { get; set; }
    }

    [XmlRoot(ElementName = "ABOUT_VERSIONS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class ABOUT_VERSIONS
    {
        [XmlElement(ElementName = "ABOUT_VERSION", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<ABOUT_VERSION> ABOUT_VERSION { get; set; }
    }

    [XmlRoot(ElementName = "ASSET_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class ASSET_DETAIL
    {
        [XmlElement(ElementName = "AssetAccountIdentifier", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string AssetAccountIdentifier { get; set; }
        [XmlElement(ElementName = "AssetCashOrMarketValueAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? AssetCashOrMarketValueAmount { get; set; }
        [XmlElement(ElementName = "AssetType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        //public AssetBase AssetType { get; set; }
        //public AssetBase AssetType { get; set; }
        public string AssetType { get; set; }
        [XmlElement(ElementName = "AssetTypeOtherDescription", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string AssetTypeOtherDescription { get; set; }
        [XmlElement(ElementName = "EXTENSION", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public EXTENSION EXTENSION { get; set; }
    }

    [XmlRoot(ElementName = "NAME", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class NAME
    {
        [XmlElement(ElementName = "FullName", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string FullName { get; set; }
        [XmlElement(ElementName = "FirstName", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string FirstName { get; set; }
        [XmlElement(ElementName = "LastName", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string LastName { get; set; }
        [XmlElement(ElementName = "MiddleName", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string MiddleName { get; set; }
        [XmlElement(ElementName = "SuffixName", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string SuffixName { get; set; }
    }

    [XmlRoot(ElementName = "ASSET_HOLDER", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class ASSET_HOLDER
    {
        [XmlElement(ElementName = "NAME", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public NAME NAME { get; set; }
    }

    [XmlRoot(ElementName = "ASSET", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class ASSET
    {
        [XmlElement(ElementName = "ASSET_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public ASSET_DETAIL ASSET_DETAIL { get; set; }
        [XmlElement(ElementName = "ASSET_HOLDER", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public ASSET_HOLDER ASSET_HOLDER { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public int SequenceNumber { get; set; }
        [XmlAttribute(AttributeName = "label", Namespace = "http://www.w3.org/1999/xlink")]
        public string Label { get; set; }
        [XmlElement(ElementName = "OWNED_PROPERTY", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public OWNED_PROPERTY OWNED_PROPERTY { get; set; }
    }

    [XmlRoot(ElementName = "OWNED_PROPERTY_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class OWNED_PROPERTY_DETAIL
    {
        [XmlElement(ElementName = "OwnedPropertyDispositionStatusType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        //public OwnedPropertyDispositionStatusBase OwnedPropertyDispositionStatusType { get; set; }
        public string OwnedPropertyDispositionStatusType { get; set; }
        [XmlElement(ElementName = "OwnedPropertyMaintenanceExpenseAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? OwnedPropertyMaintenanceExpenseAmount { get; set; }
        [XmlElement(ElementName = "OwnedPropertySubjectIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? OwnedPropertySubjectIndicator { get; set; }
        [XmlElement(ElementName = "OwnedPropertyRentalIncomeGrossAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? OwnedPropertyRentalIncomeGrossAmount { get; set; }
        [XmlElement(ElementName = "OwnedPropertyRentalIncomeNetAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? OwnedPropertyRentalIncomeNetAmount { get; set; }
        [XmlElement(ElementName = "OwnedPropertyLienUPBAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? OwnedPropertyLienUPBAmount;
    }

    [XmlRoot(ElementName = "ADDRESS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class ADDRESS
    {
        [XmlElement(ElementName = "AddressLineText", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string AddressLineText { get; set; }
        [XmlElement(ElementName = "CountryCode", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string CountryCode { get; set; }
        [XmlElement(ElementName = "CountryName", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string CountryName { get; set; }
        [XmlElement(ElementName = "CityName", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string CityName { get; set; }
        [XmlElement(ElementName = "PostalCode", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string PostalCode { get; set; }
        [XmlElement(ElementName = "StateCode", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string StateCode { get; set; }
        [XmlElement(ElementName = "CountyName", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string CountyName { get; set; }
        //[XmlAttribute(AttributeName = "SequenceNumber")]
        //public int? SequenceNumber { get; set; }
    }

    [XmlRoot(ElementName = "PROPERTY_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class PROPERTY_DETAIL
    {
        [XmlElement(ElementName = "PropertyCurrentUsageType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        //public PropertyCurrentUsageBase PropertyCurrentUsageType { get; set; }
        public string PropertyCurrentUsageType { get; set; }
        [XmlElement(ElementName = "PropertyEstimatedValueAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? PropertyEstimatedValueAmount { get; set; }
        [XmlElement(ElementName = "FinancedUnitCount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public int? FinancedUnitCount { get; set; }
        //[XmlElement(ElementName = "PropertyExistingCleanEnergyLienIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas", IsNullable = true)]
        //public bool? PropertyExistingCleanEnergyLienIndicator { get; set; }
        [XmlElement(ElementName = "PropertyInProjectIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas", IsNullable = true)]
        public bool? PropertyInProjectIndicator { get; set; }
        [XmlElement(ElementName = "PropertyMixedUsageIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas", IsNullable = true)]
        public bool? PropertyMixedUsageIndicator { get; set; }
        [XmlElement(ElementName = "PropertyUsageType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string PropertyUsageType { get; set; }
        [XmlElement(ElementName = "AttachmentType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string AttachmentType { get; set; }
        [XmlElement(ElementName = "ConstructionMethodType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string ConstructionMethodType { get; set; }
        [XmlElement(ElementName = "PUDIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas", IsNullable = true)]
        public bool? PUDIndicator { get; set; }
        [XmlElement(ElementName = "PropertyAcquiredDate", Namespace = "http://www.mismo.org/residential/2009/schemas", IsNullable = true)]
        public DateTime? PropertyAcquiredDate { get; set; }
        [XmlElement(ElementName = "PropertyEstateType", Namespace = "http://www.mismo.org/residential/2009/schemas", IsNullable = true)]
        public string PropertyEstateType { get; set; }
    }

    [XmlRoot(ElementName = "PROPERTY", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class PROPERTY
    {
        [XmlElement(ElementName = "ADDRESS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<ADDRESS> ADDRESS { get; set; }
        [XmlElement(ElementName = "PROPERTY_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public PROPERTY_DETAIL PROPERTY_DETAIL { get; set; }
    }

    [XmlRoot(ElementName = "OWNED_PROPERTY", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class OWNED_PROPERTY
    {
        [XmlElement(ElementName = "OWNED_PROPERTY_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public OWNED_PROPERTY_DETAIL OWNED_PROPERTY_DETAIL { get; set; }
        [XmlElement(ElementName = "PROPERTY", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public PROPERTY PROPERTY { get; set; }
    }

    [XmlRoot(ElementName = "ASSETS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class ASSETS
    {
        [XmlElement(ElementName = "ASSET", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<ASSET> ASSET { get; set; }
    }

    [XmlRoot(ElementName = "PROJECT", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class PROJECT
    {
        [XmlElement(ElementName = "PROJECT_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public PROJECT_DETAIL PROJECT_DETAIL { get; set; }
    }

    [XmlRoot(ElementName = "PROJECT_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class PROJECT_DETAIL
    {
        [XmlElement(ElementName = "ProjectLegalStructureType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string ProjectLegalStructureType { get; set; }
    }

    [XmlRoot(ElementName = "SUBJECT_PROPERTY", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class SUBJECT_PROPERTY
    {
        [XmlElement(ElementName = "ADDRESS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public ADDRESS ADDRESS { get; set; }
        [XmlElement(ElementName = "PROJECT", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public PROJECT PROJECT { get; set; }
        [XmlElement(ElementName = "PROPERTY_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public PROPERTY_DETAIL PROPERTY_DETAIL { get; set; }
        [XmlElement(ElementName = "SALES_CONTRACTS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public SALES_CONTRACTS SALES_CONTRACTS { get; set; }
    }

    [XmlRoot(ElementName = "SALES_CONTRACT_DETAIL")]
    public class SALES_CONTRACT_DETAIL
    {
        [XmlElement(ElementName = "SalesContractAmount")]
        public decimal? SalesContractAmount { get; set; }
    }

    [XmlRoot(ElementName = "SALES_CONTRACT")]
    public class SALES_CONTRACT
    {
        [XmlElement(ElementName = "SALES_CONTRACT_DETAIL")]
        public SALES_CONTRACT_DETAIL SALES_CONTRACT_DETAIL { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public string SequenceNumber { get; set; }
    }

    [XmlRoot(ElementName = "SALES_CONTRACTS")]
    public class SALES_CONTRACTS
    {
        [XmlElement(ElementName = "SALES_CONTRACT")]
        public SALES_CONTRACT SALES_CONTRACT { get; set; }
    }

    [XmlRoot(ElementName = "COLLATERAL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class COLLATERAL
    {
        [XmlElement(ElementName = "SUBJECT_PROPERTY", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public SUBJECT_PROPERTY SUBJECT_PROPERTY { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public int SequenceNumber { get; set; }
    }

    [XmlRoot(ElementName = "COLLATERALS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class COLLATERALS
    {
        [XmlElement(ElementName = "COLLATERAL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<COLLATERAL> COLLATERAL { get; set; }
    }

    [XmlRoot(ElementName = "LIABILITY_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class LIABILITY_DETAIL
    {
        [XmlElement(ElementName = "LiabilityExclusionIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? LiabilityExclusionIndicator { get; set; }
        [XmlElement(ElementName = "LiabilityMonthlyPaymentAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? LiabilityMonthlyPaymentAmount { get; set; }
        [XmlElement(ElementName = "LiabilityPayoffStatusIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? LiabilityPayoffStatusIndicator { get; set; }
        [XmlElement(ElementName = "LiabilityType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        //public LiabilityBase LiabilityType { get; set; }
        public string LiabilityType { get; set; }
        [XmlElement(ElementName = "LiabilityUnpaidBalanceAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? LiabilityUnpaidBalanceAmount { get; set; }
        [XmlElement(ElementName = "LiabilityAccountIdentifier", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string LiabilityAccountIdentifier { get; set; }
        [XmlElement(ElementName = "LiabilityRemainingTermMonthsCount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public int? LiabilityRemainingTermMonthsCount { get; set; }
        [XmlElement(ElementName = "LiabilityTypeOtherDescription", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string LiabilityTypeOtherDescription { get; set; }
        [XmlElement(ElementName = "LiabilityPaymentIncludesTaxesInsuranceIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool LiabilityPaymentIncludesTaxesInsuranceIndicator { get; set; }
    }

    [XmlRoot(ElementName = "LIABILITY_HOLDER", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class LIABILITY_HOLDER
    {
        [XmlElement(ElementName = "NAME", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public NAME NAME { get; set; }
    }

    [XmlRoot(ElementName = "LIABILITY", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class LIABILITY
    {
        [XmlElement(ElementName = "LIABILITY_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public LIABILITY_DETAIL LIABILITY_DETAIL { get; set; }
        [XmlElement(ElementName = "LIABILITY_HOLDER", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public LIABILITY_HOLDER LIABILITY_HOLDER { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public int SequenceNumber { get; set; }
        [XmlAttribute(AttributeName = "label", Namespace = "http://www.w3.org/1999/xlink")]
        public string Label { get; set; }
    }

    [XmlRoot(ElementName = "EXPENSE", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class EXPENSE
    {
        [XmlElement(ElementName = "AlimonyOwedToName", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string AlimonyOwedToName { get; set; }
        [XmlElement(ElementName = "ExpenseDescription", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string ExpenseDescription { get; set; }
        [XmlElement(ElementName = "ExpenseMonthlyPaymentAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? ExpenseMonthlyPaymentAmount { get; set; }
        [XmlElement(ElementName = "ExpenseRemainingTermMonthsCount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public int? ExpenseRemainingTermMonthsCount { get; set; }
        [XmlElement(ElementName = "ExpenseType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string ExpenseType { get; set; }
        [XmlElement(ElementName = "ExpenseTypeOtherDescription", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string ExpenseTypeOtherDescription { get; set; }
        [XmlAttribute(AttributeName = "label", Namespace = "http://www.w3.org/1999/xlink")]
        public string Label { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public int SequenceNumber { get; set; }
    }

    [XmlRoot(ElementName = "LIABILITY_SUMMARY", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class LIABILITY_SUMMARY
    {
        [XmlElement(ElementName = "TotalNonSubjectPropertyDebtsToBePaidOffAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string TotalNonSubjectPropertyDebtsToBePaidOffAmount { get; set; }
        [XmlElement(ElementName = "TotalSubjectPropertyPayoffsAndPaymentsAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string TotalSubjectPropertyPayoffsAndPaymentsAmount { get; set; }
    }

    [XmlRoot(ElementName = "LIABILITIES", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class LIABILITIES
    {
        [XmlElement(ElementName = "LIABILITY", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<LIABILITY> LIABILITY { get; set; }
        [XmlElement(ElementName = "LIABILITY_SUMMARY", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public LIABILITY_SUMMARY LIABILITY_SUMMARY { get; set; }
    }

    [XmlRoot(ElementName = "EXPENSES", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class EXPENSES
    {
        [XmlElement(ElementName = "EXPENSE", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<EXPENSE> EXPENSE { get; set; }
    }

    [XmlRoot(ElementName = "AMORTIZATION_RULE", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class AMORTIZATION_RULE
    {
        [XmlElement(ElementName = "AmortizationType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        //public AmortizationBase AmortizationType { get; set; }
        public string AmortizationType { get; set; }
        [XmlElement(ElementName = "LoanAmortizationPeriodCount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string LoanAmortizationPeriodCount { get; set; }
        [XmlElement(ElementName = "LoanAmortizationPeriodType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        //public LoanAmortizationPeriodBase LoanAmortizationPeriodType { get; set; }
        public string LoanAmortizationPeriodType { get; set; }
    }

    [XmlRoot(ElementName = "AMORTIZATION", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class AMORTIZATION
    {
        [XmlElement(ElementName = "AMORTIZATION_RULE", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public AMORTIZATION_RULE AMORTIZATION_RULE { get; set; }
    }

    [XmlRoot(ElementName = "URLA_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class URLA_DETAIL
    {
        [XmlElement(ElementName = "ApplicationSignedByLoanOriginatorDate", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string ApplicationSignedByLoanOriginatorDate { get; set; }
        [XmlElement(ElementName = "EstimatedClosingCostsAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? EstimatedClosingCostsAmount { get; set; }
        [XmlElement(ElementName = "MIAndFundingFeeFinancedAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? MIAndFundingFeeFinancedAmount { get; set; }
        [XmlElement(ElementName = "MIAndFundingFeeTotalAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? MIAndFundingFeeTotalAmount { get; set; }
        [XmlElement(ElementName = "PrepaidItemsEstimatedAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? PrepaidItemsEstimatedAmount { get; set; }
    }

    [XmlRoot(ElementName = "URLA", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class URLA
    {
        [XmlElement(ElementName = "URLA_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public URLA_DETAIL URLA_DETAIL { get; set; }
    }

    [XmlRoot(ElementName = "DOCUMENT_SPECIFIC_DATA_SET", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class DOCUMENT_SPECIFIC_DATA_SET
    {
        [XmlElement(ElementName = "URLA", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public URLA URLA { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public int SequenceNumber { get; set; }
    }

    [XmlRoot(ElementName = "DOCUMENT_SPECIFIC_DATA_SETS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class DOCUMENT_SPECIFIC_DATA_SETS
    {
        [XmlElement(ElementName = "DOCUMENT_SPECIFIC_DATA_SET", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public DOCUMENT_SPECIFIC_DATA_SET DOCUMENT_SPECIFIC_DATA_SET { get; set; }
    }

    [XmlRoot(ElementName = "HMDA_LOAN_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class HMDA_LOAN_DETAIL
    {
        [XmlElement(ElementName = "HMDA_HOEPALoanStatusIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool HMDA_HOEPALoanStatusIndicator { get; set; }
    }

    [XmlRoot(ElementName = "HMDA_LOAN", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class HMDA_LOAN
    {
        [XmlElement(ElementName = "HMDA_LOAN_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public HMDA_LOAN_DETAIL HMDA_LOAN_DETAIL { get; set; }
    }

    [XmlRoot(ElementName = "HOUSING_EXPENSE", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class HOUSING_EXPENSE
    {
        [XmlElement(ElementName = "HousingExpensePaymentAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? HousingExpensePaymentAmount { get; set; }
        [XmlElement(ElementName = "HousingExpenseTimingType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string HousingExpenseTimingType { get; set; }
        [XmlElement(ElementName = "HousingExpenseType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string HousingExpenseType { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public string SequenceNumber { get; set; }
    }
    [XmlRoot(ElementName = "HOUSING_EXPENSES", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class HOUSING_EXPENSES
    {
        [XmlElement(ElementName = "HOUSING_EXPENSE", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<HOUSING_EXPENSE> HOUSING_EXPENSE { get; set; }
    }
    [XmlRoot(ElementName = "LOAN_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class LOAN_DETAIL
    {
        [XmlElement(ElementName = "ApplicationReceivedDate", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string ApplicationReceivedDate { get; set; }
        [XmlElement(ElementName = "BalloonIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool BalloonIndicator { get; set; }
        [XmlElement(ElementName = "BelowMarketSubordinateFinancingIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? BelowMarketSubordinateFinancingIndicator { get; set; }
        [XmlElement(ElementName = "BuydownTemporarySubsidyFundingIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? BuydownTemporarySubsidyFundingIndicator { get; set; }
        [XmlElement(ElementName = "ConstructionLoanIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? ConstructionLoanIndicator { get; set; }
        [XmlElement(ElementName = "ConversionOfContractForDeedIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool ConversionOfContractForDeedIndicator { get; set; }
        [XmlElement(ElementName = "EnergyRelatedImprovementsIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? EnergyRelatedImprovementsIndicator { get; set; }
        [XmlElement(ElementName = "InterestOnlyIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? InterestOnlyIndicator { get; set; }
        [XmlElement(ElementName = "NegativeAmortizationIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? NegativeAmortizationIndicator { get; set; }
        [XmlElement(ElementName = "PrepaymentPenaltyIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? PrepaymentPenaltyIndicator { get; set; }
        [XmlElement(ElementName = "RenovationLoanIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? RenovationLoanIndicator { get; set; }
    }

    [XmlRoot(ElementName = "LOAN_IDENTIFIER", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class LOAN_IDENTIFIER
    {
        [XmlElement(ElementName = "LoanIdentifier", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string LoanIdentifier { get; set; }
        [XmlElement(ElementName = "LoanIdentifierType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string LoanIdentifierType { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public string SequenceNumber { get; set; }
    }

    [XmlRoot(ElementName = "LOAN_IDENTIFIERS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class LOAN_IDENTIFIERS
    {
        [XmlElement(ElementName = "LOAN_IDENTIFIER", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public LOAN_IDENTIFIER LOAN_IDENTIFIER { get; set; }
    }

    [XmlRoot(ElementName = "LOAN_PRODUCT_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class LOAN_PRODUCT_DETAIL
    {
        [XmlElement(ElementName = "DiscountPointsTotalAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? DiscountPointsTotalAmount { get; set; }
    }

    [XmlRoot(ElementName = "LOAN_PRODUCT", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class LOAN_PRODUCT
    {
        [XmlElement(ElementName = "LOAN_PRODUCT_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public LOAN_PRODUCT_DETAIL LOAN_PRODUCT_DETAIL { get; set; }
    }

    [XmlRoot(ElementName = "ORIGINATION_SYSTEM", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class ORIGINATION_SYSTEM
    {
        [XmlElement(ElementName = "LoanOriginationSystemName", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string LoanOriginationSystemName { get; set; }
        [XmlElement(ElementName = "LoanOriginationSystemVendorIdentifier", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string LoanOriginationSystemVendorIdentifier { get; set; }
        [XmlElement(ElementName = "LoanOriginationSystemVersionIdentifier", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string LoanOriginationSystemVersionIdentifier { get; set; }
    }

    [XmlRoot(ElementName = "ORIGINATION_SYSTEMS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class ORIGINATION_SYSTEMS
    {
        [XmlElement(ElementName = "ORIGINATION_SYSTEM", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public ORIGINATION_SYSTEM ORIGINATION_SYSTEM { get; set; }
    }

    [XmlRoot(ElementName = "TERMS_OF_LOAN", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class TERMS_OF_LOAN
    {
        [XmlElement(ElementName = "BaseLoanAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? BaseLoanAmount { get; set; }
        [XmlElement(ElementName = "LienPriorityType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        //public LienPriorityBase LienPriorityType { get; set; }
        public string LienPriorityType { get; set; }
        [XmlElement(ElementName = "LoanPurposeType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        //public LoanPurposeBase LoanPurposeType { get; set; }
        public string LoanPurposeType { get; set; }
        [XmlElement(ElementName = "MortgageType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        //public MortgageBase MortgageType { get; set; }
        public string MortgageType { get; set; }
    }

    public class CONSTRUCTION
    {
        [XmlElement(ElementName = "LandOriginalCostAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? LandOriginalCostAmount { get; set; }
    }

    [XmlRoot(ElementName = "REFINANCE", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class REFINANCE
    {
        [XmlElement(ElementName = "RefinanceCashOutDeterminationType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string RefinanceCashOutDeterminationType { get; set; }
        [XmlElement(ElementName = "RefinancePrimaryPurposeType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string RefinancePrimaryPurposeType { get; set; }

    }

    [XmlRoot(ElementName = "LOAN", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class LOAN
    {
        [XmlElement(ElementName = "AMORTIZATION", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public AMORTIZATION AMORTIZATION { get; set; }
        [XmlElement(ElementName = "CLOSING_INFORMATION", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string CLOSING_INFORMATION { get; set; }
        [XmlElement(ElementName = "CONSTRUCTION", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public CONSTRUCTION CONSTRUCTION { get; set; }

        [XmlElement(ElementName = "DOCUMENT_SPECIFIC_DATA_SETS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public DOCUMENT_SPECIFIC_DATA_SETS DOCUMENT_SPECIFIC_DATA_SETS { get; set; }
        [XmlElement(ElementName = "HMDA_LOAN", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public HMDA_LOAN HMDA_LOAN { get; set; }
        [XmlElement(ElementName = "HOUSING_EXPENSES", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public HOUSING_EXPENSES HOUSING_EXPENSES { get; set; }
        [XmlElement(ElementName = "LOAN_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public LOAN_DETAIL LOAN_DETAIL { get; set; }
        [XmlElement(ElementName = "LOAN_IDENTIFIERS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public LOAN_IDENTIFIERS LOAN_IDENTIFIERS { get; set; }
        [XmlElement(ElementName = "LOAN_PRODUCT", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public LOAN_PRODUCT LOAN_PRODUCT { get; set; }
        [XmlElement(ElementName = "ORIGINATION_SYSTEMS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public ORIGINATION_SYSTEMS ORIGINATION_SYSTEMS { get; set; }
        [XmlElement(ElementName = "QUALIFICATION", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string QUALIFICATION { get; set; }
        [XmlElement(ElementName = "REFINANCE", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public REFINANCE REFINANCE { get; set; }
        [XmlElement(ElementName = "TERMS_OF_LOAN", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public TERMS_OF_LOAN TERMS_OF_LOAN { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public int SequenceNumber { get; set; }
        [XmlAttribute(AttributeName = "LoanRoleType")]
        //public LoanRoleBase LoanRoleType { get; set; }
        public string LoanRoleType { get; set; }
        [XmlAttribute(AttributeName = "label", Namespace = "http://www.w3.org/1999/xlink")]
        public string Label { get; set; }
    }

    [XmlRoot(ElementName = "LOANS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class LOANS
    {
        [XmlElement(ElementName = "LOAN", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<LOAN> LOAN { get; set; }
    }

    [XmlRoot(ElementName = "CONTACT_POINT_EMAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class CONTACT_POINT_EMAIL
    {
        [XmlElement(ElementName = "ContactPointEmailValue", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string ContactPointEmailValue { get; set; }
    }

    [XmlRoot(ElementName = "CONTACT_POINT", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class CONTACT_POINT
    {
        [XmlElement(ElementName = "CONTACT_POINT_EMAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public CONTACT_POINT_EMAIL CONTACT_POINT_EMAIL { get; set; }
        [XmlElement(ElementName = "CONTACT_POINT_TELEPHONE", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public CONTACT_POINT_TELEPHONE CONTACT_POINT_TELEPHONE { get; set; }
        [XmlElement(ElementName = "CONTACT_POINT_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public CONTACT_POINT_DETAIL CONTACT_POINT_DETAIL { get; set; }
    }

    [XmlRoot(ElementName = "CONTACT_POINT_TELEPHONE", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class CONTACT_POINT_TELEPHONE
    {
        [XmlElement(ElementName = "ContactPointTelephoneValue", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string ContactPointTelephoneValue { get; set; }
    }

    [XmlRoot(ElementName = "CONTACT_POINT_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class CONTACT_POINT_DETAIL
    {
        [XmlElement(ElementName = "ContactPointRoleType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        //public ContactPointRoleBase ContactPointRoleType { get; set; }
        public string ContactPointRoleType { get; set; }
    }

    [XmlRoot(ElementName = "CONTACT_POINTS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class CONTACT_POINTS
    {
        [XmlElement(ElementName = "CONTACT_POINT", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<CONTACT_POINT> CONTACT_POINT { get; set; }
    }

    [XmlRoot(ElementName = "INDIVIDUAL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class INDIVIDUAL
    {
        [XmlElement(ElementName = "CONTACT_POINTS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public CONTACT_POINTS CONTACT_POINTS { get; set; }
        [XmlElement(ElementName = "NAME", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public NAME NAME { get; set; }
    }

    [XmlRoot(ElementName = "BORROWER_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class BORROWER_DETAIL
    {
        [XmlElement(ElementName = "CommunityPropertyStateResidentIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? CommunityPropertyStateResidentIndicator { get; set; }
        [XmlElement(ElementName = "DependentCount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public int? DependentCount { get; set; }
        [XmlElement(ElementName = "MaritalStatusType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string MaritalStatusType { get; set; }
        [XmlElement(ElementName = "SelfDeclaredMilitaryServiceIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool SelfDeclaredMilitaryServiceIndicator { get; set; }
    }

    [XmlRoot(ElementName = "DECLARATION_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class DECLARATION_DETAIL
    {
        [XmlElement(ElementName = "BankruptcyIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool BankruptcyIndicator { get; set; }
        [XmlElement(ElementName = "CitizenshipResidencyType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        //public CitizenshipResidencyBase CitizenshipResidencyType { get; set; }
        public string CitizenshipResidencyType { get; set; }
        [XmlElement(ElementName = "HomeownerPastThreeYearsType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string HomeownerPastThreeYearsType { get; set; }
        [XmlElement(ElementName = "IntentToOccupyType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string IntentToOccupyType { get; set; }
        [XmlElement(ElementName = "OutstandingJudgmentsIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? OutstandingJudgmentsIndicator { get; set; }
        [XmlElement(ElementName = "PartyToLawsuitIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? PartyToLawsuitIndicator { get; set; }
        [XmlElement(ElementName = "PresentlyDelinquentIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? PresentlyDelinquentIndicator { get; set; }
        [XmlElement(ElementName = "PriorPropertyForeclosureCompletedIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? PriorPropertyForeclosureCompletedIndicator { get; set; }
        [XmlElement(ElementName = "PriorPropertyTitleType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        //public PriorPropertyTitleBase PriorPropertyTitleType { get; set; }
        public string PriorPropertyTitleType { get; set; }
        [XmlElement(ElementName = "PriorPropertyUsageType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        //public PriorPropertyUsageBase PriorPropertyUsageType { get; set; }
        public string PriorPropertyUsageType { get; set; }
        [XmlElement(ElementName = "UndisclosedBorrowedFundsIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? UndisclosedBorrowedFundsIndicator { get; set; }
        [XmlElement(ElementName = "UndisclosedComakerOfNoteIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? UndisclosedComakerOfNoteIndicator { get; set; }
        [XmlElement(ElementName = "AlimonyChildSupportObligationIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? AlimonyChildSupportObligationIndicator { get; set; }
        [XmlElement(ElementName = "CoMakerEndorserOfNoteIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? CoMakerEndorserOfNoteIndicator { get; set; }
        [XmlElement(ElementName = "LoanForeclosureOrJudgmentIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? LoanForeclosureOrJudgmentIndicator { get; set; }
        [XmlElement(ElementName = "UndisclosedBorrowedFundsAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? UndisclosedBorrowedFundsAmount { get; set; }
    }

    [XmlRoot(ElementName = "DECLARATION", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class DECLARATION
    {
        [XmlElement(ElementName = "DECLARATION_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public DECLARATION_DETAIL DECLARATION_DETAIL { get; set; }
    }

    [XmlRoot(ElementName = "DEPENDENT", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class DEPENDENT
    {
        [XmlElement(ElementName = "DependentAgeYearsCount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string DependentAgeYearsCount { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public int SequenceNumber { get; set; }
    }

    [XmlRoot(ElementName = "DEPENDENTS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class DEPENDENTS
    {
        [XmlElement(ElementName = "DEPENDENT", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<DEPENDENT> DEPENDENT { get; set; }
    }

    [XmlRoot(ElementName = "CONTACT", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class CONTACT
    {
        [XmlElement(ElementName = "CONTACT_POINTS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public CONTACT_POINTS CONTACT_POINTS { get; set; }
    }

    [XmlRoot(ElementName = "CONTACTS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class CONTACTS
    {
        [XmlElement(ElementName = "CONTACT", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public CONTACT CONTACT { get; set; }
    }

    [XmlRoot(ElementName = "LEGAL_ENTITY_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class LEGAL_ENTITY_DETAIL
    {
        [XmlElement(ElementName = "FullName", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string FullName { get; set; }
    }

    [XmlRoot(ElementName = "LEGAL_ENTITY", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class LEGAL_ENTITY
    {
        [XmlElement(ElementName = "CONTACTS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public CONTACTS CONTACTS { get; set; }
        [XmlElement(ElementName = "LEGAL_ENTITY_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public LEGAL_ENTITY_DETAIL LEGAL_ENTITY_DETAIL { get; set; }
    }

    [XmlRoot(ElementName = "EMPLOYMENT", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class EMPLOYMENT
    {
        [XmlElement(ElementName = "EmploymentBorrowerSelfEmployedIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool EmploymentBorrowerSelfEmployedIndicator { get; set; }
        [XmlElement(ElementName = "EmploymentClassificationType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        //public EmploymentClassificationBase EmploymentClassificationType { get; set; }
        public string EmploymentClassificationType { get; set; }
        [XmlElement(ElementName = "EmploymentMonthlyIncomeAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? EmploymentMonthlyIncomeAmount { get; set; }
        [XmlElement(ElementName = "EmploymentPositionDescription", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string EmploymentPositionDescription { get; set; }
        [XmlElement(ElementName = "EmploymentStartDate", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string EmploymentStartDate { get; set; }
        [XmlElement(ElementName = "EmploymentStatusType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        //public EmploymentStatusBase EmploymentStatusType { get; set; }
        public string EmploymentStatusType { get; set; }
        [XmlElement(ElementName = "EmploymentTimeInLineOfWorkYearsCount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? EmploymentTimeInLineOfWorkYearsCount { get; set; }
        [XmlElement(ElementName = "EmploymentTimeInLineOfWorkMonthsCount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? EmploymentTimeInLineOfWorkMonthsCount { get; set; }
        [XmlElement(ElementName = "OwnershipInterestType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        //public OwnershipInterestBase OwnershipInterestType { get; set; }
        public string OwnershipInterestType { get; set; }
        [XmlElement(ElementName = "SpecialBorrowerEmployerRelationshipIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? SpecialBorrowerEmployerRelationshipIndicator { get; set; }
        [XmlElement(ElementName = "EmploymentEndDate", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string EmploymentEndDate { get; set; }
        [XmlElement(ElementName = "EXTENSION", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public EXTENSION EXTENSION { get; set; }
    }

    [XmlRoot(ElementName = "EMPLOYER", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class EMPLOYER
    {
        [XmlElement(ElementName = "LEGAL_ENTITY", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public LEGAL_ENTITY LEGAL_ENTITY { get; set; }
        [XmlElement(ElementName = "ADDRESS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public ADDRESS ADDRESS { get; set; }
        [XmlElement(ElementName = "EMPLOYMENT", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public EMPLOYMENT EMPLOYMENT { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public int SequenceNumber { get; set; }
        [XmlAttribute(AttributeName = "label", Namespace = "http://www.w3.org/1999/xlink")]
        public string Label { get; set; }
    }

    [XmlRoot(ElementName = "EMPLOYERS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class EMPLOYERS
    {
        [XmlElement(ElementName = "EMPLOYER", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<EMPLOYER> EMPLOYER { get; set; }
    }

    [XmlRoot(ElementName = "GOVERNMENT_MONITORING_DETAIL_EXTENSION", Namespace = "http://www.datamodelextension.org/Schema/ULAD")]
    public class GOVERNMENT_MONITORING_DETAIL_EXTENSION
    {
        [XmlElement(ElementName = "HMDAGenderType", Namespace = "http://www.datamodelextension.org/Schema/ULAD")]
        public string HMDAGenderType { get; set; }
        [XmlElement(ElementName = "ApplicationTakenMethodType", Namespace = "http://www.datamodelextension.org/Schema/ULAD")]
        public string ApplicationTakenMethodType { get; set; }
    }

    [XmlRoot(ElementName = "OTHER", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class OTHER
    {
        [XmlElement(ElementName = "GOVERNMENT_MONITORING_DETAIL_EXTENSION", Namespace = "http://www.datamodelextension.org/Schema/ULAD")]
        public GOVERNMENT_MONITORING_DETAIL_EXTENSION GOVERNMENT_MONITORING_DETAIL_EXTENSION { get; set; }
        [XmlElement(ElementName = "HMDA_RACE_DESIGNATION_EXTENSION", Namespace = "http://www.datamodelextension.org/Schema/ULAD")]
        public HMDA_RACE_DESIGNATION_EXTENSION HMDA_RACE_DESIGNATION_EXTENSION { get; set; }
        [XmlElement(ElementName = "GOVERNMENT_MONITORING_EXTENSION", Namespace = "http://www.datamodelextension.org/Schema/ULAD")]
        public GOVERNMENT_MONITORING_EXTENSION GOVERNMENT_MONITORING_EXTENSION { get; set; }
        [XmlElement(ElementName = "EMPLOYMENT_EXTENSION", Namespace = "http://www.datamodelextension.org/Schema/DU")]
        public EMPLOYMENT_EXTENSION EMPLOYMENT_EXTENSION { get; set; }
        [XmlElement(ElementName = "ASSET_DETAIL_EXTENSION", Namespace = "http://www.datamodelextension.org/Schema/ULAD")]
        public ASSET_DETAIL_EXTENSION ASSET_DETAIL_EXTENSION { get; set; }
    }

    [XmlRoot(ElementName = "ASSET_DETAIL_EXTENSION", Namespace = "http://www.datamodelextension.org/Schema/ULAD")]
    public class ASSET_DETAIL_EXTENSION
    {
        [XmlElement(ElementName = "IncludedInAssetAccountIndicator", Namespace = "http://www.datamodelextension.org/Schema/ULAD")]
        public bool IncludedInAssetAccountIndicator { get; set; }
    }

    [XmlRoot(ElementName = "EXTENSION", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class EXTENSION
    {
        [XmlElement(ElementName = "OTHER", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public OTHER OTHER { get; set; }
    }

    [XmlRoot(ElementName = "GOVERNMENT_MONITORING_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class GOVERNMENT_MONITORING_DETAIL
    {
        [XmlElement(ElementName = "HMDAEthnicityCollectedBasedOnVisualObservationOrSurnameIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? HMDAEthnicityCollectedBasedOnVisualObservationOrSurnameIndicator { get; set; }
        [XmlElement(ElementName = "HMDAEthnicityRefusalIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? HMDAEthnicityRefusalIndicator { get; set; }
        [XmlElement(ElementName = "HMDAGenderCollectedBasedOnVisualObservationOrNameIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? HMDAGenderCollectedBasedOnVisualObservationOrNameIndicator { get; set; }
        [XmlElement(ElementName = "HMDAGenderRefusalIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? HMDAGenderRefusalIndicator { get; set; }
        [XmlElement(ElementName = "HMDARaceCollectedBasedOnVisualObservationOrSurnameIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? HMDARaceCollectedBasedOnVisualObservationOrSurnameIndicator { get; set; }
        [XmlElement(ElementName = "HMDARaceRefusalIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? HMDARaceRefusalIndicator { get; set; }
        [XmlElement(ElementName = "EXTENSION", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public EXTENSION EXTENSION { get; set; }
    }

    [XmlRoot(ElementName = "HMDA_ETHNICITY_ORIGIN", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class HMDA_ETHNICITY_ORIGIN
    {
        [XmlElement(ElementName = "HMDAEthnicityOriginType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string HMDAEthnicityOriginType { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public int SequenceNumber { get; set; }
    }

    [XmlRoot(ElementName = "HMDA_ETHNICITY_ORIGINS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class HMDA_ETHNICITY_ORIGINS
    {
        [XmlElement(ElementName = "HMDA_ETHNICITY_ORIGIN", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<HMDA_ETHNICITY_ORIGIN> HMDA_ETHNICITY_ORIGIN { get; set; }
    }

    [XmlRoot(ElementName = "HMDA_RACE_DESIGNATION_EXTENSION", Namespace = "http://www.datamodelextension.org/Schema/ULAD")]
    public class HMDA_RACE_DESIGNATION_EXTENSION
    {
        [XmlElement(ElementName = "HMDARaceDesignationType", Namespace = "http://www.datamodelextension.org/Schema/ULAD")]
        public string HMDARaceDesignationType { get; set; }
    }

    [XmlRoot(ElementName = "HMDA_RACE_DESIGNATION", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class HMDA_RACE_DESIGNATION
    {
        [XmlElement(ElementName = "EXTENSION", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public EXTENSION EXTENSION { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public string SequenceNumber { get; set; }
    }

    [XmlRoot(ElementName = "HMDA_RACE_DESIGNATIONS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class HMDA_RACE_DESIGNATIONS
    {
        [XmlElement(ElementName = "HMDA_RACE_DESIGNATION", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<HMDA_RACE_DESIGNATION> HMDA_RACE_DESIGNATION { get; set; }
    }

    [XmlRoot(ElementName = "HMDA_RACE_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class HMDA_RACE_DETAIL
    {
        [XmlElement(ElementName = "HMDARaceType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string HMDARaceType { get; set; }
    }

    [XmlRoot(ElementName = "HMDA_RACE", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class HMDA_RACE
    {
        [XmlElement(ElementName = "HMDA_RACE_DESIGNATIONS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public HMDA_RACE_DESIGNATIONS HMDA_RACE_DESIGNATIONS { get; set; }
        [XmlElement(ElementName = "HMDA_RACE_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public HMDA_RACE_DETAIL HMDA_RACE_DETAIL { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public string SequenceNumber { get; set; }
    }

    [XmlRoot(ElementName = "HMDA_RACES", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class HMDA_RACES
    {
        [XmlElement(ElementName = "HMDA_RACE", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<HMDA_RACE> HMDA_RACE { get; set; }
    }

    [XmlRoot(ElementName = "HMDA_ETHNICITY", Namespace = "http://www.datamodelextension.org/Schema/ULAD")]
    public class HMDA_ETHNICITY
    {
        [XmlElement(ElementName = "HMDAEthnicityType", Namespace = "http://www.datamodelextension.org/Schema/ULAD")]
        public string HMDAEthnicityType { get; set; }
    }

    [XmlRoot(ElementName = "HMDA_ETHNICITIES", Namespace = "http://www.datamodelextension.org/Schema/ULAD")]
    public class HMDA_ETHNICITIES
    {
        [XmlElement(ElementName = "HMDA_ETHNICITY", Namespace = "http://www.datamodelextension.org/Schema/ULAD")]
        public List<HMDA_ETHNICITY> HMDA_ETHNICITY { get; set; }
    }

    [XmlRoot(ElementName = "GOVERNMENT_MONITORING_EXTENSION", Namespace = "http://www.datamodelextension.org/Schema/ULAD")]
    public class GOVERNMENT_MONITORING_EXTENSION
    {
        [XmlElement(ElementName = "HMDA_ETHNICITIES", Namespace = "http://www.datamodelextension.org/Schema/ULAD")]
        public HMDA_ETHNICITIES HMDA_ETHNICITIES { get; set; }
    }

    [XmlRoot(ElementName = "GOVERNMENT_MONITORING", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class GOVERNMENT_MONITORING
    {
        [XmlElement(ElementName = "GOVERNMENT_MONITORING_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public GOVERNMENT_MONITORING_DETAIL GOVERNMENT_MONITORING_DETAIL { get; set; }
        [XmlElement(ElementName = "HMDA_ETHNICITY_ORIGINS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public HMDA_ETHNICITY_ORIGINS HMDA_ETHNICITY_ORIGINS { get; set; }
        [XmlElement(ElementName = "HMDA_RACES", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public HMDA_RACES HMDA_RACES { get; set; }
        [XmlElement(ElementName = "EXTENSION", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public EXTENSION EXTENSION { get; set; }
    }

    [XmlRoot(ElementName = "LANDLORD_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class LANDLORD_DETAIL
    {
        [XmlElement(ElementName = "MonthlyRentAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? MonthlyRentAmount { get; set; }
    }

    [XmlRoot(ElementName = "LANDLORD", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class LANDLORD
    {
        [XmlElement(ElementName = "LANDLORD_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public LANDLORD_DETAIL LANDLORD_DETAIL { get; set; }
    }

    [XmlRoot(ElementName = "RESIDENCE_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class RESIDENCE_DETAIL
    {
        [XmlElement(ElementName = "BorrowerResidencyBasisType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string BorrowerResidencyBasisType { get; set; }
        [XmlElement(ElementName = "BorrowerResidencyDurationMonthsCount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public int? BorrowerResidencyDurationMonthsCount { get; set; }
        [XmlElement(ElementName = "BorrowerResidencyType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string BorrowerResidencyType { get; set; }
        [XmlElement(ElementName = "BorrowerResidencyDurationYearsCount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public int BorrowerResidencyDurationYearsCount { get; set; }
    }

    [XmlRoot(ElementName = "RESIDENCE", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class RESIDENCE
    {
        [XmlElement(ElementName = "ADDRESS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public ADDRESS ADDRESS { get; set; }
        [XmlElement(ElementName = "LANDLORD", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public LANDLORD LANDLORD { get; set; }
        [XmlElement(ElementName = "RESIDENCE_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public RESIDENCE_DETAIL RESIDENCE_DETAIL { get; set; }
    }

    [XmlRoot(ElementName = "RESIDENCES", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class RESIDENCES
    {
        [XmlElement(ElementName = "RESIDENCE", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<RESIDENCE> RESIDENCE { get; set; }
    }

    [XmlRoot(ElementName = "BORROWER", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class BORROWER
    {
        [XmlElement(ElementName = "BORROWER_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public BORROWER_DETAIL BORROWER_DETAIL { get; set; }
        [XmlElement(ElementName = "COUNSELING", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string COUNSELING { get; set; }
        [XmlElement(ElementName = "DECLARATION", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public DECLARATION DECLARATION { get; set; }
        [XmlElement(ElementName = "DEPENDENTS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public DEPENDENTS DEPENDENTS { get; set; }
        [XmlElement(ElementName = "EMPLOYERS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public EMPLOYERS EMPLOYERS { get; set; }
        [XmlElement(ElementName = "GOVERNMENT_MONITORING", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public GOVERNMENT_MONITORING GOVERNMENT_MONITORING { get; set; }
        [XmlElement(ElementName = "RESIDENCES", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public RESIDENCES RESIDENCES { get; set; }
        [XmlElement(ElementName = "CURRENT_INCOME", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public CURRENT_INCOME CURRENT_INCOME { get; set; }
    }

    [XmlRoot(ElementName = "ROLE_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class ROLE_DETAIL
    {
        [XmlElement(ElementName = "PartyRoleType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string PartyRoleType { get; set; }
    }

    [XmlRoot(ElementName = "ROLE", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class ROLE
    {
        [XmlElement(ElementName = "BORROWER", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public BORROWER BORROWER { get; set; }
        [XmlElement(ElementName = "ROLE_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public ROLE_DETAIL ROLE_DETAIL { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public int SequenceNumber { get; set; }
        [XmlAttribute(AttributeName = "label", Namespace = "http://www.w3.org/1999/xlink")]
        public string Label { get; set; }
        [XmlElement(ElementName = "PROPERTY_OWNER", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public PROPERTY_OWNER PROPERTY_OWNER { get; set; }
    }

    [XmlRoot(ElementName = "ROLES", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class ROLES
    {
        [XmlElement(ElementName = "ROLE", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public ROLE ROLE { get; set; }
    }

    [XmlRoot(ElementName = "TAXPAYER_IDENTIFIER", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class TAXPAYER_IDENTIFIER
    {
        [XmlElement(ElementName = "TaxpayerIdentifierType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string TaxpayerIdentifierType { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public int SequenceNumber { get; set; }
    }

    [XmlRoot(ElementName = "TAXPAYER_IDENTIFIERS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class TAXPAYER_IDENTIFIERS
    {
        [XmlElement(ElementName = "TAXPAYER_IDENTIFIER", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public TAXPAYER_IDENTIFIER TAXPAYER_IDENTIFIER { get; set; }
    }

    [XmlRoot(ElementName = "PARTY", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class PARTY
    {
        [XmlElement(ElementName = "INDIVIDUAL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public INDIVIDUAL INDIVIDUAL { get; set; }
        [XmlElement(ElementName = "ROLES", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public ROLES ROLES { get; set; }
        [XmlElement(ElementName = "TAXPAYER_IDENTIFIERS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public TAXPAYER_IDENTIFIERS TAXPAYER_IDENTIFIERS { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public int SequenceNumber { get; set; }
        [XmlElement(ElementName = "LEGAL_ENTITY", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public LEGAL_ENTITY LEGAL_ENTITY { get; set; }
        [XmlElement(ElementName = "ADDRESSES", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public ADDRESSES ADDRESSES { get; set; }
    }

    [XmlRoot(ElementName = "CURRENT_INCOME_ITEM_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class CURRENT_INCOME_ITEM_DETAIL
    {
        [XmlElement(ElementName = "CurrentIncomeMonthlyTotalAmount", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public decimal? CurrentIncomeMonthlyTotalAmount { get; set; }
        [XmlElement(ElementName = "EmploymentIncomeIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? EmploymentIncomeIndicator { get; set; }
        [XmlElement(ElementName = "IncomeType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        //public IncomeBase IncomeType { get; set; }
        public string IncomeType { get; set; }
        [XmlElement(ElementName = "SeasonalIncomeIndicator", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public bool? SeasonalIncomeIndicator { get; set; }
        [XmlElement(ElementName = "IncomeTypeOtherDescription", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string IncomeTypeOtherDescription { get; set; }
    }

    [XmlRoot(ElementName = "CURRENT_INCOME_ITEM", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class CURRENT_INCOME_ITEM
    {
        [XmlElement(ElementName = "CURRENT_INCOME_ITEM_DETAIL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public CURRENT_INCOME_ITEM_DETAIL CURRENT_INCOME_ITEM_DETAIL { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public int SequenceNumber { get; set; }
        [XmlAttribute(AttributeName = "label", Namespace = "http://www.w3.org/1999/xlink")]
        public string Label { get; set; }
    }

    [XmlRoot(ElementName = "CURRENT_INCOME_ITEMS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class CURRENT_INCOME_ITEMS
    {
        [XmlElement(ElementName = "CURRENT_INCOME_ITEM", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<CURRENT_INCOME_ITEM> CURRENT_INCOME_ITEM { get; set; }
    }

    [XmlRoot(ElementName = "CURRENT_INCOME", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class CURRENT_INCOME
    {
        [XmlElement(ElementName = "CURRENT_INCOME_ITEMS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public CURRENT_INCOME_ITEMS CURRENT_INCOME_ITEMS { get; set; }
    }

    [XmlRoot(ElementName = "EMPLOYMENT_EXTENSION", Namespace = "http://www.datamodelextension.org/Schema/DU")]
    public class EMPLOYMENT_EXTENSION
    {
        [XmlElement(ElementName = "ForeignIncomeIndicator", Namespace = "http://www.datamodelextension.org/Schema/DU")]
        public string ForeignIncomeIndicator { get; set; }
        [XmlElement(ElementName = "SeasonalIncomeIndicator", Namespace = "http://www.datamodelextension.org/Schema/DU")]
        public string SeasonalIncomeIndicator { get; set; }
    }

    [XmlRoot(ElementName = "PROPERTY_OWNER", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class PROPERTY_OWNER
    {
        [XmlElement(ElementName = "PropertyOwnerStatusType", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public string PropertyOwnerStatusType { get; set; }
    }

    [XmlRoot(ElementName = "ADDRESSES", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class ADDRESSES
    {
        [XmlElement(ElementName = "ADDRESS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public ADDRESS ADDRESS { get; set; }
    }

    [XmlRoot(ElementName = "PARTIES", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class PARTIES
    {
        [XmlElement(ElementName = "PARTY", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<PARTY> PARTY { get; set; }
    }

    //[XmlRoot(ElementName = "RELATIONSHIP", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class RELATIONSHIP
    {
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public int SequenceNumber { get; set; }
        [XmlAttribute(AttributeName = "from", Namespace = "http://www.w3.org/1999/xlink")]
        public string From { get; set; }
        [XmlAttribute(AttributeName = "to", Namespace = "http://www.w3.org/1999/xlink")]
        public string To { get; set; }
        [XmlAttribute(AttributeName = "arcrole", Namespace = "http://www.w3.org/1999/xlink")]
        public string Arcrole { get; set; }
    }

    //[XmlRoot(ElementName = "RELATIONSHIPS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class RELATIONSHIPS
    {
        [XmlElement(ElementName = "RELATIONSHIP", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<RELATIONSHIP> RELATIONSHIP { get; set; }
    }

    //[XmlRoot(ElementName = "DEAL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class DEAL
    {
        [XmlElement(ElementName = "ASSETS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public ASSETS ASSETS { get; set; }
        [XmlElement(ElementName = "COLLATERALS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public COLLATERALS COLLATERALS { get; set; }
        [XmlElement(ElementName = "EXPENSES", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public EXPENSES EXPENSES { get; set; }
        [XmlElement(ElementName = "LIABILITIES", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public LIABILITIES LIABILITIES { get; set; }
        [XmlElement(ElementName = "LOANS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public LOANS LOANS { get; set; }
        [XmlElement(ElementName = "PARTIES", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public PARTIES PARTIES { get; set; }
        [XmlElement(ElementName = "RELATIONSHIPS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public RELATIONSHIPS RELATIONSHIPS { get; set; }
        [XmlAttribute(AttributeName = "SequenceNumber")]
        public int SequenceNumber { get; set; }
    }

    //[XmlRoot(ElementName = "DEALS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class DEALS
    {
        [XmlElement(ElementName = "DEAL", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public List<DEAL> DEAL { get; set; }
    }

    //[XmlRoot(ElementName = "DEAL_SET", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class DEAL_SET
    {
        [XmlElement(ElementName = "DEALS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public DEALS DEALS { get; set; }
    }

    //[XmlRoot(ElementName = "DEAL_SETS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class DEAL_SETS
    {
        [XmlElement(ElementName = "DEAL_SET", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public DEAL_SET DEAL_SET { get; set; }
    }

    [XmlRoot(ElementName = "MESSAGE", Namespace = "http://www.mismo.org/residential/2009/schemas")]
    public class MISMO34FORMAT
    {
        public MISMO34FORMAT()
        {
            MISMOReferenceModelIdentifier = "3.4.032420160128";
        }
        [XmlElement(ElementName = "ABOUT_VERSIONS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public ABOUT_VERSIONS ABOUT_VERSIONS { get; set; }
        [XmlElement(ElementName = "DEAL_SETS", Namespace = "http://www.mismo.org/residential/2009/schemas")]
        public DEAL_SETS DEAL_SETS { get; set; }
        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns")]
        public string Xsi { get; set; }
        [XmlAttribute(AttributeName = "xlink", Namespace = "http://www.w3.org/2000/xmlns")]
        public string Xlink { get; set; }
        [XmlAttribute(AttributeName = "xmlns", Namespace = "http://www.w3.org/1999/xlink")]
        public string Xmlns { get; set; }
        [XmlAttribute(AttributeName = "ULAD", Namespace = "http://www.w3.org/2000/xmlns")]
        public string ULAD { get; set; }
        [XmlAttribute(AttributeName = "DU", Namespace = "http://www.w3.org/2000/xmlns")]
        public string DU { get; set; }
        [XmlAttribute(AttributeName = "schemaLocation", Namespace = "http://www.w3.org/2001/XMLSchema-instance")]
        public string SchemaLocation { get; set; }
        [XmlAttribute(AttributeName = "MISMOReferenceModelIdentifier")]
        public string MISMOReferenceModelIdentifier { get; set; }
    }

}

