using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class StatusCause
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? StatusId { get; set; }
        public bool IsSystem { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Opportunity> Opportunities { get; set; }

        public ICollection<OpportunityStatusLog> OpportunityStatusLogs { get; set; }

        public StatusList StatusList { get; set; }
    }
}