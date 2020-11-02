













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LendingTreeScore

    public partial class LendingTreeScore 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? LendingTreeLeadId { get; set; } // LendingTreeLeadId
        public int? ScoreId { get; set; } // ScoreId
        public string ScoreValue { get; set; } // ScoreValue (length: 500)
        public string ScoreDescription { get; set; } // ScoreDescription (length: 500)

        // Foreign keys

        /// <summary>
        /// Parent LendingTreeLead pointed by [LendingTreeScore].([LendingTreeLeadId]) (FK_LendingTreeScore_LendingTreeLead)
        /// </summary>
        public virtual LendingTreeLead LendingTreeLead { get; set; } // FK_LendingTreeScore_LendingTreeLead

        public LendingTreeScore()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
