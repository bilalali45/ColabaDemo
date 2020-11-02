













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // ServicesSetting

    public partial class ServicesSetting 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 100)
        public string UserId { get; set; } // UserId (length: 250)
        public string Password { get; set; } // Password (length: 250)
        public string WebHook { get; set; } // WebHook (length: 253)
        public bool IsActive { get; set; } // IsActive
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public string AppSid { get; set; } // AppSid (length: 50)
        public string CallbackDomain { get; set; } // CallbackDomain (length: 253)
        public string MessageAppSid { get; set; } // MessageAppSid (length: 50)
        public string VoicePushCredentialSid { get; set; } // VoicePushCredentialSid (length: 50)
        public string ApiServiceKeySid { get; set; } // APIServiceKeySid (length: 50)
        public string ApiServiceKeySecret { get; set; } // APIServiceKeySecret (length: 50)
        public string ChatPushCredentialSid { get; set; } // ChatPushCredentialSid (length: 50)

        // Reverse navigation

        /// <summary>
        /// Child CompanyPhoneInfoes where [CompanyPhoneInfo].[ServiceSettingId] point to this entity (FK_CompanyPhoneInfo_ServicesSetting)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CompanyPhoneInfo> CompanyPhoneInfoes { get; set; } // CompanyPhoneInfo.FK_CompanyPhoneInfo_ServicesSetting

        public ServicesSetting()
        {
            CompanyPhoneInfoes = new System.Collections.Generic.HashSet<CompanyPhoneInfo>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
