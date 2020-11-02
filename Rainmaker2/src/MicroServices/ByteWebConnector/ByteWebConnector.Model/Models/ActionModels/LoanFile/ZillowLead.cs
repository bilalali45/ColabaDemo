













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // ZillowLead

    public partial class ZillowLead 
    {
        public int Id { get; set; } // Id (Primary key)
        public string ZillowId { get; set; } // ZillowId (length: 50)
        public string Created { get; set; } // Created (length: 100)
        public string Type { get; set; } // Type (length: 100)
        public string Source { get; set; } // Source (length: 100)
        public decimal? Price { get; set; } // Price
        public string ReceiverLenderId { get; set; } // ReceiverLenderId (length: 100)
        public string ReceiverFirstName { get; set; } // ReceiverFirstName (length: 100)
        public string ReceiverLastName { get; set; } // ReceiverLastName (length: 100)
        public string ReceiverEmailAddress { get; set; } // ReceiverEmailAddress (length: 100)
        public string ReceiverPhoneNumber { get; set; } // ReceiverPhoneNumber (length: 100)
        public string ReceiverNmlsLicense { get; set; } // ReceiverNmlsLicense (length: 100)
        public string SenderEmailAddress { get; set; } // SenderEmailAddress (length: 100)
        public string SenderFirstName { get; set; } // SenderFirstName (length: 100)
        public string SenderLastName { get; set; } // SenderLastName (length: 100)
        public string SenderPhoneNumber { get; set; } // SenderPhoneNumber (length: 100)
        public bool? AcceptPrepaymentPenalty { get; set; } // AcceptPrepaymentPenalty
        public string AgentBusinessName { get; set; } // AgentBusinessName (length: 100)
        public string AgentEmailAddress { get; set; } // AgentEmailAddress (length: 100)
        public string AgentFirstName { get; set; } // AgentFirstName (length: 100)
        public string AgentId { get; set; } // AgentId (length: 100)
        public string AgentLastName { get; set; } // AgentLastName (length: 100)
        public string AgentPhoneNumber { get; set; } // AgentPhoneNumber (length: 100)
        public int? AnnualIncome { get; set; } // AnnualIncome
        public int? CashOut { get; set; } // CashOut
        public string City { get; set; } // City (length: 100)
        public int? ClosingTimelineDays { get; set; } // ClosingTimelineDays
        public string CoBorrowerFirstName { get; set; } // CoBorrowerFirstName (length: 100)
        public string CoBorrowerLastName { get; set; } // CoBorrowerLastName (length: 100)
        public string CreditReportId { get; set; } // CreditReportId (length: 100)
        public int? CreditReportScore { get; set; } // CreditReportScore
        public string CreditScoreRange { get; set; } // CreditScoreRange (length: 100)
        public string CurrentBacker { get; set; } // CurrentBacker (length: 100)
        public int? CurrentBalance { get; set; } // CurrentBalance
        public decimal? DebtToIncomePercent { get; set; } // DebtToIncomePercent
        public int? DownPayment { get; set; } // DownPayment
        public bool? FhaStreamlineEligible { get; set; } // FhaStreamlineEligible
        public bool? FirstTimeBuyer { get; set; } // FirstTimeBuyer
        public bool? HarpEligible { get; set; } // HarpEligible
        public bool? HasAgent { get; set; } // HasAgent
        public bool? HasBankruptcy { get; set; } // HasBankruptcy
        public bool? HasCoBorrower { get; set; } // HasCoBorrower
        public bool? HasForeclosure { get; set; } // HasForeclosure
        public bool? HasSecondMortgage { get; set; } // HasSecondMortgage
        public int? DetailLoanAmount { get; set; } // DetailLoanAmount
        public string LoanPurpose { get; set; } // LoanPurpose (length: 100)
        public decimal? LoanToValuePercent { get; set; } // LoanToValuePercent
        public string Message { get; set; } // Message
        public int? MonthlyDebts { get; set; } // MonthlyDebts
        public bool? NewConstruction { get; set; } // NewConstruction
        public string PreApprovalLetterId { get; set; } // PreApprovalLetterId (length: 100)
        public string PropertyAddress { get; set; } // PropertyAddress (length: 100)
        public string PropertyType { get; set; } // PropertyType (length: 100)
        public string PropertyUse { get; set; } // PropertyUse (length: 100)
        public int? PropertyValue { get; set; } // PropertyValue
        public string QuoteId { get; set; } // QuoteId (length: 100)
        public string RequestId { get; set; } // RequestId (length: 100)
        public bool? SelfEmployed { get; set; } // SelfEmployed
        public string StateAbbreviation { get; set; } // StateAbbreviation (length: 100)
        public string StreetAddress { get; set; } // StreetAddress (length: 100)
        public int? TotalAssets { get; set; } // TotalAssets
        public bool? WantsCashOut { get; set; } // WantsCashOut
        public bool? VaEligible { get; set; } // VaEligible
        public bool? VaFirstTimeUser { get; set; } // VaFirstTimeUser
        public bool? VaHasDisability { get; set; } // VaHasDisability
        public string VeteranType { get; set; } // VeteranType (length: 100)
        public int? YearPurchased { get; set; } // YearPurchased
        public string ZipCode { get; set; } // ZipCode (length: 100)
        public decimal? Rate { get; set; } // Rate
        public decimal? Apr { get; set; } // Apr
        public int? TermMonths { get; set; } // TermMonths
        public int? DueInMonths { get; set; } // DueInMonths
        public int? InterestOnlyMonths { get; set; } // InterestOnlyMonths
        public int? LockDays { get; set; } // LockDays
        public bool? HasPrepaymentPenalty { get; set; } // HasPrepaymentPenalty
        public bool? Jumbo { get; set; } // Jumbo
        public bool? Harp { get; set; } // Harp
        public bool? LenderPaidMortgageInsurance { get; set; } // LenderPaidMortgageInsurance
        public decimal? AnnualMortgageInsurancePremiumPercent { get; set; } // AnnualMortgageInsurancePremiumPercent
        public int? FixedRateMonths { get; set; } // FixedRateMonths
        public int? AdjustmentPeriodMonths { get; set; } // AdjustmentPeriodMonths
        public string Index { get; set; } // Index (length: 100)
        public decimal? Margin { get; set; } // Margin
        public decimal? InitialCap { get; set; } // InitialCap
        public decimal? PeriodicCap { get; set; } // PeriodicCap
        public decimal? LifetimeCap { get; set; } // LifetimeCap
        public decimal? UpfrontPremiumPercent { get; set; } // UpfrontPremiumPercent
        public decimal? AnnualPremiumPercent { get; set; } // AnnualPremiumPercent
        public bool? Streamline { get; set; } // Streamline
        public decimal? FundingFeePercent { get; set; } // FundingFeePercent
        public bool? CurrentBackerMustBeFannieMae { get; set; } // CurrentBackerMustBeFannieMae
        public bool? CurrentBackerMustBeFreddieMac { get; set; } // CurrentBackerMustBeFreddieMac
        public int? LenderCredit { get; set; } // LenderCredit
        public decimal? LenderCreditPercent { get; set; } // LenderCreditPercent
        public int? MaxAllowedLtv { get; set; } // MaxAllowedLtv
        public string LoanQuoteZillowDetailsCreated { get; set; } // LoanQuoteZillowDetailsCreated (length: 100)
        public decimal? LoanQuoteZillowDetailsApr { get; set; } // LoanQuoteZillowDetailsApr
        public int? UpfrontCost { get; set; } // UpfrontCost
        public int? MonthlyPrincipalAndInterest { get; set; } // MonthlyPrincipalAndInterest
        public int? MonthlyMortgageInsurance { get; set; } // MonthlyMortgageInsurance
        public decimal? Points { get; set; } // Points
        public int? FeesRolledIntoLoanAmount { get; set; } // FeesRolledIntoLoanAmount
        public int? LoanQuoteZillowDetailsloanAmount { get; set; } // LoanQuoteZillowDetailsloanAmount
        public int? ThirdPartyId { get; set; } // ThirdPartyId

        // Reverse navigation

        /// <summary>
        /// Child ZillowLeadFees where [ZillowLeadFee].[ThirdPartyLeadId] point to this entity (FK_ZillowLeadFee_ZillowLead)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ZillowLeadFee> ZillowLeadFees { get; set; } // ZillowLeadFee.FK_ZillowLeadFee_ZillowLead
        /// <summary>
        /// Child ZillowLeadPrograms where [ZillowLeadProgram].[ThirdPartyLeadId] point to this entity (FK_ZillowLeadProgram_ZillowLead)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ZillowLeadProgram> ZillowLeadPrograms { get; set; } // ZillowLeadProgram.FK_ZillowLeadProgram_ZillowLead

        // Foreign keys

        /// <summary>
        /// Parent ThirdPartyLead pointed by [ZillowLead].([ThirdPartyId]) (FK_ZillowLead_ThirdPartyLead)
        /// </summary>
        public virtual ThirdPartyLead ThirdPartyLead { get; set; } // FK_ZillowLead_ThirdPartyLead

        public ZillowLead()
        {
            ZillowLeadFees = new System.Collections.Generic.HashSet<ZillowLeadFee>();
            ZillowLeadPrograms = new System.Collections.Generic.HashSet<ZillowLeadProgram>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
