using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class LoanRequestMergeLog
    {
        public int Id { get; set; }
        public int LoanRequestId { get; set; }
        public int OpportunityId { get; set; }
        public DateTime MergeDateUtc { get; set; }

        public LoanRequest LoanRequest { get; set; }
    }
}