













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // MbsSecurity

    public partial class MbsSecurity 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
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
        public bool IsDeleted { get; set; } // IsDeleted
        public int? Months { get; set; } // Months

        // Reverse navigation

        /// <summary>
        /// Child MbsRates where [MbsRate].[MbsSecurityId] point to this entity (FK_MbsRate_MbsSecurity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MbsRate> MbsRates { get; set; } // MbsRate.FK_MbsRate_MbsSecurity
        /// <summary>
        /// Child MbsRateArchives where [MbsRateArchive].[MbsSecurityId] point to this entity (FK_MbsRateArchive_MbsSecurity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MbsRateArchive> MbsRateArchives { get; set; } // MbsRateArchive.FK_MbsRateArchive_MbsSecurity

        public MbsSecurity()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 196;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            MbsRates = new System.Collections.Generic.HashSet<MbsRate>();
            MbsRateArchives = new System.Collections.Generic.HashSet<MbsRateArchive>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
