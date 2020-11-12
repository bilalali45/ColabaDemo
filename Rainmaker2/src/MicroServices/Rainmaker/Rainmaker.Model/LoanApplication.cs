using System;

namespace Rainmaker.Model
{
    public class BothLosMilestoneModel
    {
        public int? milestoneId { get; set; }
        public int? losMilestoneId { get; set; }
    }
    public class MilestoneIdModel
    {
        public int loanApplicationId { get; set; }
        public int milestoneId { get; set; }
    }
    public class LosMilestoneIdModel
    {
        public int loanApplicationId { get; set; }
        public int milestoneId { get; set; }
        public int losMilestoneId { get; set; }
    }
    public class LoanIdModel
    {
        public string loanId { get; set; }
        public short losId { get; set; }
    }
    public class PostLoanApplicationModel
    {
        public int loanApplicationId { get; set; }
        public bool isDraft { get; set; }
    }

    public class LoanApplicationModel
    {
        public int? OpportunityId { get; set; }
        public int? LoanRequestId { get; set; }
        public int? BusinessUnitId { get; set; }
    }

    public class SendBorrowerEmailModel
    {
        public int loanApplicationId { get; set; }
        public string emailBody { get; set; }
        public int activityForId { get; set; }
    }
    public class GetLoanApplicationModel
    {
        public int loanApplicationId { get; set; }
    }

    public class UpdateLoanInfo
    {
        public int? loanApplicationId { get; set; }
        public DateTime? lastDocUploadDate { get; set; }
        public DateTime? lastDocRequestSentDate { get; set; }
        public int? remainingDocuments { get; set; }
        public int? outstandingDocuments { get; set; }
        public int? completedDocuments { get; set; }
    }

    public class SendEmailSupportTeam
    {
     
        public int loanApplicationId { get; set; }
        public string EmailBody { get; set; }
        public int TenantId { get; set; }
        public string ErrorDate { get; set; }
        public string ErrorCode { get; set; }
        public string DocumentName { get; set; }
        public string DocumentCategory { get; set; }
        public string DocumentExension { get; set; }
        public string Url { get; set; }
    }

    public class SupportEmailModel
    {
        public int tenantId { get; set; }
        public string milestone { get; set; }
        public string loanId { get; set; }
        public short losId { get; set; }
        public string url { get; set; }
    }
}
