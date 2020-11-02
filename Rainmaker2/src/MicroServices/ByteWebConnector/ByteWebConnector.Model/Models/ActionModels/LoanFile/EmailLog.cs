













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // EmailLog

    public partial class EmailLog 
    {
        public int Id { get; set; } // Id (Primary key)
        public string DomainUrl { get; set; } // DomainUrl (length: 150)
        public string EmailKey { get; set; } // EmailKey (length: 150)
        public string Subject { get; set; } // Subject (length: 200)
        public string FromAddress { get; set; } // FromAddress
        public string ToAddress { get; set; } // ToAddress
        public string CcAddress { get; set; } // CcAddress
        public string BccAddress { get; set; } // BccAddress
        public string FilePath { get; set; } // FilePath
        public System.DateTime? ExpireDateUtc { get; set; } // ExpireDateUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string SmtpServer { get; set; } // SmtpServer (length: 200)
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? WorkQueueId { get; set; } // WorkQueueId

        // Reverse navigation

        /// <summary>
        /// Child EmailAttachmentsLogs where [EmailAttachmentsLog].[EmailLogId] point to this entity (FK_EmailAttachmentsLog_EmailLog)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmailAttachmentsLog> EmailAttachmentsLogs { get; set; } // EmailAttachmentsLog.FK_EmailAttachmentsLog_EmailLog
        /// <summary>
        /// Child EmailLogBinders where [EmailLogBinder].[EmailLogId] point to this entity (FK_EmailLogBinder_EmailLog)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmailLogBinder> EmailLogBinders { get; set; } // EmailLogBinder.FK_EmailLogBinder_EmailLog
        /// <summary>
        /// Child EmailTrackings where [EmailTracking].[EmailLogId] point to this entity (FK_EmailTracking_EmailLog)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmailTracking> EmailTrackings { get; set; } // EmailTracking.FK_EmailTracking_EmailLog

        // Foreign keys

        /// <summary>
        /// Parent WorkQueue pointed by [EmailLog].([WorkQueueId]) (FK_EmailLog_WorkQueue)
        /// </summary>
        public virtual WorkQueue WorkQueue { get; set; } // FK_EmailLog_WorkQueue

        public EmailLog()
        {
            EntityTypeId = 31;
            EmailAttachmentsLogs = new System.Collections.Generic.HashSet<EmailAttachmentsLog>();
            EmailLogBinders = new System.Collections.Generic.HashSet<EmailLogBinder>();
            EmailTrackings = new System.Collections.Generic.HashSet<EmailTracking>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
