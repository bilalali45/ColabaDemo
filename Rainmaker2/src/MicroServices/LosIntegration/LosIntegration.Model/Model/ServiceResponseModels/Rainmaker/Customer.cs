













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // Customer

    public partial class Customer 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ContactId { get; set; } // ContactId
        public int? UserId { get; set; } // UserId
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public int? LeadSourceId { get; set; } // LeadSourceId
        public int? LeadSourceTypeId { get; set; } // LeadSourceTypeId
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? FirstVisitorId { get; set; } // FirstVisitorId
        public int? FirstSessionId { get; set; } // FirstSessionId
        public bool IsDeleted { get; set; } // IsDeleted
        public int? HearAboutUsId { get; set; } // HearAboutUsId
        public string HearAboutUsOther { get; set; } // HearAboutUsOther (length: 200)
        public int? CreatedFromId { get; set; } // CreatedFromId
        public int? AdSourceId { get; set; } // AdSourceId
        public string Remarks { get; set; } // Remarks

        // Reverse navigation

        /// <summary>
        /// Child CusSubscriptionBinders where [CusSubscriptionBinder].[CustomerId] point to this entity (FK_CusSubscriptionBinder_Customer)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CusSubscriptionBinder> CusSubscriptionBinders { get; set; } // CusSubscriptionBinder.FK_CusSubscriptionBinder_Customer
        /// <summary>
        /// Child CustomerTypeBinders where [CustomerTypeBinder].[CustomerId] point to this entity (FK_CustomerTypeBinder_Customer)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CustomerTypeBinder> CustomerTypeBinders { get; set; } // CustomerTypeBinder.FK_CustomerTypeBinder_Customer
        /// <summary>
        /// Child LoanRequests where [LoanRequest].[CustomerId] point to this entity (FK_LoanRequest_Customer)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; } // LoanRequest.FK_LoanRequest_Customer
        /// <summary>
        /// Child Notifications where [Notification].[CustomerId] point to this entity (FK_Notification_Customer)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Notification> Notifications { get; set; } // Notification.FK_Notification_Customer
        /// <summary>
        /// Child OpportunityLeadBinders where [OpportunityLeadBinder].[CustomerId] point to this entity (FK_OpportunityLeadBinder_Customer)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityLeadBinder> OpportunityLeadBinders { get; set; } // OpportunityLeadBinder.FK_OpportunityLeadBinder_Customer
        /// <summary>
        /// Child QuoteResults where [QuoteResult].[CustomerId] point to this entity (FK_QuoteResult_Customer)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<QuoteResult> QuoteResults { get; set; } // QuoteResult.FK_QuoteResult_Customer
        /// <summary>
        /// Child VendorCustomerBinders where [VendorCustomerBinder].[CustomerId] point to this entity (FK_VendorCustomerBinder_Customer)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<VendorCustomerBinder> VendorCustomerBinders { get; set; } // VendorCustomerBinder.FK_VendorCustomerBinder_Customer

        // Foreign keys

        /// <summary>
        /// Parent AdsSource pointed by [Customer].([AdSourceId]) (FK_Customer_AdsSource)
        /// </summary>
        public virtual AdsSource AdsSource { get; set; } // FK_Customer_AdsSource

        /// <summary>
        /// Parent BusinessUnit pointed by [Customer].([BusinessUnitId]) (FK_Customer_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_Customer_BusinessUnit

        /// <summary>
        /// Parent Contact pointed by [Customer].([ContactId]) (FK_Customer_Contact)
        /// </summary>
        public virtual Contact Contact { get; set; } // FK_Customer_Contact

        /// <summary>
        /// Parent EntityType pointed by [Customer].([EntityTypeId]) (FK_Customer_EntityType)
        /// </summary>
        public virtual EntityType EntityType { get; set; } // FK_Customer_EntityType

        /// <summary>
        /// Parent LeadSource pointed by [Customer].([LeadSourceId]) (FK_Customer_LeadSource)
        /// </summary>
        public virtual LeadSource LeadSource { get; set; } // FK_Customer_LeadSource

        /// <summary>
        /// Parent LeadSourceType pointed by [Customer].([LeadSourceTypeId]) (FK_Customer_LeadSourceType)
        /// </summary>
        public virtual LeadSourceType LeadSourceType { get; set; } // FK_Customer_LeadSourceType

        /// <summary>
        /// Parent UserProfile pointed by [Customer].([UserId]) (FK_Customer_UserProfile)
        /// </summary>
        public virtual UserProfile UserProfile { get; set; } // FK_Customer_UserProfile

        public Customer()
        {
            DisplayOrder = 0;
            IsActive = true;
            IsSystem = false;
            EntityTypeId = 17;
            IsDeleted = false;
            CusSubscriptionBinders = new System.Collections.Generic.HashSet<CusSubscriptionBinder>();
            CustomerTypeBinders = new System.Collections.Generic.HashSet<CustomerTypeBinder>();
            LoanRequests = new System.Collections.Generic.HashSet<LoanRequest>();
            Notifications = new System.Collections.Generic.HashSet<Notification>();
            OpportunityLeadBinders = new System.Collections.Generic.HashSet<OpportunityLeadBinder>();
            QuoteResults = new System.Collections.Generic.HashSet<QuoteResult>();
            VendorCustomerBinders = new System.Collections.Generic.HashSet<VendorCustomerBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
