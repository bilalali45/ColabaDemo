using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class LockStatusList
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
        public int LockTypeId { get; set; }
        public string CustomerDisplay { get; set; }
        public string EmployeeDisplay { get; set; }

        public ICollection<LockStatusCause> LockStatusCauses { get; set; }

        public ICollection<Opportunity> Opportunities { get; set; }

        public ICollection<OpportunityLockStatusLog> OpportunityLockStatusLogs { get; set; }
    }
}