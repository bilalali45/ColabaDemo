













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // FeeType

    public partial class FeeType 
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

        // Reverse navigation

        /// <summary>
        /// Child Fees where [Fee].[FeeTypeId] point to this entity (FK_Fee_FeeType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Fee> Fees { get; set; } // Fee.FK_Fee_FeeType
        /// <summary>
        /// Child OpportunityFees where [OpportunityFee].[FeeTypeId] point to this entity (FK_OpportunityFee_FeeType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityFee> OpportunityFees { get; set; } // OpportunityFee.FK_OpportunityFee_FeeType
        /// <summary>
        /// Child PropertyTaxes where [PropertyTax].[FeeTypeId] point to this entity (FK_PropertyTax_FeeType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PropertyTax> PropertyTaxes { get; set; } // PropertyTax.FK_PropertyTax_FeeType

        public FeeType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 116;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            Fees = new System.Collections.Generic.HashSet<Fee>();
            OpportunityFees = new System.Collections.Generic.HashSet<OpportunityFee>();
            PropertyTaxes = new System.Collections.Generic.HashSet<PropertyTax>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>