













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // Investor

    public partial class Investor 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public string Code { get; set; } // Code (length: 50)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public int Priority { get; set; } // Priority
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted
        public int? MinLockDays { get; set; } // MinLockDays
        public int? MaxLockDays { get; set; } // MaxLockDays
        public string Remarks { get; set; } // Remarks (length: 3000)

        // Reverse navigation

        /// <summary>
        /// Child CurrentRates where [CurrentRate].[InvestorId] point to this entity (FK_CurrentRate_Investor)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CurrentRate> CurrentRates { get; set; } // CurrentRate.FK_CurrentRate_Investor
        /// <summary>
        /// Child InvestorProducts where [InvestorProduct].[InvestorId] point to this entity (FK_InvestorProduct_Investor)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<InvestorProduct> InvestorProducts { get; set; } // InvestorProduct.FK_InvestorProduct_Investor
        /// <summary>
        /// Child RateArchives where [RateArchive].[InvestorId] point to this entity (FK_RateArchive_Investor)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<RateArchive> RateArchives { get; set; } // RateArchive.FK_RateArchive_Investor

        public Investor()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 60;
            IsDefault = false;
            Priority = 0;
            IsSystem = false;
            IsDeleted = false;
            CurrentRates = new System.Collections.Generic.HashSet<CurrentRate>();
            InvestorProducts = new System.Collections.Generic.HashSet<InvestorProduct>();
            RateArchives = new System.Collections.Generic.HashSet<RateArchive>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
