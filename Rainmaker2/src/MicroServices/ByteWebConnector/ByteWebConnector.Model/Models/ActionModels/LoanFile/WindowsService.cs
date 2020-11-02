













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // WindowsService

    public partial class WindowsService 
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
