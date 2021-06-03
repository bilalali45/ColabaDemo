using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;
using Newtonsoft.Json;

namespace LoanApplication.Model
{
    public class EmploymentInfo
    {
        public int? BorrowerId { get; set; }
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
        public EmploymentCategory EmploymentCategory { get; set; }
        public bool? IsCurrentIncome { get; set; }
    }

    public class EmploymentCategory
    {
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDisplayName { get; set; }
    }

    public class EmployerAddressBaseModel
    {
        //[Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int? BorrowerId { get; set; }
        //[Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        public int? IncomeInfoId { get; set; }
        public string StreetAddress { get; set; }
        public string UnitNo { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public string ZipCode { get; set; }
    }

    public class WayOfIncome : WayOfIncomeBase
    {
        public bool? IsPaidByMonthlySalary { get; set; }
        public decimal? HourlyRate { get; set; }
        public int? HoursPerWeek { get; set; }
    }

    public class EmploymentOtherAnnualIncome
    {
        public EmploymentOtherIncomeType IncomeTypeId { get; set; }
        public decimal AnnualIncome { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public class EmploymentDetailWithBorrowerOwnType
    {
        public int? OwnTypeId { get; set; }
        public string DisplayName { get; set; }
        public string Name { get; set; }
    }

    public class EmploymentDetailWithBorrower
    {
        public int BorrowerId { get; set; }
        public string BorrowerName { get; set; }
        public EmploymentDetailWithBorrowerOwnType OwnType { get; set; }
        public List<EmploymentDetailBaseModel> IncomeList { get; set; }
    }

    public class WayOfIncomeBase
    {
        public decimal EmployerAnnualSalary { get; set; }
    }

    public class AddOrUpdatePreviousEmploymentDetailModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [JsonIgnore]
        public string ErrorMessage { get; set; }
        public EmploymentInfo EmploymentInfo { get; set; }
        public EmployerAddressBaseModel EmployerAddress { get; set; }
        public WayOfIncomeBase WayOfIncome { get; set; }
        public string State { get; set; }
    }

    public class EmploymentDetailBaseModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [JsonIgnore]
        public string ErrorMessage { get; set; }
        public EmploymentInfo EmploymentInfo { get; set; }
        public EmployerAddressBaseModel EmployerAddress { get; set; }
        public WayOfIncome WayOfIncome { get; set; }
        public List<EmploymentOtherAnnualIncome> EmploymentOtherIncomes { get; set; }
    }

    public class ServiceResult<T>
    {
        
    }

    public class AddOrUpdateEmploymentDetailModel : EmploymentDetailBaseModel
    {   
        public string State { get; set; }
    }

    public class GetEmploymentDetailModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid income info id")]
        public int IncomeInfoId { get; set; }
    }

    public enum EmploymentOtherIncomeType : byte
    {
        Overtime = 1,
        Bonus = 2,
        Commission = 3,
        MilitaryEntitlements = 4,
        Other = 5
    }

    public class CurrentEmploymentDeleteModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid income info id")]
        public int IncomeInfoId { get; set; }
    }

    
}
