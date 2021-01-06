using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LeadSource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public bool? IsSystemInputOnly { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public int? ModifiedBy { get; set; }
        public string LogoFileName { get; set; }
        public string TpId { get; set; }

        public ICollection<AdsSource> AdsSources { get; set; }

        public ICollection<BusinessUnit> BusinessUnits { get; set; }

        public ICollection<Customer> Customers { get; set; }

        public ICollection<LeadSourceDefaultProduct> LeadSourceDefaultProducts { get; set; }

        public ICollection<LeadType> LeadTypes { get; set; }

        public ICollection<LoanRequest> LoanRequests { get; set; }

        public ICollection<Opportunity> Opportunities_LeadSourceId { get; set; }

        public ICollection<Opportunity> Opportunities_LeadSourceOriginalId { get; set; }

        public ICollection<RateServiceParameter> RateServiceParameters { get; set; }

        public ICollection<ThirdPartyLead> ThirdPartyLeads { get; set; }
    }
}