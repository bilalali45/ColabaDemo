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

    // CallLog
    
    public partial class Vortex_CallLog : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id
        public string Sid { get; set; } // Sid (Primary key) (length: 50)
        public System.DateTime? DateCreatedUtc { get; set; } // DateCreatedUtc
        public System.DateTime? DateUpdatedUtc { get; set; } // DateUpdatedUtc
        public string ParentCallSid { get; set; } // ParentCallSid (length: 50)
        public string AccountSid { get; set; } // AccountSid (length: 50)
        public string To { get; set; } // To (length: 50)
        public string ToFormatted { get; set; } // ToFormatted (length: 50)
        public string From { get; set; } // From (length: 50)
        public string FromFormatted { get; set; } // FromFormatted (length: 50)
        public string PhoneNumberSid { get; set; } // PhoneNumberSid (length: 50)
        public string Status { get; set; } // Status (length: 50)
        public System.DateTime? StartTimeUtc { get; set; } // StartTimeUtc
        public System.DateTime? EndTimeUtc { get; set; } // EndTimeUtc
        public string Duration { get; set; } // Duration (length: 50)
        public decimal? Price { get; set; } // Price
        public string PriceUnit { get; set; } // PriceUnit (length: 50)
        public string Direction { get; set; } // Direction (length: 50)
        public string AnsweredBy { get; set; } // AnsweredBy (length: 50)
        public string Annotation { get; set; } // Annotation (length: 50)
        public string ApiVersion { get; set; } // ApiVersion (length: 50)
        public string ForwardedFrom { get; set; } // ForwardedFrom (length: 50)
        public string GroupSid { get; set; } // GroupSid (length: 50)
        public string CallerName { get; set; } // CallerName (length: 100)
        public string Uri { get; set; } // Uri (length: 2048)
        public string SubresourceUrisNotifications { get; set; } // SubresourceUrisNotifications (length: 2048)
        public string SubresourceUrisRecordings { get; set; } // SubresourceUrisRecordings (length: 2048)

        // Reverse navigation

        /// <summary>
        /// Child Vortex_CallLogs where [CallLog].[ParentCallSid] point to this entity (FK_CallLog_CallLog)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Vortex_CallLog> Vortex_CallLogs { get; set; } // CallLog.FK_CallLog_CallLog
        /// <summary>
        /// Child Vortex_Recordings where [Recording].[CallSid] point to this entity (FK_Recording_CallLog)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Vortex_Recording> Vortex_Recordings { get; set; } // Recording.FK_Recording_CallLog

        // Foreign keys

        /// <summary>
        /// Parent Vortex_CallLog pointed by [CallLog].([ParentCallSid]) (FK_CallLog_CallLog)
        /// </summary>
        public virtual Vortex_CallLog ParentCallS { get; set; } // FK_CallLog_CallLog

        public Vortex_CallLog()
        {
            Vortex_CallLogs = new System.Collections.Generic.HashSet<Vortex_CallLog>();
            Vortex_Recordings = new System.Collections.Generic.HashSet<Vortex_Recording>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
