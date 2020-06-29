using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Identity.Models
{
    public partial class Customer
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("contactId")]
        public long? ContactId { get; set; }

        [JsonProperty("userId")]
        public long? UserId { get; set; }

        [JsonProperty("leadSourceId")]
        public object LeadSourceId { get; set; }

        [JsonProperty("leadSourceTypeId")]
        public object LeadSourceTypeId { get; set; }

        [JsonProperty("displayOrder")]
        public long DisplayOrder { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isSystem")]
        public bool IsSystem { get; set; }

        [JsonProperty("modifiedBy")]
        public object ModifiedBy { get; set; }

        [JsonProperty("modifiedOnUtc")]
        public DateTimeOffset? ModifiedOnUtc { get; set; }

        [JsonProperty("createdBy")]
        public object CreatedBy { get; set; }

        [JsonProperty("createdOnUtc")]
        public DateTimeOffset? CreatedOnUtc { get; set; }

        [JsonProperty("tpId")]
        public object TpId { get; set; }

        [JsonProperty("entityTypeId")]
        public long EntityTypeId { get; set; }

        [JsonProperty("firstVisitorId")]
        public object FirstVisitorId { get; set; }

        [JsonProperty("firstSessionId")]
        public object FirstSessionId { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("hearAboutUsId")]
        public object HearAboutUsId { get; set; }

        [JsonProperty("hearAboutUsOther")]
        public object HearAboutUsOther { get; set; }

        [JsonProperty("createdFromId")]
        public object CreatedFromId { get; set; }

        [JsonProperty("adSourceId")]
        public object AdSourceId { get; set; }

        [JsonProperty("remarks")]
        public object Remarks { get; set; }

        [JsonProperty("cusSubscriptionBinders")]
        public List<object> CusSubscriptionBinders { get; set; }

        [JsonProperty("customerTypeBinders")]
        public List<object> CustomerTypeBinders { get; set; }

        [JsonProperty("loanRequests")]
        public List<object> LoanRequests { get; set; }

        [JsonProperty("notifications")]
        public List<object> Notifications { get; set; }

        [JsonProperty("opportunityLeadBinders")]
        public List<object> OpportunityLeadBinders { get; set; }

        [JsonProperty("quoteResults")]
        public List<object> QuoteResults { get; set; }

        [JsonProperty("vendorCustomerBinders")]
        public List<object> VendorCustomerBinders { get; set; }

        [JsonProperty("adsSource")]
        public object AdsSource { get; set; }

        [JsonProperty("businessUnit")]
        public object BusinessUnit { get; set; }

        [JsonProperty("contact")]
        public Contact Contact { get; set; }

        [JsonProperty("entityType")]
        public object EntityType { get; set; }

        [JsonProperty("leadSource")]
        public object LeadSource { get; set; }

        [JsonProperty("leadSourceType")]
        public object LeadSourceType { get; set; }

        [JsonProperty("trackingState")]
        public long TrackingState { get; set; }

        [JsonProperty("modifiedProperties")]
        public object ModifiedProperties { get; set; }

        [JsonProperty("entityIdentifier")]
        public Guid EntityIdentifier { get; set; }
    }
}