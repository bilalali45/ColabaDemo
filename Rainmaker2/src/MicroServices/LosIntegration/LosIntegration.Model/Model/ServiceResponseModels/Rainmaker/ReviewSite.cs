













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // ReviewSite

    public partial class ReviewSite 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public string Url { get; set; } // Url (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted
        public int? ReviewsCounts { get; set; } // ReviewsCounts
        public decimal? StarRating { get; set; } // StarRating
        public string SystemName { get; set; } // SystemName (length: 100)

        // Reverse navigation

        /// <summary>
        /// Child ReviewComments where [ReviewComment].[ReviewSiteId] point to this entity (FK_ReviewComment_ReviewSite)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ReviewComment> ReviewComments { get; set; } // ReviewComment.FK_ReviewComment_ReviewSite

        public ReviewSite()
        {
            EntityTypeId = 108;
            ReviewComments = new System.Collections.Generic.HashSet<ReviewComment>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
