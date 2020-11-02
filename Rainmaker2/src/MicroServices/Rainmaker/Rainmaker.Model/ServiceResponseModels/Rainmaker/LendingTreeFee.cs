namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class LendingTreeFee
    {
        public int Id { get; set; }
        public int? LendingTreeLeadId { get; set; }
        public int? FeeId { get; set; }
        public string FeeValue { get; set; }
        public string FeeDescription { get; set; }

        public LendingTreeLead LendingTreeLead { get; set; }
    }
}