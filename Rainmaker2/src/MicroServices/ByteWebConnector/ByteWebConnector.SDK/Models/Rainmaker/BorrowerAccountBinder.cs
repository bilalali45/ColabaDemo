namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class BorrowerAccountBinder
    {
        public int BorrowerAccountId { get; set; }
        public int BorrowerId { get; set; }

        public Borrower Borrower { get; set; }

        public BorrowerAccount BorrowerAccount { get; set; }
    }
}