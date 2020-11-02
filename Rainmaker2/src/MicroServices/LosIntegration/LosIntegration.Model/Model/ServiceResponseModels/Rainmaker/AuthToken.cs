













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // AuthToken

    public partial class AuthToken 
    {
        public int Id { get; set; } // Id (Primary key)
        public int ApplicationKeyId { get; set; } // ApplicationKeyId
        public int UserProfileId { get; set; } // UserProfileId
        public string TpToken { get; set; } // TpToken (length: 500)
        public string Token { get; set; } // Token (length: 500)
        public System.DateTime IssuedOn { get; set; } // IssuedOn
        public System.DateTime ExpireOn { get; set; } // ExpireOn
        public bool IsExpired { get; set; } // IsExpired
        public System.DateTime CreatedOn { get; set; } // CreatedOn
        public int? SessionLogId { get; set; } // SessionLogId

        // Foreign keys

        /// <summary>
        /// Parent AuthApplicationKey pointed by [AuthToken].([ApplicationKeyId]) (FK_AuthToken_AuthApplicationKey)
        /// </summary>
        public virtual AuthApplicationKey AuthApplicationKey { get; set; } // FK_AuthToken_AuthApplicationKey

        /// <summary>
        /// Parent UserProfile pointed by [AuthToken].([UserProfileId]) (FK_AuthToken_UserProfile)
        /// </summary>
        public virtual UserProfile UserProfile { get; set; } // FK_AuthToken_UserProfile

        public AuthToken()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
