













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // ReviewPosted

    public partial class ReviewPosted 
    {
        public int Id { get; set; } // Id (Primary key)
        public int ReviewContactId { get; set; } // ReviewContactId
        public int ReviewCommentId { get; set; } // ReviewCommentId
        public int ReviewPropertyId { get; set; } // ReviewPropertyId
        public string ReviewUrl { get; set; } // ReviewUrl
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted

        // Foreign keys

        /// <summary>
        /// Parent ReviewContact pointed by [ReviewPosted].([ReviewContactId]) (FK_ReviewPosted_ReviewContact)
        /// </summary>
        public virtual ReviewContact ReviewContact { get; set; } // FK_ReviewPosted_ReviewContact

        /// <summary>
        /// Parent ReviewProperty pointed by [ReviewPosted].([ReviewPropertyId]) (FK_ReviewPosted_ReviewProperty)
        /// </summary>
        public virtual ReviewProperty ReviewProperty { get; set; } // FK_ReviewPosted_ReviewProperty

        public ReviewPosted()
        {
            IsActive = true;
            EntityTypeId = 39;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
