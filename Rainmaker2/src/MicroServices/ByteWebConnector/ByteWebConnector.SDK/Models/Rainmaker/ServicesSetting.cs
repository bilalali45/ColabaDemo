using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class ServicesSetting
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string WebHook { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public string AppSid { get; set; }
        public string CallbackDomain { get; set; }
        public string MessageAppSid { get; set; }
        public string VoicePushCredentialSid { get; set; }
        public string ApiServiceKeySid { get; set; }
        public string ApiServiceKeySecret { get; set; }
        public string ChatPushCredentialSid { get; set; }

        public ICollection<CompanyPhoneInfo> CompanyPhoneInfoes { get; set; }
    }
}