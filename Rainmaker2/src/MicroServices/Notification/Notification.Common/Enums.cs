using System;

namespace Notification.Common
{
    public enum NotificationTypeEnum
    {
        DocumentSubmission=1
    }
    public enum DeliveryModeEnum
    {
        Express=1
    }

    public enum NotificationMediumEnum
    {
        InApp=1
    }

    public enum StatusListEnum
    {
        Created=1,
        Delivered=2,
        Unread=3,
        Read=4,
        Deleted=5
    }
}
