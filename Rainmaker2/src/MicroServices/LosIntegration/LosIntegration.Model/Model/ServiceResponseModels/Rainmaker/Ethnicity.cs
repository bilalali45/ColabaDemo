













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // Ethnicity

    public partial class Ethnicity 
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
        /// Child EthnicityDetails where [EthnicityDetail].[EthnicityId] point to this entity (FK_EthnicityDetail_Ethnicity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EthnicityDetail> EthnicityDetails { get; set; } // EthnicityDetail.FK_EthnicityDetail_Ethnicity
        /// <summary>
        /// Child LoanContactEthnicityBinders where [LoanContactEthnicityBinder].[EthnicityId] point to this entity (FK_LoanContactEthnicityBinder_Ethnicity)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanContactEthnicityBinder> LoanContactEthnicityBinders { get; set; } // LoanContactEthnicityBinder.FK_LoanContactEthnicityBinder_Ethnicity

        public Ethnicity()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 194;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            EthnicityDetails = new System.Collections.Generic.HashSet<EthnicityDetail>();
            LoanContactEthnicityBinders = new System.Collections.Generic.HashSet<LoanContactEthnicityBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
