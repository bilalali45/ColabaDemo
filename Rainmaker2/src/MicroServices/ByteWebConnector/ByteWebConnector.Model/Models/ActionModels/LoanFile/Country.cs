













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // Country

    public partial class Country 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string TwoLetterIsoCode { get; set; } // TwoLetterIsoCode (length: 2)
        public string ThreeLetterIsoCode { get; set; } // ThreeLetterIsoCode (length: 3)
        public int NumericIsoCode { get; set; } // NumericIsoCode
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child AddressInfoes where [AddressInfo].[CountryId] point to this entity (FK_AddressInfo_Country)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AddressInfo> AddressInfoes { get; set; } // AddressInfo.FK_AddressInfo_Country
        /// <summary>
        /// Child ContactAddresses where [ContactAddress].[CountryId] point to this entity (FK_ContactAddress_Country)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ContactAddress> ContactAddresses { get; set; } // ContactAddress.FK_ContactAddress_Country
        /// <summary>
        /// Child States where [State].[CountryId] point to this entity (FK_State_Country)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<State> States { get; set; } // State.FK_State_Country

        public Country()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 30;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            AddressInfoes = new System.Collections.Generic.HashSet<AddressInfo>();
            ContactAddresses = new System.Collections.Generic.HashSet<ContactAddress>();
            States = new System.Collections.Generic.HashSet<State>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
