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

    // UserSessionLog
    
    public partial class Vortex_UserSessionLog : URF.Core.EF.Trackable.Entity
    {
        public long Id { get; set; } // Id (Primary key)
        public string UserAgent { get; set; } // UserAgent
        public int UserProfileId { get; set; } // UserProfileId
        public short TypeId { get; set; } // TypeId
        public System.DateTime CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsActive { get; set; } // IsActive
        public bool? IsOnCall { get; set; } // IsOnCall
        public string MobileDeviceId { get; set; } // MobileDeviceId (length: 100)
        public string DeviceToken { get; set; } // DeviceToken (length: 100)

        // Reverse navigation

        /// <summary>
        /// Child Vortex_AgentStatusLogs where [AgentStatusLog].[UserSessionId] point to this entity (FK_AgentStatusLog_Vortex.UserSessionLog_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Vortex_AgentStatusLog> Vortex_AgentStatusLogs { get; set; } // AgentStatusLog.FK_AgentStatusLog_Vortex.UserSessionLog_Id
        /// <summary>
        /// Child Vortex_ClientInfoes where [ClientInfo].[UserSessionid] point to this entity (FK_ClientInfo_UserSessionLog_Id)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Vortex_ClientInfo> Vortex_ClientInfoes { get; set; } // ClientInfo.FK_ClientInfo_UserSessionLog_Id

        // Foreign keys

        /// <summary>
        /// Parent UserProfile pointed by [UserSessionLog].([UserProfileId]) (FK_UserSessionLog_UserProfile_Id)
        /// </summary>
        public virtual UserProfile UserProfile { get; set; } // FK_UserSessionLog_UserProfile_Id

        /// <summary>
        /// Parent Vortex_DeviceType pointed by [UserSessionLog].([TypeId]) (FK_UserSessionLog_DeviceType_Id)
        /// </summary>
        public virtual Vortex_DeviceType Vortex_DeviceType { get; set; } // FK_UserSessionLog_DeviceType_Id

        public Vortex_UserSessionLog()
        {
            Vortex_AgentStatusLogs = new System.Collections.Generic.HashSet<Vortex_AgentStatusLog>();
            Vortex_ClientInfoes = new System.Collections.Generic.HashSet<Vortex_ClientInfo>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
