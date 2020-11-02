













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // SessionLogDetail

    public partial class SessionLogDetail 
    {
        public int Id { get; set; } // Id (Primary key)
        public System.DateTime? RequestOnUtc { get; set; } // RequestOnUtc
        public string UrlReferrer { get; set; } // UrlReferrer
        public string Url { get; set; } // Url
        public int? VisitorId { get; set; } // VisitorId
        public int? SessionLogId { get; set; } // SessionLogId
        public string Remarks { get; set; } // Remarks
        public int EntityTypeId { get; set; } // EntityTypeId

        // Foreign keys

        /// <summary>
        /// Parent SessionLog pointed by [SessionLogDetail].([SessionLogId]) (FK_SessionLogDetail_SessionLog)
        /// </summary>
        public virtual SessionLog SessionLog { get; set; } // FK_SessionLogDetail_SessionLog

        public SessionLogDetail()
        {
            EntityTypeId = 72;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
