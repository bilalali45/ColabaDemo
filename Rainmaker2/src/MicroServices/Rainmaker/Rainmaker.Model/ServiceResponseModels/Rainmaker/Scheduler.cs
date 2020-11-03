using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class Scheduler
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CronText { get; set; }
        public string CronControlsXml { get; set; }
        public TimeSpan? StartTimeUtc { get; set; }
        public TimeSpan? EndTimeUtc { get; set; }
        public int? MinIntervalTime { get; set; }
        public bool? ShowCronBuilder { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Campaign> Campaigns { get; set; }

        public ICollection<ScheduleActivity> ScheduleActivities { get; set; }
    }
}