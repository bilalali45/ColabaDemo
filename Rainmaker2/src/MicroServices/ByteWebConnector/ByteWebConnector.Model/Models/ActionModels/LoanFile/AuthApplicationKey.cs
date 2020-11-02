













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // AuthApplicationKey

    public partial class AuthApplicationKey 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 255)
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public int? AuthProviderId { get; set; } // AuthProviderId
        public string AppId { get; set; } // AppId (length: 255)
        public string AppSecret { get; set; } // AppSecret (length: 255)
        public string RedirectUri { get; set; } // RedirectUri (length: 500)
        public string LoginPageUrl { get; set; } // LoginPageUrl (length: 500)
        public int? TokenExpiryTime { get; set; } // TokenExpiryTime
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc

        // Reverse navigation

        /// <summary>
        /// Child AuthTokens where [AuthToken].[ApplicationKeyId] point to this entity (FK_AuthToken_AuthApplicationKey)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AuthToken> AuthTokens { get; set; } // AuthToken.FK_AuthToken_AuthApplicationKey

        // Foreign keys

        /// <summary>
        /// Parent AuthProvider pointed by [AuthApplicationKey].([AuthProviderId]) (FK_AuthApplicationKey_AuthProvider)
        /// </summary>
        public virtual AuthProvider AuthProvider { get; set; } // FK_AuthApplicationKey_AuthProvider

        /// <summary>
        /// Parent BusinessUnit pointed by [AuthApplicationKey].([BusinessUnitId]) (FK_AuthApplicationKey_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_AuthApplicationKey_BusinessUnit

        public AuthApplicationKey()
        {
            IsSystem = false;
            AuthTokens = new System.Collections.Generic.HashSet<AuthToken>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
