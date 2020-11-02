













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // IncomeType

    public partial class IncomeType 
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
        /// Child OtherIncomes where [OtherIncome].[IncomeTypeId] point to this entity (FK_OtherIncome_IncomeType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OtherIncome> OtherIncomes { get; set; } // OtherIncome.FK_OtherIncome_IncomeType

        public IncomeType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 180;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            OtherIncomes = new System.Collections.Generic.HashSet<OtherIncome>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
