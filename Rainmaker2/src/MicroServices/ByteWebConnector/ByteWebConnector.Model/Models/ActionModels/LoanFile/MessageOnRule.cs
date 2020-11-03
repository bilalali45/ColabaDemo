













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // MessageOnRule

    public partial class MessageOnRule 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public string CustomerMessage { get; set; } // CustomerMessage (length: 1073741823)
        public string EmployeeMessage { get; set; } // EmployeeMessage (length: 1073741823)
        public int MessageTypeId { get; set; } // MessageTypeId
        public int MessageViewId { get; set; } // MessageViewId
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? SortNo { get; set; } // SortNo
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child LoanRequestMessageBinders where [LoanRequestMessageBinder].[MessageOnRuleId] point to this entity (FK_LoanRequestMessageBinder_MessageOnRule)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequestMessageBinder> LoanRequestMessageBinders { get; set; } // LoanRequestMessageBinder.FK_LoanRequestMessageBinder_MessageOnRule
        /// <summary>
        /// Child RuleMessages where [RuleMessage].[MessageId] point to this entity (FK_RuleMessage_MessageOnRule)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<RuleMessage> RuleMessages { get; set; } // RuleMessage.FK_RuleMessage_MessageOnRule

        public MessageOnRule()
        {
            MessageViewId = 1;
            IsActive = true;
            EntityTypeId = 111;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            LoanRequestMessageBinders = new System.Collections.Generic.HashSet<LoanRequestMessageBinder>();
            RuleMessages = new System.Collections.Generic.HashSet<RuleMessage>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
