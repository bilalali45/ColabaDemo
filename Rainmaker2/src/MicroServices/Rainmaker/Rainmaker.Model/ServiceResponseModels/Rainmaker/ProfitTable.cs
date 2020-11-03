using System;

namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class ProfitTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RuleId { get; set; }
        public int? CalcBaseOnId { get; set; }
        public int? RoundedTypeId { get; set; }
        public decimal? Value { get; set; }
        public int? FormulaId { get; set; }
        public int? RangeSetId { get; set; }
        public int CalcTypeId { get; set; }
        public int EntityTypeId { get; set; }
        public bool IsSystem { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }

        public Formula Formula { get; set; }

        public RangeSet RangeSet { get; set; }

        public Rule Rule { get; set; }
    }
}