using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class Subscription
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CustomerDescription { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public int? SectionId { get; set; }
        public int? SubscriptionGroupId { get; set; }
        public int? SubscriptionTypeId { get; set; }

        public ICollection<ActivitySubscriptionBinder> ActivitySubscriptionBinders { get; set; }

        public ICollection<CusSubscriptionBinder> CusSubscriptionBinders { get; set; }

        public SubscriptionGroup SubscriptionGroup { get; set; }

        public SubscriptionSection SubscriptionSection { get; set; }
    }
}