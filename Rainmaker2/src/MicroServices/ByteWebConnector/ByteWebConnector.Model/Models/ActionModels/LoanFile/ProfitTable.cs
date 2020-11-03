













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // ProfitTable

    public partial class ProfitTable 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 250)
        public int RuleId { get; set; } // RuleId
        public int? CalcBaseOnId { get; set; } // CalcBaseOnId
        public int? RoundedTypeId { get; set; } // RoundedTypeId
        public decimal? Value { get; set; } // Value
        public int? FormulaId { get; set; } // FormulaId
        public int? RangeSetId { get; set; } // RangeSetId
        public int CalcTypeId { get; set; } // CalcTypeId
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted
        public bool IsActive { get; set; } // IsActive

        // Foreign keys

        /// <summary>
        /// Parent Formula pointed by [ProfitTable].([FormulaId]) (FK_ProfitTable_Formula)
        /// </summary>
        public virtual Formula Formula { get; set; } // FK_ProfitTable_Formula

        /// <summary>
        /// Parent RangeSet pointed by [ProfitTable].([RangeSetId]) (FK_ProfitTable_RangeSet)
        /// </summary>
        public virtual RangeSet RangeSet { get; set; } // FK_ProfitTable_RangeSet

        /// <summary>
        /// Parent Rule pointed by [ProfitTable].([RuleId]) (FK_ProfitTable_Rule)
        /// </summary>
        public virtual Rule Rule { get; set; } // FK_ProfitTable_Rule

        public ProfitTable()
        {
            EntityTypeId = 20;
            IsSystem = false;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
