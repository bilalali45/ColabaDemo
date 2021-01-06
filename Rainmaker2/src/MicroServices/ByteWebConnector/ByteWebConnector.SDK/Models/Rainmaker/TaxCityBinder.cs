namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class TaxCityBinder
    {
        public int PropertyTaxId { get; set; }
        public int CityId { get; set; }

        public City City { get; set; }

        public PropertyTax PropertyTax { get; set; }
    }
}