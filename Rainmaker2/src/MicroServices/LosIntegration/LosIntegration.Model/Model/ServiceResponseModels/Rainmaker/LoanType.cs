













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LoanType

    public partial class LoanType 
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
        /// Child LoanApplications where [LoanApplication].[LoanTypeId] point to this entity (FK_LoanApplication_LoanType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanApplication> LoanApplications { get; set; } // LoanApplication.FK_LoanApplication_LoanType
        /// <summary>
        /// Child LoanRequests where [LoanRequest].[FirstMortgageLoanTypeId] point to this entity (FK_LoanRequest_LoanType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; } // LoanRequest.FK_LoanRequest_LoanType
        /// <summary>
        /// Child MortgageOnProperties where [MortgageOnProperty].[LoanTypeId] point to this entity (FK_MortgageOnProperty_LoanType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MortgageOnProperty> MortgageOnProperties { get; set; } // MortgageOnProperty.FK_MortgageOnProperty_LoanType
        /// <summary>
        /// Child Products where [Product].[LoanTypeId] point to this entity (FK_Product_LoanType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Product> Products { get; set; } // Product.FK_Product_LoanType

        public LoanType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 97;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            LoanApplications = new System.Collections.Generic.HashSet<LoanApplication>();
            LoanRequests = new System.Collections.Generic.HashSet<LoanRequest>();
            MortgageOnProperties = new System.Collections.Generic.HashSet<MortgageOnProperty>();
            Products = new System.Collections.Generic.HashSet<Product>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
