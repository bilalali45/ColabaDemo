namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class MortgageOnProperty
    {
        public int Id { get; set; }
        public int? PropertyOwnId { get; set; }
        public bool? IsFirstMortgage { get; set; }
        public int? SecondLienTypeId { get; set; }
        public decimal? MortgageBalance { get; set; }
        public decimal? MortgageLimit { get; set; }
        public bool PaidAtClosing { get; set; }
        public bool? WasSmTaken { get; set; }
        public string LenderName { get; set; }
        public string AccountNumber { get; set; }
        public decimal? MonthlyPayment { get; set; }
        public decimal? PrepaymentPenalty { get; set; }
        public int? CurrentTermLeft { get; set; }
        public bool IsWithEscrow { get; set; }
        public int? ProductFamilyId { get; set; }
        public int? LoanTypeId { get; set; }
        public int? AmortizationTypeId { get; set; }
        public decimal? Rate { get; set; }
        public int? TermInYears { get; set; }

        //public LoanType LoanType { get; set; }

        //public ProductAmortizationType ProductAmortizationType { get; set; }

        //public ProductFamily ProductFamily { get; set; }

        //public PropertyInfo PropertyInfo { get; set; }

        //public SecondLienType SecondLienType { get; set; }
    }
}