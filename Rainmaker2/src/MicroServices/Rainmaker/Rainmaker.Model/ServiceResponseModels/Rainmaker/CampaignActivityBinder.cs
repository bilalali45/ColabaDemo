namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class CampaignActivityBinder
    {
        public int CampaignId { get; set; }
        public int ActivityId { get; set; }

        public Activity Activity { get; set; }

        public Campaign Campaign { get; set; }
    }
}