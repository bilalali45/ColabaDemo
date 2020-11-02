













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // Visitor

    public partial class Visitor 
    {
        public int Id { get; set; } // Id (Primary key)
        public System.DateTime CreatedDateUtc { get; set; } // CreatedDateUtc
        public System.DateTime? LastVisitedDateUtc { get; set; } // LastVisitedDateUtc
        public decimal? RandomNo { get; set; } // RandomNo
        public string VisitorCode { get; set; } // VisitorCode (length: 50)
        public int? FirstSessionId { get; set; } // FirstSessionId
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? AdsSourceId { get; set; } // AdsSourceId
        public bool IsActive { get; set; } // IsActive
        public bool IsDeleted { get; set; } // IsDeleted
        public string TpId { get; set; } // TpId (length: 50)
        public int? VisitorTypeId { get; set; } // VisitorTypeId

        // Reverse navigation

        /// <summary>
        /// Child InitialContacts where [InitialContact].[VisitorId] point to this entity (FK_InitialContact_Visitor)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<InitialContact> InitialContacts { get; set; } // InitialContact.FK_InitialContact_Visitor
        /// <summary>
        /// Child LoanRequests where [LoanRequest].[VisitorId] point to this entity (FK_LoanRequest_Visitor)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; } // LoanRequest.FK_LoanRequest_Visitor
        /// <summary>
        /// Child OtpTracings where [OtpTracing].[VisitorId] point to this entity (FK_OtpTracing_Visitor)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OtpTracing> OtpTracings { get; set; } // OtpTracing.FK_OtpTracing_Visitor
        /// <summary>
        /// Child SessionLogs where [SessionLog].[VisitorId] point to this entity (FK_SessionLog_Visitor)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<SessionLog> SessionLogs { get; set; } // SessionLog.FK_SessionLog_Visitor
        /// <summary>
        /// Child SystemEventLogs where [SystemEventLog].[VisitorId] point to this entity (FK_SystemEventLog_Visitor)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<SystemEventLog> SystemEventLogs { get; set; } // SystemEventLog.FK_SystemEventLog_Visitor

        // Foreign keys

        /// <summary>
        /// Parent AdsSource pointed by [Visitor].([AdsSourceId]) (FK_Visitor_AdsSource)
        /// </summary>
        public virtual AdsSource AdsSource { get; set; } // FK_Visitor_AdsSource

        public Visitor()
        {
            EntityTypeId = 71;
            IsActive = true;
            IsDeleted = false;
            InitialContacts = new System.Collections.Generic.HashSet<InitialContact>();
            LoanRequests = new System.Collections.Generic.HashSet<LoanRequest>();
            OtpTracings = new System.Collections.Generic.HashSet<OtpTracing>();
            SessionLogs = new System.Collections.Generic.HashSet<SessionLog>();
            SystemEventLogs = new System.Collections.Generic.HashSet<SystemEventLog>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
