using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class ScheduleActivityLog
    {
        public int Id { get; set; }
        public int ScheduleActivityId { get; set; }
        public DateTime? StartOnUtc { get; set; }
        public DateTime? EndOnUtc { get; set; }
        public bool? IsError { get; set; }
        public string Message { get; set; }

        public ScheduleActivity ScheduleActivity { get; set; }
    }
}