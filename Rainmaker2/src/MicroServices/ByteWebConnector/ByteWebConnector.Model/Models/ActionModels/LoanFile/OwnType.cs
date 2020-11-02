













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // OwnType

    public partial class OwnType 
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
        /// Child Borrowers where [Borrower].[OwnTypeId] point to this entity (FK_Borrower_OwnType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Borrower> Borrowers { get; set; } // Borrower.FK_Borrower_OwnType
        /// <summary>
        /// Child OpportunityLeadBinders where [OpportunityLeadBinder].[OwnTypeId] point to this entity (FK_OpportunityLeadBinder_OwnType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityLeadBinder> OpportunityLeadBinders { get; set; } // OpportunityLeadBinder.FK_OpportunityLeadBinder_OwnType
        /// <summary>
        /// Child VendorCustomerBinders where [VendorCustomerBinder].[OwnTypeId] point to this entity (FK_VendorCustomerBinder_OwnType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<VendorCustomerBinder> VendorCustomerBinders { get; set; } // VendorCustomerBinder.FK_VendorCustomerBinder_OwnType

        public OwnType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 150;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            Borrowers = new System.Collections.Generic.HashSet<Borrower>();
            OpportunityLeadBinders = new System.Collections.Generic.HashSet<OpportunityLeadBinder>();
            VendorCustomerBinders = new System.Collections.Generic.HashSet<VendorCustomerBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
