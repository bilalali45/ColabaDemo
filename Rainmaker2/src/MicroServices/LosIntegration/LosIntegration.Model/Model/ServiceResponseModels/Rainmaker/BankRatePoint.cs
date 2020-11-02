
















namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // BankRatePoint

    public partial class BankRatePoint 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public decimal? DiscountPointFrom { get; set; } // DiscountPointFrom
        public decimal? DiscountPointTo { get; set; } // DiscountPointTo
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
        /// Child BankRateProducts where [BankRateProduct].[PointId] point to this entity (FK_BankRateProduct_BankRatePoint)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateProduct> BankRateProducts { get; set; } // BankRateProduct.FK_BankRateProduct_BankRatePoint

        public BankRatePoint()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 105;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            BankRateProducts = new System.Collections.Generic.HashSet<BankRateProduct>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
