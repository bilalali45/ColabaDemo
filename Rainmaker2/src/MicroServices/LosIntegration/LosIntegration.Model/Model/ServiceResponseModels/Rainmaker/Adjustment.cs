













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // Adjustment

    public partial class Adjustment 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public int? RuleId { get; set; } // RuleId
        public int? CalcBaseOnId { get; set; } // CalcBaseOnId
        public int? AdjustmentTypeId { get; set; } // AdjustmentTypeId
        public int? RoundedTypeId { get; set; } // RoundedTypeId
        public decimal? Value { get; set; } // Value
        public int? FormulaId { get; set; } // FormulaId
        public int? RangeSetId { get; set; } // RangeSetId
        public int CalcTypeId { get; set; } // CalcTypeId
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted
        public bool IsActive { get; set; } // IsActive

        // Foreign keys

        /// <summary>
        /// Parent Formula pointed by [Adjustment].([FormulaId]) (FK_Adjustment_Formula)
        /// </summary>
        public virtual Formula Formula { get; set; } // FK_Adjustment_Formula

        /// <summary>
        /// Parent RangeSet pointed by [Adjustment].([RangeSetId]) (FK_Adjustment_RangeSet)
        /// </summary>
        public virtual RangeSet RangeSet { get; set; } // FK_Adjustment_RangeSet

        /// <summary>
        /// Parent Rule pointed by [Adjustment].([RuleId]) (FK_Adjustment_Rule)
        /// </summary>
        public virtual Rule Rule { get; set; } // FK_Adjustment_Rule

        public Adjustment()
        {
            EntityTypeId = 145;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
