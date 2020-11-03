using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class EmailTracking
    {
        public int Id { get; set; }
        public int EmailLogId { get; set; }
        public int? AttachmentId { get; set; }
        public int TrackingType { get; set; }
        public int? EmailLinkTrackingId { get; set; }
        public int? LinkPosition { get; set; }
        public string Recipient { get; set; }
        public string Email { get; set; }
        public string Reason { get; set; }
        public string FileName { get; set; }
        public DateTime DateTimeUtc { get; set; }
        public string IpAddress { get; set; }
        public string RefferUrl { get; set; }
        public int EntityTypeId { get; set; }

        public EmailLinkTracking EmailLinkTracking { get; set; }

        public EmailLog EmailLog { get; set; }
    }
}