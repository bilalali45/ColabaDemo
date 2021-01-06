namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class ContactAddress
    {
        public int Id { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? CountyId { get; set; }
        public string CityName { get; set; }
        public int? CityId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ZipPostalCode { get; set; }
        public string Type { get; set; }
        public int? ContactId { get; set; }
        public bool IsDeleted { get; set; }
        public int EntityTypeId { get; set; }

        public City City { get; set; }

        public Contact Contact { get; set; }

        public Country Country { get; set; }

        public County County { get; set; }

        public State State { get; set; }
    }
}