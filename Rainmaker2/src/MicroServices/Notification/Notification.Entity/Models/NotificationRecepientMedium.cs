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

    // NotificationRecepientMedium
    
    public partial class NotificationRecepientMedium : URF.Core.EF.Trackable.Entity
    {
        public long Id { get; set; } // Id (Primary key)
        public long? NotificationRecepientId { get; set; } // NotificationRecepientId
        public int? NotificationMediumid { get; set; } // NotificationMediumid

        // Foreign keys

        /// <summary>
        /// Parent NotificationMedium pointed by [NotificationRecepientMedium].([NotificationMediumid]) (FK_NotificationRecepientMedium_NotificationMedium_Id)
        /// </summary>
        public virtual NotificationMedium NotificationMedium { get; set; } // FK_NotificationRecepientMedium_NotificationMedium_Id

        /// <summary>
        /// Parent NotificationRecepient pointed by [NotificationRecepientMedium].([NotificationRecepientId]) (FK_NotificationRecepientMedium_NotificationRecepient_Id)
        /// </summary>
        public virtual NotificationRecepient NotificationRecepient { get; set; } // FK_NotificationRecepientMedium_NotificationRecepient_Id

        public NotificationRecepientMedium()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
