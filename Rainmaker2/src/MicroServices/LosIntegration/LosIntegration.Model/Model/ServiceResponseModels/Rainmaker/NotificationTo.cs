













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // NotificationTo

    public partial class NotificationTo 
    {
        public int Id { get; set; } // Id (Primary key)
        public int NotificationId { get; set; } // NotificationId
        public int UserId { get; set; } // UserId
        public string UserName { get; set; } // UserName (length: 150)
        public int? NotificationMediumId { get; set; } // NotificationMediumId
        public System.DateTime? SeenOnUtc { get; set; } // SeenOnUtc
        public System.DateTime? VisitOnUtc { get; set; } // VisitOnUtc

        // Foreign keys

        /// <summary>
        /// Parent Notification pointed by [NotificationTo].([NotificationId]) (FK_NotificationTo_Notification)
        /// </summary>
        public virtual Notification Notification { get; set; } // FK_NotificationTo_Notification

        /// <summary>
        /// Parent UserProfile pointed by [NotificationTo].([UserId]) (FK_NotificationTo_UserProfile)
        /// </summary>
        public virtual UserProfile UserProfile { get; set; } // FK_NotificationTo_UserProfile

        public NotificationTo()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
