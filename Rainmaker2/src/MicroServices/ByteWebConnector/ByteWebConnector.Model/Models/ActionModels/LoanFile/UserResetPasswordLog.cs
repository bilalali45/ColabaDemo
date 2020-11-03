













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // UserResetPasswordLog

    public partial class UserResetPasswordLog 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? VisitorId { get; set; } // VisitorId
        public int UserId { get; set; } // UserId
        public string Password { get; set; } // Password
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime CreatedOnUtc { get; set; } // CreatedOnUtc
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public int ChangeType { get; set; } // ChangeType
        public int? PasswordFormatId { get; set; } // PasswordFormatId
        public string PasswordSalt { get; set; } // PasswordSalt

        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [UserResetPasswordLog].([BusinessUnitId]) (FK_UserResetPasswordLog_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_UserResetPasswordLog_BusinessUnit

        /// <summary>
        /// Parent UserProfile pointed by [UserResetPasswordLog].([UserId]) (FK_UserResetPasswordLog_UserProfile)
        /// </summary>
        public virtual UserProfile UserProfile { get; set; } // FK_UserResetPasswordLog_UserProfile

        public UserResetPasswordLog()
        {
            EntityTypeId = 230;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
