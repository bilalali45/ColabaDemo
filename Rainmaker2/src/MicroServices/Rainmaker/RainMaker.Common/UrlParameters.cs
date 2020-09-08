namespace RainMaker.Common
{
    public class UrlParameters
    {
        public string LoanPurpose { get; set; }
        public string LoanGoal { get; set; }
        public decimal? PurchasePrice { get; set; }
        public string PropertyUsage { get; set; }
        public string PropertyType { get; set; }
        public bool? OtherLoan { get; set; }
        public int CreditRate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ExistingQueryString{ get; set; }

        public int? StateId { get; set; }
        public int? CountyId { get; set; }
        public int? CityId { get; set; }
        public string ZipCode { get; set; }
        public string StateName { get; set; }
        public string CountyName { get; set; }
        public string CityName { get; set; }
        public decimal? LoanAmount { get; set; }
        public decimal? FirstMortgageAmount { get; set; }
        public decimal? CashoutAmount { get; set; }

    }
}
