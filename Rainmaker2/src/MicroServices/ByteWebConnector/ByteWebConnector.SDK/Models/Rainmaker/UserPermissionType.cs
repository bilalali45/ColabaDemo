using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class UserPermissionType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<UserPermission> UserPermissions { get; set; }
    }
}