













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LoanToValue

    public partial class LoanToValue 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted
        public decimal? From { get; set; } // From
        public decimal? To { get; set; } // To

        // Reverse navigation

        /// <summary>
        /// Child BankRateLoanToValueBinders where [BankRateLoanToValueBinder].[LoanToValueId] point to this entity (FK_BankRateLoanToValueBinder_LoanToValue)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateLoanToValueBinder> BankRateLoanToValueBinders { get; set; } // BankRateLoanToValueBinder.FK_BankRateLoanToValueBinder_LoanToValue

        public LoanToValue()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 161;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            BankRateLoanToValueBinders = new System.Collections.Generic.HashSet<BankRateLoanToValueBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
