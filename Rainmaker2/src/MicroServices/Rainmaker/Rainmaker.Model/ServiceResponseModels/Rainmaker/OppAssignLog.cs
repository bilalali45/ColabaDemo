using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class OppAssignLog
    {
        public int Id { get; set; }
        public int? OpportunityId { get; set; }
        public int? EmployeeId { get; set; }
        public DateTime? DateUtc { get; set; }
        public bool? IsAutoAssigned { get; set; }
        public int? AssignedById { get; set; }
        public bool? IsPicked { get; set; }
        public bool? IsBroadcast { get; set; }
        public bool? IsPitched { get; set; }

        public Employee Employee { get; set; }

        public Opportunity Opportunity { get; set; }
    }
}