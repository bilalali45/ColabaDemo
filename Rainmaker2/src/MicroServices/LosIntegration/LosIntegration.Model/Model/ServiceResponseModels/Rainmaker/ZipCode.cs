













namespace LosIntegration.Model.Model.ServiceResponseModels.Rainmaker
{
    // ZipCode

    public partial class ZipCode 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? ZipNo { get; set; } // ZipNo
        public string ZipPostalCode { get; set; } // ZipPostalCode (length: 10)
        public int? StateId { get; set; } // StateId
        public int? CountyId { get; set; } // CountyId
        public int? CityId { get; set; } // CityId
        public int? DisplayOrder { get; set; } // DisplayOrder
        public bool? IsActive { get; set; } // IsActive
        public int? EntityTypeId { get; set; } // EntityTypeId
        public bool? IsDefault { get; set; } // IsDefault
        public bool? IsSystem { get; set; } // IsSystem
        public int? ModifiedBy { get; set; } // ModifiedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public bool? IsDeleted { get; set; } // IsDeleted

        // Foreign keys

        /// <summary>
        /// Parent City pointed by [ZipCode].([CityId]) (FK_ZipCode_City)
        /// </summary>
        public virtual City City { get; set; } // FK_ZipCode_City

        /// <summary>
        /// Parent County pointed by [ZipCode].([CountyId]) (FK_ZipCode_County)
        /// </summary>
        public virtual County County { get; set; } // FK_ZipCode_County

        /// <summary>
        /// Parent State pointed by [ZipCode].([StateId]) (FK_ZipCode_State)
        /// </summary>
        public virtual State State { get; set; } // FK_ZipCode_State

        public ZipCode()
        {
            EntityTypeId = 8;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
