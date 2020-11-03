













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // LogItem

    public partial class LogItem 
    {
        public System.Guid EventId { get; set; } // EventId (Primary key)
        public string AppName { get; set; } // AppName (length: 150)
        public System.DateTime? LogDateTimeUtc { get; set; } // LogDateTimeUtc
        public string Source { get; set; } // Source (length: 100)
        public string Message { get; set; } // Message (length: 1000)
        public string Form { get; set; } // Form (length: 4000)
        public string QueryString { get; set; } // QueryString (length: 2000)
        public string TargetSite { get; set; } // TargetSite (length: 300)
        public string StackTrace { get; set; } // StackTrace (length: 4000)
        public string Referer { get; set; } // Referer (length: 250)
        public string Data { get; set; } // Data (length: 500)
        public string Path { get; set; } // Path (length: 300)
        public int EntityTypeId { get; set; } // EntityTypeId

        public LogItem()
        {
            EntityTypeId = 28;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
