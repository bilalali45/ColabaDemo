using System;
using System.Collections.Generic;
using RainMaker.Entity.Models;

namespace RainMaker.Common
{
    public class QueryTemplate
    {
        public class RecentLoanRequest
        {
            public int? OpportunityId { get; set; }
            public string LoanPurpose { get; set; }
            public string State { get; set; }
            public decimal LoanAmount { get; set; }
            public string Stage { get; set; }

        }

        public class OpportunityNotification
        {
            public int Id { get; set; }
            public DateTime? AssignedOnUtc { get; set; }
        }

        public class ZipCodeStateCityPopUp
        {
            public int? StateId { get; set; }
            public int? CountyId { get; set; }
            public int? CityId { get; set; }
            public string StateName { get; set; }
            public string CountyName { get; set; }
            public string CityName { get; set; }
            public bool? Isactive { get; set; }
            public string Abbreviation { get; set; }

        }

        public class QueryDropDown
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsActive { get; set; }
        }

        public class ThirdPartyLeadViewModel
        {
            public int Id { get; set; }
            public int LeadSourceId { get; set; }
            public string Message { get; set; }
            public Nullable<System.DateTime> DateTimeUtc { get; set; }
            public string Xml { get; set; }
            public Nullable<int> OpportunityId { get; set; }
            public string LeadSourceName { get; set; }

        }

        public class StatusChart
        {
            public string LeadSource { get; set; }
            public string StatusName { get; set; }
            public int OpportunityCount { get; set; }

        }

        public class PickedAndNotPickedOpportunities
        {
            public int? NotPicked { get; set; }
            public int? Picked { get; set; }
            public int OwnerId { get; set; }
            public string Employee { get; set; }
        }

        public class OpportunityAssignment
        {
            public int? Assigned { get; set; }
            public bool? IsPickedByOwner { get; set; }
        }

        public class BlacListIPs
        {
            public long IpFrom { get; set; }
            public long IpTo { get; set; }
            public bool IsAllow { get; set; }
        }

        public class CustomerSearch
        {
            // ReSharper disable once InconsistentNaming
            public string label { get; set; }
            public string Ids { get; set; }
            public string StateName { get; set; }

        }

        public class LocationSearch
        {
            public int? StateId { get; set; }
            public string StateName { get; set; }
            public int? CityId { get; set; }
            public string CityName { get; set; }
            public int? CountyId { get; set; }
            public string CountyName { get; set; }
            public string ZipPostalCode { get; set; }
            public string Abbreviation { get; set; }
            public string Heading { get; set; }
            public string Description { get; set; }

        }

        public class LoanApplicationExport
        {
            public int LoanId { get; set; }
            public int OpportunityId { get; set; }
            public DateTime? LADate { get; set; }
            public int LoanPurpose { get; set; }
            public string EncompassLoanNumber { get; set; }
            public string EncompassGUID { get; set; }
            public decimal? LoanAmount { get; set; }
            public decimal? CashOutAmount { get; set; }
            public int? PropertyTypeId { get; set; }
            public int? PropertyUsageId { get; set; }
            public decimal? OriginalPurchasePrice { get; set; }
            public decimal? HomeDueAmount { get; set; }
            public DateTime? DateAcquired { get; set; }

            public int? NoOfUnits { get; set; }
            public decimal? TotalMonthlyPayment { get; set; }
            public decimal? PropertyValue { get; set; }
            public decimal? DownPayment { get; set; }
            public decimal? DownPaymentPrecent { get; set; }
            public string ExportLoanNumber { get; set; }
            public decimal? LifeInsuranceEstimatedMonthlyAmount { get; set; }
            public string FirstName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public string Suffix { get; set; }
            public DateTime? BDateofBirth { get; set; }

            public int? GenderId { get; set; }
            public string HomePhone { get; set; }
            public int? NoOfDependent { get; set; }
            public string DependentAge { get; set; }
            public string WorkPhone { get; set; }

