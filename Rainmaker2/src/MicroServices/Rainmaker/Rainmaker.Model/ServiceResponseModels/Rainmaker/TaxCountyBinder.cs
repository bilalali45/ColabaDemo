namespace Rainmaker.Model.ServiceResponseModels.Rainmaker
{
    public class TaxCountyBinder
    {
        public int PropertyTaxId { get; set; }
        public int CountyId { get; set; }

        public County County { get; set; }

        public PropertyTax PropertyTax { get; set; }
    }
}