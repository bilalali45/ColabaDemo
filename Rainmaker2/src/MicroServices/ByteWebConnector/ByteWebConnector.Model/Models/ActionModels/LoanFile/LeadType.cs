













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // LeadType

    public partial class LeadType 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? LeadSourceId { get; set; } // LeadSourceId
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
        /// Child LoanRequests where [LoanRequest].[LeadTypeId] point to this entity (FK_LoanRequest_LeadType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; } // LoanRequest.FK_LoanRequest_LeadType
        /// <summary>
        /// Child Opportunities where [Opportunity].[LeadTypeId] point to this entity (FK_Opportunity_LeadType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities { get; set; } // Opportunity.FK_Opportunity_LeadType

        // Foreign keys

        /// <summary>
        /// Parent LeadSource pointed by [LeadType].([LeadSourceId]) (FK_LeadType_LeadSource)
        /// </summary>
        public virtual LeadSource LeadSource { get; set; } // FK_LeadType_LeadSource

        public LeadType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 166;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            LoanRequests = new System.Collections.Generic.HashSet<LoanRequest>();
            Opportunities = new System.Collections.Generic.HashSet<Opportunity>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
