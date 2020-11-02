













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // LoanRequestMergeLog

    public partial class LoanRequestMergeLog 
    {
        public int Id { get; set; } // Id (Primary key)
        public int LoanRequestId { get; set; } // LoanRequestId
        public int OpportunityId { get; set; } // OpportunityId
        public System.DateTime MergeDateUtc { get; set; } // MergeDateUtc

        // Foreign keys

        /// <summary>
        /// Parent LoanRequest pointed by [LoanRequestMergeLog].([LoanRequestId]) (FK_LoanRequestMergeLog_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_LoanRequestMergeLog_LoanRequest

        public LoanRequestMergeLog()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
