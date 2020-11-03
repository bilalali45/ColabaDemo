













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LoanLockPeriod

    public partial class LoanLockPeriod 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public bool IsCustomerDisplay { get; set; } // IsCustomerDisplay
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted
        public int Days { get; set; } // Days

        // Reverse navigation

        /// <summary>
        /// Child BankRateParameters where [BankRateParameter].[LoanLockPeriodId] point to this entity (FK_BankRateParameter_LoanLockPeriod)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateParameter> BankRateParameters { get; set; } // BankRateParameter.FK_BankRateParameter_LoanLockPeriod

        public LoanLockPeriod()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 141;
            IsDefault = false;
            IsSystem = false;
            IsCustomerDisplay = true;
            IsDeleted = false;
            BankRateParameters = new System.Collections.Generic.HashSet<BankRateParameter>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
