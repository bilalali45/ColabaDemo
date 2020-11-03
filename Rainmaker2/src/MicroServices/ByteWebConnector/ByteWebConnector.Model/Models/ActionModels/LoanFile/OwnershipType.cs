













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // OwnershipType

    public partial class OwnershipType 
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
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child BorrowerResidences where [BorrowerResidence].[OwnershipTypeId] point to this entity (FK_BorrowerResidence_OwnershipType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerResidence> BorrowerResidences { get; set; } // BorrowerResidence.FK_BorrowerResidence_OwnershipType

        public OwnershipType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 185;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            BorrowerResidences = new System.Collections.Generic.HashSet<BorrowerResidence>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
