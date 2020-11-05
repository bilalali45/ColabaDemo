













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // ThirdPartyLead

    public partial class ThirdPartyLead 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? OpportunityId { get; set; } // OpportunityId
        public int? LoanRequestId { get; set; } // LoanRequestId
        public string TrackingNo { get; set; } // TrackingNo (length: 50)
        public string Message { get; set; } // Message
        public int LeadSourceId { get; set; } // LeadSourceId
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string LeadFileName { get; set; } // LeadFileName (length: 256)
        public string AckFileName { get; set; } // AckFileName (length: 256)
        public string MarksmanFileName { get; set; } // MarksmanFileName (length: 256)
        public int? StatusId { get; set; } // StatusId
        public decimal? LeadCost { get; set; } // LeadCost

        // Reverse navigation

        /// <summary>
        /// Child BankRateLeads where [BankRateLead].[ThirdPartyId] point to this entity (FK_BankRateLead_ThirdPartyLead)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateLead> BankRateLeads { get; set; } // BankRateLead.FK_BankRateLead_ThirdPartyLead
        /// <summary>
        /// Child LendingTreeLeads where [LendingTreeLead].[ThirdPartyId] point to this entity (FK_LendingTreeLead_ThirdPartyLead)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LendingTreeLead> LendingTreeLeads { get; set; } // LendingTreeLead.FK_LendingTreeLead_ThirdPartyLead
        /// <summary>
        /// Child ZillowLeads where [ZillowLead].[ThirdPartyId] point to this entity (FK_ZillowLead_ThirdPartyLead)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ZillowLead> ZillowLeads { get; set; } // ZillowLead.FK_ZillowLead_ThirdPartyLead

        // Foreign keys

        /// <summary>
        /// Parent LeadSource pointed by [ThirdPartyLead].([LeadSourceId]) (FK_ThirdPartyLead_LeadSource)
        /// </summary>
        public virtual LeadSource LeadSource { get; set; } // FK_ThirdPartyLead_LeadSource

        /// <summary>
        /// Parent LoanRequest pointed by [ThirdPartyLead].([LoanRequestId]) (FK_ThirdPartyLead_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_ThirdPartyLead_LoanRequest

        /// <summary>
        /// Parent Opportunity pointed by [ThirdPartyLead].([OpportunityId]) (FK_ThirdPartyLead_Opportunity)
        /// </summary>
        public virtual Opportunity Opportunity { get; set; } // FK_ThirdPartyLead_Opportunity

        /// <summary>
        /// Parent ThirdPartyStatusList pointed by [ThirdPartyLead].([StatusId]) (FK_ThirdPartyLead_ThirdPartyStatusList)
        /// </summary>
        public virtual ThirdPartyStatusList ThirdPartyStatusList { get; set; } // FK_ThirdPartyLead_ThirdPartyStatusList

        public ThirdPartyLead()
        {
            BankRateLeads = new System.Collections.Generic.HashSet<BankRateLead>();
            LendingTreeLeads = new System.Collections.Generic.HashSet<LendingTreeLead>();
            ZillowLeads = new System.Collections.Generic.HashSet<ZillowLead>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>