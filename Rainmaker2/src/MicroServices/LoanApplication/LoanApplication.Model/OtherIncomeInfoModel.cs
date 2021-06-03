using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LoanApplication.Model
{
    public class OtherIncomeInfoModel
    {
        public int id { get; set; }
        public int? loanId { get; set; }
        public int? employerAddressId { get; set; }
        public string employerName { get; set; }
        public string jobTitle { get; set; }
        public int? borrowerID { get; set; }
        public bool? isSelfEmployed { get; set; }
        public string position { get; set; }
        public int? jobTypeId { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public decimal monthlyBaseIncome { get; set; }
        public int entityTypeId { get; set; }
        public string employeeNumber { get; set; }
        public string employerComment { get; set; }
        public int yearsInProfession { get; set; }
        public int contactPersonId { get; set; }
        public bool IsCurrentjob { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool isHourlyPayment { get; set; }
        public bool isPlanToChangePriorToClosing { get; set; }
        public decimal ownershipPercentage { get; set; }
        public bool isEmplyedByPartyInTransaction { get; set; }
        public int TenantId { get; set; }
        public decimal annualbaseIncome { get; set; }
        public int incomeTypeId { get; set; }
        public decimal hoursRate { get; set; }
        public int hoursPerWeek { get; set; }
        public string description { get; set; }
    }

    public class AddOrUpdateOtherIncomeModel 
    {

      
        public int? IncomeInfoId { get; set; }

        [Required(ErrorMessage ="BorrowerId is required")]
        [Range(1,int.MaxValue,ErrorMessage ="Invalid borrower id")]
        public int BorrowerId { get; set; }

        [Required(ErrorMessage = "Loan application id is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }


        public decimal? MonthlyBaseIncome { get; set; }

        
        public decimal? AnnualBaseIncome { get; set; }

        
        public string Description { get; set; }

        [Required]
        public IncomeTypes IncomeTypeId { get; set; }

        public string State { get; set; }

      
    }
    

   
    

    public class IncomeInfos
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

        public EmployerAddressBaseModels Address { get; set; }
        public List <OtherIncome> OtherIncomes { get; set; }
    }

    public class EmployerAddressBaseModels
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int? BorrowerId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        public int? IncomeInfoId { get; set; }
        public string StreetAddress { get; set; }
        public string UnitNo { get; set; }
        public int? CityId { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public string ZipCode { get; set; }
    }

    public class WayOfIncomes
    {
        public bool GetPaidBySalary { get; set; }
        public decimal? EmployerAnnualSalary { get; set; }
        public decimal? HourlyRate { get; set; }
        public int? HoursPerWeek { get; set; }
    }

    public class OtherIncome
    {
        public int? IncomeTypeId { get; set; }
        public decimal? AnnualIncome { get; set; }
        public decimal? MonthlyIncome { get; set; }
    }

    public class EmployerDetailBaseModels
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        public int? OwnerTypeId { get; set; }
        [JsonIgnore]
        public string ErrorMessage { get; set; }
        public List<IncomeInfos> IncomeInfos { get; set; }
       
        

    }

    public class EmployerDetailModels : EmployerDetailBaseModels
    {
        public string State { get; set; }
    }

    public class EmployerDetailGetModels
    {
        [Range(1, int.MaxValue, ErrorMessage = "Invalid loan application id")]
        public int LoanApplicationId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid borrower id")]
        public int BorrowerId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Invalid income info id")]
        public int IncomeInfoId { get; set; }
    }
    public enum EmployerOtherIncomeTypes : byte
    {
        Overtime = 1,
        Bonus = 2,
        Commission = 3,
        MilitaryEntitlements = 4,
        Other = 5
    }

    public class IncomeInfoSummaryModel
    {
       



    }
    public enum IncomeTypes : int
    {
        EmploymentInfo=1,
        SelfEmployment=2,
        Partnership=3,
        Cooperation=4,
        MilitaryPay=5,
        SocialSecurity=6,
        Pension=7,
        Ira401K=8,
        OtherRetirement=9,
        Rental=10,
        Alimony=11,
        ChildSupport=12,
        SeparateMaintenance=13,
        FosterCare=14,
        Annuity=15,
        CapitalGains=16,
        InterestDividends=17,
        NotesReceivable=18,
        Trust=19,
        HousingOrParsonage=20,
        MortgageCreditCertificate=21,
        MortgageDiąerentialPayments=22,
        PublicAssistance=23,
        UnemploymentBenefits=24,
        VACompensation=25,
        AutomobileAllowance=26,
        BoarderIncome=27,
        RoyaltyPayments=28,
        Disability=29,
        OtherIncomeSource=30




    }
}
