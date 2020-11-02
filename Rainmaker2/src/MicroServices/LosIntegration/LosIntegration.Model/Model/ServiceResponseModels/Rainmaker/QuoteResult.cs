













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // QuoteResult

    public partial class QuoteResult 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? VisitorId { get; set; } // VisitorId
        public int? LoanRequestId { get; set; } // LoanRequestId
        public int? CustomerId { get; set; } // CustomerId
        public int? EmployeId { get; set; } // EmployeId
        public int? CreatedFromId { get; set; } // CreatedFromId
        public string RequestXml { get; set; } // RequestXml (length: 255)
        public string FinalXml { get; set; } // FinalXml (length: 255)
        public bool? HasResult { get; set; } // HasResult
        public bool? HasError { get; set; } // HasError
        public string ErrorMessage { get; set; } // ErrorMessage (length: 1000)
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? BenchMarkRateId { get; set; } // BenchMarkRateId
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public int? OriginalQuoteId { get; set; } // OriginalQuoteId
        public System.DateTime? OriginalQuoteOnUtc { get; set; } // OriginalQuoteOnUtc
        public string Guid { get; set; } // Guid (length: 50)

        // Reverse navigation

        /// <summary>
        /// Child CampaignQueues where [CampaignQueue].[QuoteResultId] point to this entity (FK_CampaignQueue_QuoteResult)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CampaignQueue> CampaignQueues { get; set; } // CampaignQueue.FK_CampaignQueue_QuoteResult
        /// <summary>
        /// Child WorkQueues where [WorkQueue].[QuoteResultId] point to this entity (FK_WorkQueue_QuoteResult)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<WorkQueue> WorkQueues { get; set; } // WorkQueue.FK_WorkQueue_QuoteResult

        // Foreign keys

        /// <summary>
        /// Parent BenchMarkRate pointed by [QuoteResult].([BenchMarkRateId]) (FK_QuoteResult_BenchMarkRate)
        /// </summary>
        public virtual BenchMarkRate BenchMarkRate { get; set; } // FK_QuoteResult_BenchMarkRate

        /// <summary>
        /// Parent Customer pointed by [QuoteResult].([CustomerId]) (FK_QuoteResult_Customer)
        /// </summary>
        public virtual Customer Customer { get; set; } // FK_QuoteResult_Customer

        /// <summary>
        /// Parent Employee pointed by [QuoteResult].([EmployeId]) (FK_QuoteResult_Employee)
        /// </summary>
        public virtual Employee Employee { get; set; } // FK_QuoteResult_Employee

        /// <summary>
        /// Parent LoanRequest pointed by [QuoteResult].([LoanRequestId]) (FK_QuoteResult_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_QuoteResult_LoanRequest

        public QuoteResult()
        {
            EntityTypeId = 160;
            CampaignQueues = new System.Collections.Generic.HashSet<CampaignQueue>();
            WorkQueues = new System.Collections.Generic.HashSet<WorkQueue>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
