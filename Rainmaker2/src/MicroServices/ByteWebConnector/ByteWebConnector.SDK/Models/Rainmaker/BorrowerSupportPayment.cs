namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class BorrowerSupportPayment
    {
        public int Id { get; set; }
        public int? BorrowerId { get; set; }
        public int? SupportPaymentTypeId { get; set; }
        public decimal? MonthlyPayment { get; set; }
        public int? RemainingMonth { get; set; }
        public string PaymentTo { get; set; }

        public Borrower Borrower { get; set; }

        public SupportPaymentType SupportPaymentType { get; set; }
    }
}