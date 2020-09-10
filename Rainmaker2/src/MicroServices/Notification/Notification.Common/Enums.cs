namespace Notification.Common
{
    public enum NotificationType
    {
        DocumentSubmission=1
    }
    public enum DeliveryMode
    {
        Express=1,
        Queued=2,
        Off=3
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
        Deleted=5,
        Unseen=6
    }
}
