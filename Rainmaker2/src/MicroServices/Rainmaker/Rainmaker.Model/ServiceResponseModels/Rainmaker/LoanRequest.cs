using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class LoanRequest
    {
        public int Id { get; set; }
        public int? VisitorId { get; set; }
        public int? OpportunityId { get; set; }
        public int? CustomerId { get; set; }
        public int? EmployeId { get; set; }
        public int LoanPurposeId { get; set; }
        public int? LoanGoalId { get; set; }
        public int StateId { get; set; }
        public int CountyId { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public int? PropertyPurchaseYear { get; set; }
        public decimal LoanAmount { get; set; }
        public decimal PropertyValue { get; set; }
        public int PropertyTypeId { get; set; }
        public int PropertyUsageId { get; set; }
        public int CreditScoreNo { get; set; }
        public int LockPeriodDays { get; set; }
        public bool? IsNewlyBuiltHome { get; set; }
        public bool EscrowWaiver { get; set; }
        public bool IsSecondLien { get; set; }
        public decimal? CashOutAmount { get; set; }
        public decimal? FirstMortgageBalance { get; set; }
        public decimal? TotalJuniorLien { get; set; }
        public decimal? MortgageToBePaid { get; set; }
        public decimal? MortgageToBeSubordinate { get; set; }
        public decimal? SecondLienBalance { get; set; }
        public int? ResidencyTypeId { get; set; }
        public int? OwnRentalProperties { get; set; }
        public int? DtiHousing { get; set; }
        public int? DtiTotal { get; set; }
        public decimal? ResidualIncome { get; set; }
        public decimal? TotalAssets { get; set; }
        public decimal? AnnualIncome { get; set; }
        public decimal? MonthlyDebts { get; set; }
        public int? DocTypeId { get; set; }
        public DateTime SearchDateUtc { get; set; }
        public int SessionLogId { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsValid { get; set; }
        public int? LeadSourceId { get; set; }
        public int? LeadSourceTypeId { get; set; }
        public int? LeadCreatedFromId { get; set; }
        public int? AdSourceId { get; set; }
        public int? BusinessUnitId { get; set; }
        public bool? IsEligible { get; set; }
        public decimal? FinancedFeePaid { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public DateTime? EstimatedClosingDate { get; set; }
        public int? TypeId { get; set; }
        public int? LeadTypeId { get; set; }
        public decimal? Deposit { get; set; }
        public decimal? FirstMortgageRate { get; set; }
        public int? FirstMortgageLoanTypeId { get; set; }
        public int? FirstMortgageAmortizationTypeId { get; set; }
        public int? FirstMortgageLoanTermsYears { get; set; }
        public int? CopyFromId { get; set; }
        public int? MoveToId { get; set; }

        //        public System.Collections.Generic.ICollection<BenchMark> BenchMarks { get; set; }

        //public System.Collections.Generic.ICollection<CampaignQueue> CampaignQueues { get; set; }

        //public System.Collections.Generic.ICollection<DefaultLoanParameter> DefaultLoanParameters { get; set; }

        ////public System.Collections.Generic.ICollection<LoanRequestDetail> LoanRequestDetails { get; set; }

        //public System.Collections.Generic.ICollection<LoanRequestMergeLog> LoanRequestMergeLogs { get; set; }

        //public System.Collections.Generic.ICollection<LoanRequestMessageBinder> LoanRequestMessageBinders { get; set; }

        //public System.Collections.Generic.ICollection<LoanRequestProduct> LoanRequestProducts { get; set; }

        //public System.Collections.Generic.ICollection<LoanRequestProductClass> LoanRequestProductClasses { get; set; }

        //public System.Collections.Generic.ICollection<LoanRequestProductType> LoanRequestProductTypes { get; set; }

        //public System.Collections.Generic.ICollection<LoanRequestQuestionBinding> LoanRequestQuestionBindings { get; set; }

        // public System.Collections.Generic.ICollection<Opportunity> Opportunities { get; set; }

        //public System.Collections.Generic.ICollection<OpportunityPropertyTax> OpportunityPropertyTaxes { get; set; }

        //        public System.Collections.Generic.ICollection<QuoteResult> QuoteResults { get; set; }

        //public System.Collections.Generic.ICollection<RateServiceParameter> RateServiceParameters { get; set; }

        //public System.Collections.Generic.ICollection<SecondLien> SecondLiens { get; set; }

        //public System.Collections.Generic.ICollection<ThirdPartyLead> ThirdPartyLeads { get; set; }

        //        public System.Collections.Generic.ICollection<WorkQueue> WorkQueues { get; set; }

        //public AdsSource AdsSource { get; set; }

        public BusinessUnit BusinessUnit { get; set; }

        public City City { get; set; }

        public County County { get; set; }

        public Customer Customer { get; set; }

        public Employee Employee { get; set; }

        //public LeadSource LeadSource { get; set; }

        //public LeadSourceType LeadSourceType { get; set; }

        //public LeadType LeadType { get; set; }

        //public LoanDocType LoanDocType { get; set; }

        public LoanGoal LoanGoal { get; set; }

        public LoanPurpose LoanPurpose { get; set; }

        public LoanType LoanType { get; set; }

        public ProductAmortizationType ProductAmortizationType { get; set; }

        public PropertyType PropertyType { get; set; }

        public PropertyUsage PropertyUsage { get; set; }

        public ResidencyType ResidencyType { get; set; }

        public State State { get; set; }

        //public Visitor Visitor { get; set; }

    }
}