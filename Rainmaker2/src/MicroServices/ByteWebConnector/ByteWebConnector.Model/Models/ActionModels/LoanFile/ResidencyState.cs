













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // ResidencyState

    public partial class ResidencyState 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int? ResidencyTypeId { get; set; } // ResidencyTypeId
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool? IsNonPermanent { get; set; } // IsNonPermanent
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
        /// Child LoanContacts where [LoanContact].[ResidencyStateId] point to this entity (FK_LoanContact_ResidencyState)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanContact> LoanContacts { get; set; } // LoanContact.FK_LoanContact_ResidencyState

        // Foreign keys

        /// <summary>
        /// Parent ResidencyType pointed by [ResidencyState].([ResidencyTypeId]) (FK_ResidencyState_ResidencyType)
        /// </summary>
        public virtual ResidencyType ResidencyType { get; set; } // FK_ResidencyState_ResidencyType

        public ResidencyState()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 204;
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