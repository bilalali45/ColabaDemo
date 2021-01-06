using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class CampaignQueue
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public int EntityRefTypeId { get; set; }
        public int EntityRefId { get; set; }

        public int? LoanRequestId { get; set; }

        //        public int? QuoteResultId { get; set; }
        public int? StatusId { get; set; }

        //        public int? WorkQueueFromId { get; set; }
        //        public int? WorkQueueToId { get; set; }
        public DateTime? FromUtc { get; set; }
        public DateTime? ToUtc { get; set; }
        public int? NoOfTries { get; set; }
        public bool? IsCompleted { get; set; }
        public DateTime? CompletedOnUtc { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public DateTime? CreatedOnUtc { get; set; }

        public Campaign Campaign { get; set; }

        public LoanRequest LoanRequest { get; set; }

        //        public QuoteResult QuoteResult { get; set; }
    }
}