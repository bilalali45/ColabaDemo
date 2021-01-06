using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class Customer
    {
        public int Id { get; set; }
        public int? ContactId { get; set; }
        public int? UserId { get; set; }
        public int? BusinessUnitId { get; set; }
        public int? LeadSourceId { get; set; }
        public int? LeadSourceTypeId { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string TpId { get; set; }
        public int EntityTypeId { get; set; }
        public int? FirstVisitorId { get; set; }
        public int? FirstSessionId { get; set; }
        public bool IsDeleted { get; set; }
        public int? HearAboutUsId { get; set; }
        public string HearAboutUsOther { get; set; }
        public int? CreatedFromId { get; set; }
        public int? AdSourceId { get; set; }
        public string Remarks { get; set; }

        public ICollection<CusSubscriptionBinder> CusSubscriptionBinders { get; set; }

        public ICollection<CustomerTypeBinder> CustomerTypeBinders { get; set; }

        public ICollection<LoanRequest> LoanRequests { get; set; }

        public ICollection<Notification> Notifications { get; set; }

        public ICollection<OpportunityLeadBinder> OpportunityLeadBinders { get; set; }

        //        public System.Collections.Generic.ICollection<QuoteResult> QuoteResults { get; set; }

        public ICollection<VendorCustomerBinder> VendorCustomerBinders { get; set; }

        public AdsSource AdsSource { get; set; }

        public BusinessUnit BusinessUnit { get; set; }

        public Contact Contact { get; set; }

        public EntityType EntityType { get; set; }

        public LeadSource LeadSource { get; set; }

        public LeadSourceType LeadSourceType { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}