using System.Collections.Generic;

namespace LoanApplication.Model
{
    public class GetAllIncomesForHomeScreenModel
    {
        public decimal? TotalMonthlyQualifyingIncome { get; set; }
        public List<BorrowerIncome> Borrowers { get; set; }

        public class BorrowerIncome
        {
            public int BorrowerId { get; set; }
            public string BorrowerName { get; set; }
            public int? OwnTypeId { get; set; }
            public string OwnTypeName { get; set; }
            public string OwnTypeDisplayName { get; set; }
            public List<Income> Incomes { get; set; }
            public decimal? MonthlyIncome { get; set; }
        }

        public class Income
        {
            public string IncomeName { get; set; }

            public decimal? IncomeValue { get; set; }
            public int IncomeId { get; set; }
            public int IncomeTypeId { get; set; }
            public string IncomeTypeDisplayName { get; set; }
            public EmploymentCategory EmploymentCategory { get; set; }
            public bool? IsCurrentIncome { get; set; }
        }

        public class MyMoneyIncomeCategory
        {
            public int? CategoryId { get; set; }
            public string CategoryName { get; set; }
            public string CategoryDisplayName { get; set; }
        }
    }
}