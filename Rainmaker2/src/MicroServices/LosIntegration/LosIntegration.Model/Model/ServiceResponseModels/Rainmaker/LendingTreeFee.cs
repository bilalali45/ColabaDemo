













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LendingTreeFee

    public partial class LendingTreeFee 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? LendingTreeLeadId { get; set; } // LendingTreeLeadId
        public int? FeeId { get; set; } // FeeId
        public string FeeValue { get; set; } // FeeValue (length: 500)
        public string FeeDescription { get; set; } // FeeDescription (length: 500)

        // Foreign keys

        /// <summary>
        /// Parent LendingTreeLead pointed by [LendingTreeFee].([LendingTreeLeadId]) (FK_LendingTreeFee_LendingTreeLead)
        /// </summary>
        public virtual LendingTreeLead LendingTreeLead { get; set; } // FK_LendingTreeFee_LendingTreeLead

        public LendingTreeFee()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
