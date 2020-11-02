













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // PaymentOn

    public partial class PaymentOn 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? Month { get; set; } // Month
        public int? NoOfMonths { get; set; } // NoOfMonths
        public int? PropertyTaxId { get; set; } // PropertyTaxId

        // Foreign keys

        /// <summary>
        /// Parent PropertyTax pointed by [PaymentOn].([PropertyTaxId]) (FK_PaymentOn_PropertyTax)
        /// </summary>
        public virtual PropertyTax PropertyTax { get; set; } // FK_PaymentOn_PropertyTax

        public PaymentOn()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
