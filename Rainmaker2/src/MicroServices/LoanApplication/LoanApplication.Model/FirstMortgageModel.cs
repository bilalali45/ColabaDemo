using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApplication.Model
{
   public class FirstMortgageModel
    {
        public int Id { get; set; }
        public decimal? PropertyTax { get; set; }
        public bool? PropertyTaxesIncludeinPayment { get; set; }
        public decimal? HomeOwnerInsurance { get; set; }
        public bool? HomeOwnerInsuranceIncludeinPayment { get; set; }
        public decimal? FloodInsurance { get; set; }
        public bool? FloodInsuranceIncludeinPayment { get; set; }
        public bool? PaidAtClosing { get; set; }
        public int LoanApplicationId { get; set; }
        public decimal? FirstMortgagePayment { get; set; }
        public decimal? UnpaidFirstMortgagePayment { get; set; }
        public decimal? HelocCreditLimit { get; set; }
        public bool? IsHeloc { get; set; }
        public string State { get; set; }

    }
    public class SecondMortgageModel
    {
        public int Id { get; set; }
        public int LoanApplicationId { get; set; }
        public decimal? SecondMortgagePayment { get; set; }
        public decimal? UnpaidSecondMortgagePayment { get; set; }
        public bool? PaidAtClosing { get; set; }
        public decimal? HelocCreditLimit { get; set; }
        public bool? IsHeloc { get; set; }
        public string State { get; set; }

    }
}
