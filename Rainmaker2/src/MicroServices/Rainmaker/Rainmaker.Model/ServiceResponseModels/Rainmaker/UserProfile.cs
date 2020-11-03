using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string LoweredUserName { get; set; }
        public string MobileAlias { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime LastActivityDateUtc { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string TpId { get; set; }
        public int EntityTypeId { get; set; }
        public string Password { get; set; }
        public int PasswordFormatId { get; set; }
        public string PasswordSalt { get; set; }
        public int? EntityRefTypeId { get; set; }
        public int? EntityRefId { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSystemAdmin { get; set; }
        public bool? IsLoggedIn { get; set; }
        public string LastIpAddress { get; set; }
        public DateTime? LastLoginOnUtc { get; set; }
        public DateTime? LastLogoutOnUtc { get; set; }
        public int? BusinessUnitId { get; set; }

        //public System.Collections.Generic.ICollection<Acl> Acls { get; set; }

        //public System.Collections.Generic.ICollection<AuditTrail> AuditTrails { get; set; }

        //public System.Collections.Generic.ICollection<AuthToken> AuthTokens { get; set; }

        //public System.Collections.Generic.ICollection<Customer> Customers { get; set; }

        //public System.Collections.Generic.ICollection<Employee> Employees { get; set; }

        //public System.Collections.Generic.ICollection<Note> Notes { get; set; }

        //public System.Collections.Generic.ICollection<NoteDetail> NoteDetails { get; set; }

        //public System.Collections.Generic.ICollection<NotificationTo> NotificationToes { get; set; }

        //public System.Collections.Generic.ICollection<SystemEventLog> SystemEventLogs { get; set; }

        //public System.Collections.Generic.ICollection<UserAuthBinder> UserAuthBinders { get; set; }

        //public System.Collections.Generic.ICollection<UserGridSetting> UserGridSettings { get; set; }

        //public System.Collections.Generic.ICollection<UserInRole> UserInRoles { get; set; }

        //public System.Collections.Generic.ICollection<UserResetPasswordKey> UserResetPasswordKeys { get; set; }

        //public System.Collections.Generic.ICollection<UserResetPasswordLog> UserResetPasswordLogs { get; set; }

        ////public System.Collections.Generic.ICollection<Vortex_ActivityLog> Vortex_ActivityLogs { get; set; }

        ////public System.Collections.Generic.ICollection<Vortex_UserSessionLog> Vortex_UserSessionLogs { get; set; }

        ////public System.Collections.Generic.ICollection<Vortex_UserSetting> Vortex_UserSettings { get; set; }

        //public BusinessUnit BusinessUnit { get; set; }

        //public EntityType EntityRefType { get; set; }

        //public EntityType EntityType_EntityTypeId { get; set; }

    }
}