using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class Sitemap
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? UserPermissionId { get; set; }
        public string Url { get; set; }
        public bool IsParent { get; set; }
        public bool? IsExecutable { get; set; }
        public int? ParentId { get; set; }
        public bool IsActive { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public string IconClass { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsPermissive { get; set; }

        public ICollection<Sitemap> Sitemaps { get; set; }

        public Sitemap Parent { get; set; }

        public UserPermission UserPermission { get; set; }
    }
}