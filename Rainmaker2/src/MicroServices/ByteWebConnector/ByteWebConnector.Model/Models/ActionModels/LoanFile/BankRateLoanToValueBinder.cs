
















namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // BankRateLoanToValueBinder

    public partial class BankRateLoanToValueBinder 
    {
        public int BankRateProductId { get; set; } // BankRateProductId (Primary key)
        public int LoanToValueId { get; set; } // LoanToValueId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent BankRateProduct pointed by [BankRateLoanToValueBinder].([BankRateProductId]) (FK_BankRateLoanToValueBinder_BankRateProduct)
        /// </summary>
        public virtual BankRateProduct BankRateProduct { get; set; } // FK_BankRateLoanToValueBinder_BankRateProduct

        /// <summary>
        /// Parent LoanToValue pointed by [BankRateLoanToValueBinder].([LoanToValueId]) (FK_BankRateLoanToValueBinder_LoanToValue)
        /// </summary>
        public virtual LoanToValue LoanToValue { get; set; } // FK_BankRateLoanToValueBinder_LoanToValue

        public BankRateLoanToValueBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
