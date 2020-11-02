using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class NotificationObject
    {
        public int Id { get; set; }
        public int? NotificationTypeId { get; set; }
        public int? EntityId { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? Active { get; set; }
    }
}