













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // BorrowerBankRuptcy

    public partial class BorrowerBankRuptcy 
    {
        public int BorrowerId { get; set; } // BorrowerId (Primary key)
        public int BankRuptcyId { get; set; } // BankRuptcyId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent BankRuptcy pointed by [BorrowerBankRuptcy].([BankRuptcyId]) (FK_BorrowerBankRuptcy_BankRuptcy)
        /// </summary>
        public virtual BankRuptcy BankRuptcy { get; set; } // FK_BorrowerBankRuptcy_BankRuptcy

        /// <summary>
        /// Parent Borrower pointed by [BorrowerBankRuptcy].([BorrowerId]) (FK_BorrowerBankRuptcy_Borrower)
        /// </summary>
        public virtual Borrower Borrower { get; set; } // FK_BorrowerBankRuptcy_Borrower

        public BorrowerBankRuptcy()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
