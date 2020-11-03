













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // WindowsServiceRunLog

    public partial class WindowsServiceRunLog 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? WindowsServiceId { get; set; } // WindowsServiceId
        public System.DateTime? RunOnUtc { get; set; } // RunOnUtc

        // Foreign keys

        /// <summary>
        /// Parent WindowsService pointed by [WindowsServiceRunLog].([WindowsServiceId]) (FK_WindowsServiceRunLog_WindowsService)
        /// </summary>
        public virtual WindowsService WindowsService { get; set; } // FK_WindowsServiceRunLog_WindowsService

        public WindowsServiceRunLog()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
