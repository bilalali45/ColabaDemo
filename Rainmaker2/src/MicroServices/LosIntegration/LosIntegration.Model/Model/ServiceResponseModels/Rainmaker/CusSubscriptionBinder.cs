













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // CusSubscriptionBinder

    public partial class CusSubscriptionBinder 
    {
        public int Id { get; set; } // Id (Primary key)
        public int CustomerId { get; set; } // CustomerId
        public int SubscriptionId { get; set; } // SubscriptionId
        public int? SubTypeId { get; set; } // SubTypeId

        // Foreign keys

        /// <summary>
        /// Parent Customer pointed by [CusSubscriptionBinder].([CustomerId]) (FK_CusSubscriptionBinder_Customer)
        /// </summary>
        public virtual Customer Customer { get; set; } // FK_CusSubscriptionBinder_Customer

        /// <summary>
        /// Parent Subscription pointed by [CusSubscriptionBinder].([SubscriptionId]) (FK_CusSubscriptionBinder_Subscription)
        /// </summary>
        public virtual Subscription Subscription { get; set; } // FK_CusSubscriptionBinder_Subscription

        public CusSubscriptionBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
