













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // OtherEmploymentIncomeType

    public partial class OtherEmploymentIncomeType 
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
        /// Child OtherEmploymentIncomes where [OtherEmploymentIncome].[OtherIncomeTypeId] point to this entity (FK_OtherEmploymentIncome_OtherEmploymentIncomeType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OtherEmploymentIncome> OtherEmploymentIncomes { get; set; } // OtherEmploymentIncome.FK_OtherEmploymentIncome_OtherEmploymentIncomeType

        public OtherEmploymentIncomeType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 186;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            OtherEmploymentIncomes = new System.Collections.Generic.HashSet<OtherEmploymentIncome>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
