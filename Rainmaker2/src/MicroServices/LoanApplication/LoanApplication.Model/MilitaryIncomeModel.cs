using System;

namespace LoanApplication.Model
{
    public class MilitaryIncomeModel //todo reqiured and range is misisng
    {
        public int? LoanApplicationId { get; set; }
        public int? Id { get; set; }
        public int BorrowerId { get; set; }
        public string EmployerName { get; set; }
        public string JobTitle { get; set; }
        public DateTime? StartDate { get; set; }
        public int? YearsInProfession { get; set; }
        public GenericAddressModel Address { get; set; }
        public decimal MonthlyBaseSalary { get; set; }
        public decimal MilitaryEntitlements { get; set; }
        public string State { get; set; }
    }
}