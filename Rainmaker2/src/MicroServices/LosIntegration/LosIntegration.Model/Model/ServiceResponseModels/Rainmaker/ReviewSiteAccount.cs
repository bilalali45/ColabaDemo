













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // ReviewSiteAccount

    public partial class ReviewSiteAccount 
    {
        public int Id { get; set; } // Id (Primary key)
        public int ReviewSiteId { get; set; } // ReviewSiteId
        public int ReviewContactId { get; set; } // ReviewContactId
        public string UserName { get; set; } // UserName (length: 50)
        public string Password { get; set; } // Password (length: 50)
        public System.DateTime? PostedOnUtc { get; set; } // PostedOnUtc
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted

        // Foreign keys

        /// <summary>
        /// Parent ReviewContact pointed by [ReviewSiteAccount].([ReviewContactId]) (FK_ReviewSiteAccount_ReviewContact)
        /// </summary>
        public virtual ReviewContact ReviewContact { get; set; } // FK_ReviewSiteAccount_ReviewContact

        public ReviewSiteAccount()
        {
            IsActive = true;
            EntityTypeId = 7;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
