using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class Visitor
    {
        public int Id { get; set; }
        public DateTime CreatedDateUtc { get; set; }
        public DateTime? LastVisitedDateUtc { get; set; }
        public decimal? RandomNo { get; set; }
        public string VisitorCode { get; set; }
        public int? FirstSessionId { get; set; }
        public int EntityTypeId { get; set; }
        public int? AdsSourceId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string TpId { get; set; }
        public int? VisitorTypeId { get; set; }

        public ICollection<InitialContact> InitialContacts { get; set; }

        public ICollection<LoanRequest> LoanRequests { get; set; }

        public ICollection<OtpTracing> OtpTracings { get; set; }

        public ICollection<SessionLog> SessionLogs { get; set; }

        public ICollection<SystemEventLog> SystemEventLogs { get; set; }

        public AdsSource AdsSource { get; set; }
    }
}