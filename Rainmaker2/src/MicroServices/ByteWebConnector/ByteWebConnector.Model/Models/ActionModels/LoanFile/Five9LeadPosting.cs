













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // Five9LeadPosting

    public partial class Five9LeadPosting 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? StatusId { get; set; } // StatusId
        public int? NoOfTries { get; set; } // NoOfTries
        public bool IsFailed { get; set; } // IsFailed
        public System.DateTime? PostedOnUtc { get; set; } // PostedOnUtc
        public System.DateTime? LastTriedOnUtc { get; set; } // LastTriedOnUtc
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc

        // Reverse navigation

        /// <summary>
        /// Child Five9LeadPostingLog where [Five9LeadPostingLog].[OpportunityId] point to this entity (FK_Five9LeadPostingLog_Five9LeadPosting)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Five9LeadPostingLog> Five9LeadPostingLog { get; set; } // Five9LeadPostingLog.FK_Five9LeadPostingLog_Five9LeadPosting

        // Foreign keys

        /// <summary>
        /// Parent Opportunity pointed by [Five9LeadPosting].([Id]) (FK_Five9LeadPosting_Opportunity)
        /// </summary>
        public virtual Opportunity Opportunity { get; set; } // FK_Five9LeadPosting_Opportunity

        public Five9LeadPosting()
        {
            Five9LeadPostingLog = new System.Collections.Generic.HashSet<Five9LeadPostingLog>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
