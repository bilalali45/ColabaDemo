namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class OtherEmploymentIncomeHistory
    {
        public int Id { get; set; }
        public int? OtherEmploymentIncomeId { get; set; }
        public int? Year { get; set; }
        public decimal? AnnualIncome { get; set; }

        //public OtherEmploymentIncome OtherEmploymentIncome { get; set; }

    }
}