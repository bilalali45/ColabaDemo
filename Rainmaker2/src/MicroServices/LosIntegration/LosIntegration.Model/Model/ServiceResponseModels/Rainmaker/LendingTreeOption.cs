













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LendingTreeOption

    public partial class LendingTreeOption 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? LendingTreeLeadId { get; set; } // LendingTreeLeadId
        public int? OptionId { get; set; } // OptionId
        public string Option { get; set; } // Option (length: 500)

        // Foreign keys

        /// <summary>
        /// Parent LendingTreeLead pointed by [LendingTreeOption].([LendingTreeLeadId]) (FK_LendingTreeOption_LendingTreeLead)
        /// </summary>
        public virtual LendingTreeLead LendingTreeLead { get; set; } // FK_LendingTreeOption_LendingTreeLead

        public LendingTreeOption()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
