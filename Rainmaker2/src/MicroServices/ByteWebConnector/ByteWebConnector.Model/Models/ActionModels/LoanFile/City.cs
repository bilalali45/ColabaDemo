













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // City

    public partial class City 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 200)
        public string Description { get; set; } // Description (length: 500)
        public int? StateId { get; set; } // StateId
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool IsDeleted { get; set; } // IsDeleted
        public string TpId { get; set; } // TpId (length: 50)

        // Reverse navigation

        /// <summary>
        /// Child AddressInfoes where [AddressInfo].[CityId] point to this entity (FK_AddressInfo_City)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AddressInfo> AddressInfoes { get; set; } // AddressInfo.FK_AddressInfo_City
        /// <summary>
        /// Child AdsGeoLocations where [AdsGeoLocation].[CityId] point to this entity (FK_AdsGeoLocation_City)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<AdsGeoLocation> AdsGeoLocations { get; set; } // AdsGeoLocation.FK_AdsGeoLocation_City
        /// <summary>
        /// Child Branches where [Branch].[CityId] point to this entity (FK_Branch_City)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Branch> Branches { get; set; } // Branch.FK_Branch_City
        /// <summary>
        /// Child ContactAddresses where [ContactAddress].[CityId] point to this entity (FK_ContactAddress_City)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ContactAddress> ContactAddresses { get; set; } // ContactAddress.FK_ContactAddress_City
        /// <summary>
        /// Child LoanRequests where [LoanRequest].[CityId] point to this entity (FK_LoanRequest_City)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<LoanRequest> LoanRequests { get; set; } // LoanRequest.FK_LoanRequest_City
        /// <summary>
        /// Child OfficeMetroBinders where [OfficeMetroBinder].[CityId] point to this entity (FK_OfficeMetroBinder_City)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<OfficeMetroBinder> OfficeMetroBinders { get; set; } // OfficeMetroBinder.FK_OfficeMetroBinder_City
        /// <summary>
        /// Child PromotionalPrograms where [PromotionalProgram].[CityId] point to this entity (FK_PromotionalProgram_City)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PromotionalProgram> PromotionalPrograms { get; set; } // PromotionalProgram.FK_PromotionalProgram_City
        /// <summary>
        /// Child ReviewComments where [ReviewComment].[CityId] point to this entity (FK_ReviewComment_City)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ReviewComment> ReviewComments { get; set; } // ReviewComment.FK_ReviewComment_City
        /// <summary>
        /// Child ReviewProperties where [ReviewProperty].[CityId] point to this entity (FK_ReviewProperty_City)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ReviewProperty> ReviewProperties { get; set; } // ReviewProperty.FK_ReviewProperty_City
        /// <summary>
        /// Child TaxCityBinders where [TaxCityBinder].[CityId] point to this entity (FK_TaxCityBinder_City)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<TaxCityBinder> TaxCityBinders { get; set; } // TaxCityBinder.FK_TaxCityBinder_City
        /// <summary>
        /// Child ZipCodes where [ZipCode].[CityId] point to this entity (FK_ZipCode_City)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<ZipCode> ZipCodes { get; set; } // ZipCode.FK_ZipCode_City

        // Foreign keys

        /// <summary>
        /// Parent State pointed by [City].([StateId]) (FK_City_State)
        /// </summary>
        public virtual State State { get; set; } // FK_City_State

        public City()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 119;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            AddressInfoes = new System.Collections.Generic.HashSet<AddressInfo>();
            AdsGeoLocations = new System.Collections.Generic.HashSet<AdsGeoLocation>();
            Branches = new System.Collections.Generic.HashSet<Branch>();
            ContactAddresses = new System.Collections.Generic.HashSet<ContactAddress>();
            LoanRequests = new System.Collections.Generic.HashSet<LoanRequest>();
            OfficeMetroBinders = new System.Collections.Generic.HashSet<OfficeMetroBinder>();
            PromotionalPrograms = new System.Collections.Generic.HashSet<PromotionalProgram>();
            ReviewComments = new System.Collections.Generic.HashSet<ReviewComment>();
            ReviewProperties = new System.Collections.Generic.HashSet<ReviewProperty>();
            TaxCityBinders = new System.Collections.Generic.HashSet<TaxCityBinder>();
            ZipCodes = new System.Collections.Generic.HashSet<ZipCode>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
