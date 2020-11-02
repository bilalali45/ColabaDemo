













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LiabilityType

    public partial class LiabilityType 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public bool? IsObligation { get; set; } // IsObligation
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
        /// Child BorrowerLiabilities where [BorrowerLiability].[LiabilityTypeId] point to this entity (FK_BorrowerLiability_LiabilityType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerLiability> BorrowerLiabilities { get; set; } // BorrowerLiability.FK_BorrowerLiability_LiabilityType

        public LiabilityType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 188;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            BorrowerLiabilities = new System.Collections.Generic.HashSet<BorrowerLiability>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
