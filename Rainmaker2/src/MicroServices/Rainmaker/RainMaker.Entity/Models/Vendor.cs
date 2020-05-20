// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 2.1
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace RainMaker.Entity.Models
{
    using System;
    using System.Collections.Generic;

    // Vendor
    
    public partial class Vendor : URF.Core.EF.Trackable.Entity
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 150)
        public string Description { get; set; } // Description (length: 500)
        public string Number { get; set; } // Number (length: 50)
        public int? VendorTypeId { get; set; } // VendorTypeId
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
        /// Child VendorCustomerBinders where [VendorCustomerBinder].[VendorId] point to this entity (FK_VendorCustomerBinder_Vendor)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<VendorCustomerBinder> VendorCustomerBinders { get; set; } // VendorCustomerBinder.FK_VendorCustomerBinder_Vendor

        // Foreign keys

        /// <summary>
        /// Parent VendorType pointed by [Vendor].([VendorTypeId]) (FK_Vendor_VendorType)
        /// </summary>
        public virtual VendorType VendorType { get; set; } // FK_Vendor_VendorType

        public Vendor()
        {
            DisplayOrder = 0;
            IsActive = true;
            EntityTypeId = 51;
            IsDefault = false;
            IsSystem = false;
            IsDeleted = false;
            VendorCustomerBinders = new System.Collections.Generic.HashSet<VendorCustomerBinder>();
            InitializePartial();
        }

        partial void InitializePartial();
    }

}
// </auto-generated>
