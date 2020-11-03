namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class OpportunityLeadBinder
    {
        public int Id { get; set; }
        public int OpportunityId { get; set; }
        public int CustomerId { get; set; }
        public int OwnTypeId { get; set; }

        public Customer Customer { get; set; }

        public Opportunity Opportunity { get; set; }

        public OwnType OwnType { get; set; }
    }
}