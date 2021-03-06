













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // UserRole

    public partial class UserRole 
    {
        public int Id { get; set; } // Id (Primary key)
        public string RoleName { get; set; } // RoleName (length: 256)
        public string LoweredRoleName { get; set; } // LoweredRoleName (length: 256)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDeleted { get; set; } // IsDeleted
        public bool? IsCustomerRole { get; set; } // IsCustomerRole

        // Reverse navigation

        /// <summary>
        /// Child UserInRoles where [UserInRole].[RoleId] point to this entity (FK_UserInRole_UserRole)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserInRole> UserInRoles { get; set; } // UserInRole.FK_UserInRole_UserRole
        /// <summary>
        /// Child UserPermissionRoleBinders where [UserPermissionRoleBinder].[RoleId] point to this entity (FK_UserPermissionRoleBinder_UserRole)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserPermissionRoleBinder> UserPermissionRoleBinders { get; set; } // UserPermissionRoleBinder.FK_UserPermissionRoleBinder_UserRole

        public UserRole()
        {
            DisplayOrder = 0;
            IsActive = true;
            IsSystem = false;
            EntityTypeId = 136;
            IsDeleted = false;
            UserInRoles = new System.Collections.Generic.HashSet<UserInRole>();
            UserPermissionRoleBinders = new System.Collections.Generic.HashSet<UserPermissionRoleBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
