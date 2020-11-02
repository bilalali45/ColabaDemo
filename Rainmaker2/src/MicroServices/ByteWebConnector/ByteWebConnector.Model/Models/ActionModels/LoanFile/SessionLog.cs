













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // SessionLog

    public partial class SessionLog 
    {
        public int Id { get; set; } // Id (Primary key)
        public string SessionId { get; set; } // SessionId (length: 500)
        public System.DateTime? StartOnUtc { get; set; } // StartOnUtc
        public System.DateTime? EndOnUtc { get; set; } // EndOnUtc
        public System.DateTime? LastSeenOnUtc { get; set; } // LastSeenOnUtc
        public string UrlReferrer { get; set; } // UrlReferrer
        public string IpAddress { get; set; } // IpAddress (length: 50)
        public string Country { get; set; } // Country (length: 500)
        public string State { get; set; } // State (length: 500)
        public string City { get; set; } // City (length: 500)
        public int? VisitorId { get; set; } // VisitorId
        public string Remarks { get; set; } // Remarks
        public string Url { get; set; } // Url
        public int? AdsSourceId { get; set; } // AdsSourceId
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public int? UserId { get; set; } // UserId
        public int? ApplicationId { get; set; } // ApplicationId

        // Reverse navigation

        /// <summary>
        /// Child OtpTracings where [OtpTracing].[SessionLogId] point to this entity (FK_OtpTracing_SessionLog)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OtpTracing> OtpTracings { get; set; } // OtpTracing.FK_OtpTracing_SessionLog
        /// <summary>
        /// Child SessionLogDetails where [SessionLogDetail].[SessionLogId] point to this entity (FK_SessionLogDetail_SessionLog)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<SessionLogDetail> SessionLogDetails { get; set; } // SessionLogDetail.FK_SessionLogDetail_SessionLog

        // Foreign keys

        /// <summary>
        /// Parent AdsSource pointed by [SessionLog].([AdsSourceId]) (FK_SessionLog_AdsSource)
        /// </summary>
        public virtual AdsSource AdsSource { get; set; } // FK_SessionLog_AdsSource

        /// <summary>
        /// Parent Visitor pointed by [SessionLog].([VisitorId]) (FK_SessionLog_Visitor)
        /// </summary>
        public virtual Visitor Visitor { get; set; } // FK_SessionLog_Visitor

        public SessionLog()
        {
            EntityTypeId = 85;
            OtpTracings = new System.Collections.Generic.HashSet<OtpTracing>();
            SessionLogDetails = new System.Collections.Generic.HashSet<SessionLogDetail>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
