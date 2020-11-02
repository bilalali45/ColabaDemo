













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // ActivitySubscriptionBinder

    public partial class ActivitySubscriptionBinder 
    {
        public int ActivityId { get; set; } // ActivityId (Primary key)
        public int SubscriptionId { get; set; } // SubscriptionId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent Activity pointed by [ActivitySubscriptionBinder].([ActivityId]) (FK_ActivitySubscriptionBinder_Activity)
        /// </summary>
        public virtual Activity Activity { get; set; } // FK_ActivitySubscriptionBinder_Activity

        /// <summary>
        /// Parent Subscription pointed by [ActivitySubscriptionBinder].([SubscriptionId]) (FK_ActivitySubscriptionBinder_Subscription)
        /// </summary>
        public virtual Subscription Subscription { get; set; } // FK_ActivitySubscriptionBinder_Subscription

        public ActivitySubscriptionBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
