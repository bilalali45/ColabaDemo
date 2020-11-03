













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // BorrowerAccountBinder

    public partial class BorrowerAccountBinder 
    {
        public int BorrowerAccountId { get; set; } // BorrowerAccountId (Primary key)
        public int BorrowerId { get; set; } // BorrowerId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent Borrower pointed by [BorrowerAccountBinder].([BorrowerId]) (FK_BorrowerAccountBinder_Borrower)
        /// </summary>
        public virtual Borrower Borrower { get; set; } // FK_BorrowerAccountBinder_Borrower

        /// <summary>
        /// Parent BorrowerAccount pointed by [BorrowerAccountBinder].([BorrowerAccountId]) (FK_BorrowerAccountBinder_BorrowerAccount)
        /// </summary>
        public virtual BorrowerAccount BorrowerAccount { get; set; } // FK_BorrowerAccountBinder_BorrowerAccount

        public BorrowerAccountBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
