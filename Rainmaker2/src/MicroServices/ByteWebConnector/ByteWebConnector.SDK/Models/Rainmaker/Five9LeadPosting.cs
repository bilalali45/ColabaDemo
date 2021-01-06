using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class Five9LeadPosting
    {
        public int Id { get; set; }
        public int? StatusId { get; set; }
        public int? NoOfTries { get; set; }
        public bool IsFailed { get; set; }
        public DateTime? PostedOnUtc { get; set; }
        public DateTime? LastTriedOnUtc { get; set; }
        public DateTime? CreatedOnUtc { get; set; }

        public ICollection<Five9LeadPostingLog> Five9LeadPostingLog { get; set; }

        public Opportunity Opportunity { get; set; }
    }
}