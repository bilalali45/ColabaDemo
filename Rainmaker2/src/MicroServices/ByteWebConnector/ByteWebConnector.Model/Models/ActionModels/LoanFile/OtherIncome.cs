













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // OtherIncome

    public partial class OtherIncome 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? BorrowerId { get; set; } // BorrowerId
        public int? IncomeTypeId { get; set; } // IncomeTypeId
        public decimal? MonthlyAmount { get; set; } // MonthlyAmount
        public string Description { get; set; } // Description (length: 500)

        // Foreign keys

        /// <summary>
        /// Parent Borrower pointed by [OtherIncome].([BorrowerId]) (FK_OtherIncome_Borrower)
        /// </summary>
        public virtual Borrower Borrower { get; set; } // FK_OtherIncome_Borrower

        /// <summary>
        /// Parent IncomeType pointed by [OtherIncome].([IncomeTypeId]) (FK_OtherIncome_IncomeType)
        /// </summary>
        public virtual IncomeType IncomeType { get; set; } // FK_OtherIncome_IncomeType

        public OtherIncome()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
