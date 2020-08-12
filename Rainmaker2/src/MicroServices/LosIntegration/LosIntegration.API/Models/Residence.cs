namespace LosIntegration.API.Models
{
    public class Residence
    {
        public int AppNo { get; set; }
        public int ResidenceId { get; set; }
        public int? BorrowerId { get; set; }
        public int DisplayOrder { get; set; }
        public bool Current { get; set; }
        public string FullAddress { get; set; }
        public string CityStateZip { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string LivingStatus { get; set; }
        public int? NoYears { get; set; }
        public int? NoMonths { get; set; }
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
        public int? MonthlyRent { get; set; }
        public int StreetContainsUnitNumberOv { get; set; }
        public long FileDataId { get; set; }


        public object GetRainmakerBorrowerResidence() 
        {
            throw new System.NotImplementedException();
        }
    }
}
