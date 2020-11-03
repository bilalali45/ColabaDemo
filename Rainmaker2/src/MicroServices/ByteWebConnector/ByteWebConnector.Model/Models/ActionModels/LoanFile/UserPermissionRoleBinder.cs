













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // UserPermissionRoleBinder

    public partial class UserPermissionRoleBinder 
    {
        public int RoleId { get; set; } // RoleId (Primary key)
        public int PermissionId { get; set; } // PermissionId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent UserPermission pointed by [UserPermissionRoleBinder].([PermissionId]) (FK_UserPermissionRoleBinder_UserPermission)
        /// </summary>
        public virtual UserPermission UserPermission { get; set; } // FK_UserPermissionRoleBinder_UserPermission

        /// <summary>
        /// Parent UserRole pointed by [UserPermissionRoleBinder].([RoleId]) (FK_UserPermissionRoleBinder_UserRole)
        /// </summary>
        public virtual UserRole UserRole { get; set; } // FK_UserPermissionRoleBinder_UserRole

        public UserPermissionRoleBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
