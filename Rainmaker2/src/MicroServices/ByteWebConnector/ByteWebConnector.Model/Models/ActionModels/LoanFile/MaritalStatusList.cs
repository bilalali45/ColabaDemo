













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // MaritalStatusList

    public partial class MaritalStatusList 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int? MaritalStatusTypeId { get; set; } // MaritalStatusTypeId
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
        /// Child LoanContacts where [LoanContact].[MaritalStatusId] point to this entity (FK_LoanContact_MaritalStatusList)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanContact> LoanContacts { get; set; } // LoanContact.FK_LoanContact_MaritalStatusList

        // Foreign keys

        /// <summary>
        /// Parent MaritalStatusType pointed by [MaritalStatusList].([MaritalStatusTypeId]) (FK_MaritalStatusList_MaritalStatusType)
        /// </summary>
        public virtual MaritalStatusType MaritalStatusType { get; set; } // FK_MaritalStatusList_MaritalStatusType

        public MaritalStatusList()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 187;
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
