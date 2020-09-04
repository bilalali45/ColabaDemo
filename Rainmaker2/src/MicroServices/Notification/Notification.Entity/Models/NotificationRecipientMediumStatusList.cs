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


namespace Notification.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // NotificationRecipientMediumStatusList
    
    public partial class NotificationRecipientMediumStatusList : URF.Core.EF.Trackable.Entity
    {
        public long Id { get; set; } // Id (Primary key)
        public System.DateTime UpdatedOn { get; set; } // UpdatedOn
        public long NotificationRecepientMediumId { get; set; } // NotificationRecepientMediumId
        public byte StatusId { get; set; } // StatusId

        // Foreign keys

        /// <summary>
        /// Parent NotificationRecepientMedium pointed by [NotificationRecipientMediumStatusList].([NotificationRecepientMediumId]) (FK_NotificationRecipientMediumStatusList_NotificationRecepientMedium)
        /// </summary>
        public virtual NotificationRecepientMedium NotificationRecepientMedium { get; set; } // FK_NotificationRecipientMediumStatusList_NotificationRecepientMedium

        /// <summary>
        /// Parent StatusListEnum pointed by [NotificationRecipientMediumStatusList].([StatusId]) (FK_NotificationRecipientMediumStatusList_StatusListEnum)
        /// </summary>
        public virtual StatusListEnum StatusListEnum { get; set; } // FK_NotificationRecipientMediumStatusList_StatusListEnum

        public NotificationRecipientMediumStatusList()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>