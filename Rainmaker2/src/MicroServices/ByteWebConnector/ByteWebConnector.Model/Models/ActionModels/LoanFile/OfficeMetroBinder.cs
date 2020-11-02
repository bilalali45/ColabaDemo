













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // OfficeMetroBinder

    public partial class OfficeMetroBinder 
    {
        public int Id { get; set; } // Id (Primary key)
        public int? StateId { get; set; } // StateId
        public int? CountyId { get; set; } // CountyId
        public int? CityId { get; set; } // CityId
        public string RegionCode { get; set; } // RegionCode (length: 50)
        public string MetroCode { get; set; } // MetroCode (length: 3)
        public int? BranchId { get; set; } // BranchId
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

        // Foreign keys

        /// <summary>
        /// Parent Branch pointed by [OfficeMetroBinder].([BranchId]) (FK_OfficeMetroBinder_Branch)
        /// </summary>
        public virtual Branch Branch { get; set; } // FK_OfficeMetroBinder_Branch

        /// <summary>
        /// Parent City pointed by [OfficeMetroBinder].([CityId]) (FK_OfficeMetroBinder_City)
        /// </summary>
        public virtual City City { get; set; } // FK_OfficeMetroBinder_City

        /// <summary>
        /// Parent County pointed by [OfficeMetroBinder].([CountyId]) (FK_OfficeMetroBinder_County)
        /// </summary>
        public virtual County County { get; set; } // FK_OfficeMetroBinder_County

        /// <summary>
        /// Parent State pointed by [OfficeMetroBinder].([StateId]) (FK_OfficeMetroBinder_State)
        /// </summary>
        public virtual State State { get; set; } // FK_OfficeMetroBinder_State

        public OfficeMetroBinder()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 25;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
