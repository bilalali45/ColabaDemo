namespace ByteWebConnector.API.Models.ClientModels
{
    public class SubPropertyEntity
    {
        public string City { get; set; }
        public long FileDataId { get; set; }
        public decimal? OriginalPurchasePrice { get; set; }
        public int? DateAcquiredYear { get; set; }
        public decimal? PropertyValue { get; set; }
        public int? PAddressUnitNo { get; set; }
        public string PAddressZipCode { get; set; }
        public string PAddressStreet { get; set; }
        public string CountyName { get; set; }
        public string Abbreviation { get; set; }
        public string PAddressCityName { get; set; }
    }
}