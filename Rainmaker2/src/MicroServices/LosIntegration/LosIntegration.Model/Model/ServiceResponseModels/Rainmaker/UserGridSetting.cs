













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // UserGridSetting

    public partial class UserGridSetting 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? UserId { get; set; } // UserId
        public string GridName { get; set; } // GridName (length: 300)
        public string Setting { get; set; } // Setting (length: 2000)

        // Foreign keys

        /// <summary>
        /// Parent UserProfile pointed by [UserGridSetting].([UserId]) (FK_UserGridSetting_UserProfile)
        /// </summary>
        public virtual UserProfile UserProfile { get; set; } // FK_UserGridSetting_UserProfile

        public UserGridSetting()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
