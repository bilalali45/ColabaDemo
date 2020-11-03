













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // AddressInfo

    public partial class AddressInfo 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public int? CountryId { get; set; } // CountryId
        public string CountryName { get; set; } // CountryName (length: 200)
        public int? StateId { get; set; } // StateId
        public string StateName { get; set; } // StateName (length: 200)
        public int? CountyId { get; set; } // CountyId
        public string CountyName { get; set; } // CountyName (length: 200)
        public string CityName { get; set; } // CityName (length: 200)
        public int? CityId { get; set; } // CityId
        public string StreetAddress { get; set; } // StreetAddress (length: 500)
        public string ZipCode { get; set; } // ZipCode (length: 10)
        public string UnitNo { get; set; } // UnitNo (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted
        public int EntityTypeId { get; set; } // EntityTypeId

        // Reverse navigation

        /// <summary>
        /// Child BorrowerLiabilities where [BorrowerLiability].[AddressInfoId] point to this entity (FK_BorrowerLiability_AddressInfo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerLiability> BorrowerLiabilities { get; set; } // BorrowerLiability.FK_BorrowerLiability_AddressInfo
        /// <summary>
        /// Child BorrowerResidences where [BorrowerResidence].[LandLordAddressId] point to this entity (FK_BorrowerResidence_AddressInfo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerResidence> BorrowerResidences_LandLordAddressId { get; set; } // BorrowerResidence.FK_BorrowerResidence_AddressInfo
        /// <summary>
        /// Child BorrowerResidences where [BorrowerResidence].[LoanAddressId] point to this entity (FK_BorrowerResidence_LoanAddress)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<BorrowerResidence> BorrowerResidences_LoanAddressId { get; set; } // BorrowerResidence.FK_BorrowerResidence_LoanAddress
        /// <summary>
        /// Child EmploymentInfoes where [EmploymentInfo].[EmployerAddressId] point to this entity (FK_EmploymentInfo_AddressInfo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<EmploymentInfo> EmploymentInfoes { get; set; } // EmploymentInfo.FK_EmploymentInfo_AddressInfo
        /// <summary>
        /// Child PropertyInfoes where [PropertyInfo].[AddressInfoId] point to this entity (FK_PropertyInfo_AddressInfo)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<PropertyInfo> PropertyInfoes { get; set; } // PropertyInfo.FK_PropertyInfo_AddressInfo

        // Foreign keys

        /// <summary>
        /// Parent City pointed by [AddressInfo].([CityId]) (FK_AddressInfo_City)
        /// </summary>
        public virtual City City { get; set; } // FK_AddressInfo_City

        /// <summary>
        /// Parent Country pointed by [AddressInfo].([CountryId]) (FK_AddressInfo_Country)
        /// </summary>
        public virtual Country Country { get; set; } // FK_AddressInfo_Country

        /// <summary>
        /// Parent County pointed by [AddressInfo].([CountyId]) (FK_AddressInfo_County)
        /// </summary>
        public virtual County County { get; set; } // FK_AddressInfo_County

        /// <summary>
        /// Parent State pointed by [AddressInfo].([StateId]) (FK_AddressInfo_State)
        /// </summary>
        public virtual State State { get; set; } // FK_AddressInfo_State

        public AddressInfo()
        {
            IsDeleted = false;
            EntityTypeId = 10;
            BorrowerLiabilities = new System.Collections.Generic.HashSet<BorrowerLiability>();
            BorrowerResidences_LandLordAddressId = new System.Collections.Generic.HashSet<BorrowerResidence>();
            BorrowerResidences_LoanAddressId = new System.Collections.Generic.HashSet<BorrowerResidence>();
            EmploymentInfoes = new System.Collections.Generic.HashSet<EmploymentInfo>();
            PropertyInfoes = new System.Collections.Generic.HashSet<PropertyInfo>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
