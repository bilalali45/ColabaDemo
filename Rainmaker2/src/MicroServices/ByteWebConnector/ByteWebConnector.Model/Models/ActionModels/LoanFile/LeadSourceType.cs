













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // LeadSourceType

    public partial class LeadSourceType 
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
        /// Child AdsSources where [AdsSource].[LeadSourceTypeId] point to this entity (FK_AdsSource_LeadSourceType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AdsSource> AdsSources { get; set; } // AdsSource.FK_AdsSource_LeadSourceType
        /// <summary>
        /// Child Customers where [Customer].[LeadSourceTypeId] point to this entity (FK_Customer_LeadSourceType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Customer> Customers { get; set; } // Customer.FK_Customer_LeadSourceType
        /// <summary>
        /// Child LoanRequests where [LoanRequest].[LeadSourceTypeId] point to this entity (FK_LoanRequest_LeadSourceType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; } // LoanRequest.FK_LoanRequest_LeadSourceType
        /// <summary>
        /// Child Opportunities where [Opportunity].[LeadSourceTypeId] point to this entity (FK_Opportunity_LeadSourceType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities { get; set; } // Opportunity.FK_Opportunity_LeadSourceType

        public LeadSourceType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 66;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            AdsSources = new System.Collections.Generic.HashSet<AdsSource>();
            Customers = new System.Collections.Generic.HashSet<Customer>();
            LoanRequests = new System.Collections.Generic.HashSet<LoanRequest>();
            Opportunities = new System.Collections.Generic.HashSet<Opportunity>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
