













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // BenchMark

    public partial class BenchMark 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? BusinessUnitId { get; set; } // BusinessUnitId
        public int? LoanRequestId { get; set; } // LoanRequestId
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDeleted { get; set; } // IsDeleted
        public bool IsActive { get; set; } // IsActive

        // Reverse navigation

        /// <summary>
        /// Child BenchMarkRates where [BenchMarkRate].[BenchMarkId] point to this entity (FK_BenchMarkRate_BenchMark)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BenchMarkRate> BenchMarkRates { get; set; } // BenchMarkRate.FK_BenchMarkRate_BenchMark

        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [BenchMark].([BusinessUnitId]) (FK_BenchMark_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_BenchMark_BusinessUnit

        /// <summary>
        /// Parent LoanRequest pointed by [BenchMark].([LoanRequestId]) (FK_BenchMark_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_BenchMark_LoanRequest

        public BenchMark()
        {
            EntityTypeId = 62;
            BenchMarkRates = new System.Collections.Generic.HashSet<BenchMarkRate>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
