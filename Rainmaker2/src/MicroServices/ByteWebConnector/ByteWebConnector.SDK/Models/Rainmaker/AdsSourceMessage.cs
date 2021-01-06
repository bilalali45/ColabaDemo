namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class AdsSourceMessage
    {
        public int Id { get; set; }
        public int? AdSourceId { get; set; }
        public int? LocalStringResourceId { get; set; }
        public string ResourceName { get; set; }
        public int? MessageLocationId { get; set; }

        public AdsSource AdsSource { get; set; }

        public LocaleStringResource LocaleStringResource { get; set; }
    }
}