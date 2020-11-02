













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // UserAuthBinder

    public partial class UserAuthBinder 
    {
        public int UserProfileId { get; set; } // UserProfileId (Primary key)
        public int AuthProviderId { get; set; } // AuthProviderId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent AuthProvider pointed by [UserAuthBinder].([AuthProviderId]) (FK_UserAuthBinder_AuthProvider)
        /// </summary>
        public virtual AuthProvider AuthProvider { get; set; } // FK_UserAuthBinder_AuthProvider

        /// <summary>
        /// Parent UserProfile pointed by [UserAuthBinder].([UserProfileId]) (FK_UserAuthBinder_UserProfile)
        /// </summary>
        public virtual UserProfile UserProfile { get; set; } // FK_UserAuthBinder_UserProfile

        public UserAuthBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
