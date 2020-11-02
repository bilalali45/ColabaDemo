
















namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // Product

    public partial class Product 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string AliasName { get; set; } // AliasName (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public string ProductDetail { get; set; } // ProductDetail (length: 1000)
        public int? ProductTypeId { get; set; } // ProductTypeId
        public int? LoanTypeId { get; set; } // LoanTypeId
        public int? AmortizationTypeId { get; set; } // AmortizationTypeId
        public int? AmortizationPeriodMonths { get; set; } // AmortizationPeriodMonths
        public int? FixedTermMonths { get; set; } // FixedTermMonths
        public int? InitialArmTermMonth { get; set; } // InitialArmTermMonth
        public int? SubsequentChangeMonth { get; set; } // SubsequentChangeMonth
        public int? LoanIndexTypeId { get; set; } // LoanIndexTypeId
        public int? ProductFamilyId { get; set; } // ProductFamilyId
        public int? AusProcessingTypeId { get; set; } // AusProcessingTypeId
        public int? ProductQualifierId { get; set; } // ProductQualifierId
        public int? ProductClassId { get; set; } // ProductClassId
        public int? ProductBestExId { get; set; } // ProductBestExId
        public int? AgencyId { get; set; } // AgencyId
        public decimal? ResidualIncome { get; set; } // ResidualIncome
        public decimal? MiCutOff { get; set; } // MiCutOff
        public decimal? InitialRateCap { get; set; } // InitialRateCap
        public decimal? PeriodicRateCap { get; set; } // PeriodicRateCap
        public decimal? LifeTimeCap { get; set; } // LifeTimeCap
        public decimal? Margin { get; set; } // Margin
        public decimal? Floor { get; set; } // Floor
        public decimal? MinRate { get; set; } // MinRate
        public decimal? MaxRate { get; set; } // MaxRate
        public int? MinLockDays { get; set; } // MinLockDays
        public int? MaxLockDays { get; set; } // MaxLockDays
        public decimal? MinLoanAmount { get; set; } // MinLoanAmount
        public decimal? MaxLoanAmount { get; set; } // MaxLoanAmount
        public bool? IsInterestOnly { get; set; } // IsInterestOnly
        public int? InterestOnlyMonth { get; set; } // InterestOnlyMonth
        public bool? IsBalloonPayment { get; set; } // IsBalloonPayment
        public int? BalloonTermMonth { get; set; } // BalloonTermMonth
        public bool? IsPrepayPenalty { get; set; } // IsPrepayPenalty
        public int? PrepayPenaltyId { get; set; } // PrepayPenaltyId
        public int? PrepayPenaltyTermMonth { get; set; } // PrepayPenaltyTermMonth
        public bool? IsAssumable { get; set; } // IsAssumable
        public bool? IsServicing { get; set; } // IsServicing
        public bool? IsNegativeAmortization { get; set; } // IsNegativeAmortization
        public bool? IsTemporaryRateBuydown { get; set; } // IsTemporaryRateBuydown
        public decimal? InitialBuydownRate { get; set; } // InitialBuydownRate
        public int? LatePaymentDays { get; set; } // LatePaymentDays
        public decimal? LatePaymentPercent { get; set; } // LatePaymentPercent
        public int? PrepaidInterestDays { get; set; } // PrepaidInterestDays
        public string LeName { get; set; } // LeName (length: 500)
        public string ProductCode { get; set; } // ProductCode (length: 50)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public string TpName { get; set; } // TpName (length: 500)
        public bool IsDeleted { get; set; } // IsDeleted
        public string Remarks { get; set; } // Remarks (length: 3000)

        // Reverse navigation

        /// <summary>
        /// Child BankRateProducts where [BankRateProduct].[ProductId] point to this entity (FK_BankRateProduct_Product)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateProduct> BankRateProducts { get; set; } // BankRateProduct.FK_BankRateProduct_Product
        /// <summary>
        /// Child CurrentRates where [CurrentRate].[ProductId] point to this entity (FK_CurrentRate_Product)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CurrentRate> CurrentRates { get; set; } // CurrentRate.FK_CurrentRate_Product
        /// <summary>
        /// Child InvestorProducts where [InvestorProduct].[ProductId] point to this entity (FK_InvestorProduct_Product)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<InvestorProduct> InvestorProducts { get; set; } // InvestorProduct.FK_InvestorProduct_Product
        /// <summary>
        /// Child LoanApplications where [LoanApplication].[ProductId] point to this entity (FK_LoanApplication_Product)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanApplication> LoanApplications { get; set; } // LoanApplication.FK_LoanApplication_Product
        /// <summary>
        /// Child LoanRequestProducts where [LoanRequestProduct].[ProductId] point to this entity (FK_LoanRequestProduct_Product)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequestProduct> LoanRequestProducts { get; set; } // LoanRequestProduct.FK_LoanRequestProduct_Product
        /// <summary>
        /// Child ProductAds where [ProductAd].[ProductId] point to this entity (FK_ProductAd_Product)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ProductAd> ProductAds { get; set; } // ProductAd.FK_ProductAd_Product
        /// <summary>
        /// Child ProductCatalogs where [ProductCatalog].[ProductId] point to this entity (FK_ProductCatalog_Product)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ProductCatalog> ProductCatalogs { get; set; } // ProductCatalog.FK_ProductCatalog_Product
        /// <summary>
        /// Child ProductTemplatekeys where [ProductTemplatekey].[ProductId] point to this entity (FK_ProductTemplatekey_Product)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ProductTemplatekey> ProductTemplatekeys { get; set; } // ProductTemplatekey.FK_ProductTemplatekey_Product
        /// <summary>
        /// Child RateArchives where [RateArchive].[ProductId] point to this entity (FK_RateArchive_Product)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<RateArchive> RateArchives { get; set; } // RateArchive.FK_RateArchive_Product

        // Foreign keys

        /// <summary>
        /// Parent Agency pointed by [Product].([AgencyId]) (FK_Product_Agency)
        /// </summary>
        public virtual Agency Agency { get; set; } // FK_Product_Agency

        /// <summary>
        /// Parent AusProcessingType pointed by [Product].([AusProcessingTypeId]) (FK_Product_AusProcessingType)
        /// </summary>
        public virtual AusProcessingType AusProcessingType { get; set; } // FK_Product_AusProcessingType

        /// <summary>
        /// Parent LoanIndexType pointed by [Product].([LoanIndexTypeId]) (FK_Product_LoanIndexType)
        /// </summary>
        public virtual LoanIndexType LoanIndexType { get; set; } // FK_Product_LoanIndexType

        /// <summary>
        /// Parent LoanType pointed by [Product].([LoanTypeId]) (FK_Product_LoanType)
        /// </summary>
        public virtual LoanType LoanType { get; set; } // FK_Product_LoanType

        /// <summary>
        /// Parent PrepayPenalty pointed by [Product].([PrepayPenaltyId]) (FK_Product_PrepayPenalty)
        /// </summary>
        public virtual PrepayPenalty PrepayPenalty { get; set; } // FK_Product_PrepayPenalty

        /// <summary>
        /// Parent ProductAmortizationType pointed by [Product].([AmortizationTypeId]) (FK_Product_ProductAmortizationType)
        /// </summary>
        public virtual ProductAmortizationType ProductAmortizationType { get; set; } // FK_Product_ProductAmortizationType

        /// <summary>
        /// Parent ProductBestEx pointed by [Product].([ProductBestExId]) (FK_Product_ProductBestEx)
        /// </summary>
        public virtual ProductBestEx ProductBestEx { get; set; } // FK_Product_ProductBestEx

        /// <summary>
        /// Parent ProductClass pointed by [Product].([ProductClassId]) (FK_Product_ProductClass)
        /// </summary>
        public virtual ProductClass ProductClass { get; set; } // FK_Product_ProductClass

        /// <summary>
        /// Parent ProductFamily pointed by [Product].([ProductFamilyId]) (FK_Product_ProductFamily)
        /// </summary>
        public virtual ProductFamily ProductFamily { get; set; } // FK_Product_ProductFamily

        /// <summary>
        /// Parent ProductQualifier pointed by [Product].([ProductQualifierId]) (FK_Product_ProductQualifier)
        /// </summary>
        public virtual ProductQualifier ProductQualifier { get; set; } // FK_Product_ProductQualifier

        /// <summary>
        /// Parent ProductType pointed by [Product].([ProductTypeId]) (FK_Product_ProductType)
        /// </summary>
        public virtual ProductType ProductType { get; set; } // FK_Product_ProductType

        public Product()
        {
            IsBalloonPayment = false;
            IsPrepayPenalty = false;
            IsAssumable = false;
            IsServicing = false;
            LatePaymentDays = 15;
            LatePaymentPercent = 5m;
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 156;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            BankRateProducts = new System.Collections.Generic.HashSet<BankRateProduct>();
            CurrentRates = new System.Collections.Generic.HashSet<CurrentRate>();
            InvestorProducts = new System.Collections.Generic.HashSet<InvestorProduct>();
            LoanApplications = new System.Collections.Generic.HashSet<LoanApplication>();
            LoanRequestProducts = new System.Collections.Generic.HashSet<LoanRequestProduct>();
            ProductAds = new System.Collections.Generic.HashSet<ProductAd>();
            ProductCatalogs = new System.Collections.Generic.HashSet<ProductCatalog>();
            ProductTemplatekeys = new System.Collections.Generic.HashSet<ProductTemplatekey>();
            RateArchives = new System.Collections.Generic.HashSet<RateArchive>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
