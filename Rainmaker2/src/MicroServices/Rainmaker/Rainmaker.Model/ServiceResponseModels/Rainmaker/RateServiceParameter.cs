using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class RateServiceParameter
    {
        public int Id { get; set; }
        public int? BusinessUnitId { get; set; }
        public int? LeadSourceId { get; set; }
        public int? LoanRequestId { get; set; }
        public int? ProductFamilyId { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? ModifiedBy { get; set; }
        public int EntityTypeId { get; set; }

        public int? StringResourceId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public ICollection<CurrentRate> CurrentRates { get; set; }

        public ICollection<RateArchive> RateArchives { get; set; }

        public BusinessUnit BusinessUnit { get; set; }

        public LeadSource LeadSource { get; set; }

        public LoanRequest LoanRequest { get; set; }

        public LocaleStringResource LocaleStringResource { get; set; }

        public ProductFamily ProductFamily { get; set; }
    }
}