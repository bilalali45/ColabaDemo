namespace ByteWebConnector.API.Models
{
    public class Residence
    {
        public int AppNo { get; set; }
        public int ResidenceID { get; set; }
        public int? BorrowerID { get; set; }
        public int DisplayOrder { get; set; }
        public bool Current { get; set; }
        public string FullAddress { get; set; }
        public string CityStateZip { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int LivingStatus { get; set; }
        public string NoYears { get; set; }
        public string NoMonths { get; set; }
        public string LLName { get; set; }
        public string LLAttn { get; set; }
        public string LLFullAddress { get; set; }
        public string LLCityStateZip { get; set; }
        public string LLStreet { get; set; }
        public string LLCity { get; set; }
        public string LLState { get; set; }
        public string LLZip { get; set; }
        public string LLPhone { get; set; }
        public string Notes { get; set; }
        public string LLFax { get; set; }
        public string Country { get; set; }
        public object MonthlyRent { get; set; }
        public int StreetContainsUnitNumberOV { get; set; }
        public long FileDataID { get; set; }

    }
}
