













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // EmailLogTrackingView

    public partial class EmailLogTrackingView 
    {
        public int Id { get; set; } // Id (Primary key)
        public string EmailKey { get; set; } // EmailKey (length: 150)
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string FromAddress { get; set; } // FromAddress
        public string ToAddress { get; set; } // ToAddress
        public string CcAddress { get; set; } // CcAddress
        public string BccAddress { get; set; } // BccAddress
        public int? WorkQueueId { get; set; } // WorkQueueId
        public int? TrackingType { get; set; } // TrackingType
        public int? EmailLinkTrackingId { get; set; } // EmailLinkTrackingId
        public string TrackingName { get; set; } // TrackingName (length: 200)
        public int? LinkPosition { get; set; } // LinkPosition
        public string Email { get; set; } // Email (length: 150)
        public System.DateTime? DateTimeUtc { get; set; } // DateTimeUtc
        public string IpAddress { get; set; } // IpAddress (length: 150)
        public string RefferUrl { get; set; } // RefferUrl
        public string Reason { get; set; } // Reason (length: 500)
        public string FileName { get; set; } // FileName (length: 255)
        public string Subject { get; set; } // Subject (length: 200)

        public EmailLogTrackingView()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
