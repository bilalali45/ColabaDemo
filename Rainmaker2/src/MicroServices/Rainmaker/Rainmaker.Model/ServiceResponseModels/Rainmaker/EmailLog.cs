using System;
using System.Collections.Generic;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class EmailLog
    {
        public int Id { get; set; }
        public string DomainUrl { get; set; }
        public string EmailKey { get; set; }
        public string Subject { get; set; }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public string CcAddress { get; set; }
        public string BccAddress { get; set; }
        public string FilePath { get; set; }
        public DateTime? ExpireDateUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string SmtpServer { get; set; }

        public int EntityTypeId { get; set; }
        //        public int? WorkQueueId { get; set; }

        public ICollection<EmailAttachmentsLog> EmailAttachmentsLogs { get; set; }

        public ICollection<EmailLogBinder> EmailLogBinders { get; set; }

        public ICollection<EmailTracking> EmailTrackings { get; set; }

        //        //public WorkQueue WorkQueue { get; set; }
    }
}