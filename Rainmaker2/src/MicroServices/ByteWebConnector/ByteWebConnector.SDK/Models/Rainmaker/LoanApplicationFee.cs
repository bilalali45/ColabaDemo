namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LoanApplicationFee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? LoanApplicationId { get; set; }
        public int? FeeId { get; set; }
        public int? FeeDetailId { get; set; }
        public int? OpportunityFeeId { get; set; }
        public bool? IsCredit { get; set; }
        public decimal? Value { get; set; }
        public int? CreditedById { get; set; }
        public int? PaidById { get; set; }
        public decimal? PaidBeforeClosing { get; set; }
        public decimal? PaidAtClosing { get; set; }

        public Fee Fee { get; set; }

        public FeeDetail FeeDetail { get; set; }

        public LoanApplication LoanApplication { get; set; }

        public OpportunityFee OpportunityFee { get; set; }

        public PaidBy PaidBy { get; set; }
    }
}