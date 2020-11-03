













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // DemographicMedium

    public partial class DemographicMedium 
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
        /// Child LoanContacts where [LoanContact].[DemographicMediumId] point to this entity (FK_LoanContact_DemographicMedium)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanContact> LoanContacts { get; set; } // LoanContact.FK_LoanContact_DemographicMedium

        public DemographicMedium()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 203;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            LoanContacts = new System.Collections.Generic.HashSet<LoanContact>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
