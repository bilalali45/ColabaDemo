namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class CusSubscriptionBinder
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int SubscriptionId { get; set; }
        public int? SubTypeId { get; set; }

        public Customer Customer { get; set; }

        public Subscription Subscription { get; set; }
    }
}