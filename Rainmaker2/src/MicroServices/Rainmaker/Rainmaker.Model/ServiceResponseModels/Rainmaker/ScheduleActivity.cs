using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class ScheduleActivity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SchedulerId { get; set; }
        public int SystemActivityId { get; set; }
        public int? QueueStatusId { get; set; }
        public bool IsActive { get; set; }
        public bool StopOnError { get; set; }
        public DateTime? LastStartUtc { get; set; }
        public DateTime? LastEndUtc { get; set; }
        public DateTime? LastSuccessUtc { get; set; }
        public DateTime? NextRunUtc { get; set; }
        public int? NextRunOffset { get; set; }
        public int EntityTypeId { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsSystem { get; set; }

        public ICollection<ScheduleActivityLog> ScheduleActivityLogs { get; set; }

        public Scheduler Scheduler { get; set; }
    }
}