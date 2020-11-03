













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // SecondLienType

    public partial class SecondLienType 
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
        /// Child MortgageOnProperties where [MortgageOnProperty].[SecondLienTypeId] point to this entity (FK_MortgageOnProperty_SecondLienType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MortgageOnProperty> MortgageOnProperties { get; set; } // MortgageOnProperty.FK_MortgageOnProperty_SecondLienType
        /// <summary>
        /// Child SecondLiens where [SecondLien].[SecondLienTypeId] point to this entity (FK_SecondLien_SecondLienType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<SecondLien> SecondLiens { get; set; } // SecondLien.FK_SecondLien_SecondLienType

        public SecondLienType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 14;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            MortgageOnProperties = new System.Collections.Generic.HashSet<MortgageOnProperty>();
            SecondLiens = new System.Collections.Generic.HashSet<SecondLien>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
