using Newtonsoft.Json;
using System;

namespace Identity.Models
{
    public partial class EmployeePhoneBinder
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("employeeId")]
        public long EmployeeId { get; set; }

        [JsonProperty("companyPhoneInfoId")]
        public long CompanyPhoneInfoId { get; set; }

        [JsonProperty("typeId")]
        public long TypeId { get; set; }

        [JsonProperty("companyPhoneInfo")]
        public CompanyPhoneInfo CompanyPhoneInfo { get; set; }

        [JsonProperty("trackingState")]
        public long TrackingState { get; set; }

        [JsonProperty("modifiedProperties")]
        public object ModifiedProperties { get; set; }

        [JsonProperty("entityIdentifier")]
        public Guid EntityIdentifier { get; set; }
    }

 
}
