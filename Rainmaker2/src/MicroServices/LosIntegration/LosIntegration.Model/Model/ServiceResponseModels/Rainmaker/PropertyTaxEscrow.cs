













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // PropertyTaxEscrow

    public partial class PropertyTaxEscrow 
    {
        public int Id { get; set; } // Id (Primary key)
        public int PaidById { get; set; } // PaidById
        public int PropertyDetailId { get; set; } // PropertyDetailId
        public decimal AnnuallyPayment { get; set; } // AnnuallyPayment
        public int? EscrowMonth { get; set; } // EscrowMonth
        public int? EscrowEntityTypeId { get; set; } // EscrowEntityTypeId
        public decimal? PrePaid { get; set; } // PrePaid
        public int? PrePaidMonth { get; set; } // PrePaidMonth

        // Foreign keys

        /// <summary>
        /// Parent EscrowEntityType pointed by [PropertyTaxEscrow].([EscrowEntityTypeId]) (FK_PropertyTaxEscrow_EscrowEntityType)
        /// </summary>
        public virtual EscrowEntityType EscrowEntityType { get; set; } // FK_PropertyTaxEscrow_EscrowEntityType

        /// <summary>
        /// Parent PaidBy pointed by [PropertyTaxEscrow].([PaidById]) (FK_PropertyTaxEscrow_PaidBy)
        /// </summary>
        public virtual PaidBy PaidBy { get; set; } // FK_PropertyTaxEscrow_PaidBy

        /// <summary>
        /// Parent PropertyInfo pointed by [PropertyTaxEscrow].([PropertyDetailId]) (FK_PropertyTaxEscrow_PropertyInfo)
        /// </summary>
        public virtual PropertyInfo PropertyInfo { get; set; } // FK_PropertyTaxEscrow_PropertyInfo

        public PropertyTaxEscrow()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
