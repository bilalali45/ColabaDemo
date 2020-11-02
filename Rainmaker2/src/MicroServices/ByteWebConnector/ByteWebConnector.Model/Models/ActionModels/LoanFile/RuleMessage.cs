













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // RuleMessage

    public partial class RuleMessage 
    {
        public int MessageId { get; set; } // MessageId (Primary key)
        public int RuleId { get; set; } // RuleId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent MessageOnRule pointed by [RuleMessage].([MessageId]) (FK_RuleMessage_MessageOnRule)
        /// </summary>
        public virtual MessageOnRule MessageOnRule { get; set; } // FK_RuleMessage_MessageOnRule

        /// <summary>
        /// Parent Rule pointed by [RuleMessage].([RuleId]) (FK_RuleMessage_Rule)
        /// </summary>
        public virtual Rule Rule { get; set; } // FK_RuleMessage_Rule

        public RuleMessage()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
