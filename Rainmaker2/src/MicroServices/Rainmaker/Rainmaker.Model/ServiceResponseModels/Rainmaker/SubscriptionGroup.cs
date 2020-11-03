using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class SubscriptionGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? ControlTypeId { get; set; }
        public int DisplayOrder { get; set; }
        public int EntityTypeId { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }
    }
}