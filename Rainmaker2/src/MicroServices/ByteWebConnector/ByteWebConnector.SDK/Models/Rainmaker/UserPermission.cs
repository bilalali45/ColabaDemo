using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class UserPermission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int EntityTypeId { get; set; }
        public int? ObjectTypeId { get; set; }
        public int? UserPermissionTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }

        public ICollection<Sitemap> Sitemaps { get; set; }

        public ICollection<UserPermissionRoleBinder> UserPermissionRoleBinders { get; set; }

        public ObjectType ObjectType { get; set; }

        public UserPermissionType UserPermissionType { get; set; }
    }
}