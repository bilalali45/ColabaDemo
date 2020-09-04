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

    // NotificationRecepientStatusLog
    
    public partial class NotificationRecepientStatusLog : URF.Core.EF.Trackable.Entity
    {
        public long Id { get; set; } // Id (Primary key)
        public System.DateTime UpdatedOn { get; set; } // UpdatedOn
        public long NotificationRecepientId { get; set; } // NotificationRecepientId
        public byte StatusId { get; set; } // StatusId

        // Foreign keys

        /// <summary>
        /// Parent NotificationRecepient pointed by [NotificationRecepientStatusLog].([NotificationRecepientId]) (FK_NotificationRecepientStatusLog_NotificationRecepient_Id)
        /// </summary>
        public virtual NotificationRecepient NotificationRecepient { get; set; } // FK_NotificationRecepientStatusLog_NotificationRecepient_Id

        /// <summary>
        /// Parent StatusListEnum pointed by [NotificationRecepientStatusLog].([StatusId]) (FK_NotificationRecepientStatusLog_StatusListEnum_Id)
        /// </summary>
        public virtual StatusListEnum StatusListEnum { get; set; } // FK_NotificationRecepientStatusLog_StatusListEnum_Id

        public NotificationRecepientStatusLog()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>