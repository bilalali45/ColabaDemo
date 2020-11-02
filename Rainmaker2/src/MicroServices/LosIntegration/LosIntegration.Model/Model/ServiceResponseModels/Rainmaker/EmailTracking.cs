













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // EmailTracking

    public partial class EmailTracking 
    {
        public int Id { get; set; } // Id (Primary key)
        public int EmailLogId { get; set; } // EmailLogId
        public int? AttachmentId { get; set; } // AttachmentId
        public int TrackingType { get; set; } // TrackingType
        public int? EmailLinkTrackingId { get; set; } // EmailLinkTrackingId
        public int? LinkPosition { get; set; } // LinkPosition
        public string Recipient { get; set; } // Recipient (length: 150)
        public string Email { get; set; } // Email (length: 150)
        public string Reason { get; set; } // Reason (length: 500)
        public string FileName { get; set; } // FileName (length: 255)
        public System.DateTime DateTimeUtc { get; set; } // DateTimeUtc
        public string IpAddress { get; set; } // IpAddress (length: 150)
        public string RefferUrl { get; set; } // RefferUrl
        public int EntityTypeId { get; set; } // EntityTypeId

        // Foreign keys

        /// <summary>
        /// Parent EmailLinkTracking pointed by [EmailTracking].([EmailLinkTrackingId]) (FK_EmailTracking_EmailLinkTracking)
        /// </summary>
        public virtual EmailLinkTracking EmailLinkTracking { get; set; } // FK_EmailTracking_EmailLinkTracking

        /// <summary>
        /// Parent EmailLog pointed by [EmailTracking].([EmailLogId]) (FK_EmailTracking_EmailLog)
        /// </summary>
        public virtual EmailLog EmailLog { get; set; } // FK_EmailTracking_EmailLog

        public EmailTracking()
        {
            EntityTypeId = 54;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
