













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // EthnicityDetail

    public partial class EthnicityDetail 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int? EthnicityId { get; set; } // EthnicityId
        public bool? IsOther { get; set; } // IsOther
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
        /// Child LoanContactEthnicityBinders where [LoanContactEthnicityBinder].[EthnicityDetailId] point to this entity (FK_LoanContactEthnicityBinder_EthnicityDetail)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanContactEthnicityBinder> LoanContactEthnicityBinders { get; set; } // LoanContactEthnicityBinder.FK_LoanContactEthnicityBinder_EthnicityDetail

        // Foreign keys

        /// <summary>
        /// Parent Ethnicity pointed by [EthnicityDetail].([EthnicityId]) (FK_EthnicityDetail_Ethnicity)
        /// </summary>
        public virtual Ethnicity Ethnicity { get; set; } // FK_EthnicityDetail_Ethnicity

        public EthnicityDetail()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 212;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            LoanContactEthnicityBinders = new System.Collections.Generic.HashSet<LoanContactEthnicityBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
