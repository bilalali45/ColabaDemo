













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // ReviewContact

    public partial class ReviewContact 
    {
        public int Id { get; set; } // Id (Primary key)
        public string CustomerFirstName { get; set; } // CustomerFirstName (length: 50)
        public string CustomerLastName { get; set; } // CustomerLastName (length: 50)
        public string CustomerAlias { get; set; } // CustomerAlias (length: 50)
        public string EmailAddress { get; set; } // EmailAddress (length: 50)
        public string EmailPassword { get; set; } // EmailPassword (length: 50)
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child ReviewPosteds where [ReviewPosted].[ReviewContactId] point to this entity (FK_ReviewPosted_ReviewContact)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ReviewPosted> ReviewPosteds { get; set; } // ReviewPosted.FK_ReviewPosted_ReviewContact
        /// <summary>
        /// Child ReviewSiteAccounts where [ReviewSiteAccount].[ReviewContactId] point to this entity (FK_ReviewSiteAccount_ReviewContact)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ReviewSiteAccount> ReviewSiteAccounts { get; set; } // ReviewSiteAccount.FK_ReviewSiteAccount_ReviewContact

        public ReviewContact()
        {
            IsActive = true;
            EntityTypeId = 81;
            IsDeleted = false;
            ReviewPosteds = new System.Collections.Generic.HashSet<ReviewPosted>();
            ReviewSiteAccounts = new System.Collections.Generic.HashSet<ReviewSiteAccount>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
