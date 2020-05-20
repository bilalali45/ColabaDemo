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
    using System;
    using System.Collections.Generic;

    // SubscriptionSection
    
    public partial class SubscriptionSection : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public int EntityTypeId { get; set; } // EntityTypeId

        // Reverse navigation

        /// <summary>
        /// Child Subscriptions where [Subscription].[SectionId] point to this entity (FK_Subscription_SubscriptionSection)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Subscription> Subscriptions { get; set; } // Subscription.FK_Subscription_SubscriptionSection

        public SubscriptionSection()
        {
            DisplayOrder = 0;
            EntityTypeId = 80;
            Subscriptions = new System.Collections.Generic.HashSet<Subscription>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
