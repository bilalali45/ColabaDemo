













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // LendingTreeLead

    public partial class LendingTreeLead 
    {
        public int Id { get; set; } // Id (Primary key)
        public string RequestType { get; set; } // RequestType (length: 500)
        public string LoanRequestType { get; set; } // LoanRequestType (length: 500)
        public string IsTest { get; set; } // IsTest (length: 500)
        public int? TrackingNumber { get; set; } // TrackingNumber
        public string RequestAssignmentDate { get; set; } // RequestAssignmentDate (length: 500)
        public string ContactAddress { get; set; } // ContactAddress (length: 500)
        public string ContactCity { get; set; } // ContactCity (length: 500)
        public string ContactState { get; set; } // ContactState (length: 500)
        public string ContactZip { get; set; } // ContactZip (length: 500)
        public string ContactPhoneExtension { get; set; } // ContactPhoneExtension (length: 500)
        public string ConsumerGeoPhoneAreaCode { get; set; } // ConsumerGeoPhoneAreaCode (length: 500)
        public string ConsumerGeoPhoneCountryCode { get; set; } // ConsumerGeoPhoneCountryCode (length: 500)
        public string FirstName { get; set; } // FirstName (length: 500)
        public string LastName { get; set; } // LastName (length: 500)
        public string TimeToContact { get; set; } // TimeToContact (length: 500)
        public string IsMaskedEmail { get; set; } // IsMaskedEmail (length: 500)
        public string Email { get; set; } // Email (length: 500)
        public string IsMaskedPhone { get; set; } // IsMaskedPhone (length: 500)
        public string Phone { get; set; } // Phone (length: 500)
        public string DateOfBirth { get; set; } // DateOfBirth (length: 500)
        public string Ssn { get; set; } // Ssn (length: 500)
        public string IsMilitary { get; set; } // IsMilitary (length: 500)
        public string AssignedCreditValue { get; set; } // AssignedCreditValue (length: 500)
        public int? SelfCreditRatingId { get; set; } // SelfCreditRatingId
        public string SelfCreditRating { get; set; } // SelfCreditRating (length: 500)
        public int? BankruptcyId { get; set; } // BankruptcyId
        public string Bankruptcy { get; set; } // Bankruptcy (length: 500)
        public int? ForeclosureId { get; set; } // ForeclosureId
        public string Foreclosure { get; set; } // Foreclosure (length: 500)
        public string FirstTimeHomeBuyer { get; set; } // FirstTimeHomeBuyer (length: 500)
        public string WorkingWithAgent { get; set; } // WorkingWithAgent (length: 500)
        public string FoundHome { get; set; } // FoundHome (length: 500)
        public string PropertyPurchaseYear { get; set; } // PropertyPurchaseYear (length: 500)
        public string PropertyPurchasePrice { get; set; } // PropertyPurchasePrice (length: 500)
        public string AnnualIncome { get; set; } // AnnualIncome (length: 500)
        public string ExistingCustomerRelationship { get; set; } // ExistingCustomerRelationship (length: 500)
        public int? ResidenceTypeId { get; set; } // ResidenceTypeId
        public string ResidenceType { get; set; } // ResidenceType (length: 500)
        public string PurchaseTimeFrame { get; set; } // PurchaseTimeFrame (length: 500)
        public string IsSelfEmployedField { get; set; } // IsSelfEmployedField (length: 500)
        public int? EmploymentStatusId { get; set; } // EmploymentStatusId
        public string EmploymentStatus { get; set; } // EmploymentStatus (length: 500)
        public string EmployerName { get; set; } // EmployerName (length: 500)
        public string UniversityAttended { get; set; } // UniversityAttended (length: 500)
        public string HighestDegreeObtained { get; set; } // HighestDegreeObtained (length: 500)
        public string GraduateDegree { get; set; } // GraduateDegree (length: 500)
        public string StudentLoanCreditorName { get; set; } // StudentLoanCreditorName (length: 500)
        public string StudentLoanBalance { get; set; } // StudentLoanBalance (length: 500)
        public string StudentLoanStartDate { get; set; } // StudentLoanStartDate (length: 500)
        public string StudentLoanMonthlyPayment { get; set; } // StudentLoanMonthlyPayment (length: 500)
        public string StudentLoanTerm { get; set; } // StudentLoanTerm (length: 500)
        public string TotalPaymentCount { get; set; } // TotalPaymentCount (length: 500)
        public int? PropertyPrice { get; set; } // PropertyPrice
        public int? PropertyValue { get; set; } // PropertyValue
        public string PropertyCity { get; set; } // PropertyCity (length: 500)
        public string PropertyState { get; set; } // PropertyState (length: 500)
        public string PropertyZip { get; set; } // PropertyZip (length: 500)
        public string PropertyCounty { get; set; } // PropertyCounty (length: 500)
        public string PropertyMsa { get; set; } // PropertyMsa (length: 500)
        public string IsTarget { get; set; } // IsTarget (length: 500)
        public int? PropertyTypeId { get; set; } // PropertyTypeId
        public string PropertyType { get; set; } // PropertyType (length: 500)
        public int? PropertyUseId { get; set; } // PropertyUseId
        public string PropertyUse { get; set; } // PropertyUse (length: 500)
        public string DownPayment { get; set; } // DownPayment (length: 500)
        public string LoanAmount { get; set; } // LoanAmount (length: 500)
        public int? CashOut { get; set; } // CashOut
        public string FirstMortgageBalance { get; set; } // FirstMortgageBalance (length: 500)
        public string SecondMortgageBalance { get; set; } // SecondMortgageBalance (length: 500)
        public string Ltv { get; set; } // Ltv (length: 500)
        public string PresentLtv { get; set; } // PresentLtv (length: 500)
        public string PresentCltv { get; set; } // PresentCltv (length: 500)
        public string ProposedLtv { get; set; } // ProposedLtv (length: 500)
        public string ProposedCltv { get; set; } // ProposedCltv (length: 500)
        public string Term { get; set; } // Term (length: 500)
        public int? LoanInfoLoanRequestTypeId { get; set; } // LoanInfoLoanRequestTypeId
        public string LoanInfoLoanRequestType { get; set; } // LoanInfoLoanRequestType (length: 500)
        public string TrusteePartnerId { get; set; } // TrusteePartnerId (length: 500)
        public string NameOfPartner { get; set; } // NameOfPartner (length: 500)
        public string FilterName { get; set; } // FilterName (length: 500)
        public int? FilterCategoryId { get; set; } // FilterCategoryId
        public string FilterCategory { get; set; } // FilterCategory (length: 500)
        public string FilterRoutingId { get; set; } // FilterRoutingId (length: 500)
        public string RoutingParams { get; set; } // RoutingParams (length: 500)
        public string LoExternalId { get; set; } // LoExternalId (length: 500)
        public int? ThirdPartyId { get; set; } // ThirdPartyId

        // Reverse navigation

        /// <summary>
        /// Child LendingTreeFees where [LendingTreeFee].[LendingTreeLeadId] point to this entity (FK_LendingTreeFee_LendingTreeLead)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LendingTreeFee> LendingTreeFees { get; set; } // LendingTreeFee.FK_LendingTreeFee_LendingTreeLead
        /// <summary>
        /// Child LendingTreeLoanRequestPurposes where [LendingTreeLoanRequestPurpose].[LendingTreeLeadId] point to this entity (FK_LendingTreeLoanRequestPurpose_LendingTreeLead)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LendingTreeLoanRequestPurpose> LendingTreeLoanRequestPurposes { get; set; } // LendingTreeLoanRequestPurpose.FK_LendingTreeLoanRequestPurpose_LendingTreeLead
        /// <summary>
        /// Child LendingTreeOptions where [LendingTreeOption].[LendingTreeLeadId] point to this entity (FK_LendingTreeOption_LendingTreeLead)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LendingTreeOption> LendingTreeOptions { get; set; } // LendingTreeOption.FK_LendingTreeOption_LendingTreeLead
        /// <summary>
        /// Child LendingTreeQuotes where [LendingTreeQuote].[LendingTreeLeadId] point to this entity (FK_LendingTreeQuote_LendingTreeLead)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LendingTreeQuote> LendingTreeQuotes { get; set; } // LendingTreeQuote.FK_LendingTreeQuote_LendingTreeLead
        /// <summary>
        /// Child LendingTreeScores where [LendingTreeScore].[LendingTreeLeadId] point to this entity (FK_LendingTreeScore_LendingTreeLead)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LendingTreeScore> LendingTreeScores { get; set; } // LendingTreeScore.FK_LendingTreeScore_LendingTreeLead

        // Foreign keys

        /// <summary>
        /// Parent ThirdPartyLead pointed by [LendingTreeLead].([ThirdPartyId]) (FK_LendingTreeLead_ThirdPartyLead)
        /// </summary>
        public virtual ThirdPartyLead ThirdPartyLead { get; set; } // FK_LendingTreeLead_ThirdPartyLead

        public LendingTreeLead()
        {
            LendingTreeFees = new System.Collections.Generic.HashSet<LendingTreeFee>();
            LendingTreeLoanRequestPurposes = new System.Collections.Generic.HashSet<LendingTreeLoanRequestPurpose>();
            LendingTreeOptions = new System.Collections.Generic.HashSet<LendingTreeOption>();
            LendingTreeQuotes = new System.Collections.Generic.HashSet<LendingTreeQuote>();
            LendingTreeScores = new System.Collections.Generic.HashSet<LendingTreeScore>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
