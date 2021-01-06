namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class AddressInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public string CountryName { get; set; }
        public int? StateId { get; set; }
        public string StateName { get; set; }
        public int? CountyId { get; set; }
        public string CountyName { get; set; }
        public string CityName { get; set; }
        public int? CityId { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public string UnitNo { get; set; }
        public bool IsDeleted { get; set; }
        public int EntityTypeId { get; set; }

        public City City { get; set; }

        public Country Country { get; set; }

        public County County { get; set; }

        public State State { get; set; }
    }
}