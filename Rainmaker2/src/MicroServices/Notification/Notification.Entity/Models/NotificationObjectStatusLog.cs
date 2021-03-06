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

    // NotificationObjectStatusLog
    
    public partial class NotificationObjectStatusLog : URF.Core.EF.Trackable.Entity
    {
        public long Id { get; set; } // Id (Primary key)
        public System.DateTime UpdatedOn { get; set; } // UpdatedOn
        public byte StatusId { get; set; } // StatusId
        public long NotificationObjectId { get; set; } // NotificationObjectId

        // Foreign keys

        /// <summary>
        /// Parent NotificationObject pointed by [NotificationObjectStatusLog].([NotificationObjectId]) (FK_NotificationObjectStatusLog_NotificationObject_Id)
        /// </summary>
        public virtual NotificationObject NotificationObject { get; set; } // FK_NotificationObjectStatusLog_NotificationObject_Id

        /// <summary>
        /// Parent StatusListEnum pointed by [NotificationObjectStatusLog].([StatusId]) (FK_NotificationObjectStatusLog_StatusListEnum_Id)
        /// </summary>
        public virtual StatusListEnum StatusListEnum { get; set; } // FK_NotificationObjectStatusLog_StatusListEnum_Id

        public NotificationObjectStatusLog()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
