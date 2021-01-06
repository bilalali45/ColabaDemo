namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class CampaignTriggerBinder
    {
        public int CampaignTriggerId { get; set; }
        public int CampaignId { get; set; }

        public Campaign Campaign { get; set; }

        public CampaignTrigger CampaignTrigger { get; set; }
    }
}