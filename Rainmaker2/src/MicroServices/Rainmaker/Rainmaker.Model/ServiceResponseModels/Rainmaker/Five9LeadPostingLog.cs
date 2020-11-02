using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class Five9LeadPostingLog
    {
        public int Id { get; set; }
        public int OpportunityId { get; set; }
        public string Error { get; set; }
        public DateTime TriedOnUtc { get; set; }

        public Five9LeadPosting Five9LeadPosting { get; set; }
    }
}