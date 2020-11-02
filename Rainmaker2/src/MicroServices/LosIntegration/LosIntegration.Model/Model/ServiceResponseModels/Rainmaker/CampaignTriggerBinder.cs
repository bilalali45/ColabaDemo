













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // CampaignTriggerBinder

    public partial class CampaignTriggerBinder 
    {
        public int CampaignTriggerId { get; set; } // CampaignTriggerId (Primary key)
        public int CampaignId { get; set; } // CampaignId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent Campaign pointed by [CampaignTriggerBinder].([CampaignId]) (FK_CampaignTriggerBinder_Campaign)
        /// </summary>
        public virtual Campaign Campaign { get; set; } // FK_CampaignTriggerBinder_Campaign

        /// <summary>
        /// Parent CampaignTrigger pointed by [CampaignTriggerBinder].([CampaignTriggerId]) (FK_CampaignTriggerBinder_CampaignTrigger)
        /// </summary>
        public virtual CampaignTrigger CampaignTrigger { get; set; } // FK_CampaignTriggerBinder_CampaignTrigger

        public CampaignTriggerBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
