// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 2.1
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace RainMaker.Entity.Models
{
    // ActivitySubscriptionBinder

    public partial class ActivitySubscriptionBinder : URF.Core.EF.Trackable.Entity
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