            public string CellPhone { get; set; }
            public string EmailAddress { get; set; }
            public int? ResidencyTypeId { get; set; }
            public int? ResidencyStateId { get; set; }
            public int? MaritalStatusId { get; set; }
            //public int? OwnershipTypeId { get; set; }
            //public decimal? MonthlyRent { get; set; }
            //public string AccountInNameOf { get; set; }
            //public DateTime? FromDate { get; set; }
            //public DateTime? ToDate { get; set; }
            //public bool? IsSameAsPropertyAddress { get; set; }


            public string PAddressName { get; set; }
            public string PAddressSateName { get; set; }
            public int? PAddressSateId { get; set; }
            public string PAddressCountyName { get; set; }
            public int? PAddressCountyId { get; set; }
            public string PAddressCityName { get; set; }
            public int? PAddressCityId { get; set; }
            public string PAddressStreet { get; set; }
            public string PAddressZipCode { get; set; }
            public string PAddressUnitNo { get; set; }


            //public string BResAddressName { get; set; }
            //public string BResAddressStateName { get; set; }
            //public string BResAddressCountyName { get; set; }
            //public string BResAddressCityName { get; set; }
            //public string BResAddressStreet { get; set; }
            //public string BResAddressZipCode { get; set; }
            //public string BResAddressUnitNo { get; set; }

            public decimal? FirstMOPMortgageBalance { get; set; }
            public decimal? FirstMOPMonthlyPayment { get; set; }
            public decimal? FirstMortgageLimit { get; set; }
            public decimal? SecondMOPMortgageBalance { get; set; }
            public decimal? SecondMOPMonthlyPayment { get; set; }
            public decimal? SecondMortgageLimit { get; set; }

            //public int? BRAAccountTypeId { get; set; }
            //public string BRAAccountTitle { get; set; }
            //public string BRAAccountNumber { get; set; }
            //public decimal? BRABalance { get; set; }
            //public DateTime? BalanceDate { get; set; }
            //public int? BRAIsJoinType { get; set; }
            public string LoanForeclosureOrJudgementIndicator { get; set; }
            public string BankruptcyIndicator { get; set; }
            public string PropertyForeclosedPastSevenYearsIndicator { get; set; }
            public string PartyToLawsuitIndicator { get; set; }
            public string OutstandingJudgementsIndicator { get; set; }
            public string PresentlyDelinquentIndicator { get; set; }
            public string BorrowedDownPaymentIndicator { get; set; }
            public string CoMakerEndorserOfNoteIndicator { get; set; }
            public string DeclarationsJIndicator { get; set; }
            public string DeclarationsKIndicator { get; set; }
            public string IntentToOccupyIndicator { get; set; }
            public string HomeownerPastThreeYearsIndicator { get; set; }
            public string AlimonyChildSupportObligationIndicator { get; set; }
            public string PriorPropertyUsageType { get; set; }
            public string PriorPropertyTitleType { get; set; }

            //public string AlimonyName { get; set; }
            //public decimal? AlimonyMonthlyPayment { get; set; }
            //public decimal? AlimonyBalance { get; set; }
            //public int? AlimonyMonth { get; set; }
            //public int? AlimonyTypeId { get; set; }
            public decimal? MortgageToBeSubordinate { get; set; }
            public int? AmortizationTypeId { get; set; }
            public int? MortgageTypeId { get; set; }

            public string InverviewerName { get; set; }
            public string NmlsLoanOriginatorId { get; set; }
            public string InterviewerPhoneNumber { get; set; }
            public string InterviewerEmail { get; set; }
            public string LoanSource { get; set; }
            public string LeadSource { get; set; }
            public string EncompassUserID { get; set; }
            public int? LeadSourceId { get; set; }
            public int? LeadId { get; set; }
            public DateTime? EstimatedClosingDate { get; set; }
            public int? LoanRequestId { get; set; }
            public int? PriceId { get; set; }

            public LoanApplication LoanDetail;
            public List<EmploymentInfo> CurrentJobs;
            public List<EmploymentInfo> PreviousJobs;
            public List<OtherIncome> OtherIncomes;
            public List<BorrowerResidence> BorrowerResidences;
            public List<BorrowerAccount> BorrowerAccounts;
            public List<PropertyInfo> AdditionalProperties;
            public List<BorrowerLiability> BorrowerLiabilities;
            public List<BorrowerLiability> BorrowerAlimonyLiabilities;
        }

