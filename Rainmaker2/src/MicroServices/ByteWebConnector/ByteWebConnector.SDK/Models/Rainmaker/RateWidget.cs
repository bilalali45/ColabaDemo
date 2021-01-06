using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class RateWidget
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Apr { get; set; }
        public string Code { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public string Heading { get; set; }
        public bool IsActive { get; set; }
    }
}