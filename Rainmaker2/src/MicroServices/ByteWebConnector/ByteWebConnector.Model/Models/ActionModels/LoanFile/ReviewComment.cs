













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // ReviewComment

    public partial class ReviewComment 
    {
        public int Id { get; set; } // Id (Primary key)
        public string ReviewComment_ { get; set; } // ReviewComment (length: 1000)
        public string CustomerName { get; set; } // CustomerName (length: 500)
        public string Email { get; set; } // Email (length: 250)
        public int ReviewSiteId { get; set; } // ReviewSiteId
        public int? LoanPurposeId { get; set; } // LoanPurposeId
        public int BusinessUnitId { get; set; } // BusinessUnitId
        public int? StateId { get; set; } // StateId
        public int? CountyId { get; set; } // CountyId
        public int? CityId { get; set; } // CityId
        public string CityName { get; set; } // CityName (length: 500)
        public string ZipCode { get; set; } // ZipCode (length: 50)
        public string StreetAddress { get; set; } // StreetAddress (length: 500)
        public int? StarRating { get; set; } // StarRating
        public bool DisplayReviewPage { get; set; } // DisplayReviewPage
        public bool DisplayCustomerPage { get; set; } // DisplayCustomerPage
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted
        public bool IsActive { get; set; } // IsActive
        public int? DisplayOrder { get; set; } // DisplayOrder

        // Foreign keys

        /// <summary>
        /// Parent BusinessUnit pointed by [ReviewComment].([BusinessUnitId]) (FK_ReviewComment_BusinessUnit)
        /// </summary>
        public virtual BusinessUnit BusinessUnit { get; set; } // FK_ReviewComment_BusinessUnit

        /// <summary>
        /// Parent City pointed by [ReviewComment].([CityId]) (FK_ReviewComment_City)
        /// </summary>
        public virtual City City { get; set; } // FK_ReviewComment_City

        /// <summary>
        /// Parent County pointed by [ReviewComment].([CountyId]) (FK_ReviewComment_County)
        /// </summary>
        public virtual County County { get; set; } // FK_ReviewComment_County

        /// <summary>
        /// Parent LoanPurpose pointed by [ReviewComment].([LoanPurposeId]) (FK_ReviewComment_LoanPurpose)
        /// </summary>
        public virtual LoanPurpose LoanPurpose { get; set; } // FK_ReviewComment_LoanPurpose

        /// <summary>
        /// Parent ReviewSite pointed by [ReviewComment].([ReviewSiteId]) (FK_ReviewComment_ReviewSite)
        /// </summary>
        public virtual ReviewSite ReviewSite { get; set; } // FK_ReviewComment_ReviewSite

        /// <summary>
        /// Parent State pointed by [ReviewComment].([StateId]) (FK_ReviewComment_State)
        /// </summary>
        public virtual State State { get; set; } // FK_ReviewComment_State

        public ReviewComment()
        {
            EntityTypeId = 99;
            IsDeleted = false;
            IsActive = true;
            DisplayOrder = 0;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
