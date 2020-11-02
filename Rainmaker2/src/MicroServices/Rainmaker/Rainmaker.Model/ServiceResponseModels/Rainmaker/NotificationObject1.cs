using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class NotificationObject1
    {
        public int Id { get; set; }
        public int? NotificationTypeId { get; set; }
        public int? EntityId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? Active { get; set; }
        public byte? Status { get; set; }

        public ICollection<NotificationChange> NotificationChanges { get; set; }
    }
}