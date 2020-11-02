













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // ReviewProperty

    public partial class ReviewProperty 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? StateId { get; set; } // StateId
        public int? CountyId { get; set; } // CountyId
        public int? CityId { get; set; } // CityId
        public string CityName { get; set; } // CityName (length: 200)
        public string ZipCode { get; set; } // ZipCode (length: 20)
        public string StreetAddress { get; set; } // StreetAddress (length: 200)
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child ReviewPosteds where [ReviewPosted].[ReviewPropertyId] point to this entity (FK_ReviewPosted_ReviewProperty)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ReviewPosted> ReviewPosteds { get; set; } // ReviewPosted.FK_ReviewPosted_ReviewProperty

        // Foreign keys

        /// <summary>
        /// Parent City pointed by [ReviewProperty].([CityId]) (FK_ReviewProperty_City)
        /// </summary>
        public virtual City City { get; set; } // FK_ReviewProperty_City

        /// <summary>
        /// Parent County pointed by [ReviewProperty].([CountyId]) (FK_ReviewProperty_County)
        /// </summary>
        public virtual County County { get; set; } // FK_ReviewProperty_County

        /// <summary>
        /// Parent State pointed by [ReviewProperty].([StateId]) (FK_ReviewProperty_State)
        /// </summary>
        public virtual State State { get; set; } // FK_ReviewProperty_State

        public ReviewProperty()
        {
            IsActive = true;
            EntityTypeId = 90;
            IsDeleted = false;
            ReviewPosteds = new System.Collections.Generic.HashSet<ReviewPosted>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
