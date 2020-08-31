using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Identity.Models
{
    public partial class CompanyPhoneInfo
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("serviceName")]
        public string ServiceName { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("entityTypeId")]
        public long EntityTypeId { get; set; }

        [JsonProperty("isDefault")]
        public bool IsDefault { get; set; }

        [JsonProperty("displayOrder")]
        public long DisplayOrder { get; set; }

        [JsonProperty("isActive")]
        public bool IsActive { get; set; }

        [JsonProperty("isSystem")]
        public bool IsSystem { get; set; }

        [JsonProperty("modifiedBy")]
        public long? ModifiedBy { get; set; }

        [JsonProperty("modifiedOnUtc")]
        public DateTimeOffset? ModifiedOnUtc { get; set; }

        [JsonProperty("createdBy")]
        public long? CreatedBy { get; set; }

        [JsonProperty("createdOnUtc")]
        public DateTimeOffset? CreatedOnUtc { get; set; }

        [JsonProperty("tpId")]
        public object TpId { get; set; }

        [JsonProperty("serviceSettingId")]
        public long? ServiceSettingId { get; set; }

        [JsonProperty("type")]
        public object Type { get; set; }

        [JsonProperty("defaultStatusId")]
        public object DefaultStatusId { get; set; }

        [JsonProperty("branchPhones")]
        public List<object> BranchPhones { get; set; }

        [JsonProperty("branchPhoneBinders")]
        public List<object> BranchPhoneBinders { get; set; }

        [JsonProperty("businessUnitPhones")]
        public List<object> BusinessUnitPhones { get; set; }

        [JsonProperty("businessUnitPhoneBinders")]
        public List<object> BusinessUnitPhoneBinders { get; set; }

        [JsonProperty("employeePhoneBinders")]
        public List<object> EmployeePhoneBinders { get; set; }

        [JsonProperty("servicesSetting")]
        public object ServicesSetting { get; set; }

        [JsonProperty("trackingState")]
        public long TrackingState { get; set; }

        [JsonProperty("modifiedProperties")]
        public object ModifiedProperties { get; set; }

        [JsonProperty("entityIdentifier")]
        public Guid EntityIdentifier { get; set; }
    }
}
