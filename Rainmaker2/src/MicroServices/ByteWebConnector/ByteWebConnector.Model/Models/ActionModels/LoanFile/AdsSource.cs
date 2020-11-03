













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // AdsSource

    public partial class AdsSource 
    {
        public int Id { get; set; } // Id (Primary key)
        public string SourceNumber { get; set; } // SourceNumber (length: 50)
        public string Name { get; set; } // Name (length: 150)
        public System.DateTime? StartDateUtc { get; set; } // StartDateUtc
        public System.DateTime? EndDateUtc { get; set; } // EndDateUtc
        public bool IsGeoTargeted { get; set; } // IsGeoTargeted
        public bool IsNationwide { get; set; } // IsNationwide
        public int? LeadSourceTypeId { get; set; } // LeadSourceTypeId
        public int? LeadSourceId { get; set; } // LeadSourceId
        public int? AdsPromotionId { get; set; } // AdsPromotionId
        public int? AdsSizeId { get; set; } // AdsSizeId
        public int? AdsTypeId { get; set; } // AdsTypeId
        public int? AdsPageLocationId { get; set; } // AdsPageLocationId
        public decimal? AdsCost { get; set; } // AdsCost
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child AdsGeoLocations where [AdsGeoLocation].[AdsSourceId] point to this entity (FK_AdsGeoLocation_AdsSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AdsGeoLocation> AdsGeoLocations { get; set; } // AdsGeoLocation.FK_AdsGeoLocation_AdsSource
        /// <summary>
        /// Child AdsSourceMessages where [AdsSourceMessage].[AdSourceId] point to this entity (FK_AdsSourceMessage_AdsSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AdsSourceMessage> AdsSourceMessages { get; set; } // AdsSourceMessage.FK_AdsSourceMessage_AdsSource
        /// <summary>
        /// Child Customers where [Customer].[AdSourceId] point to this entity (FK_Customer_AdsSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Customer> Customers { get; set; } // Customer.FK_Customer_AdsSource
        /// <summary>
        /// Child LoanRequests where [LoanRequest].[AdSourceId] point to this entity (FK_LoanRequest_AdsSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; } // LoanRequest.FK_LoanRequest_AdsSource
        /// <summary>
        /// Child Opportunities where [Opportunity].[AdSourceId] point to this entity (FK_Opportunity_AdsSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities { get; set; } // Opportunity.FK_Opportunity_AdsSource
        /// <summary>
        /// Child SessionLogs where [SessionLog].[AdsSourceId] point to this entity (FK_SessionLog_AdsSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<SessionLog> SessionLogs { get; set; } // SessionLog.FK_SessionLog_AdsSource
        /// <summary>
        /// Child Visitors where [Visitor].[AdsSourceId] point to this entity (FK_Visitor_AdsSource)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Visitor> Visitors { get; set; } // Visitor.FK_Visitor_AdsSource

        // Foreign keys

        /// <summary>
        /// Parent AdsPageLocation pointed by [AdsSource].([AdsPageLocationId]) (FK_AdsSource_AdsPageLocation)
        /// </summary>
        public virtual AdsPageLocation AdsPageLocation { get; set; } // FK_AdsSource_AdsPageLocation

        /// <summary>
        /// Parent AdsPromotion pointed by [AdsSource].([AdsPromotionId]) (FK_AdsSource_AdsPromotion)
        /// </summary>
        public virtual AdsPromotion AdsPromotion { get; set; } // FK_AdsSource_AdsPromotion

        /// <summary>
        /// Parent AdsSize pointed by [AdsSource].([AdsSizeId]) (FK_AdsSource_AdsSize)
        /// </summary>
        public virtual AdsSize AdsSize { get; set; } // FK_AdsSource_AdsSize

        /// <summary>
        /// Parent AdsType pointed by [AdsSource].([AdsTypeId]) (FK_AdsSource_AdsType)
        /// </summary>
        public virtual AdsType AdsType { get; set; } // FK_AdsSource_AdsType

        /// <summary>
        /// Parent LeadSource pointed by [AdsSource].([LeadSourceId]) (FK_AdsSource_LeadSource)
        /// </summary>
        public virtual LeadSource LeadSource { get; set; } // FK_AdsSource_LeadSource

        /// <summary>
        /// Parent LeadSourceType pointed by [AdsSource].([LeadSourceTypeId]) (FK_AdsSource_LeadSourceType)
        /// </summary>
        public virtual LeadSourceType LeadSourceType { get; set; } // FK_AdsSource_LeadSourceType

        public AdsSource()
        {
            DisplayOrder = 0;
            IsActive = true;
            IsDefault = false;
            IsSystem = false;
            EntityTypeId = 144;
            IsDeleted = false;
            AdsGeoLocations = new System.Collections.Generic.HashSet<AdsGeoLocation>();
            AdsSourceMessages = new System.Collections.Generic.HashSet<AdsSourceMessage>();
            Customers = new System.Collections.Generic.HashSet<Customer>();
            LoanRequests = new System.Collections.Generic.HashSet<LoanRequest>();
            Opportunities = new System.Collections.Generic.HashSet<Opportunity>();
            SessionLogs = new System.Collections.Generic.HashSet<SessionLog>();
            Visitors = new System.Collections.Generic.HashSet<Visitor>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
