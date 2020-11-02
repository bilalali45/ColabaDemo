













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // ContactAddress

    public partial class ContactAddress 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? CountryId { get; set; } // CountryId
        public int? StateId { get; set; } // StateId
        public int? CountyId { get; set; } // CountyId
        public string CityName { get; set; } // CityName (length: 200)
        public int? CityId { get; set; } // CityId
        public string Address1 { get; set; } // Address1
        public string Address2 { get; set; } // Address2
        public string ZipPostalCode { get; set; } // ZipPostalCode
        public string Type { get; set; } // Type (length: 50)
        public int? ContactId { get; set; } // ContactId
        public bool IsDeleted { get; set; } // IsDeleted
        public int EntityTypeId { get; set; } // EntityTypeId

        // Foreign keys

        /// <summary>
        /// Parent City pointed by [ContactAddress].([CityId]) (FK_ContactAddress_City)
        /// </summary>
        public virtual City City { get; set; } // FK_ContactAddress_City

        /// <summary>
        /// Parent Contact pointed by [ContactAddress].([ContactId]) (FK_ContactAddress_Contact)
        /// </summary>
        public virtual Contact Contact { get; set; } // FK_ContactAddress_Contact

        /// <summary>
        /// Parent Country pointed by [ContactAddress].([CountryId]) (FK_ContactAddress_Country)
        /// </summary>
        public virtual Country Country { get; set; } // FK_ContactAddress_Country

        /// <summary>
        /// Parent County pointed by [ContactAddress].([CountyId]) (FK_ContactAddress_County)
        /// </summary>
        public virtual County County { get; set; } // FK_ContactAddress_County

        /// <summary>
        /// Parent State pointed by [ContactAddress].([StateId]) (FK_ContactAddress_State)
        /// </summary>
        public virtual State State { get; set; } // FK_ContactAddress_State

        public ContactAddress()
        {
            IsDeleted = false;
            EntityTypeId = 92;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
