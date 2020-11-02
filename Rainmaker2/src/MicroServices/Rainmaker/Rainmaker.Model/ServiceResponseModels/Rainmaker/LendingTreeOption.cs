namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class LendingTreeOption
    {
        public int Id { get; set; }
        public int? LendingTreeLeadId { get; set; }
        public int? OptionId { get; set; }
        public string Option { get; set; }

        public LendingTreeLead LendingTreeLead { get; set; }
    }
}