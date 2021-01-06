using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class LendingTreeLead
    {
        public int Id { get; set; }
        public string RequestType { get; set; }
        public string LoanRequestType { get; set; }
        public string IsTest { get; set; }
        public int? TrackingNumber { get; set; }
        public string RequestAssignmentDate { get; set; }
        public string ContactAddress { get; set; }
        public string ContactCity { get; set; }
        public string ContactState { get; set; }
        public string ContactZip { get; set; }
        public string ContactPhoneExtension { get; set; }
        public string ConsumerGeoPhoneAreaCode { get; set; }
        public string ConsumerGeoPhoneCountryCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TimeToContact { get; set; }
        public string IsMaskedEmail { get; set; }
        public string Email { get; set; }
        public string IsMaskedPhone { get; set; }
        public string Phone { get; set; }
        public string DateOfBirth { get; set; }
        public string Ssn { get; set; }
        public string IsMilitary { get; set; }
        public string AssignedCreditValue { get; set; }
        public int? SelfCreditRatingId { get; set; }
        public string SelfCreditRating { get; set; }
        public int? BankruptcyId { get; set; }
        public string Bankruptcy { get; set; }
        public int? ForeclosureId { get; set; }
        public string Foreclosure { get; set; }
        public string FirstTimeHomeBuyer { get; set; }
        public string WorkingWithAgent { get; set; }
        public string FoundHome { get; set; }
        public string PropertyPurchaseYear { get; set; }
        public string PropertyPurchasePrice { get; set; }
        public string AnnualIncome { get; set; }
        public string ExistingCustomerRelationship { get; set; }
        public int? ResidenceTypeId { get; set; }
        public string ResidenceType { get; set; }
        public string PurchaseTimeFrame { get; set; }
        public string IsSelfEmployedField { get; set; }
        public int? EmploymentStatusId { get; set; }
        public string EmploymentStatus { get; set; }
        public string EmployerName { get; set; }
        public string UniversityAttended { get; set; }
        public string HighestDegreeObtained { get; set; }
        public string GraduateDegree { get; set; }
        public string StudentLoanCreditorName { get; set; }
        public string StudentLoanBalance { get; set; }
        public string StudentLoanStartDate { get; set; }
        public string StudentLoanMonthlyPayment { get; set; }
        public string StudentLoanTerm { get; set; }
        public string TotalPaymentCount { get; set; }
        public int? PropertyPrice { get; set; }
        public int? PropertyValue { get; set; }
        public string PropertyCity { get; set; }
        public string PropertyState { get; set; }
        public string PropertyZip { get; set; }
        public string PropertyCounty { get; set; }
        public string PropertyMsa { get; set; }
        public string IsTarget { get; set; }
        public int? PropertyTypeId { get; set; }
        public string PropertyType { get; set; }
        public int? PropertyUseId { get; set; }
        public string PropertyUse { get; set; }
        public string DownPayment { get; set; }
        public string LoanAmount { get; set; }
        public int? CashOut { get; set; }
        public string FirstMortgageBalance { get; set; }
        public string SecondMortgageBalance { get; set; }
        public string Ltv { get; set; }
        public string PresentLtv { get; set; }
        public string PresentCltv { get; set; }
        public string ProposedLtv { get; set; }
        public string ProposedCltv { get; set; }
        public string Term { get; set; }
        public int? LoanInfoLoanRequestTypeId { get; set; }
        public string LoanInfoLoanRequestType { get; set; }
        public string TrusteePartnerId { get; set; }
        public string NameOfPartner { get; set; }
        public string FilterName { get; set; }
        public int? FilterCategoryId { get; set; }
        public string FilterCategory { get; set; }
        public string FilterRoutingId { get; set; }
        public string RoutingParams { get; set; }
        public string LoExternalId { get; set; }
        public int? ThirdPartyId { get; set; }

        public ICollection<LendingTreeFee> LendingTreeFees { get; set; }

        public ICollection<LendingTreeLoanRequestPurpose> LendingTreeLoanRequestPurposes { get; set; }

        public ICollection<LendingTreeOption> LendingTreeOptions { get; set; }

        public ICollection<LendingTreeQuote> LendingTreeQuotes { get; set; }

        public ICollection<LendingTreeScore> LendingTreeScores { get; set; }

        public ThirdPartyLead ThirdPartyLead { get; set; }
    }
}