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

    // UserNotificationMedium
    
    public partial class UserNotificationMedium : URF.Core.EF.Trackable.Entity
    {
        public int UserId { get; set; } // UserId (Primary key)
        public int NotificationMediumId { get; set; } // NotificationMediumId (Primary key)
        public int? NotificationTypeId { get; set; } // NotificationTypeId
        public int TenantId { get; set; } // TenantId
        public bool? IsActive { get; set; } // IsActive

        // Foreign keys

        /// <summary>
        /// Parent NotificationMedium pointed by [UserNotificationMedium].([NotificationMediumId]) (FK_UserNotificationMedium_NotificationMedium_Id)
        /// </summary>
        public virtual NotificationMedium NotificationMedium { get; set; } // FK_UserNotificationMedium_NotificationMedium_Id

        /// <summary>
        /// Parent NotificationType pointed by [UserNotificationMedium].([NotificationTypeId]) (FK_UserNotificationMedium_NotificationType_Id)
        /// </summary>
        public virtual NotificationType NotificationType { get; set; } // FK_UserNotificationMedium_NotificationType_Id

        public UserNotificationMedium()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
