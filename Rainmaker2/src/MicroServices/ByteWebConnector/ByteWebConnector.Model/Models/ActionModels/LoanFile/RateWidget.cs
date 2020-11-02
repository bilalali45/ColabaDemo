













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // RateWidget

    public partial class RateWidget 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Product { get; set; } // Product (length: 500)
        public decimal? Rate { get; set; } // Rate
        public decimal? Apr { get; set; } // Apr
        public string Code { get; set; } // Code (length: 50)
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string Heading { get; set; } // Heading (length: 500)
        public bool IsActive { get; set; } // IsActive

        public RateWidget()
        {
            IsActive = true;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
