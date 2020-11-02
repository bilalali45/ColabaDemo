namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class LendingTreeLoanRequestPurpose
    {
        public int Id { get; set; }
        public int? LendingTreeLeadId { get; set; }
        public int? LoanRequestPurposeId { get; set; }
        public string LoanRequestPurpose { get; set; }

        public LendingTreeLead LendingTreeLead { get; set; }
    }
}