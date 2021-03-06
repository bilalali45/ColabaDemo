













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // UserProfile

    public partial class UserProfile 
    {
        public int Id { get; set; } // Id (Primary key)
        public string UserName { get; set; } // UserName (length: 256)
        public string LoweredUserName { get; set; } // LoweredUserName (length: 256)
        public string MobileAlias { get; set; } // MobileAlias (length: 50)
        public bool IsAnonymous { get; set; } // IsAnonymous
        public System.DateTime LastActivityDateUtc { get; set; } // LastActivityDateUtc
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public int EntityTypeId { get; set; } // EntityTypeId
        public string Password { get; set; } // Password
        public int PasswordFormatId { get; set; } // PasswordFormatId
        public string PasswordSalt { get; set; } // PasswordSalt
        public int? EntityRefTypeId { get; set; } // EntityRefTypeId
        public int? EntityRefId { get; set; } // EntityRefId
        public bool IsDeleted { get; set; } // IsDeleted
        public bool IsSystemAdmin { get; set; } // IsSystemAdmin
        public bool? IsLoggedIn { get; set; } // IsLoggedIn
        public string LastIpAddress { get; set; } // LastIpAddress (length: 200)
        public System.DateTime? LastLoginOnUtc { get; set; } // LastLoginOnUtc
        public System.DateTime? LastLogoutOnUtc { get; set; } // LastLogoutOnUtc
        public int? BusinessUnitId { get; set; } // BusinessUnitId

        // Reverse navigation

        /// <summary>
        /// Child Acls where [Acl].[UserId] point to this entity (FK_Acl_UserProfile)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Acl> Acls { get; set; } // Acl.FK_Acl_UserProfile
        /// <summary>
        /// Child AuditTrails where [AuditTrail].[UserId] point to this entity (FK_AuditTrail_UserProfile)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AuditTrail> AuditTrails { get; set; } // AuditTrail.FK_AuditTrail_UserProfile
        /// <summary>
        /// Child AuthTokens where [AuthToken].[UserProfileId] point to this entity (FK_AuthToken_UserProfile)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AuthToken> AuthTokens { get; set; } // AuthToken.FK_AuthToken_UserProfile
        /// <summary>
        /// Child Customers where [Customer].[UserId] point to this entity (FK_Customer_UserProfile)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Customer> Customers { get; set; } // Customer.FK_Customer_UserProfile
        /// <summary>
        /// Child Employees where [Employee].[UserId] point to this entity (FK_Employee_UserProfile)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Employee> Employees { get; set; } // Employee.FK_Employee_UserProfile
        /// <summary>
        /// Child Notes where [Note].[ModifiedBy] point to this entity (FK_Note_UserProfile)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Note> Notes { get; set; } // Note.FK_Note_UserProfile
        /// <summary>
        /// Child NoteDetails where [NoteDetail].[ModifiedBy] point to this entity (FK_NoteDetail_UserProfile)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<NoteDetail> NoteDetails { get; set; } // NoteDetail.FK_NoteDetail_UserProfile
        /// <summary>
        /// Child NotificationToes where [NotificationTo].[UserId] point to this entity (FK_NotificationTo_UserProfile)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<NotificationTo> NotificationToes { get; set; } // NotificationTo.FK_NotificationTo_UserProfile
        /// <summary>
        /// Child SystemEventLogs where [SystemEventLog].[UserId] point to this entity (FK_SystemEventLog_UserProfile)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<SystemEventLog> SystemEventLogs { get; set; } // SystemEventLog.FK_SystemEventLog_UserProfile
        /// <summary>
        /// Child UserAuthBinders where [UserAuthBinder].[UserProfileId] point to this entity (FK_UserAuthBinder_UserProfile)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserAuthBinder> UserAuthBinders { get; set; } // UserAuthBinder.FK_UserAuthBinder_UserProfile
        /// <summary>
        /// Child UserGridSettings where [UserGridSetting].[UserId] point to this entity (FK_UserGridSetting_UserProfile)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserGridSetting> UserGridSettings { get; set; } // UserGridSetting.FK_UserGridSetting_UserProfile
        /// <summary>
        /// Child UserInRoles where [UserInRole].[UserId] point to this entity (FK_UserInRole_UserProfile)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserInRole> UserInRoles { get; set; } // UserInRole.FK_UserInRole_UserProfile
        /// <summary>
        /// Child UserResetPasswordKeys where [UserResetPasswordKey].[UserId] point to this entity (FK_UserResetPasswordKey_UserProfile)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserResetPasswordKey> UserResetPasswordKeys { get; set; } // UserResetPasswordKey.FK_UserResetPasswordKey_UserProfile
        /// <summary>
        /// Child UserResetPasswordLogs where [UserResetPasswordLog].[UserId] point to this entity (FK_UserResetPasswordLog_UserProfile)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserResetPasswordLog> UserResetPasswordLogs { get; set; } // UserResetPasswordLog.FK_UserResetPasswordLog_UserProfile
      
        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [UserProfile].([BusinessUnitId]) (FK_UserProfile_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_UserProfile_BusinessUnit

        /// <summary>
        /// Parent EntityType pointed by [UserProfile].([EntityRefTypeId]) (FK_UserProfile_EntityRefType)
        /// </summary>
        public virtual EntityType EntityRefType { get; set; } // FK_UserProfile_EntityRefType

        /// <summary>
        /// Parent EntityType pointed by [UserProfile].([EntityTypeId]) (FK_UserProfile_EntityType)
        /// </summary>
        public virtual EntityType EntityType_EntityTypeId { get; set; } // FK_UserProfile_EntityType

        public UserProfile()
        {
            DisplayOrder = 0;
            IsActive = true;
            IsSystem = false;
            EntityTypeId = 151;
            IsDeleted = false;
            IsSystemAdmin = false;
            Acls = new System.Collections.Generic.HashSet<Acl>();
            
            AuditTrails = new System.Collections.Generic.HashSet<AuditTrail>();
            AuthTokens = new System.Collections.Generic.HashSet<AuthToken>();
            Customers = new System.Collections.Generic.HashSet<Customer>();
            Employees = new System.Collections.Generic.HashSet<Employee>();
            Notes = new System.Collections.Generic.HashSet<Note>();
            NoteDetails = new System.Collections.Generic.HashSet<NoteDetail>();
            NotificationToes = new System.Collections.Generic.HashSet<NotificationTo>();
            SystemEventLogs = new System.Collections.Generic.HashSet<SystemEventLog>();
            UserAuthBinders = new System.Collections.Generic.HashSet<UserAuthBinder>();
            UserGridSettings = new System.Collections.Generic.HashSet<UserGridSetting>();
            UserInRoles = new System.Collections.Generic.HashSet<UserInRole>();
            UserResetPasswordKeys = new System.Collections.Generic.HashSet<UserResetPasswordKey>();
            UserResetPasswordLogs = new System.Collections.Generic.HashSet<UserResetPasswordLog>();
            
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
