













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // MarksmanQuote

    public partial class MarksmanQuote 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? MarksmanLeadId { get; set; } // MarksmanLeadId
        public string VendorId { get; set; } // VendorId (length: 50)
        public string ProductId { get; set; } // ProductId (length: 50)
        public decimal? Rate { get; set; } // Rate
        public decimal? Price { get; set; } // Price
        public decimal? Apr { get; set; } // Apr
        public string LtProductName { get; set; } // LtProductName (length: 150)
        public decimal? MonthlyPayment { get; set; } // MonthlyPayment

        // Foreign keys

        /// <summary>
        /// Parent MarksmanLead pointed by [MarksmanQuote].([MarksmanLeadId]) (FK_MarksmanQuote_MarksmanLead)
        /// </summary>
        public virtual MarksmanLead MarksmanLead { get; set; } // FK_MarksmanQuote_MarksmanLead

        public MarksmanQuote()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
