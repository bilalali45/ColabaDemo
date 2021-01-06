using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class PromotionalProgram
    {
        public int Id { get; set; }
        public int BusinessUnitId { get; set; }
        public string PageName { get; set; }
        public int? StateId { get; set; }
        public int? CountyId { get; set; }
        public int? CityId { get; set; }
        public int? ZipCode { get; set; }
        public int PageId { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public bool IsSystem { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? ModifiedBy { get; set; }
        public int? LoanPurposeId { get; set; }
        public bool IsActive { get; set; }

        public BusinessUnit BusinessUnit { get; set; }

        public City City { get; set; }

        public County County { get; set; }

        public LoanPurpose LoanPurpose { get; set; }

        public State State { get; set; }
    }
}