













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // State

    public partial class State 
    {
        public int Id { get; set; } // Id (Primary key)
        public int CountryId { get; set; } // CountryId
        public string Name { get; set; } // Name (length: 150)
        public string Abbreviation { get; set; } // Abbreviation (length: 50)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public bool IsLicenseActive { get; set; } // IsLicenseActive
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted
        public int? CushionMonth { get; set; } // CushionMonth
        public int? TaxMonth { get; set; } // TaxMonth
        public int? InsuranceMonth { get; set; } // InsuranceMonth

        // Reverse navigation

        /// <summary>
        /// Child AddressInfoes where [AddressInfo].[StateId] point to this entity (FK_AddressInfo_State)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AddressInfo> AddressInfoes { get; set; } // AddressInfo.FK_AddressInfo_State
        /// <summary>
        /// Child AdsGeoLocations where [AdsGeoLocation].[StateId] point to this entity (FK_AdsGeoLocation_State)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AdsGeoLocation> AdsGeoLocations { get; set; } // AdsGeoLocation.FK_AdsGeoLocation_State
        /// <summary>
        /// Child BankRateInstances where [BankRateInstance].[StateId] point to this entity (FK_BankRateInstance_State)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BankRateInstance> BankRateInstances { get; set; } // BankRateInstance.FK_BankRateInstance_State
        /// <summary>
        /// Child Branches where [Branch].[StateId] point to this entity (FK_Branch_State)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Branch> Branches { get; set; } // Branch.FK_Branch_State
        /// <summary>
        /// Child Cities where [City].[StateId] point to this entity (FK_City_State)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<City> Cities { get; set; } // City.FK_City_State
        /// <summary>
        /// Child ContactAddresses where [ContactAddress].[StateId] point to this entity (FK_ContactAddress_State)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ContactAddress> ContactAddresses { get; set; } // ContactAddress.FK_ContactAddress_State
        /// <summary>
        /// Child Counties where [County].[StateId] point to this entity (FK_County_State)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<County> Counties { get; set; } // County.FK_County_State
        /// <summary>
        /// Child EmployeeCsrLoBinders where [EmployeeCsrLoBinder].[StateId] point to this entity (FK_EmployeeCsrLoBinder_State)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmployeeCsrLoBinder> EmployeeCsrLoBinders { get; set; } // EmployeeCsrLoBinder.FK_EmployeeCsrLoBinder_State
        /// <summary>
        /// Child EmployeeLicenses where [EmployeeLicense].[StateId] point to this entity (FK_EmployeeLicense_State)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmployeeLicense> EmployeeLicenses { get; set; } // EmployeeLicense.FK_EmployeeLicense_State
        /// <summary>
        /// Child LoanRequests where [LoanRequest].[StateId] point to this entity (FK_LoanRequest_State)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; } // LoanRequest.FK_LoanRequest_State
        /// <summary>
        /// Child OfficeMetroBinders where [OfficeMetroBinder].[StateId] point to this entity (FK_OfficeMetroBinder_State)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OfficeMetroBinder> OfficeMetroBinders { get; set; } // OfficeMetroBinder.FK_OfficeMetroBinder_State
        /// <summary>
        /// Child PromotionalPrograms where [PromotionalProgram].[StateId] point to this entity (FK_PromotionalProgram_State)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PromotionalProgram> PromotionalPrograms { get; set; } // PromotionalProgram.FK_PromotionalProgram_State
        /// <summary>
        /// Child PropertyTaxes where [PropertyTax].[StateId] point to this entity (FK_PropertyTax_State)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PropertyTax> PropertyTaxes { get; set; } // PropertyTax.FK_PropertyTax_State
        /// <summary>
        /// Child ReviewComments where [ReviewComment].[StateId] point to this entity (FK_ReviewComment_State)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ReviewComment> ReviewComments { get; set; } // ReviewComment.FK_ReviewComment_State
        /// <summary>
        /// Child ReviewProperties where [ReviewProperty].[StateId] point to this entity (FK_ReviewProperty_State)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ReviewProperty> ReviewProperties { get; set; } // ReviewProperty.FK_ReviewProperty_State
        /// <summary>
        /// Child ZipCodes where [ZipCode].[StateId] point to this entity (FK_ZipCode_State)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ZipCode> ZipCodes { get; set; } // ZipCode.FK_ZipCode_State

        // Foreign keys

        /// <summary>
        /// Parent Country pointed by [State].([CountryId]) (FK_State_Country)
        /// </summary>
        public virtual Country Country { get; set; } // FK_State_Country

        public State()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 112;
            IsDefault = false;
            IsSystem = false;
            IsLicenseActive = true;
            IsDeleted = false;
            AddressInfoes = new System.Collections.Generic.HashSet<AddressInfo>();
            AdsGeoLocations = new System.Collections.Generic.HashSet<AdsGeoLocation>();
            BankRateInstances = new System.Collections.Generic.HashSet<BankRateInstance>();
            Branches = new System.Collections.Generic.HashSet<Branch>();
            Cities = new System.Collections.Generic.HashSet<City>();
            ContactAddresses = new System.Collections.Generic.HashSet<ContactAddress>();
            Counties = new System.Collections.Generic.HashSet<County>();
            EmployeeCsrLoBinders = new System.Collections.Generic.HashSet<EmployeeCsrLoBinder>();
            EmployeeLicenses = new System.Collections.Generic.HashSet<EmployeeLicense>();
            LoanRequests = new System.Collections.Generic.HashSet<LoanRequest>();
            OfficeMetroBinders = new System.Collections.Generic.HashSet<OfficeMetroBinder>();
            PromotionalPrograms = new System.Collections.Generic.HashSet<PromotionalProgram>();
            PropertyTaxes = new System.Collections.Generic.HashSet<PropertyTax>();
            ReviewComments = new System.Collections.Generic.HashSet<ReviewComment>();
            ReviewProperties = new System.Collections.Generic.HashSet<ReviewProperty>();
            ZipCodes = new System.Collections.Generic.HashSet<ZipCode>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
