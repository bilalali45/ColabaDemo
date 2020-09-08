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
    // WindowsService

    public partial class WindowsService : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 500)
        public string SystemName { get; set; } // SystemName (length: 250)
        public System.DateTime? LastRunOnUtc { get; set; } // LastRunOnUtc
        public int? IntervalSeconds { get; set; } // IntervalSeconds

        // Reverse navigation

        /// <summary>
        /// Child WindowsServiceRunLogs where [WindowsServiceRunLog].[WindowsServiceId] point to this entity (FK_WindowsServiceRunLog_WindowsService)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<WindowsServiceRunLog> WindowsServiceRunLogs { get; set; } // WindowsServiceRunLog.FK_WindowsServiceRunLog_WindowsService

        public WindowsService()
        {
            WindowsServiceRunLogs = new System.Collections.Generic.HashSet<WindowsServiceRunLog>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
