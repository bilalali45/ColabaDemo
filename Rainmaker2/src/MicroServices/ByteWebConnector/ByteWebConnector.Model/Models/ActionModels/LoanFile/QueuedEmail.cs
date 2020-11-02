













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // QueuedEmail

    public partial class QueuedEmail 
    {
        public int Id { get; set; } // Id (Primary key)
        public int Priority { get; set; } // Priority
        public string FromAddress { get; set; } // FromAddress (length: 500)
        public string FromName { get; set; } // FromName (length: 500)
        public string ToAddress { get; set; } // ToAddress (length: 500)
        public string ToName { get; set; } // ToName (length: 500)
        public string Cc { get; set; } // Cc (length: 500)
        public string Bcc { get; set; } // Bcc (length: 500)
        public string Subject { get; set; } // Subject (length: 1000)
        public string Body { get; set; } // Body (length: 1073741823)
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int SentTries { get; set; } // SentTries
        public System.DateTime? SentOnUtc { get; set; } // SentOnUtc
        public int EmailAccountId { get; set; } // EmailAccountId
        public int EntityTypeId { get; set; } // EntityTypeId

        // Foreign keys

        /// <summary>
        /// Parent EmailAccount pointed by [QueuedEmail].([EmailAccountId]) (FK_QueuedEmail_EmailAccount)
        /// </summary>
        public virtual EmailAccount EmailAccount { get; set; } // FK_QueuedEmail_EmailAccount

        public QueuedEmail()
        {
            EntityTypeId = 67;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
