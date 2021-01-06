using System;

namespace ByteWebConnector.SDK.Models.Rainmaker
{
    public class ReviewComment
    {
        public int Id { get; set; }
        public string ReviewComment_ { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public int ReviewSiteId { get; set; }
        public int? LoanPurposeId { get; set; }
        public int BusinessUnitId { get; set; }
        public int? StateId { get; set; }
        public int? CountyId { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public string ZipCode { get; set; }
        public string StreetAddress { get; set; }
        public int? StarRating { get; set; }
        public bool DisplayReviewPage { get; set; }
        public bool DisplayCustomerPage { get; set; }
        public int EntityTypeId { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedOnUtc { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOnUtc { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public int? DisplayOrder { get; set; }

        public BusinessUnit BusinessUnit { get; set; }

        public City City { get; set; }

        public County County { get; set; }

        public LoanPurpose LoanPurpose { get; set; }

        public ReviewSite ReviewSite { get; set; }

        public State State { get; set; }
    }
}