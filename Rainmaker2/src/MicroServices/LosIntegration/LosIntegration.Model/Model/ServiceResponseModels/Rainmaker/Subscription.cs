
















namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // Subscription

    public partial class Subscription 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public string CustomerDescription { get; set; } // CustomerDescription (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted
        public int? SectionId { get; set; } // SectionId
        public int? SubscriptionGroupId { get; set; } // SubscriptionGroupId
        public int? SubscriptionTypeId { get; set; } // SubscriptionTypeId

        // Reverse navigation

        /// <summary>
        /// Child ActivitySubscriptionBinders where [ActivitySubscriptionBinder].[SubscriptionId] point to this entity (FK_ActivitySubscriptionBinder_Subscription)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ActivitySubscriptionBinder> ActivitySubscriptionBinders { get; set; } // ActivitySubscriptionBinder.FK_ActivitySubscriptionBinder_Subscription
        /// <summary>
        /// Child CusSubscriptionBinders where [CusSubscriptionBinder].[SubscriptionId] point to this entity (FK_CusSubscriptionBinder_Subscription)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CusSubscriptionBinder> CusSubscriptionBinders { get; set; } // CusSubscriptionBinder.FK_CusSubscriptionBinder_Subscription

        // Foreign keys

        /// <summary>
        /// Parent SubscriptionGroup pointed by [Subscription].([SubscriptionGroupId]) (FK_Subscription_SubscriptionGroup)
        /// </summary>
        public virtual SubscriptionGroup SubscriptionGroup { get; set; } // FK_Subscription_SubscriptionGroup

        /// <summary>
        /// Parent SubscriptionSection pointed by [Subscription].([SectionId]) (FK_Subscription_SubscriptionSection)
        /// </summary>
        public virtual SubscriptionSection SubscriptionSection { get; set; } // FK_Subscription_SubscriptionSection

        public Subscription()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 59;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            ActivitySubscriptionBinders = new System.Collections.Generic.HashSet<ActivitySubscriptionBinder>();
            CusSubscriptionBinders = new System.Collections.Generic.HashSet<CusSubscriptionBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
