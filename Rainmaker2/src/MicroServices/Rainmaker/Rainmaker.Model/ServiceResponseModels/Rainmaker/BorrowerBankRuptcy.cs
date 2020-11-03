namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class BorrowerBankRuptcy
    {
        public int BorrowerId { get; set; }
        public int BankRuptcyId { get; set; }

        public BankRuptcy BankRuptcy { get; set; }

        //public Borrower Borrower { get; set; }

    }
}