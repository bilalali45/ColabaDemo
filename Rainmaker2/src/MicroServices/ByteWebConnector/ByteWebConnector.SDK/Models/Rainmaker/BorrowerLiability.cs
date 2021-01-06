namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class BorrowerLiability
    {
        public int Id { get; set; }
        public int? BorrowerId { get; set; }
        public int? AddressInfoId { get; set; }
        public string CompanyName { get; set; }
        public string AccountNumber { get; set; }
        public int? LiabilityTypeId { get; set; }
        public decimal? MonthlyPayment { get; set; }
        public decimal? Balance { get; set; }
        public bool? WillBePaidByThisLoan { get; set; }
        public int? RemainingMonth { get; set; }

        public AddressInfo AddressInfo { get; set; }

        //public Borrower Borrower { get; set; }

        public LiabilityType LiabilityType { get; set; }

    }
}