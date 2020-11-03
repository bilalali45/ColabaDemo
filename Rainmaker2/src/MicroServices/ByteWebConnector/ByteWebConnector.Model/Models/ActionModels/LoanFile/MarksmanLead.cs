













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // MarksmanLead

    public partial class MarksmanLead 
    {
        public int Id { get; set; } // Id (Primary key)
        public string LeadFileName { get; set; } // LeadFileName (length: 256)
        public string AckFileName { get; set; } // AckFileName (length: 256)
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? ProspectId { get; set; } // ProspectId
        public string ProspectTrackingId { get; set; } // ProspectTrackingId (length: 250)
        public int? ProspectSource { get; set; } // ProspectSource

        // Reverse navigation

        /// <summary>
        /// Parent (One-to-One) MarksmanLead pointed by [MarksmanLeadDetail].[Id] (FK_MarksmanLeadDetail_MarksmanLead)
        /// </summary>
        public virtual MarksmanLeadDetail MarksmanLeadDetail { get; set; } // MarksmanLeadDetail.FK_MarksmanLeadDetail_MarksmanLead
        /// <summary>
        /// Child MarksmanQuotes where [MarksmanQuote].[MarksmanLeadId] point to this entity (FK_MarksmanQuote_MarksmanLead)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MarksmanQuote> MarksmanQuotes { get; set; } // MarksmanQuote.FK_MarksmanQuote_MarksmanLead

        public MarksmanLead()
        {
            MarksmanQuotes = new System.Collections.Generic.HashSet<MarksmanQuote>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
