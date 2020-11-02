













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // CreditScore

    public partial class CreditScore 
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
        public int? From { get; set; } // From
        public int? To { get; set; } // To

        // Reverse navigation

        /// <summary>
        /// Child BankRateCreditScoreBinders where [BankRateCreditScoreBinder].[CreditScoreId] point to this entity (FK_BankRateCreditScoreBinder_CreditScore)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateCreditScoreBinder> BankRateCreditScoreBinders { get; set; } // BankRateCreditScoreBinder.FK_BankRateCreditScoreBinder_CreditScore

        public CreditScore()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 107;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            BankRateCreditScoreBinders = new System.Collections.Generic.HashSet<BankRateCreditScoreBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
