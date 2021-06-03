using System;
using System.Collections.Generic;
using System.Text;

namespace LoanApplication.Model
{
    public class IncomeInfoViewModel
    {
        public int? IncomeInfoId { get; set; }
        public int? BorrowerId { get; set; }
        public int? IncomeTypeId { get; set; }
        public decimal? MonthlyBaseIncome { get; set; } 
        public string EmployerName { get; set; } 
        public string Description { get; set; }
    }
}
