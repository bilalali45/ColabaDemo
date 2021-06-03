using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace LoanApplication.Model
{
    public class EmploymentInfoBaseModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        public string EmployerName { get; set; }
        public  string JobTitle { get; set; }
        public DateTime? StartDate { get; set; }
        public int YearsInProfession { get; set; }
        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}", ErrorMessage ="Phone number is not valid")]
        public string EmployerPhoneNumber { get; set; }
        public bool EmployedByFamilyOrParty { get; set; }
        public decimal? OwnershipInterest { get; set; }
        public int? IncomeInfoId { get; set; }
        public string State { get; set; }
    }

    public class EmploymentInfoGetModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
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
        
    }

    

    public class EmployerAddressModel : EmployerAddressBaseModel
    {
        public string State { get; set; }
    }

    public class EmployerAnnualSalaryModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid income info id")]
        public int IncomeInfoId { get; set; }
        public decimal? AnnualBaseSalary { get; set; }
        public decimal? MonthlyBaseSalary { get; set; }
        public string State { get; set; }
    }

    public class EmployerSalaryGetModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid income info id")]
        public int IncomeInfoId { get; set; }
        public decimal? AnnualBaseSalary { get; set; }
        public decimal? MonthlyBaseSalary { get; set; }
        public bool? IsHourlyBase { get; set; }
        public decimal? HourlyRate { get; set; }
        public int? HoursPerWeek { get; set; }
    }

    public class EmployerHourlyModel
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid income info id")]
        public int IncomeInfoId { get; set; }
        public decimal? HourlyRate { get; set; }
        public int? HoursPerWeek { get; set; }
        public string State { get; set; }
    }

    public class EmploymentOtherIncome
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public class AddOrUpdateEmployerOtherIncome
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        public int? IncomeInfoId { get; set; }
        public List<EmploymentOtherAnnualIncome> EmployerOtherIncomes { get; set; }
        public string State { get; set; }
    }

    public class BorrowerEmploymentHistory
    {
        public int BorrowerId { get; set; }
        public string BorrowerName { get; set; }
        public List<EmploymentHistory> EmploymentHistory { get; set; }
    }

    public class EmploymentHistory
    {
        public int IncomeInfoId { get; set; }
        public string EmployerName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsCurrentEmployment { get; set; }
    }

    public class EmploymentHistoryModel
    {
        public int LoanApplicationId { get; set; }
        public bool RequiresHistory { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string ErrorMessage { get; set; }
        public List<BorrowerEmploymentHistory> BorrowerEmploymentHistory { get; set; }
    }

    public enum EmployerOtherIncomeType : byte
    {
        Overtime = 1,
        Bonus = 2,
        Commission = 3,
        MilitaryEntitlements = 4,
        Other = 5
    }

    public enum BorrowerIncomeTypes : int
    {
        EmploymentInfo = 1,
        SelfEmployment = 2,
        Partnership = 3,
        Cooperation = 4,
        MilitaryPay = 5,
        SocialSecurity = 6,
        Pension = 7,
        Ira401K = 8,
        Other = 9,
        Rental = 10,
        Alimony = 11,
        ChildSupport = 12,
        SeparateMaintenance = 13,
        FosterCare = 14,
        Annuity = 15,
        CapitalGains = 16,
        InterestDividends = 17,
        NotesReceivable = 18,
        Trust = 19,
        HousingOrParsonage = 20,
        MortgageCreditCertificate = 21,
        MortgageDiaerentialPayment = 22,
        PublicAssistance = 23,
        UnemploymentBenefits = 24,
        VaCompensation = 25,
        BoarderIncome = 27,
        RoyaltyPayments = 28,
        Disability = 29,
        OtherIncomeSource = 30
    }

    public enum BorrowerIncomeCategory : int
    {
        Employment = 1,
        SelfEmployment = 2,
        Business = 3,
        MilitaryPay = 4,
        Retirement = 5,
        Rental = 6,
        Other = 7
    }
    
}
