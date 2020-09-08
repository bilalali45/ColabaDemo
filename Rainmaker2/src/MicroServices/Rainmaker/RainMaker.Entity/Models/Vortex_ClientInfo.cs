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
    // ClientInfo

    public partial class Vortex_ClientInfo : URF.Core.EF.Trackable.Entity
    {
        public long Id { get; set; } // Id (Primary key)
        public long UserSessionid { get; set; } // UserSessionid
        public string VortexVersion { get; set; } // VortexVersion (length: 150)
        public string IpAddress { get; set; } // IPAddress (length: 150)
        public string Country { get; set; } // Country (length: 150)
        public string State { get; set; } // State (length: 150)
        public string City { get; set; } // City (length: 150)
        public string NetworkInterfaces { get; set; } // NetworkInterfaces
        public string OsPlatForm { get; set; } // OsPlatForm (length: 150)
        public string OsRelease { get; set; } // OsRelease (length: 50)
        public string OsType { get; set; } // OsType (length: 150)
        public string Arch { get; set; } // Arch (length: 50)
        public string CpUs { get; set; } // CPUs
        public string HomeDir { get; set; } // HomeDir (length: 50)
        public string HostName { get; set; } // HostName (length: 50)
        public string UserInfo { get; set; } // UserInfo
        public string UpTime { get; set; } // UpTime (length: 50)

        // Foreign keys

        /// <summary>
        /// Parent Vortex_UserSessionLog pointed by [ClientInfo].([UserSessionid]) (FK_ClientInfo_UserSessionLog_Id)
        /// </summary>
        public virtual Vortex_UserSessionLog Vortex_UserSessionLog { get; set; } // FK_ClientInfo_UserSessionLog_Id

        public Vortex_ClientInfo()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
