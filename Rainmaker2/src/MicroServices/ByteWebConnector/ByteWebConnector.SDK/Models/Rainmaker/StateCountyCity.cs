namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class StateCountyCity
    {
        public int Id { get; set; }
        public int? StateId { get; set; }
        public int? CountyId { get; set; }
        public int? CityId { get; set; }
        public string StateName { get; set; }
        public string Abbreviation { get; set; }
        public string CountyName { get; set; }
        public string CountyType { get; set; }
        public string CityName { get; set; }
        public string DisplayName { get; set; }
        public string SearchKey { get; set; }
        public string Ids { get; set; }
    }
}