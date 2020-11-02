













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // UserResetPasswordKey

    public partial class UserResetPasswordKey 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? VisitorId { get; set; } // VisitorId
        public int UserId { get; set; } // UserId
        public string PasswordKey { get; set; } // PasswordKey (length: 500)
        public System.DateTime ExpireOnUtc { get; set; } // ExpireOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime CreatedOnUtc { get; set; } // CreatedOnUtc
        public System.DateTime? ResetOnUtc { get; set; } // ResetOnUtc
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? BusinessUnitId { get; set; } // BusinessUnitId

        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [UserResetPasswordKey].([BusinessUnitId]) (FK_UserResetPasswordKey_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_UserResetPasswordKey_BusinessUnit

        /// <summary>
        /// Parent UserProfile pointed by [UserResetPasswordKey].([UserId]) (FK_UserResetPasswordKey_UserProfile)
        /// </summary>
        public virtual UserProfile UserProfile { get; set; } // FK_UserResetPasswordKey_UserProfile

        public UserResetPasswordKey()
        {
            EntityTypeId = 159;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
