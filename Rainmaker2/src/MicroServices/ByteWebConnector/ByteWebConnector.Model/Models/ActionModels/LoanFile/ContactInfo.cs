













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // ContactInfo

    public partial class ContactInfo 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Info { get; set; } // Info (length: 150)
        public string Type { get; set; } // Type (length: 50)
        public string InfoType { get; set; } // InfoType (length: 50)
        public bool? IsPrimary { get; set; } // IsPrimary
        public int? ContactId { get; set; } // ContactId
        public bool IsDeleted { get; set; } // IsDeleted
        public int? ValidityId { get; set; } // ValidityId
        public int EntityTypeId { get; set; } // EntityTypeId
        public int? UseForId { get; set; } // UseForId

        // Foreign keys

        /// <summary>
        /// Parent Contact pointed by [ContactInfo].([ContactId]) (FK_ContactInfo_Contact)
        /// </summary>
        public virtual Contact Contact { get; set; } // FK_ContactInfo_Contact

        public ContactInfo()
        {
            IsDeleted = false;
            EntityTypeId = 29;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
