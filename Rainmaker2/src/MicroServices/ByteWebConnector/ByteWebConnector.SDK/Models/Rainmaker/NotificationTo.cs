using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class NotificationTo
    {
        public int Id { get; set; }
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int? NotificationMediumId { get; set; }
        public DateTime? SeenOnUtc { get; set; }
        public DateTime? VisitOnUtc { get; set; }

        public Notification Notification { get; set; }

        public UserProfile UserProfile { get; set; }
    }
}