        public class ZipCodeSelection
        {
            public int ZipCode { get; set; }
            public int StateId { get; set; }
            public int CountyId { get; set; }
            public int CityId { get; set; }
        }

        public class RuleResultId
        {
            public int Id { get; set; }

        }

        public class SubsCription
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string CustomerDescription { get; set; }
            public bool IsActive { get; set; }
            public int DisplayOrder { get; set; }
            public int? SubscriptionSectionId { get; set; }
            public string SubscriptionSectionName { get; set; }
            public int? CusSubscriptionBinderId { get; set; }
            public int? CustomerId { get; set; }
            public string SubscriptionName { get; set; }
            public int? SubscriptionGroupId { get; set; }
            public bool IsDeleted { get; set; }
            public int? SectionId { get; set; }
            public bool IsDefault { get; set; }
            public bool IsSystem { get; set; }
            public int? SubscriptionTypeId { get; set; }
            public string SubscriptionSectionDesc { get; set; }
            public int SubscriptionSectionDisplayOrder { get; set; }
            public int EntityTypeId { get; set; }
            public string SubscriptionGroupName { get; set; }
            public int? ControlTypeId { get; set; }

        }

        public class TempZillowLoanData
        {
            public string LoanNumber { get; set; } // Loan # (length: 255)
            public string StreetAddress { get; set; } // Street Address (length: 255)
            public string City { get; set; } // City (length: 255)
            public string County { get; set; } // County (length: 255)
            public string State { get; set; } // State (length: 255)
            public string Zip { get; set; } // Zip (length: 255)
            public double? PurchasePrice { get; set; } // Purchase Price
            public double? AppraisedValue { get; set; } // Appraised Value
            public double? LoanAmt { get; set; } // Loan Amt
            public string ZillowEst35PropertyValue { get; set; } // Zillow Est# Property Value (length: 255)
            public System.DateTime? FundingDate { get; set; } // Funding Date
            public double? Ltv { get; set; } // LTV
            public string LoanType { get; set; } // Loan Type (length: 255)
            public string LoanProgram { get; set; } // Loan Program (length: 255)
            public string LoanPurpose { get; set; } // Loan Purpose (length: 255)
            public string Occupancy40P47S47I41 { get; set; } // Occupancy (P/S/I) (length: 255)
            public double? InterestRate { get; set; } // Interest Rate
            public double? MtgPymt { get; set; } // Mtg Pymt
            public string MtgIns { get; set; } // Mtg Ins (length: 255)
            public string LoanOfficer { get; set; } // Loan Officer (length: 255)
            public string BorrowerName { get; set; } // Borrower Name (length: 255)
            public string BorrHomePhone { get; set; } // Borr Home Phone (length: 255)
            public string BorrCell { get; set; } // Borr Cell (length: 255)
            public string BorrEmail { get; set; } // Borr Email (length: 255)
            public string BorrMaritalStatus { get; set; } // Borr Marital Status (length: 255)
            public string BorrIntentToOccupancy { get; set; } // Borr Intent to Occupancy (length: 255)
            public string CoBorrowerName { get; set; } // CoBorrower Name (length: 255)
            public string CoBorrHomePhone { get; set; } // Co-Borr Home Phone (length: 255)
            public string CoBorrCell { get; set; } // Co-Borr Cell (length: 255)
            public string CoBorrEmail { get; set; } // Co-Borr Email (length: 255)
            public string CoBorrMaritalStatus { get; set; } // Co-Borr Marital Status (length: 255)
            public string CoBorrIntentToOccupancy { get; set; } // Co-Borr Intent to Occupancy (length: 255)
            public string Xml { get; set; }
            public decimal? ZestimateAmount { get; set; }
            public DateTime? ZestimateLastUpdated { get; set; }
            public decimal? ZestimateValueChange { get; set; }
            public decimal? ZestimateValuationHigh { get; set; }
            public decimal? ZestimateValuationLow { get; set; }
        }
    }
}
