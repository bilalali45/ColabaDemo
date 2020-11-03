using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class EmailLogTrackingView
    {
        public int Id { get; set; }
        public string EmailKey { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string CcAddress { get; set; }

        public string BccAddress { get; set; }

        //        public int? WorkQueueId { get; set; }
        public int? TrackingType { get; set; }
        public int? EmailLinkTrackingId { get; set; }
        public string TrackingName { get; set; }
        public int? LinkPosition { get; set; }
        public string Email { get; set; }
        public DateTime? DateTimeUtc { get; set; }
        public string IpAddress { get; set; }
        public string RefferUrl { get; set; }
        public string Reason { get; set; }
        public string FileName { get; set; }
        public string Subject { get; set; }
    }
}