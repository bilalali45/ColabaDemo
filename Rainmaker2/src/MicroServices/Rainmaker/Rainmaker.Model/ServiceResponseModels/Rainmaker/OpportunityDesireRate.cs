namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class OpportunityDesireRate
    {
        public int Id { get; set; }
        public int? OpportunityId { get; set; }
        public int? ProductTypeId { get; set; }
        public decimal? Rate { get; set; }

        public Opportunity Opportunity { get; set; }

        public ProductType ProductType { get; set; }
    }
}