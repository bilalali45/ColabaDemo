namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class OtherIncome
    {
        public int Id { get; set; }
        public int? BorrowerId { get; set; }
        public int? IncomeTypeId { get; set; }
        public decimal? MonthlyAmount { get; set; }
        public string Description { get; set; }

        //public Borrower Borrower { get; set; }

        //public IncomeType IncomeType { get; set; }

    }
}