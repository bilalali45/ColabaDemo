













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // EmailLogBinder

    public partial class EmailLogBinder 
    {
        public int EmailLogId { get; set; } // EmailLogId (Primary key)
        public int ContactId { get; set; } // ContactId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent Contact pointed by [EmailLogBinder].([ContactId]) (FK_EmailLogBinder_Contact)
        /// </summary>
        public virtual Contact Contact { get; set; } // FK_EmailLogBinder_Contact

        /// <summary>
        /// Parent EmailLog pointed by [EmailLogBinder].([EmailLogId]) (FK_EmailLogBinder_EmailLog)
        /// </summary>
        public virtual EmailLog EmailLog { get; set; } // FK_EmailLogBinder_EmailLog

        public EmailLogBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
