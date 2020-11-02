













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // ContactEmailInfo

    public partial class ContactEmailInfo 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Email { get; set; } // Email (length: 150)
        public string Type { get; set; } // Type (length: 50)
        public bool? IsPrimary { get; set; } // IsPrimary
        public int? ContactId { get; set; } // ContactId
        public bool IsDeleted { get; set; } // IsDeleted
        public int? ValidityId { get; set; } // ValidityId
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? UseForId { get; set; } // UseForId

        // Reverse navigation

        /// <summary>
        /// Child FollowUps where [FollowUp].[ContactEmailId] point to this entity (FK_FollowUp_ContactEmailInfo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FollowUp> FollowUps { get; set; } // FollowUp.FK_FollowUp_ContactEmailInfo

        // Foreign keys

        /// <summary>
        /// Parent Contact pointed by [ContactEmailInfo].([ContactId]) (FK_ContactEmailInfo_Contact)
        /// </summary>
        public virtual Contact Contact { get; set; } // FK_ContactEmailInfo_Contact

        public ContactEmailInfo()
        {
            IsDeleted = false;
            EntityTypeId = 178;
            FollowUps = new System.Collections.Generic.HashSet<FollowUp>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
