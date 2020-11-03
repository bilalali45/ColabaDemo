













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // LockDaysOnRule

    public partial class LockDaysOnRule 
    {
        public int Id { get; set; } // Id (Primary key)
        public int RuleId { get; set; } // RuleId
        public int ActionTypeId { get; set; } // ActionTypeId
        public int Value { get; set; } // Value
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsActive { get; set; } // IsActive
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted

        // Foreign keys

        /// <summary>
        /// Parent Rule pointed by [LockDaysOnRule].([RuleId]) (FK_LockDaysOnRule_Rule)
        /// </summary>
        public virtual Rule Rule { get; set; } // FK_LockDaysOnRule_Rule

        public LockDaysOnRule()
        {
            EntityTypeId = 146;
            IsActive = true;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
