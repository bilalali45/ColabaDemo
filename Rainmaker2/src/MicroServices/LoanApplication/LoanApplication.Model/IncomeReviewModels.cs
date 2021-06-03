using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace LoanApplication.Model
{
    public class IncomeReviewOwnType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public class IncomeReviewIncomeCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public class IncomeReviewIncomeType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IncomeReviewIncomeCategory IncomeCategory { get; set; }
        public List<IncomeReviewList> IncomeList { get; set; }
    }

    public class IncomeInfoForReview
    {
        public string EmployerName { get; set; }
        public string JobTitle { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? YearsInProfession { get; set; }
        public string EmployerPhoneNumber { get; set; }
        public bool? EmployedByFamilyOrParty { get; set; }
        public bool HasOwnershipInterest { get; set; }
        public decimal? OwnershipInterest { get; set; }
        public int? IncomeInfoId { get; set; }
        public bool? IsCurrentIncome { get; set; }
        public EmployerAddressForIncomeReview IncomeAddress { get; set; }
        public WayOfIncomeForReview WayOfIncome { get; set; }
    }

    public class IncomeReviewList
    {
        public IncomeInfoForReview IncomeInfo { get; set; }
        //public EmployerAddressForIncomeReview IncomeAddress { get; set; }
        //public WayOfIncomeForReview WayOfIncome { get; set; }
    }

    public class GetBorrowerWithIncomesForReviewModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string ErrorMessage { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<IncomeReviewBorrowerModel> Borrowers { get; set; }
    }

    public class WayOfIncomeForReview
    {
        public bool? IsPaidByMonthlySalary { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? HourlyRate { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? HoursPerWeek { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? AnnualSalary { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? MonthlySalary { get; set; }
    }

    public class IncomeReviewBorrowerModel
    {
        public int BorrowerId { get; set; }
        public string BorrowerName { get; set; }
        public IncomeReviewOwnType OwnType { get; set; }
        public List<IncomeReviewIncomeType> IncomeTypes { get; set; }
    }

    public class EmployerAddressForIncomeReview
    {
        public string StreetAddress { get; set; }
        public string UnitNo { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public string ZipCode { get; set; }
    }
}
