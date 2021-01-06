namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class BusinessUnitPhoneBinder
    {
        public int BusinessUnitId { get; set; }
        public int CompanyPhoneInfoId { get; set; }

        public BusinessUnit BusinessUnit { get; set; }

        public CompanyPhoneInfo CompanyPhoneInfo { get; set; }
    }
}