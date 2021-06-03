using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApplication.Model
{

    public class GetMyMoneyHomeScreenModel
    {
        public decimal? TotalMonthlyQualifyingIncome { get; set; }
        public List<BorrowerIncome> Borrowers { get; set; }
    }

    public class BorrowerIncome
    {
        public string BorrowerName { get; set; }
        public List<Income> Incomes { get; set; }
        public decimal? MonthlyIncome { get; set; }
    }

    public class Income
    {
        public string IncomeName { get; set; }

        public decimal? IncomeValue { get; set; }


    }
}
