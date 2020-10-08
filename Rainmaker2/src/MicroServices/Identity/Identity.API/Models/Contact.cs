using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Identity.Models
{
    public partial class Contact
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("middleName")]
        public object MiddleName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("nickName")]
        public object NickName { get; set; }

        [JsonProperty("prefix")]
        public object Prefix { get; set; }

        [JsonProperty("suffix")]
        public object Suffix { get; set; }

        [JsonProperty("preferred")]
        public string Preferred { get; set; }

        [JsonProperty("company")]
        public object Company { get; set; }

        [JsonProperty("entityTypeId")]
        public long EntityTypeId { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("preferredId")]
        public object PreferredId { get; set; }

        [JsonProperty("ssn")]
        public object Ssn { get; set; }

        [JsonProperty("dobUtc")]
        public object DobUtc { get; set; }

        [JsonProperty("yrsSchool")]
        public object YrsSchool { get; set; }

        [JsonProperty("maritalStatusId")]
        public object MaritalStatusId { get; set; }

        [JsonProperty("gender")]
        public object Gender { get; set; }

        [JsonProperty("ethnicityId")]
        public object EthnicityId { get; set; }

        [JsonProperty("contactAddresses")]
        public List<object> ContactAddresses { get; set; }

        [JsonProperty("contactEmailInfoes")]
        public List<object> ContactEmailInfoes { get; set; }

        [JsonProperty("contactInfoes")]
        public List<object> ContactInfoes { get; set; }

        [JsonProperty("contactPhoneInfoes")]
        public List<object> ContactPhoneInfoes { get; set; }

        [JsonProperty("customers")]
        public List<object> Customers { get; set; }

        [JsonProperty("emailLogBinders")]
        public List<object> EmailLogBinders { get; set; }

        [JsonProperty("employees")]
        public List<object> Employees { get; set; }

        [JsonProperty("followUps")]
        public List<object> FollowUps { get; set; }

        [JsonProperty("loanContacts")]
        public List<object> LoanContacts { get; set; }

        [JsonProperty("vortex_FollowUps")]
        public List<object> VortexFollowUps { get; set; }

        [JsonProperty("trackingState")]
        public long TrackingState { get; set; }

        [JsonProperty("modifiedProperties")]
        public object ModifiedProperties { get; set; }

        [JsonProperty("entityIdentifier")]
        public Guid EntityIdentifier { get; set; }
    }
}