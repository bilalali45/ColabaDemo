













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // MaritalStatusType

    public partial class MaritalStatusType 
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
        /// Child LoanContacts where [LoanContact].[UnmarriedStatusId] point to this entity (FK_LoanContact_MaritalStatusType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanContact> LoanContacts { get; set; } // LoanContact.FK_LoanContact_MaritalStatusType
        /// <summary>
        /// Child MaritalStatusLists where [MaritalStatusList].[MaritalStatusTypeId] point to this entity (FK_MaritalStatusList_MaritalStatusType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MaritalStatusList> MaritalStatusLists { get; set; } // MaritalStatusList.FK_MaritalStatusList_MaritalStatusType

        public MaritalStatusType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 218;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            LoanContacts = new System.Collections.Generic.HashSet<LoanContact>();
            MaritalStatusLists = new System.Collections.Generic.HashSet<MaritalStatusList>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>