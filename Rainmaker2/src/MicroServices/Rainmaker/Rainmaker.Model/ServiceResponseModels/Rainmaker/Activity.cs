













namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{


    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ActivityTypeId { get; set; }
        public int? TemplateId { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsRecurring { get; set; }
        public int? RecurDuration { get; set; }
        public int? RecurCount { get; set; }
        public int ActivityForId { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public System.DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public System.DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public int? EntityRefTypeId { get; set; }
        public bool RequiredSubscription { get; set; }
        public int? BusinessUnitId { get; set; }
        public bool? IsCustomerDefault { get; set; }
        public string Utm { get; set; }
        public int? MinimumInterval { get; set; }






        public System.Collections.Generic.ICollection<ActivitySubscriptionBinder> ActivitySubscriptionBinders { get; set; }



        public System.Collections.Generic.ICollection<CampaignActivityBinder> CampaignActivityBinders { get; set; }



        //        public System.Collections.Generic.ICollection<WorkQueue> WorkQueues { get; set; }






        public ActivityType ActivityType { get; set; }




        public BusinessUnit BusinessUnit { get; set; }




        //public Template Template { get; set; }

      

        
    }

}

