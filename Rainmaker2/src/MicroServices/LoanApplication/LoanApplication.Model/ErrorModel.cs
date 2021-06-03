using System;

namespace LoanApplication.Model
{
    public class ErrorModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    public class LoanSummary
    {
        public int Id { get; set; }
        public string LoanPurpose { get; set; }
        public string StateName { get; set; }
        public string CountyName { get; set; }
        public string CityName { get; set; }
        public string StreetAddress { get; set; }
        public string ZipCode { get; set; }
        public decimal? LoanAmount { get; set; }
        public string CountryName { get; set; }
        public string UnitNumber { get; set; }
        public int MileStoneId { get; set; }
        public string MileStone { get; set; }
    }
}
