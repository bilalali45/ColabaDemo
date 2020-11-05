













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // ResidencyType

    public partial class ResidencyType 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
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
        /// Child LoanContacts where [LoanContact].[ResidencyTypeId] point to this entity (FK_LoanContact_ResidencyType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanContact> LoanContacts { get; set; } // LoanContact.FK_LoanContact_ResidencyType
        /// <summary>
        /// Child LoanRequests where [LoanRequest].[ResidencyTypeId] point to this entity (FK_LoanRequest_ResidencyType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; } // LoanRequest.FK_LoanRequest_ResidencyType
        /// <summary>
        /// Child ResidencyStates where [ResidencyState].[ResidencyTypeId] point to this entity (FK_ResidencyState_ResidencyType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ResidencyState> ResidencyStates { get; set; } // ResidencyState.FK_ResidencyState_ResidencyType

        public ResidencyType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 158;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            LoanContacts = new System.Collections.Generic.HashSet<LoanContact>();
            LoanRequests = new System.Collections.Generic.HashSet<LoanRequest>();
            ResidencyStates = new System.Collections.Generic.HashSet<ResidencyState>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>