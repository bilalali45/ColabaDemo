using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class CurrentRateView
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? RateTypeId { get; set; }
        public DateTime? PriceDateUtc { get; set; }
        public decimal? Price { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Apr { get; set; }
        public int? BusinessUnitId { get; set; }
        public int RateParameterId { get; set; }
        public int? LeadSourceId { get; set; }
        public string PrdouctName { get; set; }
        public string LoanTypeName { get; set; }
        public string ProductFamilyName { get; set; }
        public int? ProductAdRateTypeId { get; set; }
        public int? ProductFamilyId { get; set; }
        public decimal? MaxLoanAmount { get; set; }
        public int? DisplayOrder { get; set; }
        public int? PFamilyDisplayOrder { get; set; }
        public string ProductAliasName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductDetail { get; set; }
    }
}