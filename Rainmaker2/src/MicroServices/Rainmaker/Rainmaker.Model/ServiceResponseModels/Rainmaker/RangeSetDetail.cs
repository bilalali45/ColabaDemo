namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class RangeSetDetail
    {
        public int Id { get; set; }
        public int? RangeSetId { get; set; }
        public decimal? RangeFrom { get; set; }
        public decimal? RangeTo { get; set; }
        public decimal? FixedValue { get; set; }
        public decimal? Percentage { get; set; }
        public int? FormulaId { get; set; }

        public Formula Formula { get; set; }

        public RangeSet RangeSet { get; set; }
    }
}