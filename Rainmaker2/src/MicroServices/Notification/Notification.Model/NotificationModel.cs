using System;
using System.Collections.Generic;
using System.Text;

namespace Notification.Model
{
    public class NotificationModel
    {
        public int NotificationType { get; set; }
        public int EntityId { get; set; }
        public string CustomTextJson { get; set; }
    }
}
