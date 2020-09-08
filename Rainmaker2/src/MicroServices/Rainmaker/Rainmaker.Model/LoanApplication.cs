using System;

namespace Rainmaker.Model
{
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
}
