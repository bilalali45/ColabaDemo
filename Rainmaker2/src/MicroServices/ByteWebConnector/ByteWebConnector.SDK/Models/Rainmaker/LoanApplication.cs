using System;
using System.Collections.Generic;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public partial class LoanApplication
    {
        public int Id { get; set; }
        public string LoanNumber { get; set; }
        public string AgencyNumber { get; set; }
        public int? VisitorId { get; set; }
        public int? OpportunityId { get; set; }
        public int? BusinessUnitId { get; set; }
        public int? CustomerId { get; set; }
        public int? LoanOriginatorId { get; set; }
        public int? LoanGoalId { get; set; }
        public int LoanPurposeId { get; set; }
        public string OtherLoanPurpose { get; set; }
        public int? LoanPurposeProgramId { get; set; }
        public int? CreditScoreNo { get; set; }
        public int? ProductId { get; set; }
        public int? LoanTypeId { get; set; }
        public string OtherLoanType { get; set; }
        public int? StatusId { get; set; }
        public int? LockPeriodDays { get; set; }
        public decimal? NoteRate { get; set; }
        public decimal? QualifyingRate { get; set; }
        public decimal? Price { get; set; }
        public int? LoanTermMonths { get; set; }
        public int? ProductAmortizationTypeId { get; set; }
        public string OtherAmortization { get; set; }
        public bool? EscrowWaiver { get; set; }
        public decimal? LoanAmount { get; set; }
        public decimal? CashOutAmount { get; set; }
        public decimal? Deposit { get; set; }
        public decimal? MonthlyPi { get; set; }
        public decimal? MonthlyMi { get; set; }
        public decimal? MonthlyEscrow { get; set; }
        public DateTime? ExpectedClosingDate { get; set; }
        public bool? EverHadAVaLoan { get; set; }
        public bool? VaLoanStatusId { get; set; }
        public bool? FirstTimeHomeBuyer { get; set; }
        public int? DtiHousing { get; set; }
        public int? DtiTotal { get; set; }
        public int? DocTypeId { get; set; }
        public int? CompletedById { get; set; }
        public DateTime? SearchDateUtc { get; set; }
        public int? SessionId { get; set; }
        public string FinalXml { get; set; }
        public int EntityTypeId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public int? SubjectPropertyDetailId { get; set; }
        public int? NoOfPeopleLiveIn { get; set; }
        public decimal? AssetsUseForDownpayment { get; set; }
        public string DownPaymentExplanation { get; set; }
        public int? InformationMediumId { get; set; }
        public bool? IsApplyingJointCredit { get; set; }
        public int? NoOfBorrower { get; set; }
        public int? ProjectTypeId { get; set; }
        public byte[] LoanApplicationFlowState { get; set; }
        public string EncompassId { get; set; }
        public string EncompassNumber { get; set; }
        public string ByteLoanNumber { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string ByteFileName { get; set; }

        public LoanDocumentPipeLine LoanDocumentPipeLine { get; set; }

        public ICollection<Borrower> Borrowers { get; set; }

        public ICollection<BorrowerConsent> BorrowerConsents { get; set; }

        public ICollection<LoanApplicationFee> LoanApplicationFees { get; set; }

        public ICollection<LoanDocument> LoanDocuments { get; set; }

        public ICollection<MortgageEducation> MortgageEducations { get; set; }

        public ICollection<TransactionInfoBinder> TransactionInfoBinders { get; set; }

        public ICollection<VaDetail> VaDetails { get; set; }

        public BusinessUnit BusinessUnit { get; set; }

        public Employee Employee { get; set; }

        public InformationMedium InformationMedium { get; set; }

        public LoanGoal LoanGoal { get; set; }

        public LoanPurpose LoanPurpose { get; set; }

        public LoanPurposeProgram LoanPurposeProgram { get; set; }

        public LoanType LoanType { get; set; }

        public Opportunity Opportunity { get; set; }

        public Product Product { get; set; }

        public ProductAmortizationType ProductAmortizationType { get; set; }

        public ProjectType ProjectType { get; set; }

        public PropertyInfo PropertyInfo { get; set; }

        public StatusList StatusList { get; set; }

    }
}