













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // tempRecording

    public partial class TempRecording 
    {
        public int Id { get; set; } // Id (Primary key)
        public string AccountSid { get; set; } // AccountSid (Primary key) (length: 50)
        public string ApiVersion { get; set; } // ApiVersion (Primary key) (length: 50)
        public string CallSid { get; set; } // CallSid (Primary key) (length: 50)
        public string ConferenceSid { get; set; } // ConferenceSid (length: 50)
        public System.DateTime? DateCreatedUtc { get; set; } // DateCreatedUtc
        public System.DateTime? DateUpdatedUtc { get; set; } // DateUpdatedUtc
        public System.DateTime? StartTimeUtc { get; set; } // StartTimeUtc
        public int Duration { get; set; } // Duration (Primary key)
        public string Sid { get; set; } // Sid (Primary key) (length: 50)
        public decimal? Price { get; set; } // Price
        public string PriceUnit { get; set; } // PriceUnit (Primary key) (length: 50)
        public string Status { get; set; } // Status (Primary key) (length: 50)
        public int? Channels { get; set; } // Channels
        public string Source { get; set; } // Source (Primary key) (length: 50)
        public int? ErrorCode { get; set; } // ErrorCode
        public string Uri { get; set; } // Uri (Primary key) (length: 2048)
        public string EncryptionDetails { get; set; } // EncryptionDetails (length: 50)
        public string SubresourceUrisAddOnResults { get; set; } // SubresourceUrisAddOnResults (Primary key) (length: 2048)
        public string SubresourceUrisTranscriptions { get; set; } // SubresourceUrisTranscriptions (Primary key) (length: 2048)
        public bool RecordingFileDownloaded { get; set; } // RecordingFileDownloaded (Primary key)

        public TempRecording()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
