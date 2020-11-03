













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // TaxCountyBinder

    public partial class TaxCountyBinder 
    {
        public int PropertyTaxId { get; set; } // PropertyTaxId (Primary key)
        public int CountyId { get; set; } // CountyId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent County pointed by [TaxCountyBinder].([CountyId]) (FK_TaxCountyBinder_County)
        /// </summary>
        public virtual County County { get; set; } // FK_TaxCountyBinder_County

        /// <summary>
        /// Parent PropertyTax pointed by [TaxCountyBinder].([PropertyTaxId]) (FK_TaxCountyBinder_PropertyTax)
        /// </summary>
        public virtual PropertyTax PropertyTax { get; set; } // FK_TaxCountyBinder_PropertyTax

        public TaxCountyBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
