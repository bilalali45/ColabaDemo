













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // AuthProvider

    public partial class AuthProvider 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc

        // Reverse navigation

        /// <summary>
        /// Child AuthApplicationKeys where [AuthApplicationKey].[AuthProviderId] point to this entity (FK_AuthApplicationKey_AuthProvider)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AuthApplicationKey> AuthApplicationKeys { get; set; } // AuthApplicationKey.FK_AuthApplicationKey_AuthProvider
        /// <summary>
        /// Child UserAuthBinders where [UserAuthBinder].[AuthProviderId] point to this entity (FK_UserAuthBinder_AuthProvider)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserAuthBinder> UserAuthBinders { get; set; } // UserAuthBinder.FK_UserAuthBinder_AuthProvider

        public AuthProvider()
        {
            IsSystem = false;
            AuthApplicationKeys = new System.Collections.Generic.HashSet<AuthApplicationKey>();
            UserAuthBinders = new System.Collections.Generic.HashSet<UserAuthBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
