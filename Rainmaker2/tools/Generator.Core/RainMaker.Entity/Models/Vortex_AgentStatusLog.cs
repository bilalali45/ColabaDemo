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

    // AgentStatusLog
    
    public partial class Vortex_AgentStatusLog : URF.Core.EF.Trackable.Entity
    {
        public long Id { get; set; } // Id (Primary key)
        public int StatusId { get; set; } // StatusId
        public System.DateTime TimeStamp { get; set; } // TimeStamp
        public long UserSessionId { get; set; } // UserSessionId

        // Foreign keys

        /// <summary>
        /// Parent Vortex_StatusList pointed by [AgentStatusLog].([StatusId]) (FK_Vortex.LoginStatusLog_Vortex.StatusList)
        /// </summary>
        public virtual Vortex_StatusList Vortex_StatusList { get; set; } // FK_Vortex.LoginStatusLog_Vortex.StatusList

        /// <summary>
        /// Parent Vortex_UserSessionLog pointed by [AgentStatusLog].([UserSessionId]) (FK_AgentStatusLog_Vortex.UserSessionLog_Id)
        /// </summary>
        public virtual Vortex_UserSessionLog Vortex_UserSessionLog { get; set; } // FK_AgentStatusLog_Vortex.UserSessionLog_Id

        public Vortex_AgentStatusLog()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
