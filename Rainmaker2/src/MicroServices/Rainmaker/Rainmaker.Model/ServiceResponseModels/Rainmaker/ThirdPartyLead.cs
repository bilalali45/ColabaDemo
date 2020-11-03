using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class ThirdPartyLead
    {
        public int Id { get; set; }
        public int? OpportunityId { get; set; }
        public int? LoanRequestId { get; set; }
        public string TrackingNo { get; set; }
        public string Message { get; set; }
        public int LeadSourceId { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string LeadFileName { get; set; }
        public string AckFileName { get; set; }
        public string MarksmanFileName { get; set; }
        public int? StatusId { get; set; }
        public decimal? LeadCost { get; set; }

        //public System.Collections.Generic.ICollection<BankRateLead> BankRateLeads { get; set; }

        public ICollection<LendingTreeLead> LendingTreeLeads { get; set; }

        public ICollection<ZillowLead> ZillowLeads { get; set; }

        public LeadSource LeadSource { get; set; }

        public LoanRequest LoanRequest { get; set; }

        public Opportunity Opportunity { get; set; }

        public ThirdPartyStatusList ThirdPartyStatusList { get; set; }
    }
}