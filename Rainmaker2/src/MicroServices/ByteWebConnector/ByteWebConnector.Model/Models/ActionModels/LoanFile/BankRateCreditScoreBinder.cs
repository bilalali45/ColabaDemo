
















namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // BankRateCreditScoreBinder

    public partial class BankRateCreditScoreBinder 
    {
        public int BankRateProductId { get; set; } // BankRateProductId (Primary key)
        public int CreditScoreId { get; set; } // CreditScoreId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent BankRateProduct pointed by [BankRateCreditScoreBinder].([BankRateProductId]) (FK_BankRateCreditScoreBinder_BankRateProduct)
        /// </summary>
        public virtual BankRateProduct BankRateProduct { get; set; } // FK_BankRateCreditScoreBinder_BankRateProduct

        /// <summary>
        /// Parent CreditScore pointed by [BankRateCreditScoreBinder].([CreditScoreId]) (FK_BankRateCreditScoreBinder_CreditScore)
        /// </summary>
        public virtual CreditScore CreditScore { get; set; } // FK_BankRateCreditScoreBinder_CreditScore

        public BankRateCreditScoreBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
