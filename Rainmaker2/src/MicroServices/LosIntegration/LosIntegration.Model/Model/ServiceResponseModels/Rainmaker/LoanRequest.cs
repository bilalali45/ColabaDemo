













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LoanRequest

    public partial class LoanRequest 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? VisitorId { get; set; } // VisitorId
        public int? OpportunityId { get; set; } // OpportunityId
        public int? CustomerId { get; set; } // CustomerId
        public int? EmployeId { get; set; } // EmployeId
        public int LoanPurposeId { get; set; } // LoanPurposeId
        public int? LoanGoalId { get; set; } // LoanGoalId
        public int StateId { get; set; } // StateId
        public int CountyId { get; set; } // CountyId
        public int? CityId { get; set; } // CityId
        public string CityName { get; set; } // CityName (length: 200)
        public string StreetAddress { get; set; } // StreetAddress (length: 200)
        public string ZipCode { get; set; } // ZipCode (length: 20)
        public int? PropertyPurchaseYear { get; set; } // PropertyPurchaseYear
        public decimal LoanAmount { get; set; } // LoanAmount
        public decimal PropertyValue { get; set; } // PropertyValue
        public int PropertyTypeId { get; set; } // PropertyTypeId
        public int PropertyUsageId { get; set; } // PropertyUsageId
        public int CreditScoreNo { get; set; } // CreditScoreNo
        public int LockPeriodDays { get; set; } // LockPeriodDays
        public bool? IsNewlyBuiltHome { get; set; } // IsNewlyBuiltHome
        public bool EscrowWaiver { get; set; } // EscrowWaiver
        public bool IsSecondLien { get; set; } // IsSecondLien
        public decimal? CashOutAmount { get; set; } // CashOutAmount
        public decimal? FirstMortgageBalance { get; set; } // FirstMortgageBalance
        public decimal? TotalJuniorLien { get; set; } // TotalJuniorLien
        public decimal? MortgageToBePaid { get; set; } // MortgageToBePaid
        public decimal? MortgageToBeSubordinate { get; set; } // MortgageToBeSubordinate
        public decimal? SecondLienBalance { get; set; } // SecondLienBalance
        public int? ResidencyTypeId { get; set; } // ResidencyTypeId
        public int? OwnRentalProperties { get; set; } // OwnRentalProperties
        public int? DtiHousing { get; set; } // DtiHousing
        public int? DtiTotal { get; set; } // DtiTotal
        public decimal? ResidualIncome { get; set; } // ResidualIncome
        public decimal? TotalAssets { get; set; } // TotalAssets
        public decimal? AnnualIncome { get; set; } // AnnualIncome
        public decimal? MonthlyDebts { get; set; } // MonthlyDebts
        public int? DocTypeId { get; set; } // DocTypeId
        public System.DateTime SearchDateUtc { get; set; } // SearchDateUtc
        public int SessionLogId { get; set; } // SessionLogId
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsActive { get; set; } // IsActive
        public bool IsDeleted { get; set; } // IsDeleted
        public bool? IsValid { get; set; } // IsValid
        public int? LeadSourceId { get; set; } // LeadSourceId
        public int? LeadSourceTypeId { get; set; } // LeadSourceTypeId
        public int? LeadCreatedFromId { get; set; } // LeadCreatedFromId
        public int? AdSourceId { get; set; } // AdSourceId
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public bool? IsEligible { get; set; } // IsEligible
        public decimal? FinancedFeePaid { get; set; } // FinancedFeePaid
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public System.DateTime? EstimatedClosingDate { get; set; } // EstimatedClosingDate
        public int? TypeId { get; set; } // TypeId
        public int? LeadTypeId { get; set; } // LeadTypeId
        public decimal? Deposit { get; set; } // Deposit
        public decimal? FirstMortgageRate { get; set; } // FirstMortgageRate
        public int? FirstMortgageLoanTypeId { get; set; } // FirstMortgageLoanTypeId
        public int? FirstMortgageAmortizationTypeId { get; set; } // FirstMortgageAmortizationTypeId
        public int? FirstMortgageLoanTermsYears { get; set; } // FirstMortgageLoanTermsYears
        public int? CopyFromId { get; set; } // CopyFromId
        public int? MoveToId { get; set; } // MoveToId

        // Reverse navigation

        /// <summary>
        /// Child BenchMarks where [BenchMark].[LoanRequestId] point to this entity (FK_BenchMark_LoanRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BenchMark> BenchMarks { get; set; } // BenchMark.FK_BenchMark_LoanRequest
        /// <summary>
        /// Child CampaignQueues where [CampaignQueue].[LoanRequestId] point to this entity (FK_CampaignQueue_LoanRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<CampaignQueue> CampaignQueues { get; set; } // CampaignQueue.FK_CampaignQueue_LoanRequest
        /// <summary>
        /// Child DefaultLoanParameters where [DefaultLoanParameter].[LoanRequestId] point to this entity (FK_DefaultLoanParameter_LoanRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<DefaultLoanParameter> DefaultLoanParameters { get; set; } // DefaultLoanParameter.FK_DefaultLoanParameter_LoanRequest
        /// <summary>
        /// Child LoanRequestDetails where [LoanRequestDetail].[LoanRequestId] point to this entity (FK_LoanRequestDetail_LoanRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequestDetail> LoanRequestDetails { get; set; } // LoanRequestDetail.FK_LoanRequestDetail_LoanRequest
        /// <summary>
        /// Child LoanRequestMergeLogs where [LoanRequestMergeLog].[LoanRequestId] point to this entity (FK_LoanRequestMergeLog_LoanRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequestMergeLog> LoanRequestMergeLogs { get; set; } // LoanRequestMergeLog.FK_LoanRequestMergeLog_LoanRequest
        /// <summary>
        /// Child LoanRequestMessageBinders where [LoanRequestMessageBinder].[LoanRequestId] point to this entity (FK_LoanRequestMessageBinder_LoanRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequestMessageBinder> LoanRequestMessageBinders { get; set; } // LoanRequestMessageBinder.FK_LoanRequestMessageBinder_LoanRequest
        /// <summary>
        /// Child LoanRequestProducts where [LoanRequestProduct].[LoanRequestId] point to this entity (FK_LoanRequestProduct_LoanRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequestProduct> LoanRequestProducts { get; set; } // LoanRequestProduct.FK_LoanRequestProduct_LoanRequest
        /// <summary>
        /// Child LoanRequestProductClasses where [LoanRequestProductClass].[LoanRequestId] point to this entity (FK_LoanRequestProductClass_LoanRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequestProductClass> LoanRequestProductClasses { get; set; } // LoanRequestProductClass.FK_LoanRequestProductClass_LoanRequest
        /// <summary>
        /// Child LoanRequestProductTypes where [LoanRequestProductType].[LoanRequestId] point to this entity (FK_LoanRequestProductType_LoanRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequestProductType> LoanRequestProductTypes { get; set; } // LoanRequestProductType.FK_LoanRequestProductType_LoanRequest
        /// <summary>
        /// Child LoanRequestQuestionBindings where [LoanRequestQuestionBinding].[LoanRequestId] point to this entity (FK_LoanRequestQuestionBinding_LoanRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequestQuestionBinding> LoanRequestQuestionBindings { get; set; } // LoanRequestQuestionBinding.FK_LoanRequestQuestionBinding_LoanRequest
        /// <summary>
        /// Child Opportunities where [Opportunity].[LoanRequestId] point to this entity (FK_Opportunity_LoanRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Opportunity> Opportunities { get; set; } // Opportunity.FK_Opportunity_LoanRequest
        /// <summary>
        /// Child OpportunityPropertyTaxes where [OpportunityPropertyTax].[LoanRequestId] point to this entity (FK_OpportunityPropertyTax_LoanRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OpportunityPropertyTax> OpportunityPropertyTaxes { get; set; } // OpportunityPropertyTax.FK_OpportunityPropertyTax_LoanRequest
        /// <summary>
        /// Child QuoteResults where [QuoteResult].[LoanRequestId] point to this entity (FK_QuoteResult_LoanRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<QuoteResult> QuoteResults { get; set; } // QuoteResult.FK_QuoteResult_LoanRequest
        /// <summary>
        /// Child RateServiceParameters where [RateServiceParameter].[LoanRequestId] point to this entity (FK_RateServiceParameter_LoanRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<RateServiceParameter> RateServiceParameters { get; set; } // RateServiceParameter.FK_RateServiceParameter_LoanRequest
        /// <summary>
        /// Child SecondLiens where [SecondLien].[LoanRequestId] point to this entity (FK_SecondLien_LoanRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<SecondLien> SecondLiens { get; set; } // SecondLien.FK_SecondLien_LoanRequest
        /// <summary>
        /// Child ThirdPartyLeads where [ThirdPartyLead].[LoanRequestId] point to this entity (FK_ThirdPartyLead_LoanRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ThirdPartyLead> ThirdPartyLeads { get; set; } // ThirdPartyLead.FK_ThirdPartyLead_LoanRequest
        /// <summary>
        /// Child WorkQueues where [WorkQueue].[LoanRequestId] point to this entity (FK_WorkQueue_LoanRequest)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<WorkQueue> WorkQueues { get; set; } // WorkQueue.FK_WorkQueue_LoanRequest

        // Foreign keys

        /// <summary>
        /// Parent AdsSource pointed by [LoanRequest].([AdSourceId]) (FK_LoanRequest_AdsSource)
        /// </summary>
        public virtual AdsSource AdsSource { get; set; } // FK_LoanRequest_AdsSource

        /// <summary>
        /// Parent BusinessUnit pointed by [LoanRequest].([BusinessUnitId]) (FK_LoanRequest_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_LoanRequest_BusinessUnit

        /// <summary>
        /// Parent City pointed by [LoanRequest].([CityId]) (FK_LoanRequest_City)
        /// </summary>
        public virtual City City { get; set; } // FK_LoanRequest_City

        /// <summary>
        /// Parent County pointed by [LoanRequest].([CountyId]) (FK_LoanRequest_County)
        /// </summary>
        public virtual County County { get; set; } // FK_LoanRequest_County

        /// <summary>
        /// Parent Customer pointed by [LoanRequest].([CustomerId]) (FK_LoanRequest_Customer)
        /// </summary>
        public virtual Customer Customer { get; set; } // FK_LoanRequest_Customer

        /// <summary>
        /// Parent Employee pointed by [LoanRequest].([EmployeId]) (FK_LoanRequest_Employee)
        /// </summary>
        public virtual Employee Employee { get; set; } // FK_LoanRequest_Employee

        /// <summary>
        /// Parent LeadSource pointed by [LoanRequest].([LeadSourceId]) (FK_LoanRequest_LeadSource)
        /// </summary>
        public virtual LeadSource LeadSource { get; set; } // FK_LoanRequest_LeadSource

        /// <summary>
        /// Parent LeadSourceType pointed by [LoanRequest].([LeadSourceTypeId]) (FK_LoanRequest_LeadSourceType)
        /// </summary>
        public virtual LeadSourceType LeadSourceType { get; set; } // FK_LoanRequest_LeadSourceType

        /// <summary>
        /// Parent LeadType pointed by [LoanRequest].([LeadTypeId]) (FK_LoanRequest_LeadType)
        /// </summary>
        public virtual LeadType LeadType { get; set; } // FK_LoanRequest_LeadType

        /// <summary>
        /// Parent LoanDocType pointed by [LoanRequest].([DocTypeId]) (FK_LoanRequest_LoanDocType)
        /// </summary>
        public virtual LoanDocType LoanDocType { get; set; } // FK_LoanRequest_LoanDocType

        /// <summary>
        /// Parent LoanGoal pointed by [LoanRequest].([LoanGoalId]) (FK_LoanRequest_LoanGoal)
        /// </summary>
        public virtual LoanGoal LoanGoal { get; set; } // FK_LoanRequest_LoanGoal

        /// <summary>
        /// Parent LoanPurpose pointed by [LoanRequest].([LoanPurposeId]) (FK_LoanRequest_LoanPurpose)
        /// </summary>
        public virtual LoanPurpose LoanPurpose { get; set; } // FK_LoanRequest_LoanPurpose

        /// <summary>
        /// Parent LoanType pointed by [LoanRequest].([FirstMortgageLoanTypeId]) (FK_LoanRequest_LoanType)
        /// </summary>
        public virtual LoanType LoanType { get; set; } // FK_LoanRequest_LoanType

        /// <summary>
        /// Parent ProductAmortizationType pointed by [LoanRequest].([FirstMortgageAmortizationTypeId]) (FK_LoanRequest_ProductAmortizationType)
        /// </summary>
        public virtual ProductAmortizationType ProductAmortizationType { get; set; } // FK_LoanRequest_ProductAmortizationType

        /// <summary>
        /// Parent PropertyType pointed by [LoanRequest].([PropertyTypeId]) (FK_LoanRequest_PropertyType)
        /// </summary>
        public virtual PropertyType PropertyType { get; set; } // FK_LoanRequest_PropertyType

        /// <summary>
        /// Parent PropertyUsage pointed by [LoanRequest].([PropertyUsageId]) (FK_LoanRequest_PropertyUsage)
        /// </summary>
        public virtual PropertyUsage PropertyUsage { get; set; } // FK_LoanRequest_PropertyUsage

        /// <summary>
        /// Parent ResidencyType pointed by [LoanRequest].([ResidencyTypeId]) (FK_LoanRequest_ResidencyType)
        /// </summary>
        public virtual ResidencyType ResidencyType { get; set; } // FK_LoanRequest_ResidencyType

        /// <summary>
        /// Parent State pointed by [LoanRequest].([StateId]) (FK_LoanRequest_State)
        /// </summary>
        public virtual State State { get; set; } // FK_LoanRequest_State

        /// <summary>
        /// Parent Visitor pointed by [LoanRequest].([VisitorId]) (FK_LoanRequest_Visitor)
        /// </summary>
        public virtual Visitor Visitor { get; set; } // FK_LoanRequest_Visitor

        public LoanRequest()
        {
            EntityTypeId = 61;
            IsActive = true;
            IsDeleted = false;
            FinancedFeePaid = 0m;
            BenchMarks = new System.Collections.Generic.HashSet<BenchMark>();
            CampaignQueues = new System.Collections.Generic.HashSet<CampaignQueue>();
            DefaultLoanParameters = new System.Collections.Generic.HashSet<DefaultLoanParameter>();
            LoanRequestDetails = new System.Collections.Generic.HashSet<LoanRequestDetail>();
            LoanRequestMergeLogs = new System.Collections.Generic.HashSet<LoanRequestMergeLog>();
            LoanRequestMessageBinders = new System.Collections.Generic.HashSet<LoanRequestMessageBinder>();
            LoanRequestProducts = new System.Collections.Generic.HashSet<LoanRequestProduct>();
            LoanRequestProductClasses = new System.Collections.Generic.HashSet<LoanRequestProductClass>();
            LoanRequestProductTypes = new System.Collections.Generic.HashSet<LoanRequestProductType>();
            LoanRequestQuestionBindings = new System.Collections.Generic.HashSet<LoanRequestQuestionBinding>();
            Opportunities = new System.Collections.Generic.HashSet<Opportunity>();
            OpportunityPropertyTaxes = new System.Collections.Generic.HashSet<OpportunityPropertyTax>();
            QuoteResults = new System.Collections.Generic.HashSet<QuoteResult>();
            RateServiceParameters = new System.Collections.Generic.HashSet<RateServiceParameter>();
            SecondLiens = new System.Collections.Generic.HashSet<SecondLien>();
            ThirdPartyLeads = new System.Collections.Generic.HashSet<ThirdPartyLead>();
            WorkQueues = new System.Collections.Generic.HashSet<WorkQueue>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
