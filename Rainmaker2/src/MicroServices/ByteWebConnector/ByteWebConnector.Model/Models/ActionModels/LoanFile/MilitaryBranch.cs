













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // MilitaryBranch

    public partial class MilitaryBranch 
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
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child VaDetails where [VaDetails].[MilitaryBranchId] point to this entity (FK_VaDetails_MilitaryBranch)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<VaDetail> VaDetails { get; set; } // VaDetails.FK_VaDetails_MilitaryBranch

        public MilitaryBranch()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 202;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            VaDetails = new System.Collections.Generic.HashSet<VaDetail>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
