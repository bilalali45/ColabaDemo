













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // PropertyInfo

    public partial class PropertyInfo 
    {
        public int Id { get; set; } // Id (Primary key)
        public string LenderName { get; set; } // LenderName (length: 150)
        public string AccountNumber { get; set; } // AccountNumber (length: 50)
        public int? AddressInfoId { get; set; } // AddressInfoId
        public string PropertyStatus { get; set; } // PropertyStatus (length: 50)
        public int? PropertyTypeId { get; set; } // PropertyTypeId
        public int? PropertyUsageId { get; set; } // PropertyUsageId
        public bool? IsMixedUseProperty { get; set; } // IsMixedUseProperty
        public decimal? OriginalPurchasePrice { get; set; } // OriginalPurchasePrice
        public System.DateTime? BuiltDate { get; set; } // BuiltDate
        public System.DateTime? DateAcquired { get; set; } // DateAcquired
        public int? NoOfUnits { get; set; } // NoOfUnits
        public decimal? LandValue { get; set; } // LandValue
        public decimal? ImprovementCost { get; set; } // ImprovementCost
        public int? ConstructionPeriod { get; set; } // ConstructionPeriod
        public string DescribeImprovement { get; set; } // DescribeImprovement (length: 2000)
        public bool? IsImprovementToBeMade { get; set; } // IsImprovementToBeMade
        public decimal? PropertyValue { get; set; } // PropertyValue
        public decimal? RentalIncome { get; set; } // RentalIncome
        public decimal? RentalPercentage { get; set; } // RentalPercentage
        public decimal? ParticipationPercentage { get; set; } // ParticipationPercentage
        public decimal? FirstMortgageBalance { get; set; } // FirstMortgageBalance
        public bool? IsSecondLien { get; set; } // IsSecondLien
        public decimal? SecondLienBalance { get; set; } // SecondLienBalance
        public decimal? SecondLienLimit { get; set; } // SecondLienLimit
        public decimal? MortgageToBePaid { get; set; } // MortgageToBePaid
        public decimal? MortgageToBeSubordinate { get; set; } // MortgageToBeSubordinate
        public decimal? TotalMonthlyPayment { get; set; } // TotalMonthlyPayment
        public bool? IsWithEscrow { get; set; } // IsWithEscrow
        public string LegalDescription { get; set; } // LegalDescription (length: 2000)
        public string HoaName { get; set; } // HoaName (length: 150)
        public decimal? HoaDues { get; set; } // HoaDues
        public string TitleNamesWillbe { get; set; } // TitleNamesWillbe (length: 1000)
        public int? TitleMannerId { get; set; } // TitleMannerId
        public int? TitleEstateId { get; set; } // TitleEstateId
        public int? TitleTrustInfoId { get; set; } // TitleTrustInfoId
        public int? TitleLandTenureId { get; set; } // TitleLandTenureId
        public System.DateTime? LeaseholdExpireDate { get; set; } // LeaseholdExpireDate
        public bool? IsCompletedCashout { get; set; } // IsCompletedCashout
        public bool? IntentToSellPriorToPurchase { get; set; } // IntentToSellPriorToPurchase
        public decimal? MonthlyDue { get; set; } // MonthlyDue

        // Reverse navigation

        /// <summary>
        /// Child BorrowerProperties where [BorrowerProperty].[PropertyInfoId] point to this entity (FK_BorrowerProperty_PropertyInfo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerProperty> BorrowerProperties { get; set; } // BorrowerProperty.FK_BorrowerProperty_PropertyInfo
        /// <summary>
        /// Child LoanApplications where [LoanApplication].[SubjectPropertyDetailId] point to this entity (FK_LoanApplication_PropertyInfo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanApplication> LoanApplications { get; set; } // LoanApplication.FK_LoanApplication_PropertyInfo
        /// <summary>
        /// Child MortgageOnProperties where [MortgageOnProperty].[PropertyOwnId] point to this entity (FK_MortgageOnProperty_PropertyInfo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MortgageOnProperty> MortgageOnProperties { get; set; } // MortgageOnProperty.FK_MortgageOnProperty_PropertyInfo
        /// <summary>
        /// Child PropertyTaxEscrows where [PropertyTaxEscrow].[PropertyDetailId] point to this entity (FK_PropertyTaxEscrow_PropertyInfo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PropertyTaxEscrow> PropertyTaxEscrows { get; set; } // PropertyTaxEscrow.FK_PropertyTaxEscrow_PropertyInfo

        // Foreign keys

        /// <summary>
        /// Parent AddressInfo pointed by [PropertyInfo].([AddressInfoId]) (FK_PropertyInfo_AddressInfo)
        /// </summary>
        public virtual AddressInfo AddressInfo { get; set; } // FK_PropertyInfo_AddressInfo

        /// <summary>
        /// Parent PropertyType pointed by [PropertyInfo].([PropertyTypeId]) (FK_PropertyInfo_PropertyType)
        /// </summary>
        public virtual PropertyType PropertyType { get; set; } // FK_PropertyInfo_PropertyType

        /// <summary>
        /// Parent PropertyUsage pointed by [PropertyInfo].([PropertyUsageId]) (FK_PropertyInfo_PropertyUsage)
        /// </summary>
        public virtual PropertyUsage PropertyUsage { get; set; } // FK_PropertyInfo_PropertyUsage

        /// <summary>
        /// Parent TitleEstate pointed by [PropertyInfo].([TitleEstateId]) (FK_PropertyInfo_TitleEstate)
        /// </summary>
        public virtual TitleEstate TitleEstate { get; set; } // FK_PropertyInfo_TitleEstate

        /// <summary>
        /// Parent TitleLandTenure pointed by [PropertyInfo].([TitleLandTenureId]) (FK_PropertyInfo_TitleLandTenure)
        /// </summary>
        public virtual TitleLandTenure TitleLandTenure { get; set; } // FK_PropertyInfo_TitleLandTenure

        /// <summary>
        /// Parent TitleManner pointed by [PropertyInfo].([TitleEstateId]) (FK_PropertyInfo_TitleManner)
        /// </summary>
        public virtual TitleManner TitleManner { get; set; } // FK_PropertyInfo_TitleManner

        /// <summary>
        /// Parent TitleTrustInfo pointed by [PropertyInfo].([TitleTrustInfoId]) (FK_PropertyInfo_TitleTrustInfo)
        /// </summary>
        public virtual TitleTrustInfo TitleTrustInfo { get; set; } // FK_PropertyInfo_TitleTrustInfo

        public PropertyInfo()
        {
            BorrowerProperties = new System.Collections.Generic.HashSet<BorrowerProperty>();
            LoanApplications = new System.Collections.Generic.HashSet<LoanApplication>();
            MortgageOnProperties = new System.Collections.Generic.HashSet<MortgageOnProperty>();
            PropertyTaxEscrows = new System.Collections.Generic.HashSet<PropertyTaxEscrow>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
