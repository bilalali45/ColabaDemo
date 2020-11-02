













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // LoanRequestMessageBinder

    public partial class LoanRequestMessageBinder 
    {
        public int LoanRequestId { get; set; } // LoanRequestId (Primary key)
        public int MessageOnRuleId { get; set; } // MessageOnRuleId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent LoanRequest pointed by [LoanRequestMessageBinder].([LoanRequestId]) (FK_LoanRequestMessageBinder_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_LoanRequestMessageBinder_LoanRequest

        /// <summary>
        /// Parent MessageOnRule pointed by [LoanRequestMessageBinder].([MessageOnRuleId]) (FK_LoanRequestMessageBinder_MessageOnRule)
        /// </summary>
        public virtual MessageOnRule MessageOnRule { get; set; } // FK_LoanRequestMessageBinder_MessageOnRule

        public LoanRequestMessageBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
