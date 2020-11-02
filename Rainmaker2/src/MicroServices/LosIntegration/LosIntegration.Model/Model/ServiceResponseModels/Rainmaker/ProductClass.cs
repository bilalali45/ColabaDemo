













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // ProductClass

    public partial class ProductClass 
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
        /// Child LoanRequestProductClasses where [LoanRequestProductClass].[ProductClassId] point to this entity (FK_LoanRequestProductClass_ProductClass)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequestProductClass> LoanRequestProductClasses { get; set; } // LoanRequestProductClass.FK_LoanRequestProductClass_ProductClass
        /// <summary>
        /// Child Products where [Product].[ProductClassId] point to this entity (FK_Product_ProductClass)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Product> Products { get; set; } // Product.FK_Product_ProductClass

        public ProductClass()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 63;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            LoanRequestProductClasses = new System.Collections.Generic.HashSet<LoanRequestProductClass>();
            Products = new System.Collections.Generic.HashSet<Product>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
