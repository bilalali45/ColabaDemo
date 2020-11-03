













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // CustomerType

    public partial class CustomerType 
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
        /// Child CustomerTypeBinders where [CustomerTypeBinder].[CustomerTypeId] point to this entity (FK_CustomerTypeBinder_CustomerType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CustomerTypeBinder> CustomerTypeBinders { get; set; } // CustomerTypeBinder.FK_CustomerTypeBinder_CustomerType

        public CustomerType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 57;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            CustomerTypeBinders = new System.Collections.Generic.HashSet<CustomerTypeBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
