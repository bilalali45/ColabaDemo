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

    // TenantSettings
    
    public partial class TenantSetting : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int TenantId { get; set; } // TenantId
        public short DeliveryModeId { get; set; } // DeliveryModeId
        public int NotificationMediumId { get; set; } // NotificationMediumId
        public int NotificationTypeId { get; set; } // NotificationTypeId

        // Foreign keys

        /// <summary>
        /// Parent DeliveryModeEnum pointed by [TenantSettings].([DeliveryModeId]) (FK_TenantDeliveryMode_DeliveryModeEnum)
        /// </summary>
        public virtual DeliveryModeEnum DeliveryModeEnum { get; set; } // FK_TenantDeliveryMode_DeliveryModeEnum

        /// <summary>
        /// Parent NotificationMedium pointed by [TenantSettings].([NotificationMediumId]) (FK_TenantSettings_NotificationMedium)
        /// </summary>
        public virtual NotificationMedium NotificationMedium { get; set; } // FK_TenantSettings_NotificationMedium

        /// <summary>
        /// Parent NotificationType pointed by [TenantSettings].([NotificationTypeId]) (FK_TenantDeliveryMode_NotificationType)
        /// </summary>
        public virtual NotificationType NotificationType { get; set; } // FK_TenantDeliveryMode_NotificationType

        public TenantSetting()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
