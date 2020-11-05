













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // CampaignQueue

    public partial class CampaignQueue 
    {
        public int Id { get; set; } // Id (Primary key)
        public int CampaignId { get; set; } // CampaignId
        public int EntityRefTypeId { get; set; } // EntityRefTypeId
        public int EntityRefId { get; set; } // EntityRefId
        public int? LoanRequestId { get; set; } // LoanRequestId
        public int? QuoteResultId { get; set; } // QuoteResultId
        public int? StatusId { get; set; } // StatusId
        public int? WorkQueueFromId { get; set; } // WorkQueueFromId
        public int? WorkQueueToId { get; set; } // WorkQueueToId
        public System.DateTime? FromUtc { get; set; } // FromUtc
        public System.DateTime? ToUtc { get; set; } // ToUtc
        public int? NoOfTries { get; set; } // NoOfTries
        public bool? IsCompleted { get; set; } // IsCompleted
        public System.DateTime? CompletedOnUtc { get; set; } // CompletedOnUtc
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc

        // Foreign keys

        /// <summary>
        /// Parent Campaign pointed by [CampaignQueue].([CampaignId]) (FK_CampaignQueue_Campaign)
        /// </summary>
        public virtual Campaign Campaign { get; set; } // FK_CampaignQueue_Campaign

        /// <summary>
        /// Parent LoanRequest pointed by [CampaignQueue].([LoanRequestId]) (FK_CampaignQueue_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_CampaignQueue_LoanRequest

        /// <summary>
        /// Parent QuoteResult pointed by [CampaignQueue].([QuoteResultId]) (FK_CampaignQueue_QuoteResult)
        /// </summary>
        public virtual QuoteResult QuoteResult { get; set; } // FK_CampaignQueue_QuoteResult

        public CampaignQueue()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>