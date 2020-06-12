using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Identity.Models
{
    public partial class UserProfile
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("loweredUserName")]
        public string LoweredUserName { get; set; }

        [JsonProperty("mobileAlias")]
        public object MobileAlias { get; set; }

        [JsonProperty("isAnonymous")]
        public bool IsAnonymous { get; set; }

        [JsonProperty("lastActivityDateUtc")]
        public DateTimeOffset LastActivityDateUtc { get; set; }

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

        [JsonProperty("entityTypeId")]
        public long EntityTypeId { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("passwordFormatId")]
        public long PasswordFormatId { get; set; }

        //[JsonProperty("passwordSalt")]
        //[JsonConverter(typeof(ParseStringConverter))]
        //public long PasswordSalt { get; set; }

        [JsonProperty("entityRefTypeId")]
        public long EntityRefTypeId { get; set; }

        [JsonProperty("entityRefId")]
        public long? EntityRefId { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("isSystemAdmin")]
        public bool IsSystemAdmin { get; set; }

        [JsonProperty("isLoggedIn")]
        public bool? IsLoggedIn { get; set; }

        [JsonProperty("lastIpAddress")]
        public object LastIpAddress { get; set; }

        [JsonProperty("lastLoginOnUtc")]
        public DateTimeOffset? LastLoginOnUtc { get; set; }

        [JsonProperty("lastLogoutOnUtc")]
        public object LastLogoutOnUtc { get; set; }

        [JsonProperty("businessUnitId")]
        public object BusinessUnitId { get; set; }

        [JsonProperty("acls")]
        public List<object> Acls { get; set; }

        [JsonProperty("auditTrails")]
        public List<object> AuditTrails { get; set; }

        [JsonProperty("authTokens")]
        public List<object> AuthTokens { get; set; }

        [JsonProperty("customers")]
        public List<object> Customers { get; set; }

        [JsonProperty("employees")]
        public List<Employee> Employees { get; set; }

        [JsonProperty("notes")]
        public List<object> Notes { get; set; }

        [JsonProperty("noteDetails")]
        public List<object> NoteDetails { get; set; }

        [JsonProperty("notificationToes")]
        public List<object> NotificationToes { get; set; }

        [JsonProperty("systemEventLogs")]
        public List<object> SystemEventLogs { get; set; }

        [JsonProperty("userAuthBinders")]
        public List<object> UserAuthBinders { get; set; }

        [JsonProperty("userGridSettings")]
        public List<object> UserGridSettings { get; set; }

        [JsonProperty("userInRoles")]
        public List<object> UserInRoles { get; set; }

        [JsonProperty("userResetPasswordKeys")]
        public List<object> UserResetPasswordKeys { get; set; }

        [JsonProperty("userResetPasswordLogs")]
        public List<object> UserResetPasswordLogs { get; set; }

        [JsonProperty("vortex_ActivityLogs")]
        public List<object> VortexActivityLogs { get; set; }

        [JsonProperty("vortex_UserSessionLogs")]
        public List<object> VortexUserSessionLogs { get; set; }

        [JsonProperty("vortex_UserSettings")]
        public List<object> VortexUserSettings { get; set; }

        [JsonProperty("businessUnit")]
        public object BusinessUnit { get; set; }

        [JsonProperty("entityRefType")]
        public object EntityRefType { get; set; }

        [JsonProperty("entityType_EntityTypeId")]
        public object EntityTypeEntityTypeId { get; set; }

        [JsonProperty("trackingState")]
        public long TrackingState { get; set; }

        [JsonProperty("modifiedProperties")]
        public object ModifiedProperties { get; set; }

        [JsonProperty("entityIdentifier")]
        public Guid EntityIdentifier { get; set; }
    }

}
