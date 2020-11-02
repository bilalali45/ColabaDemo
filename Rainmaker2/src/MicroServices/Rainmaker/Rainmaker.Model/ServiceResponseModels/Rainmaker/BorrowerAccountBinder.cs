namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class BorrowerAccountBinder
    {
        public int BorrowerAccountId { get; set; }
        public int BorrowerId { get; set; }

        public Borrower Borrower { get; set; }

        public BorrowerAccount BorrowerAccount { get; set; }
    }
}