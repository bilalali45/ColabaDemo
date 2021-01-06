using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class Investor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDefault { get; set; }
        public int Priority { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string TpId { get; set; }
        public bool IsDeleted { get; set; }
        public int? MinLockDays { get; set; }
        public int? MaxLockDays { get; set; }
        public string Remarks { get; set; }

        public ICollection<CurrentRate> CurrentRates { get; set; }

        public ICollection<InvestorProduct> InvestorProducts { get; set; }

        public ICollection<RateArchive> RateArchives { get; set; }
    }
}