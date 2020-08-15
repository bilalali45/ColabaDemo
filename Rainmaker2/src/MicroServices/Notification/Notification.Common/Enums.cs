using System;

namespace Notification.Common
{
    public class Enums
    {
        public enum NotificationType
        {
            DocumentSubmission=1
        }
        public enum DeliveryMode
        {
            Express=1
        }

        public enum NotificationMedium
        {
            InApp=1
        }

        public enum StatusList
        {
            Created=1,
            Delivered=2,
            Unread=3,
            Read=4,
            Deleted=5
        }
    }
}
