using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LoanApplication.Model
{
    public class RetirementIncomeInfoModel 
    {
        [Required(ErrorMessage = "Loan Application Id Is Required")]
        public int LoanApplicationId { get; set; }
        public int? IncomeInfoId { get; set; }

        [Required(ErrorMessage = "Borrower Id Is Required")]
        public int BorrowerId { get; set; }
        [Required(ErrorMessage = "Income Type Is Required")]
        public int IncomeTypeId { get; set; }
        public decimal MonthlyBaseIncome { get; set; }
        public string EmployerName { get; set; }
        public string Description { get; set; }
        public string State { get; set; }
    }

    public class GetBorrowerIncomesModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
    }

    public class GetBorrowerIncomesOwnTypeDataModel
    {
        public int? OwnTypeId { get; set; }
        public string Name { get; set; }
        public string OwnTypeDisplayName { get; set; }
    }

    public class GetBorrowerIncomesIncomeDataModel
    {
        public int? IncomeInfoId { get; set; }
        public string EmployerName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrentEmployment { get; set; }
        public GetBorrowerIncomesIncomeTypeDataModel IncomeType { get; set; }
        public GetBorrowerIncomeTypeCategoryDataModel EmploymentCategory { get; set; }
    }

    public class GetBorrowerIncomesIncomeTypeDataModel
    {
        public int? IncomeTypeId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public class GetBorrowerIncomeTypeCategoryDataModel
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDisplayName { get; set; }
    }

    public class GetBorrowerIncomesDataModel
    {
        public int LoanApplicationId { get; set; }
        public int BorrowerId { get; set; }
        public string BorrowerName { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string ErrorMessage { get; set; }
        public GetBorrowerIncomesOwnTypeDataModel OwnType { get; set; }
        public List<GetBorrowerIncomesIncomeDataModel> BorrowerIncomes { get; set; }
    }
}
