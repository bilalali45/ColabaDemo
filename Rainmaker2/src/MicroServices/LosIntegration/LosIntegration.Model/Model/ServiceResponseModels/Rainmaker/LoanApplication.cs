













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LoanApplication

    public partial class LoanApplication 
    {
        public int Id { get; set; } // Id (Primary key)
        public string LoanNumber { get; set; } // LoanNumber (length: 50)
        public string AgencyNumber { get; set; } // AgencyNumber (length: 50)
        public int? VisitorId { get; set; } // VisitorId
        public int? OpportunityId { get; set; } // OpportunityId
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public int? CustomerId { get; set; } // CustomerId
        public int? LoanOriginatorId { get; set; } // LoanOriginatorId
        public int? LoanGoalId { get; set; } // LoanGoalId
        public int LoanPurposeId { get; set; } // LoanPurposeId
        public string OtherLoanPurpose { get; set; } // OtherLoanPurpose (length: 150)
        public int? LoanPurposeProgramId { get; set; } // LoanPurposeProgramId
        public int? CreditScoreNo { get; set; } // CreditScoreNo
        public int? ProductId { get; set; } // ProductId
        public int? LoanTypeId { get; set; } // LoanTypeId
        public string OtherLoanType { get; set; } // OtherLoanType (length: 150)
        public int? StatusId { get; set; } // StatusId
        public int? LockPeriodDays { get; set; } // LockPeriodDays
        public decimal? NoteRate { get; set; } // NoteRate
        public decimal? QualifyingRate { get; set; } // QualifyingRate
        public decimal? Price { get; set; } // Price
        public int? LoanTermMonths { get; set; } // LoanTermMonths
        public int? ProductAmortizationTypeId { get; set; } // ProductAmortizationTypeId
        public string OtherAmortization { get; set; } // OtherAmortization (length: 150)
        public bool? EscrowWaiver { get; set; } // EscrowWaiver
        public decimal? LoanAmount { get; set; } // LoanAmount
        public decimal? CashOutAmount { get; set; } // CashOutAmount
        public decimal? Deposit { get; set; } // Deposit
        public decimal? MonthlyPi { get; set; } // MonthlyPi
        public decimal? MonthlyMi { get; set; } // MonthlyMi
        public decimal? MonthlyEscrow { get; set; } // MonthlyEscrow
        public System.DateTime? ExpectedClosingDate { get; set; } // ExpectedClosingDate
        public bool? EverHadAVaLoan { get; set; } // EverHadAVaLoan
        public bool? VaLoanStatusId { get; set; } // VaLoanStatusId
        public bool? FirstTimeHomeBuyer { get; set; } // FirstTimeHomeBuyer
        public int? DtiHousing { get; set; } // DtiHousing
        public int? DtiTotal { get; set; } // DtiTotal
        public int? DocTypeId { get; set; } // DocTypeId
        public int? CompletedById { get; set; } // CompletedById
        public System.DateTime? SearchDateUtc { get; set; } // SearchDateUtc
        public int? SessionId { get; set; } // SessionId
        public string FinalXml { get; set; } // FinalXml (length: 1073741823)
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool? IsActive { get; set; } // IsActive
        public bool? IsDeleted { get; set; } // IsDeleted
        public int? SubjectPropertyDetailId { get; set; } // SubjectPropertyDetailId
        public int? NoOfPeopleLiveIn { get; set; } // NoOfPeopleLiveIn
        public decimal? AssetsUseForDownpayment { get; set; } // AssetsUseForDownpayment
        public string DownPaymentExplanation { get; set; } // DownPaymentExplanation (length: 3000)
        public int? InformationMediumId { get; set; } // InformationMediumId
        public bool? IsApplyingJointCredit { get; set; } // IsApplyingJointCredit
        public int? NoOfBorrower { get; set; } // NoOfBorrower
        public int? ProjectTypeId { get; set; } // ProjectTypeId
        public byte[] LoanApplicationFlowState { get; set; } // LoanApplicationFlowState
        public string EncompassId { get; set; } // EncompassId (length: 50)
        public string EncompassNumber { get; set; } // EncompassNumber (length: 50)
        public string ByteLoanNumber { get; set; } // ByteLoanNumber (length: 50)
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string ByteFileName { get; set; } // ByteFileName (length: 50)

        public System.DateTime? BytePostDateUtc { get; set; }
        // Reverse navigation

        /// <summary>
        /// Parent (One-to-One) LoanApplication pointed by [LoanDocumentPipeLine].[LoanApplicationId] (FK_LoanDocumentPipeLine_LoanApplication)
        /// </summary>
        public virtual LoanDocumentPipeLine LoanDocumentPipeLine { get; set; } // LoanDocumentPipeLine.FK_LoanDocumentPipeLine_LoanApplication
        /// <summary>
        /// Child Borrowers where [Borrower].[LoanApplicationId] point to this entity (FK_Borrower_LoanApplication)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Borrower> Borrowers { get; set; } // Borrower.FK_Borrower_LoanApplication
        /// <summary>
        /// Child BorrowerConsents where [BorrowerConsent].[LoanApplicationId] point to this entity (FK_BorrowerConsent_LoanApplication)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerConsent> BorrowerConsents { get; set; } // BorrowerConsent.FK_BorrowerConsent_LoanApplication
        /// <summary>
        /// Child LoanApplicationFees where [LoanApplicationFee].[LoanApplicationId] point to this entity (FK_LoanApplicationFee_LoanApplication)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanApplicationFee> LoanApplicationFees { get; set; } // LoanApplicationFee.FK_LoanApplicationFee_LoanApplication
        /// <summary>
        /// Child LoanDocuments where [LoanDocument].[LoanApplicationId] point to this entity (FK_LoanDocument_LoanApplication)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanDocument> LoanDocuments { get; set; } // LoanDocument.FK_LoanDocument_LoanApplication
        /// <summary>
        /// Child MortgageEducations where [MortgageEducation].[MortgageEducationTypeId] point to this entity (FK_MortgageEducation_LoanApplication)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<MortgageEducation> MortgageEducations { get; set; } // MortgageEducation.FK_MortgageEducation_LoanApplication
        /// <summary>
        /// Child TransactionInfoBinders where [TransactionInfoBinder].[LoanApplicationId] point to this entity (FK_TransactionInfoBinder_LoanApplication)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TransactionInfoBinder> TransactionInfoBinders { get; set; } // TransactionInfoBinder.FK_TransactionInfoBinder_LoanApplication
        /// <summary>
        /// Child VaDetails where [VaDetails].[LoanApplicationId] point to this entity (FK_VaDetails_LoanApplication)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<VaDetail> VaDetails { get; set; } // VaDetails.FK_VaDetails_LoanApplication

        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [LoanApplication].([BusinessUnitId]) (FK_LoanApplication_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_LoanApplication_BusinessUnit

        /// <summary>
        /// Parent Employee pointed by [LoanApplication].([LoanOriginatorId]) (FK_LoanApplication_Employee)
        /// </summary>
        public virtual Employee Employee { get; set; } // FK_LoanApplication_Employee

        /// <summary>
        /// Parent InformationMedium pointed by [LoanApplication].([InformationMediumId]) (FK_LoanApplication_InformationMedium)
        /// </summary>
        public virtual InformationMedium InformationMedium { get; set; } // FK_LoanApplication_InformationMedium

        /// <summary>
        /// Parent LoanGoal pointed by [LoanApplication].([LoanGoalId]) (FK_LoanApplication_LoanGoal)
        /// </summary>
        public virtual LoanGoal LoanGoal { get; set; } // FK_LoanApplication_LoanGoal

        /// <summary>
        /// Parent LoanPurpose pointed by [LoanApplication].([LoanPurposeId]) (FK_LoanApplication_LoanPurpose)
        /// </summary>
        public virtual LoanPurpose LoanPurpose { get; set; } // FK_LoanApplication_LoanPurpose

        /// <summary>
        /// Parent LoanPurposeProgram pointed by [LoanApplication].([LoanPurposeProgramId]) (FK_LoanApplication_LoanPurposeProgram)
        /// </summary>
        public virtual LoanPurposeProgram LoanPurposeProgram { get; set; } // FK_LoanApplication_LoanPurposeProgram

        /// <summary>
        /// Parent LoanType pointed by [LoanApplication].([LoanTypeId]) (FK_LoanApplication_LoanType)
        /// </summary>
        public virtual LoanType LoanType { get; set; } // FK_LoanApplication_LoanType

        /// <summary>
        /// Parent Opportunity pointed by [LoanApplication].([OpportunityId]) (FK_LoanApplication_Opportunity)
        /// </summary>
        public virtual Opportunity Opportunity { get; set; } // FK_LoanApplication_Opportunity

        /// <summary>
        /// Parent Product pointed by [LoanApplication].([ProductId]) (FK_LoanApplication_Product)
        /// </summary>
        public virtual Product Product { get; set; } // FK_LoanApplication_Product

        /// <summary>
        /// Parent ProductAmortizationType pointed by [LoanApplication].([ProductAmortizationTypeId]) (FK_LoanApplication_ProductAmortizationType)
        /// </summary>
        public virtual ProductAmortizationType ProductAmortizationType { get; set; } // FK_LoanApplication_ProductAmortizationType

        /// <summary>
        /// Parent ProjectType pointed by [LoanApplication].([ProjectTypeId]) (FK_LoanApplication_ProjectType)
        /// </summary>
        public virtual ProjectType ProjectType { get; set; } // FK_LoanApplication_ProjectType

        /// <summary>
        /// Parent PropertyInfo pointed by [LoanApplication].([SubjectPropertyDetailId]) (FK_LoanApplication_PropertyInfo)
        /// </summary>
        public virtual PropertyInfo PropertyInfo { get; set; } // FK_LoanApplication_PropertyInfo

        /// <summary>
        /// Parent StatusList pointed by [LoanApplication].([StatusId]) (FK_LoanApplication_StatusList)
        /// </summary>
        public virtual StatusList StatusList { get; set; } // FK_LoanApplication_StatusList

        public LoanApplication()
        {
            EntityTypeId = 18;
            IsActive = true;
            IsDeleted = false;
            Borrowers = new System.Collections.Generic.HashSet<Borrower>();
            BorrowerConsents = new System.Collections.Generic.HashSet<BorrowerConsent>();
            LoanApplicationFees = new System.Collections.Generic.HashSet<LoanApplicationFee>();
            LoanDocuments = new System.Collections.Generic.HashSet<LoanDocument>();
            MortgageEducations = new System.Collections.Generic.HashSet<MortgageEducation>();
            TransactionInfoBinders = new System.Collections.Generic.HashSet<TransactionInfoBinder>();
            VaDetails = new System.Collections.Generic.HashSet<VaDetail>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
