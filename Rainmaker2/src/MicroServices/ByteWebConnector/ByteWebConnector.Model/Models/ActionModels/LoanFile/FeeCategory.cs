













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // FeeCategory

    public partial class FeeCategory 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public string Prefix { get; set; } // Prefix (length: 50)
        public string Suffix { get; set; } // Suffix (length: 50)
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
        /// Child Fees where [Fee].[FeeCategoryId] point to this entity (FK_Fee_FeeCategory)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Fee> Fees { get; set; } // Fee.FK_Fee_FeeCategory

        public FeeCategory()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 172;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            Fees = new System.Collections.Generic.HashSet<Fee>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
