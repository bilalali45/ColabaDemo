













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // EscrowEntityType
    ///<summary>
    /// This table stores list of Taxes and insurance like
    /// School Tax
    /// County Tax
    /// City Tax
    /// Other Tax
    /// Home owners Insurance
    /// Other Insurance
    ///</summary>

    public partial class EscrowEntityType 
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

        ///<summary>
        /// This is to identify the Taxation Group like Tax or Insurance.
        ///</summary>
        public int? EscrowGroupId { get; set; } // EscrowGroupId

        // Reverse navigation

        /// <summary>
        /// Child OpportunityPropertyTaxes where [OpportunityPropertyTax].[EscrowEntityTypeId] point to this entity (FK_OpportunityPropertyTax_EscrowEntityType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityPropertyTax> OpportunityPropertyTaxes { get; set; } // OpportunityPropertyTax.FK_OpportunityPropertyTax_EscrowEntityType
        /// <summary>
        /// Child PropertyTaxes where [PropertyTax].[EscrowEntityTypeId] point to this entity (FK_PropertyTax_EscrowEntityType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PropertyTax> PropertyTaxes { get; set; } // PropertyTax.FK_PropertyTax_EscrowEntityType
        /// <summary>
        /// Child PropertyTaxEscrows where [PropertyTaxEscrow].[EscrowEntityTypeId] point to this entity (FK_PropertyTaxEscrow_EscrowEntityType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PropertyTaxEscrow> PropertyTaxEscrows { get; set; } // PropertyTaxEscrow.FK_PropertyTaxEscrow_EscrowEntityType

        public EscrowEntityType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 100;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            OpportunityPropertyTaxes = new System.Collections.Generic.HashSet<OpportunityPropertyTax>();
            PropertyTaxes = new System.Collections.Generic.HashSet<PropertyTax>();
            PropertyTaxEscrows = new System.Collections.Generic.HashSet<PropertyTaxEscrow>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
