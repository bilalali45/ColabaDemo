













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // Region

    public partial class Region 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int? RegionTypeId { get; set; } // RegionTypeId
        public string TypeList { get; set; } // TypeList
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
        public string States { get; set; } // States
        public string Counties { get; set; } // Counties
        public string ZipCodes { get; set; } // ZipCodes
        public string Cities { get; set; } // Cities
        public bool IsDeleted { get; set; } // IsDeleted
        public string Xml { get; set; } // Xml (length: 1073741823)

        public Region()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 36;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
