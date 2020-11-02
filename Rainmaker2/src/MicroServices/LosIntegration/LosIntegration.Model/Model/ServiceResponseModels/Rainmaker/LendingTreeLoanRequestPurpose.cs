













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // LendingTreeLoanRequestPurpose

    public partial class LendingTreeLoanRequestPurpose 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? LendingTreeLeadId { get; set; } // LendingTreeLeadId
        public int? LoanRequestPurposeId { get; set; } // LoanRequestPurposeId
        public string LoanRequestPurpose { get; set; } // LoanRequestPurpose (length: 500)

        // Foreign keys

        /// <summary>
        /// Parent LendingTreeLead pointed by [LendingTreeLoanRequestPurpose].([LendingTreeLeadId]) (FK_LendingTreeLoanRequestPurpose_LendingTreeLead)
        /// </summary>
        public virtual LendingTreeLead LendingTreeLead { get; set; } // FK_LendingTreeLoanRequestPurpose_LendingTreeLead

        public LendingTreeLoanRequestPurpose()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
