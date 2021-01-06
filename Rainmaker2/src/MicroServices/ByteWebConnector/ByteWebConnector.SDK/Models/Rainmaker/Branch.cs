using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
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
        public bool IsHeadOffice { get; set; }
        public int? HeadOfficeId { get; set; }
        public int? EmailAccountId { get; set; }
        public int? TimeZoneId { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CountyId { get; set; }
        public string CityName { get; set; }
        public int? CityId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipPostalCode { get; set; }
        public string NmlsNo { get; set; }
        public string LoanUrl { get; set; }
        public string ScheduleUrl { get; set; }

        public ICollection<Branch> Branches { get; set; }

        public ICollection<BranchEmail> BranchEmails { get; set; }

        public ICollection<BranchPhone> BranchPhones { get; set; }

        public ICollection<BranchPhoneBinder> BranchPhoneBinders { get; set; }

        public ICollection<Employee> Employees { get; set; }

        public ICollection<OfficeMetroBinder> OfficeMetroBinders { get; set; }

        public ICollection<Opportunity> Opportunities { get; set; }

        public ICollection<Setting> Settings { get; set; }

        public Branch HeadOffice { get; set; }

        public City City { get; set; }

        public County County { get; set; }

        public EmailAccount EmailAccount { get; set; }

        public State State { get; set; }

        public TimeZone TimeZone { get; set; }
    }
}