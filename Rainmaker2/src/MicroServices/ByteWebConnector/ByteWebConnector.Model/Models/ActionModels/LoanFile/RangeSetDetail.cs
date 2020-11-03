













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // RangeSetDetail

    public partial class RangeSetDetail 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? RangeSetId { get; set; } // RangeSetId
        public decimal? RangeFrom { get; set; } // RangeFrom
        public decimal? RangeTo { get; set; } // RangeTo
        public decimal? FixedValue { get; set; } // FixedValue
        public decimal? Percentage { get; set; } // Percentage
        public int? FormulaId { get; set; } // FormulaId

        // Foreign keys

        /// <summary>
        /// Parent Formula pointed by [RangeSetDetail].([FormulaId]) (FK_RangeSetDetail_Formula)
        /// </summary>
        public virtual Formula Formula { get; set; } // FK_RangeSetDetail_Formula

        /// <summary>
        /// Parent RangeSet pointed by [RangeSetDetail].([RangeSetId]) (FK_RangeSetDetail_RangeSet)
        /// </summary>
        public virtual RangeSet RangeSet { get; set; } // FK_RangeSetDetail_RangeSet

        public RangeSetDetail()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
