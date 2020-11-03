













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LeadSource

    public partial class LeadSource 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public string Code { get; set; } // Code (length: 50)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public bool? IsSystemInputOnly { get; set; } // IsSystemInputOnly
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted
        public int? ModifiedBy { get; set; } // ModifiedBy
        public string LogoFileName { get; set; } // LogoFileName (length: 250)
        public string TpId { get; set; } // TpId (length: 50)

        // Reverse navigation

        /// <summary>
        /// Child AdsSources where [AdsSource].[LeadSourceId] point to this entity (FK_AdsSource_LeadSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AdsSource> AdsSources { get; set; } // AdsSource.FK_AdsSource_LeadSource
        /// <summary>
        /// Child BusinessUnits where [BusinessUnit].[DefaultLeadSourceId] point to this entity (FK_BusinessUnit_LeadSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BusinessUnit> BusinessUnits { get; set; } // BusinessUnit.FK_BusinessUnit_LeadSource
        /// <summary>
        /// Child Customers where [Customer].[LeadSourceId] point to this entity (FK_Customer_LeadSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Customer> Customers { get; set; } // Customer.FK_Customer_LeadSource
        /// <summary>
        /// Child LeadSourceDefaultProducts where [LeadSourceDefaultProduct].[LeadSourceId] point to this entity (FK_LeadSourceDefaultProduct_LeadSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LeadSourceDefaultProduct> LeadSourceDefaultProducts { get; set; } // LeadSourceDefaultProduct.FK_LeadSourceDefaultProduct_LeadSource
        /// <summary>
        /// Child LeadTypes where [LeadType].[LeadSourceId] point to this entity (FK_LeadType_LeadSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LeadType> LeadTypes { get; set; } // LeadType.FK_LeadType_LeadSource
        /// <summary>
        /// Child LoanRequests where [LoanRequest].[LeadSourceId] point to this entity (FK_LoanRequest_LeadSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; } // LoanRequest.FK_LoanRequest_LeadSource
        /// <summary>
        /// Child Opportunities where [Opportunity].[LeadSourceId] point to this entity (FK_Opportunity_LeadSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities_LeadSourceId { get; set; } // Opportunity.FK_Opportunity_LeadSource
        /// <summary>
        /// Child Opportunities where [Opportunity].[LeadSourceOriginalId] point to this entity (FK_Opportunity_OriginalLeadSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities_LeadSourceOriginalId { get; set; } // Opportunity.FK_Opportunity_OriginalLeadSource
        /// <summary>
        /// Child RateServiceParameters where [RateServiceParameter].[LeadSourceId] point to this entity (FK_RateServiceParameter_LeadSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<RateServiceParameter> RateServiceParameters { get; set; } // RateServiceParameter.FK_RateServiceParameter_LeadSource
        /// <summary>
        /// Child ThirdPartyLeads where [ThirdPartyLead].[LeadSourceId] point to this entity (FK_ThirdPartyLead_LeadSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ThirdPartyLead> ThirdPartyLeads { get; set; } // ThirdPartyLead.FK_ThirdPartyLead_LeadSource

        public LeadSource()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 74;
            IsDefault = false;
            IsSystem = false;
            IsSystemInputOnly = false;
            IsDeleted = false;
            AdsSources = new System.Collections.Generic.HashSet<AdsSource>();
            BusinessUnits = new System.Collections.Generic.HashSet<BusinessUnit>();
            Customers = new System.Collections.Generic.HashSet<Customer>();
            LeadSourceDefaultProducts = new System.Collections.Generic.HashSet<LeadSourceDefaultProduct>();
            LeadTypes = new System.Collections.Generic.HashSet<LeadType>();
            LoanRequests = new System.Collections.Generic.HashSet<LoanRequest>();
            Opportunities_LeadSourceId = new System.Collections.Generic.HashSet<Opportunity>();
            Opportunities_LeadSourceOriginalId = new System.Collections.Generic.HashSet<Opportunity>();
            RateServiceParameters = new System.Collections.Generic.HashSet<RateServiceParameter>();
            ThirdPartyLeads = new System.Collections.Generic.HashSet<ThirdPartyLead>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
