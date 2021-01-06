using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LoanRequestDetail
    {
        public int Id { get; set; }
        public string RequestTpXml { get; set; }
        public string ResponseTpXml { get; set; }
        public bool IsDeleted { get; set; }
        public int EntityTypeId { get; set; }
        public bool? HasResult { get; set; }
        public bool? HasError { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime? RequestStartTimeUtc { get; set; }
        public DateTime? RequestEndTimeUtc { get; set; }
        public int? LoanRequestId { get; set; }
        public int? NoOfProduct { get; set; }
        public string Guid { get; set; }

        public LoanRequest LoanRequest { get; set; }
    }
}