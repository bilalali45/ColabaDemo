













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // DefaultLoanParameter

    public partial class DefaultLoanParameter 
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
        public bool? IsSystem { get; set; } // IsSystem

        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [DefaultLoanParameter].([BusinessUnitId]) (FK_DefaultLoanParameter_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_DefaultLoanParameter_BusinessUnit

        /// <summary>
        /// Parent LoanRequest pointed by [DefaultLoanParameter].([LoanRequestId]) (FK_DefaultLoanParameter_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_DefaultLoanParameter_LoanRequest

        public DefaultLoanParameter()
        {
            EntityTypeId = 229;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
