













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // OtherEmploymentIncomeHistory

    public partial class OtherEmploymentIncomeHistory 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? OtherEmploymentIncomeId { get; set; } // OtherEmploymentIncomeId
        public int? Year { get; set; } // Year
        public decimal? AnnualIncome { get; set; } // AnnualIncome

        // Foreign keys

        /// <summary>
        /// Parent OtherEmploymentIncome pointed by [OtherEmploymentIncomeHistory].([OtherEmploymentIncomeId]) (FK_OtherEmploymentIncomeHistory_OtherEmploymentIncome)
        /// </summary>
        public virtual OtherEmploymentIncome OtherEmploymentIncome { get; set; } // FK_OtherEmploymentIncomeHistory_OtherEmploymentIncome

        public OtherEmploymentIncomeHistory()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
