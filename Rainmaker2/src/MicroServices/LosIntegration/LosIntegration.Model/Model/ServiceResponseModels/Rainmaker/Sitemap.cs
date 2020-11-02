













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // Sitemap

    public partial class Sitemap 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Title { get; set; } // Title (length: 200)
        public string Description { get; set; } // Description (length: 500)
        public int? UserPermissionId { get; set; } // UserPermissionId
        public string Url { get; set; } // Url (length: 1500)
        public bool IsParent { get; set; } // IsParent
        public bool? IsExecutable { get; set; } // IsExecutable
        public int? ParentId { get; set; } // ParentId
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDeleted { get; set; } // IsDeleted
        public string IconClass { get; set; } // IconClass (length: 100)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsPermissive { get; set; } // IsPermissive

        // Reverse navigation

        /// <summary>
        /// Child Sitemaps where [Sitemap].[ParentId] point to this entity (FK_Sitemap_Sitemap)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Sitemap> Sitemaps { get; set; } // Sitemap.FK_Sitemap_Sitemap

        // Foreign keys

        /// <summary>
        /// Parent Sitemap pointed by [Sitemap].([ParentId]) (FK_Sitemap_Sitemap)
        /// </summary>
        public virtual Sitemap Parent { get; set; } // FK_Sitemap_Sitemap

        /// <summary>
        /// Parent UserPermission pointed by [Sitemap].([UserPermissionId]) (FK_Sitemap_UserPermission)
        /// </summary>
        public virtual UserPermission UserPermission { get; set; } // FK_Sitemap_UserPermission

        public Sitemap()
        {
            IsActive = true;
            EntityTypeId = 133;
            IsDeleted = false;
            DisplayOrder = 0;
            IsPermissive = false;
            Sitemaps = new System.Collections.Generic.HashSet<Sitemap>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
