using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class OtpTracing
    {
        public long Id { get; set; }
        public string Phone { get; set; }
        public string IpAddress { get; set; }
        public DateTime? DateUtc { get; set; }
        public int? TracingTypeId { get; set; }
        public string CodeEntered { get; set; }
        public int? ContactId { get; set; }
        public int? OpportunityId { get; set; }
        public DateTime? OtpCreatedOn { get; set; }
        public string Email { get; set; }
        public DateTime? OtpUpdatedOn { get; set; }
        public string TransactionId { get; set; }
        public int? StatusCode { get; set; }
        public int? SessionLogId { get; set; }
        public int? VisitorId { get; set; }
        public string Message { get; set; }
        public string ResponseJson { get; set; }
        public int? BusinessUnitId { get; set; }
        public string CarrierType { get; set; }
        public string CarrierName { get; set; }

        public Contact Contact { get; set; }

        public Opportunity Opportunity { get; set; }

        public SessionLog SessionLog { get; set; }

        public Visitor Visitor { get; set; }
    }
}