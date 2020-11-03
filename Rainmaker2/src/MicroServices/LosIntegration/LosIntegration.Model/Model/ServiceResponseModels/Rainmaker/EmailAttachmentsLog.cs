













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // EmailAttachmentsLog

    public partial class EmailAttachmentsLog 
    {
        public int Id { get; set; } // Id (Primary key)
        public int EmailLogId { get; set; } // EmailLogId
        public string FilePath { get; set; } // FilePath
        public string FileDisplayName { get; set; } // FileDisplayName (length: 200)
        public string FileType { get; set; } // FileType (length: 10)
        public int? FileSizeKBs { get; set; } // FileSizeKBs
        public int EntityTypeId { get; set; } // EntityTypeId

        // Foreign keys

        /// <summary>
        /// Parent EmailLog pointed by [EmailAttachmentsLog].([EmailLogId]) (FK_EmailAttachmentsLog_EmailLog)
        /// </summary>
        public virtual EmailLog EmailLog { get; set; } // FK_EmailAttachmentsLog_EmailLog

        public EmailAttachmentsLog()
        {
            EntityTypeId = 47;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
