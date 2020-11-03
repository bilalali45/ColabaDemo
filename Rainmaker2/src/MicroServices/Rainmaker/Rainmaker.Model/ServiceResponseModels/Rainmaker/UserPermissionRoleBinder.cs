namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class UserPermissionRoleBinder
    {
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public UserPermission UserPermission { get; set; }

        public UserRole UserRole { get; set; }
    }
}