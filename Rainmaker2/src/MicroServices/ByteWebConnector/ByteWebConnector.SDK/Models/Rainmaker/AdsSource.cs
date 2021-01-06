using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class AdsSource
    {
        public int Id { get; set; }
        public string SourceNumber { get; set; }
        public string Name { get; set; }
        public DateTime? StartDateUtc { get; set; }
        public DateTime? EndDateUtc { get; set; }
        public bool IsGeoTargeted { get; set; }
        public bool IsNationwide { get; set; }
        public int? LeadSourceTypeId { get; set; }
        public int? LeadSourceId { get; set; }
        public int? AdsPromotionId { get; set; }
        public int? AdsSizeId { get; set; }
        public int? AdsTypeId { get; set; }
        public int? AdsPageLocationId { get; set; }
        public decimal? AdsCost { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<AdsGeoLocation> AdsGeoLocations { get; set; }

        public ICollection<AdsSourceMessage> AdsSourceMessages { get; set; }

        public ICollection<Customer> Customers { get; set; }

        public ICollection<LoanRequest> LoanRequests { get; set; }

        public ICollection<Opportunity> Opportunities { get; set; }

        public ICollection<SessionLog> SessionLogs { get; set; }

        public ICollection<Visitor> Visitors { get; set; }

        public AdsPageLocation AdsPageLocation { get; set; }

        public AdsPromotion AdsPromotion { get; set; }

        public AdsSize AdsSize { get; set; }

        public AdsType AdsType { get; set; }

        public LeadSource LeadSource { get; set; }

        public LeadSourceType LeadSourceType { get; set; }
    }
}