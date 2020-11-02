













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // FeeBlock

    public partial class FeeBlock 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public string Footer { get; set; } // Footer (length: 256)
        public string Contents { get; set; } // Contents (length: 1073741823)
        public bool IsRolledUp { get; set; } // IsRolledUp
        public int? ParentId { get; set; } // ParentId
        public int? GroupTypeId { get; set; } // GroupTypeId
        public bool IsLenderBlock { get; set; } // IsLenderBlock
        public string Number { get; set; } // Number (length: 50)
        public int? FeeTypeId { get; set; } // FeeTypeId
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

        // Reverse navigation

        /// <summary>
        /// Child Fees where [Fee].[FeeBlockId] point to this entity (FK_Fee_FeeBlock)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Fee> Fees { get; set; } // Fee.FK_Fee_FeeBlock
        /// <summary>
        /// Child FeeBlocks where [FeeBlock].[ParentId] point to this entity (FK_FeeBlock_FeeBlock)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FeeBlock> FeeBlocks { get; set; } // FeeBlock.FK_FeeBlock_FeeBlock
        /// <summary>
        /// Child OpportunityFees where [OpportunityFee].[FeeBlockId] point to this entity (FK_OpportunityFee_FeeBlock)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityFee> OpportunityFees { get; set; } // OpportunityFee.FK_OpportunityFee_FeeBlock
        /// <summary>
        /// Child PropertyTaxes where [PropertyTax].[FeeBlockId] point to this entity (FK_PropertyTax_FeeBlock)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PropertyTax> PropertyTaxes { get; set; } // PropertyTax.FK_PropertyTax_FeeBlock

        // Foreign keys

        /// <summary>
        /// Parent FeeBlock pointed by [FeeBlock].([ParentId]) (FK_FeeBlock_FeeBlock)
        /// </summary>
        public virtual FeeBlock Parent { get; set; } // FK_FeeBlock_FeeBlock

        public FeeBlock()
        {
            IsRolledUp = false;
            IsLenderBlock = false;
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 95;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            Fees = new System.Collections.Generic.HashSet<Fee>();
            FeeBlocks = new System.Collections.Generic.HashSet<FeeBlock>();
            OpportunityFees = new System.Collections.Generic.HashSet<OpportunityFee>();
            PropertyTaxes = new System.Collections.Generic.HashSet<PropertyTax>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
