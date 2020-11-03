













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // UserPermission

    public partial class UserPermission 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public string Category { get; set; } // Category (length: 50)
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? ObjectTypeId { get; set; } // ObjectTypeId
        public int? UserPermissionTypeId { get; set; } // UserPermissionTypeId
        public bool IsActive { get; set; } // IsActive
        public bool IsDeleted { get; set; } // IsDeleted
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc

        // Reverse navigation

        /// <summary>
        /// Child Sitemaps where [Sitemap].[UserPermissionId] point to this entity (FK_Sitemap_UserPermission)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Sitemap> Sitemaps { get; set; } // Sitemap.FK_Sitemap_UserPermission
        /// <summary>
        /// Child UserPermissionRoleBinders where [UserPermissionRoleBinder].[PermissionId] point to this entity (FK_UserPermissionRoleBinder_UserPermission)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<UserPermissionRoleBinder> UserPermissionRoleBinders { get; set; } // UserPermissionRoleBinder.FK_UserPermissionRoleBinder_UserPermission

        // Foreign keys

        /// <summary>
        /// Parent ObjectType pointed by [UserPermission].([ObjectTypeId]) (FK_UserPermission_ObjectType)
        /// </summary>
        public virtual ObjectType ObjectType { get; set; } // FK_UserPermission_ObjectType

        /// <summary>
        /// Parent UserPermissionType pointed by [UserPermission].([UserPermissionTypeId]) (FK_UserPermission_UserPermissionType)
        /// </summary>
        public virtual UserPermissionType UserPermissionType { get; set; } // FK_UserPermission_UserPermissionType

        public UserPermission()
        {
            EntityTypeId = 114;
            IsActive = true;
            IsDeleted = false;
            IsSystem = false;
            Sitemaps = new System.Collections.Generic.HashSet<Sitemap>();
            UserPermissionRoleBinders = new System.Collections.Generic.HashSet<UserPermissionRoleBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
