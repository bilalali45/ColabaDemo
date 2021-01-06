namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class TaxCountyBinder
    {
        public int PropertyTaxId { get; set; }
        public int CountyId { get; set; }

        public County County { get; set; }

        public PropertyTax PropertyTax { get; set; }
    }
}