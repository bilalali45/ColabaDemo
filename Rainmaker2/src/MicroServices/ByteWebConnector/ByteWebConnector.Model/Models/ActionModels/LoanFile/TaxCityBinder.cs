













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // TaxCityBinder

    public partial class TaxCityBinder 
    {
        public int PropertyTaxId { get; set; } // PropertyTaxId (Primary key)
        public int CityId { get; set; } // CityId (Primary key)

        // Foreign keys

        /// <summary>
        /// Parent City pointed by [TaxCityBinder].([CityId]) (FK_TaxCityBinder_City)
        /// </summary>
        public virtual City City { get; set; } // FK_TaxCityBinder_City

        /// <summary>
        /// Parent PropertyTax pointed by [TaxCityBinder].([PropertyTaxId]) (FK_TaxCityBinder_PropertyTax)
        /// </summary>
        public virtual PropertyTax PropertyTax { get; set; } // FK_TaxCityBinder_PropertyTax

        public TaxCityBinder()
        {
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
