using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class BorrowerConsentLog
    {
        public int Id { get; set; }
        public int? BorrowerConsentId { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool? IsAccepted { get; set; }
        public string IpAddress { get; set; }

        public BorrowerConsent BorrowerConsent { get; set; }
    }
}