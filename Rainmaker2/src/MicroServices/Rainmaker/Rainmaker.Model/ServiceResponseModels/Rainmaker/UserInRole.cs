namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class UserInRole
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public UserProfile UserProfile { get; set; }

        public UserRole UserRole { get; set; }
    }
}