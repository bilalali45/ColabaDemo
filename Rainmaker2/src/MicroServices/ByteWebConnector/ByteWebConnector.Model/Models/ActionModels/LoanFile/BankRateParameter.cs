
















namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // BankRateParameter

    public partial class BankRateParameter 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public int? InstanceId { get; set; } // InstanceId
        public int? PointId { get; set; } // PointId
        public int? TierId { get; set; } // TierId
        public int? ProductId { get; set; } // ProductId
        public int? BankRateProductId { get; set; } // BankRateProductId
        public decimal? MinLoan { get; set; } // MinLoan
        public decimal? MaxLoan { get; set; } // MaxLoan
        public decimal? PropertyValue { get; set; } // PropertyValue
        public decimal? LoanAmount { get; set; } // LoanAmount
        public int StateId { get; set; } // StateId
        public int? CountyId { get; set; } // CountyId
        public int? LoanPurposeId { get; set; } // LoanPurposeId
        public int? PropertyUsageId { get; set; } // PropertyUsageId
        public int? PropertyTypeId { get; set; } // PropertyTypeId
        public int? CreditScoreId { get; set; } // CreditScoreId
        public int? LoanLockPeriodId { get; set; } // LoanLockPeriodId
        public string RequestXml { get; set; } // RequestXml
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child BankRateArchives where [BankRateArchive].[BankRateParameterId] point to this entity (FK_BankRateArchive_BankRateParameter)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateArchive> BankRateArchives { get; set; } // BankRateArchive.FK_BankRateArchive_BankRateParameter

        // Foreign keys

        /// <summary>
        /// Parent BankRateInstance pointed by [BankRateParameter].([InstanceId]) (FK_BankRateParameter_BankRateInstance)
        /// </summary>
        public virtual BankRateInstance BankRateInstance { get; set; } // FK_BankRateParameter_BankRateInstance

        /// <summary>
        /// Parent BankRateProduct pointed by [BankRateParameter].([BankRateProductId]) (FK_BankRateParameter_BankRateProduct)
        /// </summary>
        public virtual BankRateProduct BankRateProduct { get; set; } // FK_BankRateParameter_BankRateProduct

        /// <summary>
        /// Parent BankRateTier pointed by [BankRateParameter].([TierId]) (FK_BankRateParameter_BankRateTier)
        /// </summary>
        public virtual BankRateTier BankRateTier { get; set; } // FK_BankRateParameter_BankRateTier

        /// <summary>
        /// Parent LoanLockPeriod pointed by [BankRateParameter].([LoanLockPeriodId]) (FK_BankRateParameter_LoanLockPeriod)
        /// </summary>
        public virtual LoanLockPeriod LoanLockPeriod { get; set; } // FK_BankRateParameter_LoanLockPeriod

        /// <summary>
        /// Parent LoanPurpose pointed by [BankRateParameter].([LoanPurposeId]) (FK_BankRateParameter_LoanPurpose)
        /// </summary>
        public virtual LoanPurpose LoanPurpose { get; set; } // FK_BankRateParameter_LoanPurpose

        public BankRateParameter()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 32;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            BankRateArchives = new System.Collections.Generic.HashSet<BankRateArchive>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
