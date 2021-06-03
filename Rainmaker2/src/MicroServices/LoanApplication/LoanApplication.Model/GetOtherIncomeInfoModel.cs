namespace LoanApplication.Model
{
    public class GetOtherIncomeInfoModel
    {
        public int IncomeInfoId { get; set; }
        public int IncomeTypeId { get; set; }
        public string IncomeTypeName { get; set; }
        public int IncomeGroupId { get; set; }
        public string IncomeGroupName { get; set; }
        public decimal MonthlyBaseIncome { get; set; }
        public decimal AnnualBaseIncome { get; set; }
        public string Description { get; set; }
        public string FieldInfo { get; set; }
    }
}