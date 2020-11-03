













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // UserInRole

    public partial class UserInRole 
    {
        public int UserId { get; set; } // UserId (Primary key)
        public int RoleId { get; set; } // RoleId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent UserProfile pointed by [UserInRole].([UserId]) (FK_UserInRole_UserProfile)
        /// </summary>
        public virtual UserProfile UserProfile { get; set; } // FK_UserInRole_UserProfile

        /// <summary>
        /// Parent UserRole pointed by [UserInRole].([RoleId]) (FK_UserInRole_UserRole)
        /// </summary>
        public virtual UserRole UserRole { get; set; } // FK_UserInRole_UserRole

        public UserInRole()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
