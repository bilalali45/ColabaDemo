













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // CampaignActivityBinder

    public partial class CampaignActivityBinder 
    {
        public int CampaignId { get; set; } // CampaignId (Primary key)
        public int ActivityId { get; set; } // ActivityId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent Activity pointed by [CampaignActivityBinder].([ActivityId]) (FK_CampaignActivityBinder_Activity)
        /// </summary>
        public virtual Activity Activity { get; set; } // FK_CampaignActivityBinder_Activity

        /// <summary>
        /// Parent Campaign pointed by [CampaignActivityBinder].([CampaignId]) (FK_CampaignActivityBinder_Campaign)
        /// </summary>
        public virtual Campaign Campaign { get; set; } // FK_CampaignActivityBinder_Campaign

        public CampaignActivityBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
