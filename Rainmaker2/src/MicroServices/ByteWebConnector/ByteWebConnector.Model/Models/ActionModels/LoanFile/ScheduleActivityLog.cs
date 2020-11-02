













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // ScheduleActivityLog

    public partial class ScheduleActivityLog 
    {
        public int Id { get; set; } // Id (Primary key)
        public int ScheduleActivityId { get; set; } // ScheduleActivityId
        public System.DateTime? StartOnUtc { get; set; } // StartOnUtc
        public System.DateTime? EndOnUtc { get; set; } // EndOnUtc
        public bool? IsError { get; set; } // IsError
        public string Message { get; set; } // Message

        // Foreign keys

        /// <summary>
        /// Parent ScheduleActivity pointed by [ScheduleActivityLog].([ScheduleActivityId]) (FK_ScheduleActivityLog_ScheduleActivity)
        /// </summary>
        public virtual ScheduleActivity ScheduleActivity { get; set; } // FK_ScheduleActivityLog_ScheduleActivity

        public ScheduleActivityLog()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
