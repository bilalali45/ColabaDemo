using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class SessionLog
    {
        public int Id { get; set; }
        public string SessionId { get; set; }
        public DateTime? StartOnUtc { get; set; }
        public DateTime? EndOnUtc { get; set; }
        public DateTime? LastSeenOnUtc { get; set; }
        public string UrlReferrer { get; set; }
        public string IpAddress { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public int? VisitorId { get; set; }
        public string Remarks { get; set; }
        public string Url { get; set; }
        public int? AdsSourceId { get; set; }
        public int EntityTypeId { get; set; }
        public int? BusinessUnitId { get; set; }
        public int? UserId { get; set; }
        public int? ApplicationId { get; set; }

        public ICollection<OtpTracing> OtpTracings { get; set; }

        public ICollection<SessionLogDetail> SessionLogDetails { get; set; }

        public AdsSource AdsSource { get; set; }

        public Visitor Visitor { get; set; }
    }
}