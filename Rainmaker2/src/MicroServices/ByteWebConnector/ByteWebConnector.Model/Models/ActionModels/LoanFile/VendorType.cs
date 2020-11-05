













namespace ByteWebConnector.Model.Models.ActionModels.LoanFile
{
    // VendorType

    public partial class VendorType 
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public int DisplayOrder { get; set; } // DisplayOrder
        public bool IsActive { get; set; } // IsActive
        public int EntityTypeId { get; set; } // EntityTypeId
        public bool IsDefault { get; set; } // IsDefault
        public bool IsSystem { get; set; } // IsSystem
        public System.DateTime? CreatedOnUtc { get; set; } // CreatedOnUtc
        public int? CreatedBy { get; set; } // CreatedBy
        public System.DateTime? ModifiedOnUtc { get; set; } // ModifiedOnUtc
        public int? ModifiedBy { get; set; } // ModifiedBy
        public string TpId { get; set; } // TpId (length: 50)
        public bool IsDeleted { get; set; } // IsDeleted

        // Reverse navigation

        /// <summary>
        /// Child Vendors where [Vendor].[VendorTypeId] point to this entity (FK_Vendor_VendorType)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<Vendor> Vendors { get; set; } // Vendor.FK_Vendor_VendorType

        public VendorType()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 102;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            Vendors = new System.Collections.Generic.HashSet<Vendor>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>