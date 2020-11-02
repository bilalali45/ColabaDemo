













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // TriggerValue

    public partial class TriggerValue 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public decimal? Value { get; set; } // Value
        public string ChangeType { get; set; } // ChangeType (length: 10)
        public int? ChangeField { get; set; } // ChangeField
        public int? RuleId { get; set; } // RuleId
        public bool IsDeleted { get; set; } // IsDeleted
        public int EntityTypeId { get; set; } // EntityTypeId

        // Foreign keys

        /// <summary>
        /// Parent Rule pointed by [TriggerValue].([RuleId]) (FK_TriggerValue_Rule)
        /// </summary>
        public virtual Rule Rule { get; set; } // FK_TriggerValue_Rule

        public TriggerValue()
        {
            IsDeleted = false;
            EntityTypeId = 152;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
