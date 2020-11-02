
















namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // BankRateProduct

    public partial class BankRateProduct 
    {
        public int Id { get; set; } // Id (Primary key)
        public string BankRateProductCode { get; set; } // BankRateProductCode (length: 50)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int? PointId { get; set; } // PointId
        public int? ProductId { get; set; } // ProductId
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
        /// Child BankRateCreditScoreBinders where [BankRateCreditScoreBinder].[BankRateProductId] point to this entity (FK_BankRateCreditScoreBinder_BankRateProduct)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateCreditScoreBinder> BankRateCreditScoreBinders { get; set; } // BankRateCreditScoreBinder.FK_BankRateCreditScoreBinder_BankRateProduct
        /// <summary>
        /// Child BankRateLoanToValueBinders where [BankRateLoanToValueBinder].[BankRateProductId] point to this entity (FK_BankRateLoanToValueBinder_BankRateProduct)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateLoanToValueBinder> BankRateLoanToValueBinders { get; set; } // BankRateLoanToValueBinder.FK_BankRateLoanToValueBinder_BankRateProduct
        /// <summary>
        /// Child BankRateParameters where [BankRateParameter].[BankRateProductId] point to this entity (FK_BankRateParameter_BankRateProduct)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateParameter> BankRateParameters { get; set; } // BankRateParameter.FK_BankRateParameter_BankRateProduct
        /// <summary>
        /// Child BankRatePropertyTypeBinders where [BankRatePropertyTypeBinder].[BankRateProductId] point to this entity (FK_BankRatePropertyTypeBinder_BankRateProduct)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRatePropertyTypeBinder> BankRatePropertyTypeBinders { get; set; } // BankRatePropertyTypeBinder.FK_BankRatePropertyTypeBinder_BankRateProduct
        /// <summary>
        /// Child BankRatePropertyUsageBinders where [BankRatePropertyUsageBinder].[BankRateProductId] point to this entity (FK_BankRatePropertyUsageBinder_BankRateProduct)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRatePropertyUsageBinder> BankRatePropertyUsageBinders { get; set; } // BankRatePropertyUsageBinder.FK_BankRatePropertyUsageBinder_BankRateProduct

        // Foreign keys

        /// <summary>
        /// Parent BankRatePoint pointed by [BankRateProduct].([PointId]) (FK_BankRateProduct_BankRatePoint)
        /// </summary>
        public virtual BankRatePoint BankRatePoint { get; set; } // FK_BankRateProduct_BankRatePoint

        /// <summary>
        /// Parent Product pointed by [BankRateProduct].([ProductId]) (FK_BankRateProduct_Product)
        /// </summary>
        public virtual Product Product { get; set; } // FK_BankRateProduct_Product

        public BankRateProduct()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 162;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            BankRateCreditScoreBinders = new System.Collections.Generic.HashSet<BankRateCreditScoreBinder>();
            BankRateLoanToValueBinders = new System.Collections.Generic.HashSet<BankRateLoanToValueBinder>();
            BankRateParameters = new System.Collections.Generic.HashSet<BankRateParameter>();
            BankRatePropertyTypeBinders = new System.Collections.Generic.HashSet<BankRatePropertyTypeBinder>();
            BankRatePropertyUsageBinders = new System.Collections.Generic.HashSet<BankRatePropertyUsageBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
