













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // ContactPhoneInfo

    public partial class ContactPhoneInfo 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Phone { get; set; } // Phone (length: 150)
        public string Type { get; set; } // Type (length: 50)
        public bool? IsPrimary { get; set; } // IsPrimary
        public int? ContactId { get; set; } // ContactId
        public bool IsDeleted { get; set; } // IsDeleted
        public int? ValidityId { get; set; } // ValidityId
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? UseForId { get; set; } // UseForId

        // Reverse navigation

        /// <summary>
        /// Child FollowUps where [FollowUp].[ContactPhoneId] point to this entity (FK_FollowUp_ContactPhoneInfo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FollowUp> FollowUps { get; set; } // FollowUp.FK_FollowUp_ContactPhoneInfo

        // Foreign keys

        /// <summary>
        /// Parent Contact pointed by [ContactPhoneInfo].([ContactId]) (FK_ContactPhoneInfo_Contact)
        /// </summary>
        public virtual Contact Contact { get; set; } // FK_ContactPhoneInfo_Contact

        public ContactPhoneInfo()
        {
            IsDeleted = false;
            EntityTypeId = 177;
            FollowUps = new System.Collections.Generic.HashSet<FollowUp>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>