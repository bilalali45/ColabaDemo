namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class LendingTreeScore
    {
        public int Id { get; set; }
        public int? LendingTreeLeadId { get; set; }
        public int? ScoreId { get; set; }
        public string ScoreValue { get; set; }
        public string ScoreDescription { get; set; }

        public LendingTreeLead LendingTreeLead { get; set; }
    }
}