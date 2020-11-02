













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // Five9LeadPostingLog

    public partial class Five9LeadPostingLog 
    {
        public int Id { get; set; } // Id (Primary key)
        public int OpportunityId { get; set; } // OpportunityId
        public string Error { get; set; } // Error (length: 4000)
        public System.DateTime TriedOnUtc { get; set; } // TriedOnUtc

        // Foreign keys

        /// <summary>
        /// Parent Five9LeadPosting pointed by [Five9LeadPostingLog].([OpportunityId]) (FK_Five9LeadPostingLog_Five9LeadPosting)
        /// </summary>
        public virtual Five9LeadPosting Five9LeadPosting { get; set; } // FK_Five9LeadPostingLog_Five9LeadPosting

        public Five9LeadPostingLog()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
