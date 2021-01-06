using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class StatusList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public bool? IsSystemInputOnly { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public int TypeId { get; set; }
        public string CustomerDisplay { get; set; }
        public string EmployeeDisplay { get; set; }
        public int CategoryId { get; set; }
        public bool CanLockOpportunity { get; set; }

        //public System.Collections.Generic.ICollection<LoanApplication> LoanApplications { get; set; }

        //public System.Collections.Generic.ICollection<Opportunity> Opportunities { get; set; }

        //public System.Collections.Generic.ICollection<OpportunityStatusLog> OpportunityStatusLogs { get; set; }

        //public System.Collections.Generic.ICollection<StatusCause> StatusCauses { get; set; }

        //public System.Collections.Generic.ICollection<WorkFlow> WorkFlows_StatusIdFrom { get; set; }

        //public System.Collections.Generic.ICollection<WorkFlow> WorkFlows_StatusIdTo { get; set; }

    }
}