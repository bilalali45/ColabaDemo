













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // SecondLien

    public partial class SecondLien 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? LoanRequestId { get; set; } // LoanRequestId
        public int? SecondLienTypeId { get; set; } // SecondLienTypeId
        public decimal? SecondLienBalance { get; set; } // SecondLienBalance
        public decimal? SecondLienLimit { get; set; } // SecondLienLimit
        public bool SecondLienPaidAtClosing { get; set; } // SecondLienPaidAtClosing
        public bool WasSmTaken { get; set; } // WasSmTaken

        // Foreign keys

        /// <summary>
        /// Parent LoanRequest pointed by [SecondLien].([LoanRequestId]) (FK_SecondLien_LoanRequest)
        /// </summary>
        public virtual LoanRequest LoanRequest { get; set; } // FK_SecondLien_LoanRequest

        /// <summary>
        /// Parent SecondLienType pointed by [SecondLien].([SecondLienTypeId]) (FK_SecondLien_SecondLienType)
        /// </summary>
        public virtual SecondLienType SecondLienType { get; set; } // FK_SecondLien_SecondLienType

        public SecondLien()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
