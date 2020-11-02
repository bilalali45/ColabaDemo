













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // OtpTracing

    public partial class OtpTracing 
    {
        public long Id { get; set; } // Id (Primary key)
        public string Phone { get; set; } // Phone (length: 20)
        public string IpAddress { get; set; } // IpAddress (length: 50)
        public System.DateTime? DateUtc { get; set; } // DateUtc
        public int? TracingTypeId { get; set; } // TracingTypeId
        public string CodeEntered { get; set; } // CodeEntered (length: 6)
        public int? ContactId { get; set; } // ContactId
        public int? OpportunityId { get; set; } // OpportunityId
        public System.DateTime? OtpCreatedOn { get; set; } // OtpCreatedOn
        public string Email { get; set; } // Email (length: 150)
        public System.DateTime? OtpUpdatedOn { get; set; } // OtpUpdatedOn
        public string TransactionId { get; set; } // TransactionId (length: 100)
        public int? StatusCode { get; set; } // StatusCode
        public int? SessionLogId { get; set; } // SessionLogId
        public int? VisitorId { get; set; } // VisitorId
        public string Message { get; set; } // Message (length: 500)
        public string ResponseJson { get; set; } // ResponseJson
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public string CarrierType { get; set; } // CarrierType (length: 100)
        public string CarrierName { get; set; } // CarrierName (length: 200)

        // Foreign keys

        /// <summary>
        /// Parent Contact pointed by [OtpTracing].([ContactId]) (FK_OtpTracing_Contact)
        /// </summary>
        public virtual Contact Contact { get; set; } // FK_OtpTracing_Contact

        /// <summary>
        /// Parent Opportunity pointed by [OtpTracing].([OpportunityId]) (FK_OtpTracing_Opportunity)
        /// </summary>
        public virtual Opportunity Opportunity { get; set; } // FK_OtpTracing_Opportunity

        /// <summary>
        /// Parent SessionLog pointed by [OtpTracing].([SessionLogId]) (FK_OtpTracing_SessionLog)
        /// </summary>
        public virtual SessionLog SessionLog { get; set; } // FK_OtpTracing_SessionLog

        /// <summary>
        /// Parent Visitor pointed by [OtpTracing].([VisitorId]) (FK_OtpTracing_Visitor)
        /// </summary>
        public virtual Visitor Visitor { get; set; } // FK_OtpTracing_Visitor

        public OtpTracing()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
