// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 2.1
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace RainMaker.Entity.Models
{
    // BenchMarkRate

    public partial class BenchMarkRate : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ProductId { get; set; } // ProductId
        public decimal? YieldRate { get; set; } // YieldRate
        public System.DateTime? PriceDateUtc { get; set; } // PriceDateUtc
        public decimal? MinPrice { get; set; } // MinPrice
        public decimal? MaxPrice { get; set; } // MaxPrice
        public decimal? Threshold { get; set; } // Threshold
        public bool? HasWithProfit { get; set; } // HasWithProfit
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? LoanRequestId { get; set; } // LoanRequestId
        public int? QuoteResultId { get; set; } // QuoteResultId
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public int? BenchMarkId { get; set; } // BenchMarkId

        // Reverse navigation

        /// <summary>
        /// Child QuoteResults where [QuoteResult].[BenchMarkRateId] point to this entity (FK_QuoteResult_BenchMarkRate)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<QuoteResult> QuoteResults { get; set; } // QuoteResult.FK_QuoteResult_BenchMarkRate

        // Foreign keys

        /// <summary>
        /// Parent BenchMark pointed by [BenchMarkRate].([BenchMarkId]) (FK_BenchMarkRate_BenchMark)
        /// </summary>
        public virtual BenchMark BenchMark { get; set; } // FK_BenchMarkRate_BenchMark

        public BenchMarkRate()
        {
            EntityTypeId = 65;
            QuoteResults = new System.Collections.Generic.HashSet<QuoteResult>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
