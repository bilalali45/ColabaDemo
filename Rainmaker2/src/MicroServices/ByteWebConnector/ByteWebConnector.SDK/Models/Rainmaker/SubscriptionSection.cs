using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class SubscriptionSection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DisplayOrder { get; set; }
        public int EntityTypeId { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }
    }
}