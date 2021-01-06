using System;

namespace ByteWebConnector.Model.Models.ServiceResponseModels.Rainmaker.LoanApplication
{
    public class GetLoanApplicationResponse
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
        public int? MilestoneId { get; set; } // MilestoneId
        public int? LosMilestoneId { get; set; } // LosMilestoneId
        public System.DateTime? BytePostDateUtc { get; set; }
    }
}
// </auto-generated>