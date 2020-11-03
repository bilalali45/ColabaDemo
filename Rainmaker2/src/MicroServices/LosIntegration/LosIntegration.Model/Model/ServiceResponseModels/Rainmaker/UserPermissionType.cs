













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // UserPermissionType

    public partial class UserPermissionType 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 50)
        public string Description { get; set; } // Description (length: 200)

        // Reverse navigation

        /// <summary>
        /// Child UserPermissions where [UserPermission].[UserPermissionTypeId] point to this entity (FK_UserPermission_UserPermissionType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserPermission> UserPermissions { get; set; } // UserPermission.FK_UserPermission_UserPermissionType

        public UserPermissionType()
        {
            UserPermissions = new System.Collections.Generic.HashSet<UserPermission>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
